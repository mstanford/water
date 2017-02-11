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

namespace Bamboo.Compression.GZip
{
	/// <summary>
	/// Summary description for GZipper.
	/// </summary>
	public class GZipper
	{

		public static byte[] Compress(byte[] bytes)
		{
			System.IO.MemoryStream stream = new System.IO.MemoryStream();
			NZlib.GZip.GZipOutputStream gzipOutputStream = new NZlib.GZip.GZipOutputStream(stream);
			gzipOutputStream.Write(bytes, 0, bytes.Length);
			gzipOutputStream.Flush();
			gzipOutputStream.Close();
			byte[] gzipped_bytes = stream.ToArray();
			stream.Close();
			return gzipped_bytes;
		}

		public static byte[] Decompress(byte[] gzipped_bytes)
		{
			System.IO.MemoryStream output = new System.IO.MemoryStream();

			System.IO.MemoryStream stream = new System.IO.MemoryStream(gzipped_bytes);
			NZlib.GZip.GZipInputStream gzipInputStream = new NZlib.GZip.GZipInputStream(stream);

			byte[] buffer = new byte[8192];
			int bytesRead;
			while((bytesRead = gzipInputStream.Read(buffer, 0, buffer.Length)) > 0)
			{
				output.Write(buffer, 0, bytesRead);
			}

			gzipInputStream.Close();
			stream.Close();

			byte[] bytes = output.ToArray();
			output.Close();
			return bytes;
		}

		private GZipper()
		{
		}

	}
}
