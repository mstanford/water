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
	public class ExponentialMovingAverageFunction : Function
	{
		private int _period;
		private decimal? _ema;
		private decimal _c;
		private SimpleMovingAverageFunction _sma;

		public ExponentialMovingAverageFunction(int period)
		{
			this._period = period;
			this._ema = null;
			this._c = 2m / (period + 1);
			this._sma = new SimpleMovingAverageFunction(period);
		}

		public override decimal? Next(decimal value)
		{
			if (this._ema == null)
			{
				this._ema = this._sma.Next(value);
				return this._ema;
			}
			else
			{
				//this._ema = (this._c * value) + ((1 - this._c) * this._ema);
				this._ema = this._ema + (this._c * (value - this._ema));
				return this._ema;
			}
		}

		public override void Clear()
		{
			this._ema = null;
			this._sma.Clear();
		}

	}
}
