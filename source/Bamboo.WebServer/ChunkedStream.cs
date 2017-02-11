// ------------------------------------------------------------------------------
// 
// Copyright (c) 2008 Swampware, Inc.
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
	/// Summary description for ChunkedStream.
	/// </summary>
	public class ChunkedStream : System.IO.Stream
	{
		private System.IO.Stream _stream;
		private byte[] _readBuffer;
		private int _readPosition;
		private bool _readEndOfStream = false;
		private bool _wasFlushed = false;

		public ChunkedStream(System.IO.Stream stream)
		{
			this._stream = stream;
			this._readBuffer = new byte[0];
			this._readPosition = 0;
		}

		public override bool CanRead
		{
			get { return this._stream.CanRead; }
		}

		public override bool CanSeek
		{
			get { return false; }
		}

		public override bool CanWrite
		{
			get { return this._stream.CanWrite; }
		}

		public override long Length
		{
			get { throw new Exception("The method or operation is not supported."); }
		}

		public override long Position
		{
			get { throw new Exception("The method or operation is not supported."); }
			set { throw new Exception("The method or operation is not supported."); }
		}

		public override void Close()
		{
			if (this._stream != null)
			{
				this._stream.Close();
				this._stream = null;
				this._readBuffer = null;
			}
		}

		public override void Flush()
		{
			if (this._wasFlushed)
			{
				throw new System.Exception("ChunkedStream was already flushed.");
			}

			this._wasFlushed = true;

			this.Write("0\r\n");
			this.Write("\r\n");

			this._stream.Flush();
		}

		public override long Seek(long offset, System.IO.SeekOrigin origin)
		{
			throw new Exception("The method or operation is not supported.");
		}

		public override void SetLength(long value)
		{
			throw new Exception("The method or operation is not supported.");
		}

        public override int Read(byte[] buffer, int offset, int count)
        {
			if (this._readEndOfStream)
			{
				return 0;
			}

			if (this._readPosition == this._readBuffer.Length)
			{
				this._readBuffer = this.ReadChunk();
				this._readPosition = 0;

				if (this._readBuffer.Length == 0) // It couldn't read.
				{
					return 0;
				}
			}

			int remaining = this._readBuffer.Length - this._readPosition;

			if (remaining >= count) // If there's enough in the buffer.
			{
				System.Buffer.BlockCopy(this._readBuffer, this._readPosition, buffer, offset, count);
				this._readPosition += count;
				return count;
			}
			else // Read as much as you can from the buffer.
			{
				System.Buffer.BlockCopy(this._readBuffer, this._readPosition, buffer, offset, remaining);
				this._readPosition += remaining;
				return remaining + Read(buffer, offset + remaining, count - remaining);
			}
		}

		private byte[] ReadChunk()
		{
			string line = this.ReadLine();
			int length = Int32.Parse(line, System.Globalization.NumberStyles.HexNumber);
			if (length == 0)
			{
				this._readEndOfStream = true;
				return new byte[0];
			}

			byte[] bytes = new byte[length];
			int bytesRead = this._stream.Read(bytes, 0, length);
			if (bytesRead != length)
			{
				throw new System.Exception("Unable to read full buffer.");
			}
			this.ReadLine();
			return bytes;
		}

		private string ReadLine()
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

			throw new System.Exception("Unexpected end of stream.");
		}

        public override void Write(byte[] buffer, int offset, int count)
        {
			this.Write(count.ToString("x", System.Globalization.CultureInfo.InvariantCulture) + "\r\n");
			this._stream.Write(buffer, offset, count);
			this.Write("\r\n");
		}

		private void Write(string s)
		{
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
			this._stream.Write(bytes, 0, bytes.Length);
		}

	}
}
