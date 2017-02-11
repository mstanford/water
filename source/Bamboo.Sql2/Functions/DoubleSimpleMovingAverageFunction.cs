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

namespace Bamboo.Sql2.Functions
{
	public class DoubleSimpleMovingAverageFunction : Function
	{
		private SimpleMovingAverageFunction _sma1;
		private SimpleMovingAverageFunction _sma2;

		public DoubleSimpleMovingAverageFunction(int period1, int period2)
		{
			this._sma1 = new SimpleMovingAverageFunction(period1);
			this._sma2 = new SimpleMovingAverageFunction(period2);
		}

		public override decimal? Next(decimal value)
		{
			decimal? sma1 = this._sma1.Next(value);
			decimal? sma2 = this._sma2.Next(value);
			if (sma1 == null || sma2 == null)
			{
				return null;
			}
			return sma1 - sma2;
		}

		public override void Clear()
		{
			this._sma1.Clear();
			this._sma2.Clear();
		}

	}
}
