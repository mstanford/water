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

namespace bamboo.Controls.ProjectExplorer
{
	/// <summary>
	/// Summary description for SortingTree.
	/// </summary>
	public class SortingTree : System.Collections.IEnumerable
	{
		private string _name;
		private object _value;
		private System.Collections.Hashtable _nodes = new System.Collections.Hashtable();
		private bool _isLeaf = false;

		public SortingTree()
		{
			// Root node.
		}

		private SortingTree(string name)
		{
			this._name = name;
		}

		private SortingTree(string name, object value)
		{
			// Leaf node.
			this._name = name;
			this._value = value;
			this._isLeaf = true;
		}

		public virtual string Name
		{
			get { return this._name; }
		}

		public virtual object Value
		{
			get { return this._value; }
		}

		public virtual System.Collections.ICollection Nodes
		{
			get { return this._nodes.Values; }
		}

		public object this[string key]
		{
			get { return this.Lookup(key); }
		}

		public virtual bool IsLeaf()
		{
			return this._isLeaf;
		}

		public virtual void Add(System.Collections.IEnumerable keys, object value)
		{
			object key = car(keys);
			System.Collections.IList keys2 = cdr(keys);

			if(keys2.Count == 0)
			{
				if (this._nodes.ContainsKey(key))
				{
					this._nodes[key] = new SortingTree(key.ToString(), value);
				}
				else
				{
					this._nodes.Add(key, new SortingTree(key.ToString(), value));
				}
			}
			else
			{
				if(this._nodes.ContainsKey(key))
				{
					((SortingTree)this._nodes[key]).Add(keys2, value);
				}
				else
				{
					SortingTree node = new SortingTree(key.ToString());
					node.Add(keys2, value);
					this._nodes.Add(key, node);
				}
			}
		}

		public virtual void Clear()
		{
			foreach (SortingTree node in this._nodes)
			{
				node.Clear();
			}
			this._nodes.Clear();
		}

		public virtual object Lookup(string key)
		{
			return this.Lookup(new string[] { key });
		}

		public virtual object Lookup(System.Collections.IEnumerable keys)
		{
			object key = car(keys);
			System.Collections.IList keys2 = cdr(keys);

			if (keys2.Count == 0)
			{
				if (this._nodes.ContainsKey(key))
				{
					SortingTree tree = (SortingTree)this._nodes[key];
					if (tree.Nodes.Count > 0)
					{
						return tree;
					}
					else
					{
						return tree.Value;
					}
				}
				else
				{
					return null;
				}
			}
			else
			{
				if (this._nodes.ContainsKey(key))
				{
					return ((SortingTree)this._nodes[key]).Lookup(keys2);
				}
				else
				{
					return null;
				}
			}
		}

		public virtual void Remove(System.Collections.IEnumerable keys)
		{
			object key = car(keys);
			System.Collections.IList keys2 = cdr(keys);

			if (keys2.Count == 0)
			{
				this._nodes.Remove(key);
			}
			else
			{
				if (this._nodes.ContainsKey(key))
				{
					((SortingTree)this._nodes[key]).Remove(keys2);
				}
			}
		}

		public static object car(System.Collections.IEnumerable keys)
		{
			bool first = true;
			foreach (object key in keys)
			{
				if (first)
				{
					return key;
				}
			}
			throw new System.Exception("List is empty.");
		}

		public static System.Collections.IList cdr(System.Collections.IEnumerable keys)
		{
			System.Collections.ArrayList keys2 = new System.Collections.ArrayList();
			bool first = true;
			foreach (object key in keys)
			{
				if (first)
				{
					first = false;
				}
				else
				{
					keys2.Add(key);
				}
			}
			return keys2;
		}

		#region IEnumerable Members

		public System.Collections.IEnumerator GetEnumerator()
		{
			return this.Nodes.GetEnumerator();
		}

		#endregion

		public override bool Equals(object obj)
		{
			if(!(obj is SortingTree))
			{
				return false;
			}

			SortingTree tree = (SortingTree)obj;
			if(!this.Name.Equals(tree.Name))
			{
				return false;
			}
			if(!this.Value.Equals(tree.Value))
			{
				return false;
			}
			if(this.Nodes.Count != tree.Nodes.Count)
			{
				return false;
			}

			foreach (SortingTree node in tree.Nodes)
			{
				SortingTree node2 = (SortingTree)this._nodes[node.Name];
				if(!node.Equals(node2))
				{
					return false;
				}
			}

			return true;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

	}
}
