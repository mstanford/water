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
	public class BufferPool
	{
		private const int BUFFER_SIZE = 8192;

		private BlockingQueue<byte[]> _queue = new BlockingQueue<byte[]>();
		private int _size;

		public BufferPool(int size)
		{
			this._size = size;
		}

		public int Count
		{
			get { return this._size + this._queue.Count; }
		}

		public void Enqueue(byte[] buffer)
		{
			this._queue.Enqueue(buffer);
		}

		public byte[] Dequeue()
		{
			if (this._size > 0 && this._queue.Count == 0)
			{
				this._size--;
				return new byte[BUFFER_SIZE];
			}
			else
			{
				return this._queue.Dequeue();
			}
		}

	}
}
