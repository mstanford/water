// ------------------------------------------------------------------------------
// 
// Copyright (c) 2007-2008 Swampware, Inc.
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

namespace Bamboo.WebServer
{
	/// <summary>
	/// Summary description for HttpWriter.
	/// </summary>
	public class HttpWriter
	{
		private System.IO.Stream _stream;
		private bool _writeChunked = false;
		private ChunkedStream _writeChunkedStream;

		public HttpWriter(System.IO.Stream stream)
		{
			this._stream = stream;
		}

		public void Flush()
		{
			if(this._writeChunkedStream != null)
			{
				this._writeChunkedStream.Flush();
			}

			this._stream.Flush();
		}

		public void WriteStartLine(string s)
		{
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes(s + "\r\n");
			this._stream.Write(buffer, 0, buffer.Length);
		}

		public void WriteHeader(string name, string value)
		{
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes(name + ": " + value + "\r\n");
			this._stream.Write(bytes, 0, bytes.Length);

			// Read the body as chunked.
			if (name.Equals("Transfer-Encoding") && value.ToLower().Equals("chunked"))
			{
				this._writeChunked = true;
				this._writeChunkedStream = new ChunkedStream(this._stream);
			}
		}

		public void WriteEndHeader()
		{
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes("\r\n");
			this._stream.Write(bytes, 0, bytes.Length);
		}

		public void WriteBody(System.IO.Stream stream)
		{
			Copy(stream, this._stream);
		}

		private static void Copy(System.IO.Stream input, System.IO.Stream output)
		{
			if (input.Length == 0)
			{
				return;
			}

			long input_position = input.Position;
			input.Position = 0;

			byte[] buffer = new byte[8192];
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
