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
	public class GroupByIterator : Iterator
	{
		private Iterator _iterator;
		private Iterator _groupedIterator;

		public GroupByIterator(Iterator iterator, int[] groupByColumns)
		{
			this._iterator = iterator;
			this._groupedIterator = new TableIterator(Group(this._iterator.ToTable(), groupByColumns));
		}

		public override List<string> Columns()
		{
			return this._groupedIterator.Columns();
		}

		public override Bamboo.DataStructures.Tuple Next()
		{
			return this._groupedIterator.Next();
		}

		public static Bamboo.DataStructures.Table Group(Bamboo.DataStructures.Table table, int[] columns)
		{
//TODO			table.Rows.Sort(new Bamboo.DataStructures.TupleComparer(GetOrdinals(columns)));

			int count = table.Rows.Count;
			int columnsLength = columns.Length;

			object[] groupedColumns = new object[columnsLength];
			for (int k = 0; k < columnsLength; k++)
			{
				groupedColumns[k] = table.Columns[columns[k]];
			}

			List<Bamboo.DataStructures.Tuple> groupedRows = new List<Bamboo.DataStructures.Tuple>(table.Rows.Count);
			Bamboo.DataStructures.Tuple row = table.Rows[0];
			object[] groupedRow = new object[columnsLength];
			for (int k = 0; k < columnsLength; k++)
			{
				groupedRow[k] = row[columns[k]];
			}
			groupedRows.Add(new Bamboo.DataStructures.Tuple(groupedRow));

			for (int i = 1; i < count; i++)
			{
				row = table.Rows[i];
				Bamboo.DataStructures.Tuple row2 = table.Rows[i - 1];

				bool rowEquals = true;
				for (int j = 0; j < columnsLength; j++)
				{
					int cmp = System.Collections.Comparer.Default.Compare(row2[columns[j]], row[columns[j]]);
					if (cmp != 0)
					{
						rowEquals = false;
						break;
					}
				}

				if (!rowEquals)
				{
					groupedRow = new object[columnsLength];
					for (int k = 0; k < columnsLength; k++)
					{
						groupedRow[k] = row[columns[k]];
					}
					groupedRows.Add(new Bamboo.DataStructures.Tuple(groupedRow));
				}
			}

			return new Bamboo.DataStructures.Table(new Bamboo.DataStructures.Tuple(groupedColumns), groupedRows);
		}

		/*
		private Iterator R;
		private System.Collections.IEnumerable _indexes;
		private System.Collections.IEnumerable _functions;
		private int _length = 0;
		private System.Collections.IList r;

		public GroupIterator(Iterator R, System.Collections.IEnumerable indexes, System.Collections.IEnumerable functions)
		{
			this.R = new Bamboo.Query.Iterators.OrderIterator(R, new RowComparer(indexes));
			this._indexes = indexes;
			this._functions = functions;

			foreach (object index in this._indexes)
			{
				object ignore = index;
				this._length++;
			}
			foreach (object function in this._functions)
			{
				object ignore = function;
				this._length++;
			}
		}

		public override int EstimatedSize
		{
			get { return this.R.EstimatedSize; }
		}

		public override void Open()
		{
			this.R.Open();

			this.r = this.R.GetNext();
			if (this.r != null)
			{
				foreach (Bamboo.Query.Aggregators.Aggregator aggregator in this._functions)
				{
					aggregator.Open();
					aggregator.Apply(this.r);
				}
			}
		}

		public override System.Collections.IList GetNext()
		{
			// If r is null, return null.  If we never have data this method will not run.  
			// When we're out of data we can set r to null to prevent this method from 
			// continuing to run.
			if (this.r == null)
			{
				return null;
			}


			// While the row looks like the previous row, apply the row the functions and 
			// loop.  Once this row is different from the previous aggregate the results 
			// (closing the functions), then set the new row as the previous row, open the 
			// apply it functions and apply the row to the functions.
			System.Collections.IList s;
			while((s = this.R.GetNext()) != null)
			{
				if (AreInSameGroup(this.r, s))
				{
					foreach (Bamboo.Query.Aggregators.Aggregator aggregator in this._functions)
					{
						aggregator.Apply(s);
					}
				}
				else
				{
					System.Collections.IList row = CreateRow(this.r);

					this.r = s;
					foreach (Bamboo.Query.Aggregators.Aggregator aggregator in this._functions)
					{
						aggregator.Open();
						aggregator.Apply(this.r);
					}

					return row;
				}
			}


			// Out of data.  Return the last result and terminate the iterator.
			System.Collections.IList row2 = CreateRow(this.r);

			this.r = null;

			return row2;
		}

		private bool AreInSameGroup(System.Collections.IList r, System.Collections.IList s)
		{
			foreach (int index in this._indexes)
			{
				if (System.Collections.Comparer.Default.Compare(r[index], s[index]) != 0)
				{
					return false;
				}
			}
			return true;
		}

		private System.Collections.IList CreateRow(System.Collections.IList row)
		{
			// Create new row with index positions and add function values
			System.Collections.IList result = new System.Collections.ArrayList(this._length);
			foreach (int index in this._indexes)
			{
				result.Add(row[index]);
			}
			foreach (Bamboo.Query.Aggregators.Aggregator aggregator in this._functions)
			{
				result.Add(aggregator.Close());
			}
			return result;
		}

		public override void Close()
		{
			this.R.Close();
		}
		*/

	}
}
