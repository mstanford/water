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
	public class TopIterator : Iterator
	{
		private Iterator R;
		private int _max;
		private int _count = 0;

		public TopIterator(Iterator R, int max)
		{
			this.R = R;
			this._max = max;
		}

		public override int EstimatedSize
		{
			get { return this._max; }
		}

		public override void Open()
		{
			this.R.Open();
		}

		public override System.Collections.IList GetNext()
		{
			if (this._count == this._max)
			{
				return null;
			}

			System.Collections.IList r = this.R.GetNext();
			if (r == null)
			{
				return null;
			}

			this._count++;

			return r;
		}

		public override void Close()
		{
			this.R.Close();
		}

	}
}
