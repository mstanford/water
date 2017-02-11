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

namespace Bamboo.Query.Planning
{
	/// <summary>
	/// Summary description for ScanNode.
	/// </summary>
	public class TableScanNode : Node
	{
		private Bamboo.Query.Table _table;
		private System.Collections.IEnumerable _columnCoordinates;

		public TableScanNode(string tableName, Bamboo.Query.Table table)
		{
			if (table == null)
			{
				throw new System.Exception("Table is not defined: " + tableName);
			}

			this._table = table;

			ColumnCoordinate[] columnCoordinates = new ColumnCoordinate[this._table.Columns.Count];
			for (int i = 0; i < columnCoordinates.Length; i++)
			{
				columnCoordinates[i] = new ColumnCoordinate(tableName + "." + ((Bamboo.Query.Column)this._table.Columns[i]).Name, i);
			}
			this._columnCoordinates = columnCoordinates;
		}

		public override string Name
		{
			get { return "TABLE_SCAN"; }
		}

		public override System.Collections.IEnumerable ColumnCoordinates
		{
			get { return this._columnCoordinates; }
		}

		public override Bamboo.Query.Iterators.Iterator CreateIterator()
		{
			return new Bamboo.Query.Iterators.TableScanIterator(this._table);
		}

	}
}
