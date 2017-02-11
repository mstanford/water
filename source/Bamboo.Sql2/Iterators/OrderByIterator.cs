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
	public class OrderByIterator : Iterator
	{
		private Iterator _iterator;
		private List<Sort> _orderBy;
		private Iterator _sortedIterator;

		public OrderByIterator(Iterator iterator, List<Sort> orderBy)
		{
			this._iterator = iterator;
			this._orderBy = orderBy;

			Bamboo.DataStructures.Table table = this._iterator.ToTable();
//TODO			table.Rows.Sort(new Bamboo.DataStructures.TupleComparer(GetOrdinals(columns)));
			this._sortedIterator = new TableIterator(table);
		}

		public override List<string> Columns()
		{
			return this._sortedIterator.Columns();
		}

		public override Bamboo.DataStructures.Tuple Next()
		{
			return this._sortedIterator.Next();
		}

		public static Bamboo.DataStructures.Table Sort(Bamboo.DataStructures.Table table, int[] columns)
		{
			return null;
		}

	}
}
