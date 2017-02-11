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

namespace Bamboo.Query
{
	public class Database
	{
		private System.Collections.Hashtable _tables = new System.Collections.Hashtable();
		private System.Collections.Hashtable _views = new System.Collections.Hashtable();
		private object _lock = new object();

		public Database()
		{
		}

		public void Add(string name, Bamboo.Query.Table table)
		{
			lock (this._lock)
			{
				this._tables[name] = table;
			}
		}

		public void Add(Bamboo.Query.View view)
		{
		    lock (this._lock)
		    {
				this._views[view.Name] = view;
			}
		}

		public System.Collections.Hashtable Lookup(string[] names)
		{
			System.Collections.Hashtable tables = new System.Collections.Hashtable(names.Length);
			Bamboo.Query.View[] views = new Bamboo.Query.View[names.Length];
			int k = 0;

			lock (this._lock)
			{
				for (int i = 0; i < names.Length; i++)
				{
					string name = names[i];

					if (this._tables.ContainsKey(name))
					{
						tables.Add(name, this._tables[name]);
					}
					else if (this._views.ContainsKey(name))
					{
						views[k] = (Bamboo.Query.View)this._views[name];
						k++;
					}
					else
					{
						throw new System.Exception("Table or view does not exist: " + name);
					}
				}
			}

			for (int i = 0; i < k; i++)
			{
				Bamboo.Query.View view = views[i];
				tables.Add(view.Name, Bamboo.Query.Evaluator.Evaluate(view.Query, this));
			}

			return tables;
		}

		public void Close()
		{
			foreach (Bamboo.Query.Table table in this._tables.Values)
			{
				table.Close();
			}
			this._tables.Clear();
			this._tables = null;

			this._views.Clear();
			this._views = null;
		}

	}
}
