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

namespace Bamboo.Sql2.Iterators
{
	public class TableIterator : Iterator
	{
		private Bamboo.DataStructures.Table _table;
		private int _i;

		public TableIterator(Bamboo.DataStructures.Table table) //TODO pass in a database instead of a table.
		{
			this._table = table;
			this._i = 0;
		}

		public override List<string> Columns()
		{
			List<string> columns = new List<string>();
			for (int i = 0; i < this._table.Columns.Count; i++)
			{
				columns.Add((string)this._table.Columns[i]);
			}
			return columns;
		}

		public override Bamboo.DataStructures.Tuple Next()
		{
			if (this._i == this._table.Rows.Count)
			{
				return null;
			}
			Bamboo.DataStructures.Tuple row = this._table.Rows[this._i];
			this._i++;
			return row;
		}

	}
}
