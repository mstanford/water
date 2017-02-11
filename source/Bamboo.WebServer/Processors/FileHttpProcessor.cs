// ------------------------------------------------------------------------------
// 
// Copyright (c) 2007 Swampware, Inc.
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

namespace Bamboo.WebServer.Processors
{
	/// <summary>
	/// Summary description for FileHttpProcessor.
	/// </summary>
	public class FileHttpProcessor : HttpProcessor
	{
		private string _contentType;
		private string _path;

		public FileHttpProcessor(string contentType, string path)
		{
			this._contentType = contentType;
			this._path = path;
		}

		public string ContentType
		{
			get { return this._contentType; }
		}

		public string Path
		{
			get { return this._path; }
		}

		public override void Process(Bamboo.WebServer.HttpRequest request, Bamboo.WebServer.HttpResponse response)
		{
            response.Headers.Add("Content-Type", this._contentType);

			System.IO.FileStream fileStream = System.IO.File.OpenRead(this._path);
			Copy(fileStream, response.Output);
			fileStream.Close();
		}

		private static void Copy(System.IO.Stream input, System.IO.Stream output)
		{
			if (input.Length == 0)
			{
				return;
			}

			long input_position = input.Position;
			input.Position = 0;

			byte[] buffer = new byte[4096];
			int totalBytesRead = 0;
			while (totalBytesRead < input.Length)
			{
				int remaining = (int)input.Length - totalBytesRead;

				int bytesRead;
				if (remaining < buffer.Length)
				{
					bytesRead = input.Read(buffer, 0, remaining);
				}
				else
				{
					bytesRead = input.Read(buffer, 0, buffer.Length);
				}

				output.Write(buffer, 0, bytesRead);
				totalBytesRead += bytesRead;
			}

			input.Position = input_position;
		}

	}
}
