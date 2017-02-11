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
	public class UnionIterator : Iterator
	{
		private Iterator _a;
		private Iterator _b;
		private Iterator _iterator;
		private bool _first;

		public UnionIterator(Iterator a, Iterator b)
		{
			this._a = a;
			this._b = b;

			this._first = true;
			this._iterator = this._a;
		}

		public override List<string> Columns()
		{
			return this._a.Columns();
		}

		public override Bamboo.DataStructures.Tuple Next()
		{
			Bamboo.DataStructures.Tuple row = this._iterator.Next();
			if (row == null && this._first)
			{
				this._first = false;
				this._iterator = this._b;
				row = this._iterator.Next();
			}
			return row;
		}

	}
}
