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
    /// 
    /// This is similar in purpose to the System.IO.BufferedStream class.  The 
    /// BufferedStream class returned data on a blocking socket read instead 
    /// of blocking.
    /// 
    /// </summary>
    public class BufferedStream : System.IO.Stream
    {
        private System.IO.Stream _stream;
		private int _readBufferSize;
		private byte[] _readBuffer;
        private int _readPosition;
        private int _readLength;
		private int _writeBufferSize;
		private byte[] _writeBuffer;
		private int _writePosition;

		public BufferedStream(System.IO.Stream stream, int readBufferSize, int writeBufferSize)
        {
            this._stream = stream;
			this._readBufferSize = readBufferSize;
			this._readBuffer = new byte[8192];
			if (this._readBufferSize > this._readBuffer.Length)
			{
				this._readBufferSize = this._readBuffer.Length;
			}
			this._readPosition = 0;
            this._readLength = 0;
			this._writeBufferSize = writeBufferSize;
			this._writeBuffer = new byte[8192];
			if (this._writeBufferSize > this._writeBuffer.Length)
			{
				this._writeBufferSize = this._writeBuffer.Length;
			}
			this._writePosition = 0;
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
			if (this._readBuffer != null)
			{
				this._readBuffer = null;
			}
			if (this._writeBuffer != null)
			{
				this._writeBuffer = null;
			}
			if (this._stream != null)
            {
				this._stream.Close();
                this._stream = null;
			}
        }

        public override void Flush()
        {
            if (this._writePosition > 0)
            {
                this._stream.Write(this._writeBuffer, 0, this._writePosition);
                this._writePosition = 0;
            }
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

		public override int Read(byte[] buffer, int offset, int size)
        {
			// The old inefficient but works way:
//			int bytesRead = 0;
//			for (int i = offset; i < count; i++)
//			{
//				int n = this.ReadByte();
//				if (n == -1)
//				{
//					break;
//				}
//				buffer[i] = (byte)n;
//				bytesRead++;
//			}
//			return bytesRead;



			int available = this._readLength - this._readPosition;
			if (available == 0) // If the buffer is full.
			{
				this._readLength = this._stream.Read(this._readBuffer, 0, this._readBufferSize);
				this._readPosition = 0;

				if (this._readLength == 0) // It couldn't read.
				{
					return 0;
				}

				available = this._readLength - this._readPosition;
			}

			int bytesToRead = System.Math.Min(available, size);

			System.Buffer.BlockCopy(this._readBuffer, this._readPosition, buffer, offset, bytesToRead);
			this._readPosition += bytesToRead;
			return bytesToRead;
		}

		public override int ReadByte()
		{
			int available = this._readLength - this._readPosition;
			if (available == 0) // If the buffer is full.
			{
				this._readLength = this._stream.Read(this._readBuffer, 0, this._readBufferSize);
				this._readPosition = 0;

				if (this._readLength == 0) // It couldn't read.
				{
					return -1;
				}

				available = this._readLength - this._readPosition;
			}

			byte b = this._readBuffer[this._readPosition];
			this._readPosition++;
			return b;
		}

		public override void Write(byte[] buffer, int offset, int size)
        {
			// The old inefficient but works way:
//			for (int i = offset; i < count; i++)
//			{
//				this.WriteByte(buffer[i]);
//			}



			int remaining = size;
			while (remaining > 0)
			{
				int available = this._writeBufferSize - this._writePosition;
				if (available  == 0) // If the buffer is full.
				{
					this._stream.Write(this._writeBuffer, 0, this._writeBuffer.Length);
					this._writePosition = 0;

					available = this._writeBufferSize - this._writePosition;
				}

				int bytesToWrite = System.Math.Min(available, remaining);
				System.Buffer.BlockCopy(buffer, offset, this._writeBuffer, this._writePosition, bytesToWrite);

				this._writePosition += bytesToWrite;
				offset += bytesToWrite;
				remaining -= bytesToWrite;
			}

			this.Flush();
		}

		public override void WriteByte(byte value)
		{
			int available = this._writeBufferSize - this._writePosition;
			if (available == 0) // If the buffer is full.
			{
				this._stream.Write(this._writeBuffer, 0, this._writeBuffer.Length);
				this._writePosition = 0;

				available = this._writeBufferSize - this._writePosition;
			}

			this._writeBuffer[this._writePosition] = value;
			this._writePosition++;

			this.Flush();
		}

    }
}
