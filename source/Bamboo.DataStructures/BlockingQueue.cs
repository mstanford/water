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
using System.Threading;

namespace Bamboo.DataStructures
{
	public class BlockingQueue<T>
	{
		private Queue<T> _queue;

		public BlockingQueue()
		{
			this._queue = new Queue<T>();
		}

		public BlockingQueue(int size)
		{
			this._queue = new Queue<T>(size);
		}

		public int Count
		{
			get { return this._queue.Count; }
		}

		public T Dequeue()
		{
			lock (_queue)
			{
				while (_queue.Count <= 0) Monitor.Wait(_queue);
				return _queue.Dequeue();
			}
		}

		public void Enqueue(T data)
		{
			if (data == null) throw new ArgumentNullException("data");
			lock (_queue)
			{
				_queue.Enqueue(data);
				Monitor.Pulse(_queue);
			}
		}

	}
}
