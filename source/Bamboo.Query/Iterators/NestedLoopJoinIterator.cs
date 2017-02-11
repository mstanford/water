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
	public class NestedLoopJoinIterator : Iterator
	{
		private Iterator R;
		private Iterator S;
		private int _r_index;
		private int _s_index;
		private System.Collections.IList s;

		public NestedLoopJoinIterator(Iterator R, Iterator S, int r_index, int s_index)
		{
			this.R = R;
			this.S = S;
			this._r_index = r_index;
			this._s_index = s_index;
		}

		public override int EstimatedSize
		{
			get { return this.R.EstimatedSize * this.S.EstimatedSize; }
		}

		public override void Open()
		{
			this.R.Open();
			this.S.Open();
			s = this.S.GetNext();
		}

		public override System.Collections.IList GetNext()
		{
			System.Collections.IList r = null;
			do {
				r = this.R.GetNext();
				if(r == null)
				{
					this.R.Close();
					s = this.S.GetNext();
					if (s == null)
					{
						return null;
					}
					this.R.Open();
					r = this.R.GetNext();

				}
			} while(!CanJoin(r, s));
			return Join(r, s);
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

		private bool CanJoin(System.Collections.IList r, System.Collections.IList s)
		{
			object rVal = r[this._r_index];
			if (rVal == null)
			{
				return false;
			}
			object sVal = s[this._s_index];
			if (sVal == null)
			{
				return false;
			}
			return rVal.Equals(sVal);
		}

		public override void Close()
		{
			this.R.Close();
			this.S.Close();
		}

	}
}
