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

namespace Bamboo.DataStructures
{
	public class Table
	{
		private Tuple _columns;
		private List<Tuple> _rows;

		public Table(Tuple columns, List<Tuple> rows)
		{
			_columns = columns;
			_rows = rows;
		}

		public Table(System.Collections.IList columns, System.Collections.IList rows)
		{
			object[] values = new object[columns.Count];
			for (int i = 0; i < values.Length; i++)
			{
				values[i] = columns[i];
			}
			_columns = new Tuple(values);

			_rows = new List<Tuple>(rows.Count);
			foreach(System.Collections.IList row in rows)
			{
				values = new object[row.Count];
				for (int i = 0; i < values.Length; i++)
				{
					values[i] = row[i];
				}
				_rows.Add(new Tuple(values));
			}
		}

		public Tuple Columns
		{
			get { return this._columns; }
		}

		public List<Tuple> Rows
		{
			get { return this._rows; }
		}

	}
}
