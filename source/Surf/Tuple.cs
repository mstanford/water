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
using System.Collections.Generic;
using System.Text;

namespace Surf
{
	public class Tuple : System.Collections.IEnumerable, System.IComparable
	{
		private System.Collections.ArrayList _items = new System.Collections.ArrayList();
		private Comparer _comparer = new Comparer();

		public Tuple()
		{
		}

		public Tuple(System.Collections.IEnumerable enumerable)
		{
			foreach (object item in enumerable)
			{
				this._items.Add(item);
			}
		}

		public object this[int index]
		{
			get { return this._items[index]; }
		}

		public int Count
		{
			get { return this._items.Count; }
		}

		public void Add(object item)
		{
			this._items.Add(item);
		}

		public System.Collections.IEnumerator GetEnumerator()
		{
			return this._items.GetEnumerator();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Tuple))
			{
				return false;
			}
			Tuple tuple = (Tuple)obj;
			if (this._items.Count != tuple._items.Count)
			{
				return false;
			}
			for (int i = 0; i < this._items.Count; i++)
			{
				if (!this._items[i].Equals(tuple._items[i]))
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			System.IO.StringWriter writer = new System.IO.StringWriter();
			Printer.Print(this, writer);
			writer.Flush();
			int hashCode = writer.ToString().GetHashCode();
			writer.Close();
			return hashCode;
		}

		public int CompareTo(object obj)
		{
			return this._comparer.Compare(this, obj);
		}

	}
}
