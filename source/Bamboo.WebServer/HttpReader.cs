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
	/// Summary description for HttpReader.
	/// </summary>
	public class HttpReader
	{
		private System.IO.Stream _stream;
		private int _readContentLength = -1;
		private bool _readChunked = false;
		private ChunkedStream _readChunkedStream;
		private bool _readGZip = false;

		public HttpReader(System.IO.Stream stream)
		{
			this._stream = stream;
		}

		public string ReadStartLine()
		{
			return this.ReadLine();
		}

		public string[] ReadHeader()
		{
			string line = this.ReadLine();
			if(line == null || line.Length == 0)
			{
				return null;
			}

			int index = line.IndexOf(":");
			string name = line.Substring(0, index);
			string value = line.Substring(index + 1).Trim();

			// Keep these for later.
			if (name.ToLower().Equals("content-length"))
			{
				this._readContentLength = Int32.Parse(value);
			}
			else if (name.ToLower().Equals("transfer-encoding"))
			{
				if (value.ToLower().Equals("chunked"))
				{
					// Read the body as chunked.
					this._readChunked = true;
					this._readChunkedStream = new ChunkedStream(this._stream);
				}
			}
			else if (name.ToLower().Equals("content-encoding") && value.ToLower().IndexOf("gzip") != -1)
			{
				this._readGZip = true;
			}

			return new string[] { name, value };
		}

		public string ReadLine()
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

			int n;
			while ((n = this._stream.ReadByte()) != -1)
			{
				char ch = (char)n;

				if (ch == '\r')
				{
					if ((int)'\n' != this._stream.ReadByte())
					{
						throw new System.Exception("Invalid newline.");
					}
					return stringBuilder.ToString();
				}
				else if (ch == '\n')
				{
					return stringBuilder.ToString();
				}
				else
				{
					stringBuilder.Append(ch);
				}
			}

			if (stringBuilder.Length == 0)
			{
				return null;
			}

			return stringBuilder.ToString();
		}

		public void ReadBody(System.IO.Stream stream, byte[] buffer)
		{
			int remaining = this._readContentLength;
			if (remaining > 0)
			{
				while (remaining > 0)
				{
					if (remaining > buffer.Length)
					{
						int bytesRead = this._stream.Read(buffer, 0, buffer.Length);
						stream.Write(buffer, 0, bytesRead);
						remaining -= bytesRead;
					}
					else
					{
						int bytesRead = this._stream.Read(buffer, 0, remaining);
						stream.Write(buffer, 0, bytesRead);
						remaining -= bytesRead;
					}
				}
				stream.Position = 0;
			}
		}

	}
}
