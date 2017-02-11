// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006-2008 Swampware, Inc.
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

namespace Water
{
	/// <summary>
	/// Summary description for List.
	/// </summary>
	public sealed class List : System.Collections.IList
	{
		private System.Collections.ArrayList _list;
		private Water.ListComparer _comparer = new Water.ListComparer();

		public List()
		{
			this._list = new System.Collections.ArrayList();
		}

		public List(int size)
		{
			this._list = new System.Collections.ArrayList(size);
		}

		public List(System.Collections.ArrayList list)
		{
			this._list = list;
		}

		public List(object[] items)
		{
			this._list = new System.Collections.ArrayList(items.Length);
			for(int i = 0; i < items.Length; i++)
			{
				this._list.Add(items[i]);
			}
		}

		public List(object[,] items)
		{
			int width = items.GetLength(0);
			int height = items.GetLength(1);
			this._list = new System.Collections.ArrayList(height);
			for(int y = 0; y < height; y++)
			{
				System.Collections.ArrayList list = new System.Collections.ArrayList(width);
				for(int x = 0; x < width; x++)
				{
					list.Add(items[x,y]);
				}
				this._list.Add(list);
			}
		}

		public List(int[] items)
		{
			this._list = new System.Collections.ArrayList(items.Length);
			for(int i = 0; i < items.Length; i++)
			{
				this._list.Add(items[i]);
			}
		}

		public List(int[,] items)
		{
			int width = items.GetLength(0);
			int height = items.GetLength(1);
			this._list = new System.Collections.ArrayList(height);
			for(int y = 0; y < height; y++)
			{
				System.Collections.ArrayList list = new System.Collections.ArrayList(width);
				for(int x = 0; x < width; x++)
				{
					list.Add(items[x,y]);
				}
				this._list.Add(list);
			}
		}

		public List(long[] items)
		{
			this._list = new System.Collections.ArrayList(items.Length);
			for(int i = 0; i < items.Length; i++)
			{
				this._list.Add(items[i]);
			}
		}

		public List(long[,] items)
		{
			int width = items.GetLength(0);
			int height = items.GetLength(1);
			this._list = new System.Collections.ArrayList(height);
			for(int y = 0; y < height; y++)
			{
				System.Collections.ArrayList list = new System.Collections.ArrayList(width);
				for(int x = 0; x < width; x++)
				{
					list.Add(items[x,y]);
				}
				this._list.Add(list);
			}
		}

		public List(float[] items)
		{
			this._list = new System.Collections.ArrayList(items.Length);
			for(int i = 0; i < items.Length; i++)
			{
				this._list.Add(items[i]);
			}
		}

		public List(float[,] items)
		{
			int width = items.GetLength(0);
			int height = items.GetLength(1);
			this._list = new System.Collections.ArrayList(height);
			for(int y = 0; y < height; y++)
			{
				System.Collections.ArrayList list = new System.Collections.ArrayList(width);
				for(int x = 0; x < width; x++)
				{
					list.Add(items[x,y]);
				}
				this._list.Add(list);
			}
		}

		public List(double[] items)
		{
			this._list = new System.Collections.ArrayList(items.Length);
			for(int i = 0; i < items.Length; i++)
			{
				this._list.Add(items[i]);
			}
		}

		public List(double[,] items)
		{
			int width = items.GetLength(0);
			int height = items.GetLength(1);
			this._list = new System.Collections.ArrayList(height);
			for(int y = 0; y < height; y++)
			{
				System.Collections.ArrayList list = new System.Collections.ArrayList(width);
				for(int x = 0; x < width; x++)
				{
					list.Add(items[x,y]);
				}
				this._list.Add(list);
			}
		}

		public List(decimal[] items)
		{
			this._list = new System.Collections.ArrayList(items.Length);
			for(int i = 0; i < items.Length; i++)
			{
				this._list.Add(items[i]);
			}
		}

		public List(decimal[,] items)
		{
			int width = items.GetLength(0);
			int height = items.GetLength(1);
			this._list = new System.Collections.ArrayList(height);
			for(int y = 0; y < height; y++)
			{
				System.Collections.ArrayList list = new System.Collections.ArrayList(width);
				for(int x = 0; x < width; x++)
				{
					list.Add(items[x,y]);
				}
				this._list.Add(list);
			}
		}

		public List(string[] items)
		{
			this._list = new System.Collections.ArrayList(items.Length);
			for(int i = 0; i < items.Length; i++)
			{
				this._list.Add(items[i]);
			}
		}

		public List(string[,] items)
		{
			int width = items.GetLength(0);
			int height = items.GetLength(1);
			this._list = new System.Collections.ArrayList(height);
			for(int y = 0; y < height; y++)
			{
				System.Collections.ArrayList list = new System.Collections.ArrayList(width);
				for(int x = 0; x < width; x++)
				{
					list.Add(items[x,y]);
				}
				this._list.Add(list);
			}
		}

		public System.Collections.IEnumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		public object this[int index]
		{
			get { return this._list[index]; }
			set { this._list[index] = value; }
		}

		public object Index(int index)
		{
			return this._list[index];
		}

		public int Count
		{
			get { return this._list.Count; }
		}

		public void Add(object item)
		{
			this._list.Add(item);
		}

		public void Insert(int index, object item)
		{
			this._list.Insert(index, item);
		}

		public void Remove(object value)
		{
			this._list.Remove(value);
		}

		public void RemoveAt(int index)
		{
			this._list.RemoveAt(index);
		}

		public void Clear()
		{
			this._list.Clear();
		}

		public object First()
		{
			if(this.Count == 0)
			{
				throw new Water.Error("List is empty.");
			}

			return this[0];
		}

		public Water.List NotFirst()
		{
			if(this.Count == 0)
			{
				throw new Water.Error("List is empty.");
			}

			Water.List child = new Water.List();
			for(int i = 1; i < this.Count; i++)
			{
				child.Add(this[i]);
			}
			return child;
		}

		public object Last()
		{
			if(this.Count == 0)
			{
				throw new Water.Error("List is empty.");
			}

			return this[this.Count - 1];
		}

		public Water.List NotLast()
		{
			if(this.Count == 0)
			{
				throw new Water.Error("List is empty.");
			}

			Water.List child = new Water.List();
			for(int i = 0; i < (this.Count - 1); i++)
			{
				child.Add(this[i]);
			}
			return child;
		}

		public bool Contains(object item)
		{
			foreach(object item2 in this._list)
			{
				if(item.Equals(item2))
				{
					return true;
				}
			}
			return false;
		}

		public List Copy()
		{
			List result = new List();
			for(int i = 0; i < this.Count; i++)
			{
				result.Add(this[i]);
			}
			return result;
		}

        public void Sort()
        {
            this._list.Sort(this._comparer);
        }

        public override bool Equals(object obj)
		{
			return Water.Instructions.AreEqual(this, obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public object[] ToArray()
		{
			object[] array = new object[this.Count];
			for (int i = 0; i < this.Count; i++)
			{
				array[i] = this[i];
			}
			return array;
		}

		#region IList Members

		bool System.Collections.IList.IsReadOnly
		{
			get { return this._list.IsReadOnly; }
		}

		void System.Collections.IList.RemoveAt(int index)
		{
			this._list.RemoveAt(index);
		}

		void System.Collections.IList.Insert(int index, object value)
		{
			this._list.Insert(index, value);
		}

		int System.Collections.IList.IndexOf(object value)
		{
			return this._list.IndexOf(value);
		}

		int System.Collections.IList.Add(object value)
		{
			return this._list.Add(value);
		}

		bool System.Collections.IList.IsFixedSize
		{
			get { return this._list.IsFixedSize; }
		}

		bool System.Collections.IList.Contains(object item)
		{
			return this._list.Contains(item);
		}

		#endregion

		#region ICollection Members

		bool System.Collections.ICollection.IsSynchronized
		{
			get { return this._list.IsSynchronized; }
		}

		void System.Collections.ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		object System.Collections.ICollection.SyncRoot
		{
			get { return this._list.SyncRoot; }
		}

		#endregion

	}
}
