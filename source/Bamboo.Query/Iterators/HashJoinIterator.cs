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
	public class HashJoinIterator : Iterator
	{
		private Iterator R;
		private Iterator S;
		private int _r_index;
		private int _s_index;
		private Bamboo.Query.Index _index;
		private System.Collections.IList r = null;
		private System.Collections.IList sRows = null;
		private int sRowsPosition = -1;
		private bool _reverse;

		public HashJoinIterator(Iterator R, Iterator S, int r_index, int s_index)
		{
			if (R.EstimatedSize > S.EstimatedSize)
			{
				this.R = R;
				this.S = S;
				this._r_index = r_index;
				this._s_index = s_index;
				this._reverse = false;
			}
			else
			{
				this.R = S;
				this.S = R;
				this._r_index = s_index;
				this._s_index = r_index;
				this._reverse = true;
			}
		}

		public override int EstimatedSize
		{
			get { return this.R.EstimatedSize * this.S.EstimatedSize; }
		}

		public override void Open()
		{
			this.R.Open();
			this.S.Open();


			this._index = new Bamboo.Query.Index(new int[] { this._s_index }, this.S.EstimatedSize);
			System.Collections.IList ss;
			while ((ss = this.S.GetNext()) != null)
			{
				this._index.Add(ss);
			}
		}

		public override System.Collections.IList GetNext()
		{
			if (this.sRows == null || this.sRows.Count == this.sRowsPosition)
			{
				this.r = null;
			}

			while(this.r == null || this.sRows == null)
			{
				this.r = this.R.GetNext();
				if (this.r == null)
				{
					return null;
				}

				this.sRows = this._index[Bamboo.Query.Index.GetHash(this.r, this._r_index)];
				this.sRowsPosition = 0;
			}

			if (this.sRowsPosition == -1)
			{
				return null;
			}

			System.Collections.IList s = (System.Collections.IList)this.sRows[this.sRowsPosition];
			this.sRowsPosition++;
			if (this._reverse)
			{
				return Join(s, r);
			}
			else
			{
				return Join(r, s);
			}
		}

		private static System.Collections.IList Join(System.Collections.IList r, System.Collections.IList s)
		{
			System.Collections.IList t = new System.Collections.ArrayList(r.Count + s.Count);
			for (int i = 0; i < r.Count; i++)
			{
				t.Add(r[i]);
			}
			for (int i = 0; i < s.Count; i++)
			{
				t.Add(s[i]);
			}
			return t;
		}

		public override void Close()
		{
			this._index.Close();
			this._index = null;

			this.R.Close();
			this.S.Close();
		}

	}
}
