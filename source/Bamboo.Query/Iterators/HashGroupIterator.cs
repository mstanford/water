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

namespace Bamboo.Query.Iterators
{
	/// <summary>
	/// Summary description for HashGroupIterator.
	/// </summary>
	public class HashGroupIterator : Iterator
	{
		private Iterator R;
		private System.Collections.IEnumerable _indexes;
		private System.Collections.IEnumerable _functions;
		private int _length = 0;
		private Bamboo.Query.Index _index;
		private System.Collections.IEnumerator _enumerator;

		public HashGroupIterator(Iterator R, System.Collections.IEnumerable indexes, System.Collections.IEnumerable functions)
		{
			this.R = R;
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
			this._index = new Bamboo.Query.Index(this._indexes, this.R.EstimatedSize);

			this.R.Open();
			System.Collections.IList r;
			while ((r = this.R.GetNext()) != null)
			{
				this._index.Add(r);
			}
			this.R.Close();

			this._enumerator = this._index.Keys.GetEnumerator();
		}

		public override System.Collections.IList GetNext()
		{
			if (this._enumerator == null)
			{
				return null;
			}
			else
			{
				if (this._enumerator.MoveNext())
				{
					string key = (string)this._enumerator.Current;
					System.Collections.IList rows = this._index[key];
					return CreateRow(rows);
				}
				else
				{
					this._enumerator = null;
					return null;
				}
			}
		}

		private System.Collections.IList CreateRow(System.Collections.IList rows)
		{
			System.Collections.IList result = new System.Collections.ArrayList(this._length);
			foreach (Bamboo.Query.Aggregators.Aggregator aggregator in this._functions)
			{
				aggregator.Open();

				foreach (System.Collections.IList row in rows)
				{
					aggregator.Apply(row);
				}
			}

			System.Collections.IList row2 = (System.Collections.IList)rows[0];
			foreach (int index in this._indexes)
			{
				result.Add(row2[index]);
			}
			foreach (Bamboo.Query.Aggregators.Aggregator aggregator in this._functions)
			{
				result.Add(aggregator.Close());
			}
			return result;
		}

		public override void Close()
		{
			this._index.Close();
			this._index = null;

			this._enumerator = null;
		}

	}
}
