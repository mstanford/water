// ------------------------------------------------------------------------------
// 
// Copyright (c) 2007-2008 Swampware, Inc.
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
	/// <summary>
	/// Summary description for Table.
	/// </summary>
	public class Table
	{
		private System.Collections.ArrayList _columns;
		private System.Collections.ArrayList _rows;
		private int _refCount = 0;

		public Table()
		{
			this._columns = new System.Collections.ArrayList(0);
			this._rows = new System.Collections.ArrayList(0);

			this.Open();
		}

		public Table(System.Collections.ICollection columns)
		{
			this._columns = new System.Collections.ArrayList(columns.Count);
			foreach (string column in columns)
			{
				this.AddColumn(column);
			}

			this._rows = new System.Collections.ArrayList();

			this.Open();
		}

		public Table(System.Collections.ICollection columns, System.Collections.ICollection rows)
		{
			this._columns = new System.Collections.ArrayList(columns.Count);
			foreach (string column in columns)
			{
				this.AddColumn(column);
			}

			this._rows = new System.Collections.ArrayList(rows);

			this.Open();
		}

		public System.Collections.IList Columns
		{
			get { return this._columns; }
		}

		public System.Collections.IList Rows
		{
			get { return this._rows; }
		}

		public void AddColumn(string column)
		{
			this._columns.Add(new Bamboo.Query.Column(column));
		}

		public void AddColumn(Bamboo.Query.Column column)
		{
			this._columns.Add(column);
		}

		public void AddRow(System.Collections.IList row)
		{
			this._rows.Add(row);
		}

		public void Open()
		{
			System.Threading.Interlocked.Increment(ref this._refCount);
		}

		public void Close()
		{
			if (System.Threading.Interlocked.Decrement(ref this._refCount) == 0)
			{
				this._columns.Clear();
				this._rows.Clear();
			}
		}

	}
}
