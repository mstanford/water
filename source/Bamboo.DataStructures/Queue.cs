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
using System.Text;

namespace Bamboo.DataStructures
{
	public class Queue<T>
	{
		private T[] _array;
		private int _head;       // First valid element in the queue
		private int _tail;       // Last valid element in the queue
		private int _size;       // Number of elements.
		private int _growFactor; // 100 == 1.0, 130 == 1.3, 200 == 2.0
		private int _version;

		private const int _MinimumGrow = 4;
		private const int _ShrinkThreshold = 32;

		public Queue()
			: this(32, (float)2.0)
		{
		}

		public Queue(int capacity)
			: this(capacity, (float)2.0)
		{
		}

		public Queue(int capacity, float growFactor)
		{
			if (capacity < 0)
				throw new ArgumentOutOfRangeException();
			if (!(growFactor >= 1.0 && growFactor <= 10.0))
				throw new ArgumentOutOfRangeException();

			_array = new T[capacity];
			_head = 0;
			_tail = 0;
			_size = 0;
			_growFactor = (int)(growFactor * 100);
		}

		public int Count
		{
			get { return _size; }
		}

		public T this[int index]
		{
			get { return _array[(_head + index) % _array.Length]; }
		}

		public T Dequeue()
		{
			if (_size == 0)
				throw new InvalidOperationException();

			T removed = _array[_head];
			_array[_head] = default(T);
			_head = (_head + 1) % _array.Length;
			_size--;
			_version++;
			return removed;
		}

		public void Enqueue(T obj)
		{
			if (_size == _array.Length)
			{
				int newcapacity = (int)((long)_array.Length * (long)_growFactor / 100);
				if (newcapacity < _array.Length + _MinimumGrow)
				{
					newcapacity = _array.Length + _MinimumGrow;
				}
				SetCapacity(newcapacity);
			}

			_array[_tail] = obj;
			_tail = (_tail + 1) % _array.Length;
			_size++;
			_version++;
		}

		private void SetCapacity(int capacity)
		{
			T[] newarray = new T[capacity];
			if (_size > 0)
			{
				if (_head < _tail)
				{
					Array.Copy(_array, _head, newarray, 0, _size);
				}
				else
				{
					Array.Copy(_array, _head, newarray, 0, _array.Length - _head);
					Array.Copy(_array, 0, newarray, _array.Length - _head, _tail);
				}
			}

			_array = newarray;
			_head = 0;
			_tail = _size;
			_version++;
		}

	}
}
