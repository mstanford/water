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
	public class MemoryStream : System.IO.Stream
	{
		private long _length;
		private long _position;
		private BufferPool _bufferPool;
		private List<byte[]> _buffers = new List<byte[]>(1);
		private int _bufferIndex;
		private int _bufferLength;
		private int _bufferPosition;

		public MemoryStream(BufferPool bufferPool)
		{
			this._bufferPool = bufferPool;
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanSeek
		{
			get { return true; }
		}

		public override bool CanWrite
		{
			get { return true; }
		}

		public override long Length
		{
			get { return this._length; }
		}

		public override long Position
		{
			get { return this._position; }
			set
			{
				this._position = value;

				if (this._position == 0)
				{
					this._bufferIndex = 0;
					this._bufferPosition = 0;
					this._bufferLength = this._buffers[this._bufferIndex].Length;
				}
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
			int n = (int)(this._length - this._position);
			if (n > count)
			{
				n = count;
			}

			int bytesRemaining = n;
			while (bytesRemaining > 0)
			{
				int bytesRead = ReadBuffer(buffer, offset, bytesRemaining);
				offset += bytesRead;
				bytesRemaining -= bytesRead;
			}
			this._position += n;
			return n;
		}

		private int ReadBuffer(byte[] buffer, int offset, int count)
		{
			if (this._bufferLength == this._bufferPosition)
			{
				this._bufferIndex++;
				this._bufferPosition = 0;
				this._bufferLength = this._buffers[this._bufferIndex].Length;
			}

			int n = this._bufferLength - this._bufferPosition;
			if (n > count)
			{
				n = count;
			}

			byte[] b = this._buffers[this._bufferIndex];
			Array.Copy(b, this._bufferPosition, buffer, offset, n);
			this._bufferPosition += n;
			return n;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			int bytesRemaining = count;
			while (bytesRemaining > 0)
			{
				int bytesWritten = WriteBuffer(buffer, offset, bytesRemaining);
				offset += bytesWritten;
				bytesRemaining -= bytesWritten;
			}
		}

		private int WriteBuffer(byte[] buffer, int offset, int count)
		{
			if(this._length == this._position && this._bufferLength == this._bufferPosition)
			{
				byte[] newBuffer = this._bufferPool.Dequeue();
				this._bufferIndex = (this._length == 0) ? 0 : this._bufferIndex + 1;
				this._bufferPosition = 0;
				this._bufferLength = newBuffer.Length;
				this._buffers.Add(newBuffer);
			}

			int n = this._bufferLength - this._bufferPosition;
			if (n > count)
			{
				n = count;
			}
			byte[] b = this._buffers[this._bufferIndex];
			Array.Copy(buffer, offset, b, this._bufferPosition, n);
			this._bufferPosition += n;
			this._length += n;
			this._position += n;
			return n;
		}

		public override void Flush()
		{
		}

		public override void Close()
		{
			foreach (byte[] buffer in this._buffers)
			{
				this._bufferPool.Enqueue(buffer);
			}
			this._bufferPool = null;
		}

	}
}
