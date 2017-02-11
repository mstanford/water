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
	public class Stack<T>
	{
		private T[] _array;
		private int _size;

		public Stack()
			: this(8)
		{
		}

		public Stack(int capacity)
		{
			if (capacity < 0)
				throw new ArgumentOutOfRangeException();

			_array = new T[capacity];
			_size = 0;
		}

		public int Count
		{
			get { return _size; }
		}

		public T this[int index]
		{
			get { return _array[index]; }
		}

		public void Clear()
		{
			Array.Clear(this._array, 0, this._size);
			this._size = 0;
		}

		public T Peek()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException();
			}
			return this._array[this._size - 1];
		}

		public T Pop()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException();
			}
			this._size--;
			T value = this._array[this._size];
			this._array[this._size] = default(T);
			return value;
		}

		public void Push(T value)
		{
			if (this._size == this._array.Length)
			{
				T[] newArray = new T[this._array.Length * 2];
				Array.Copy(this._array, 0, newArray, 0, this._size);
				this._array = newArray;
			}
			this._array[this._size] = value;
			this._size++;
		}

	}
}
