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

namespace Bamboo.Query
{
    public class RowComparer : System.Collections.IComparer
    {
		private System.Collections.IEnumerable _indexes;
		private bool[] _ascendingOrder;
		private System.Collections.Comparer _comparer;

		public RowComparer(System.Collections.IEnumerable indexes)
        {
			this._indexes = indexes;
			this._ascendingOrder = null;
			this._comparer = System.Collections.Comparer.Default;
		}

		public RowComparer(int[] indexes, bool[] ascendingOrder)
		{
			this._indexes = indexes;
			this._ascendingOrder = ascendingOrder;
			this._comparer = System.Collections.Comparer.Default;
		}

		public int Compare(object r, object s)
        {
			return Compare((System.Collections.IList)r, (System.Collections.IList)s);
        }

		private int Compare(System.Collections.IList r, System.Collections.IList s)
        {
			if (this._ascendingOrder == null)
			{
				foreach (int index in this._indexes)
				{
					int cmp = this._comparer.Compare(r[index], s[index]);
					if (cmp != 0)
					{
						return cmp;
					}
				}
				return 0;
			}
			else
			{
				int i = 0;
				foreach (int index in this._indexes)
				{
					int cmp = this._comparer.Compare(r[index], s[index]);
					if (cmp != 0)
					{
						if (this._ascendingOrder[i])
						{
							return cmp;
						}
						else
						{
							return 0 - cmp;
						}
					}
					i++;
				}
				return 0;
			}
        }

    }
}
