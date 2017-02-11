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
	public class SelectIterator : Iterator
	{
		private Iterator _iterator;
		private List<Iterators.ColumnIterator> _columnIterators;

		public SelectIterator(Iterator iterator, List<Iterators.ColumnIterator> columnIterators)
		{
			this._iterator = iterator;
			this._columnIterators = columnIterators;
		}

		public override List<string> Columns()
		{
			List<string> columns = new List<string>();
			foreach (Iterators.ColumnIterator columnIterator in this._columnIterators)
			{
				columns.Add(columnIterator.Column());
			}
			return columns;
		}

		public override Bamboo.DataStructures.Tuple Next()
		{
			Bamboo.DataStructures.Tuple row = this._iterator.Next();
			if (row == null)
			{
				return null;
			}
			try
			{
				object[] values = new object[this._columnIterators.Count];
				for (int i = 0; i < values.Length; i++)
				{
					values[i] = this._columnIterators[i].Next(row);
				}
				return new Bamboo.DataStructures.Tuple(values);
			}
			catch (System.Exception exception)
			{
				throw exception;
			}
		}

	}
}
