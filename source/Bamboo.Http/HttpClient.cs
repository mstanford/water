// ------------------------------------------------------------------------------
// 
// Copyright (c) 2009 Swampware, Inc.
// 
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamboo.Http
{
	public class HttpClient
	{

		public static System.IO.Stream Get(string url, Bamboo.DataStructures.BufferPool bufferPool)
		{
			System.Net.WebRequest request = System.Net.HttpWebRequest.Create(url);
			request.Method = "GET";
			System.Net.WebResponse response = request.GetResponse();

			System.IO.Stream responseStream = new Bamboo.DataStructures.MemoryStream(bufferPool);
			Copy(response.GetResponseStream(), responseStream, (int)response.ContentLength, bufferPool);
			response.Close();
			responseStream.Position = 0;
			return responseStream;
		}

		public static System.IO.Stream Post(string url, System.IO.Stream stream, Bamboo.DataStructures.BufferPool bufferPool)
		{
			System.Net.WebRequest request = System.Net.HttpWebRequest.Create(url);
			request.Method = "POST";
			System.IO.Stream requestStream = request.GetRequestStream();
			Copy(stream, requestStream, (int)stream.Length, bufferPool);
			requestStream.Close();
			System.Net.WebResponse response = request.GetResponse();

			if (response.ContentLength == 0)
			{
				response.Close();
				return null;
			}

			System.IO.Stream responseStream = new Bamboo.DataStructures.MemoryStream(bufferPool);
			Copy(response.GetResponseStream(), responseStream, (int)response.ContentLength, bufferPool);
			response.Close();
			if (responseStream.Length > 0)
			{
				responseStream.Position = 0;
			}
			return responseStream;
		}

		private static void Copy(System.IO.Stream from, System.IO.Stream to, int length, Bamboo.DataStructures.BufferPool bufferPool)
		{
			if (length == -1)
			{
				byte[] buffer = bufferPool.Dequeue();
				while (true)
				{
					int bytesRead = from.Read(buffer, 0, buffer.Length);
					if (bytesRead == 0)
					{
						break;
					}
					to.Write(buffer, 0, bytesRead);
				}
				bufferPool.Enqueue(buffer);
			}
			else
			{
				int remaining = length;
				byte[] buffer = bufferPool.Dequeue();
				while (remaining > 0)
				{
					int bytesRead = from.Read(buffer, 0, Math.Min(buffer.Length, remaining));
					to.Write(buffer, 0, bytesRead);
					remaining -= bytesRead;
				}
				bufferPool.Enqueue(buffer);
			}
		}

	}
}
