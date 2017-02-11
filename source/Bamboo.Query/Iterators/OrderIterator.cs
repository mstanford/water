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
	public class OrderIterator : Iterator
	{
		private Iterator R;
		private System.Collections.IComparer _comparer;
		private System.Collections.ArrayList _rows = new System.Collections.ArrayList();
		private System.Collections.IEnumerator _enumerator;

		public OrderIterator(Iterator R)
		{
			this.R = R;
			this._comparer = System.Collections.Comparer.Default;
		}

		public OrderIterator(Iterator R, System.Collections.IComparer comparer)
		{
			this.R = R;
			this._comparer = comparer;
		}

		public override int EstimatedSize
		{
			get { return this.R.EstimatedSize; }
		}

		public override void Open()
		{
			this.R.Open();
			System.Collections.IList r;
			while ((r = this.R.GetNext()) != null)
			{
				this._rows.Add(r);
			}
			this.R.Close();

			this._rows.Sort(this._comparer);

			this._enumerator = this._rows.GetEnumerator();
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
					return (System.Collections.IList)this._enumerator.Current;
				}
				else
				{
					this._enumerator = null;
					return null;
				}
			}
		}

		public override void Close()
		{
			this._enumerator = null;
		}

	}
}
