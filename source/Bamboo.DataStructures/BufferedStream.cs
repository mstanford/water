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

namespace Bamboo.DataStructures
{
	public class BufferedStream : System.IO.Stream
	{
		private System.IO.Stream _stream;
		private BufferPool _bufferPool;
		private byte[] _buffer;
		private int _position = 0;
		private int _length = 0;

		public BufferedStream(System.IO.Stream stream, BufferPool bufferPool)
		{
			this._stream = stream;
			this._bufferPool = bufferPool;
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanSeek
		{
			get { return false; }
		}

		public override bool CanWrite
		{
			get { return true; }
		}

		public override long Length
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public override long Position
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public override void SetLength(long value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override long Seek(long offset, System.IO.SeekOrigin origin)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int n = this._length - this._position;
			if (n == 0)
			{
				return this._stream.Read(buffer, offset, count);
			}

			if (n > count)
			{
				n = count;
			}
			Array.Copy(this._buffer, this._position, buffer, offset, n);
			this._position += n;

			return n;
		}

		public override int ReadByte()
		{
			if(this._position == this._length)
			{
				if (this._buffer == null)
				{
					this._buffer = this._bufferPool.Dequeue();
				}
				this._length = this._stream.Read(this._buffer, 0, this._buffer.Length);
				this._position = 0;
			}
			if (this._position == this._length)
			{
				return -1;
			}
			return this._buffer[this._position++];
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			this._stream.Write(buffer, offset, count);
		}

		public override void Flush()
		{
			this._stream.Flush();
		}

		public override void Close()
		{
			if (this._buffer != null)
			{
				this._bufferPool.Enqueue(this._buffer);
			}

			this._stream.Close();

			this._stream = null;
			this._bufferPool = null;
			this._buffer = null;
		}

	}
}
