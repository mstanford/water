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

namespace Bamboo.Sql2
{
	public class MemoryDatabase : Database
	{
		private Dictionary<string, Bamboo.DataStructures.Table> _tables = new Dictionary<string, Bamboo.DataStructures.Table>();
		private Dictionary<string, Query> _views = new Dictionary<string, Query>();

		public MemoryDatabase()
		{
		}

		public override Bamboo.DataStructures.Table ReadTable(string name)
		{
			return this._tables[name];
		}

		public override Query ReadView(string name)
		{
			return this._views[name];
		}

		public void AddTable(string name, Bamboo.DataStructures.Table table)
		{
			this._tables.Add(name, table);
		}

		public void AddView(string name, Query view)
		{
			this._views.Add(name, view);
		}

		public Bamboo.DataStructures.Table Evaluate(object query)
		{
			return Evaluate(this, query);
		}

	}
}
