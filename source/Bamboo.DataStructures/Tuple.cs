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

namespace Bamboo.DataStructures
{
	public class Tuple
	{
		private object[] _values;

		public Tuple(params object[] values)
		{
			_values = values;
		}

		public Tuple(System.Collections.IList row)
		{
			_values = new object[row.Count];
			for (int i = 0; i < _values.Length; i++)
			{
				_values[i] = row[i];
			}
		}

		public int Count
		{
			get { return _values.Length; }
		}

		public object this[int index]
		{
			get { return _values[index]; }
		}

		public Tuple Copy()
		{
			int length = this._values.Length;
			object[] values = new object[length];
			System.Array.Copy(this._values, values, length);
			return new Tuple(values);
		}

		public bool Equals(int start, Tuple row2, int start2, int length)
		{
			for (int i = 0; i < length; i++)
			{
				if (!_values[i + start].Equals(row2[i + start]))
				{
					return false;
				}
			}
			return true;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Tuple))
			{
				return false;
			}

			Tuple t = (Tuple)obj;
			if (this._values.Length != t._values.Length)
			{
				return false;
			}

			for (int i = 0; i < this._values.Length; i++)
			{
				if (!this._values[i].Equals(t._values[i]))
				{
					return false;
				}
			}

			return true;
		}

		public override int GetHashCode()
		{
			int hash = 23;
			for (int i = 0; i < this._values.Length; i++)
			{
				hash *= 37 + this._values[i].GetHashCode();
			}
			return hash;
		}

	}
}
