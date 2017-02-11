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
	public class Set : System.Collections.IEnumerable, System.IComparable
	{
		private System.Collections.SortedList _items = new System.Collections.SortedList(new Comparer());
		private Comparer _comparer = new Comparer();

		public Set()
		{
		}

		public Set(System.Collections.IEnumerable enumerable)
		{
			foreach (object item in enumerable)
			{
				this.Add(item);
			}
		}

		public object this[int index]
		{
			get { return this._items.GetByIndex(index); }
		}

		public int Count
		{
			get { return this._items.Count; }
		}

		public void Add(object item)
		{
			if (!this._items.ContainsKey(item))
			{
				this._items.Add(item, item);
			}
		}

		public bool Contains(object item)
		{
		    return this._items.ContainsKey(item);
		}

		public void Remove(int index)
		{
			this._items.RemoveAt(index);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Set))
			{
				return false;
			}
			Set set = (Set)obj;
			if (this._items.Count != set._items.Count)
			{
				return false;
			}
			for (int i = 0; i < this._items.Count; i++)
			{
				if (!this._items.GetByIndex(i).Equals(set._items.GetByIndex(i)))
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

		public System.Collections.IEnumerator GetEnumerator()
		{
			return this._items.Keys.GetEnumerator();
		}

		public int CompareTo(object obj)
		{
			return this._comparer.Compare(this, obj);
		}

		public Set Union(Set b)
		{
			Set c = new Set();
			foreach (object item in this) c.Add(item);
			foreach (object item in b) c.Add(item);
			return c;
		}

		public Set Intersection(Set b)
		{
			Set c = new Set();
			foreach (object item in this) if (b.Contains(item)) c.Add(item);
			return c;
		}

		public Set Difference(Set b)
		{
			Set c = new Set();
			foreach (object item in this) if (!b.Contains(item)) c.Add(item);
			return c;
		}

		public Set CartesianProduct(Set b)
		{
			Set c = new Set();
			foreach (object aa in this)
			{
				foreach (object bb in b)
				{
					c.Add(new Surf.Tuple(new object[] { aa, bb }));
				}
			}
			return c;
		}

		public Set Domain()
		{
			Surf.Set domain = new Surf.Set();
			foreach (object obj in this)
			{
				if (!(obj is Tuple))
				{
					throw new System.Exception("Invalid relation.");
				}
				Tuple tuple = (Tuple)obj;
				if (tuple.Count != 2)
				{
					throw new System.Exception("Invalid relation.");
				}
				domain.Add(tuple[0]);
			}
			return domain;
		}

		public Set Range()
		{
			Surf.Set range = new Surf.Set();
			foreach (object obj in this)
			{
				if (!(obj is Tuple))
				{
					throw new System.Exception("Invalid relation.");
				}
				Tuple tuple = (Tuple)obj;
				if (tuple.Count != 2)
				{
					throw new System.Exception("Invalid relation.");
				}
				range.Add(tuple[1]);
			}
			return range;
		}

		public object Apply(object key)
		{
			if (key == null)
			{
				throw new System.NullReferenceException();
			}

			foreach (object obj in this)
			{
				if (!(obj is Tuple))
				{
					throw new System.Exception("Invalid function.");
				}
				Tuple tuple = (Tuple)obj;
				if (tuple.Count != 2)
				{
					throw new System.Exception("Invalid function.");
				}

				if (tuple[0].Equals(key))
				{
					return tuple[1];
				}
			}
			throw new System.Exception("Key is not defined.");
		}

		public bool IsDefined(object key)
		{
			foreach (object obj in this)
			{
				if (!(obj is Tuple))
				{
					throw new System.Exception("Invalid function.");
				}
				Tuple tuple = (Tuple)obj;
				if (tuple.Count != 2)
				{
					throw new System.Exception("Invalid function.");
				}

				if (tuple[0].Equals(key))
				{
					return true;
				}
			}
			return false;
		}

		public void Define(object key, object value)
		{
			for(int i = 0; i < this._items.Count; i++)
			{
				object obj = this._items.GetByIndex(i);

				if (!(obj is Tuple))
				{
					throw new System.Exception("Invalid function.");
				}
				Tuple tuple = (Tuple)obj;
				if (tuple.Count != 2)
				{
					throw new System.Exception("Invalid function.");
				}

				if (tuple[0].Equals(key))
				{
					this._items.Remove(tuple);
					break;
				}
			}

			object item = new Surf.Tuple(new object[] { key, value });
			this._items.Add(item, item);
		}

		public Set Transpose()
		{
			Set b = new Set();

			foreach (object obj in this)
			{
				if (!(obj is Tuple))
				{
					throw new System.Exception("Invalid relation.");
				}
				Tuple tuple = (Tuple)obj;
				if (tuple.Count != 2)
				{
					throw new System.Exception("Invalid relation.");
				}

				b.Add(new Tuple(new object[] { tuple[1], tuple[0] }));
			}

			return b;
		}

		public Set Compose(Set b)
		{
			Set c = new Set();

			foreach (object obj in this)
			{
				if (!(obj is Tuple))
				{
					throw new System.Exception("Invalid relation.");
				}
				Tuple tuple = (Tuple)obj;
				if (tuple.Count != 2)
				{
					throw new System.Exception("Invalid relation.");
				}

				c.Add(new Tuple(new object[] { tuple[0], b.Apply(tuple[1]) }));
			}

			return c;
		}

		public Set Nest()
		{
			System.Collections.Hashtable multimap = new System.Collections.Hashtable();
			foreach (object obj in this)
			{
				if (!(obj is Tuple))
				{
					throw new System.Exception("Invalid function.");
				}
				Tuple tuple = (Tuple)obj;
				if (tuple.Count != 2)
				{
					throw new System.Exception("Invalid function.");
				}

				object key = tuple[0];
				object value = tuple[1];
				if (!multimap.ContainsKey(key))
				{
					multimap.Add(key, new System.Collections.ArrayList());
				}
				((System.Collections.ArrayList)multimap[key]).Add(value);
			}

			Set protoset = new Set();
			foreach (System.Collections.DictionaryEntry dictionaryEntry in multimap)
			{
				protoset.Add(new Tuple(new object[] { dictionaryEntry.Key, new Set((System.Collections.ArrayList)dictionaryEntry.Value) }));
			}
			return protoset;
		}

		public Set Unnest()
		{
			Set b = new Set();

			foreach (Surf.Tuple obj in this)
			{
				if (!(obj is Tuple))
				{
					throw new System.Exception("Invalid relation.");
				}
				Tuple tuple = (Tuple)obj;
				if (tuple.Count != 2)
				{
					throw new System.Exception("Invalid relation.");
				}

				object key = tuple[0];
				Surf.Set value = (Surf.Set)tuple[1];
				foreach (object v in value)
				{
					b.Add(new Tuple(new object[] { key, v }));
				}
			}

			return b;
		}

	}
}
