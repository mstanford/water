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
	public class MovingAverageConvergenceDivergenceFunction : Function
	{
		private ExponentialMovingAverageFunction _ema1;
		private ExponentialMovingAverageFunction _ema2;
		private ExponentialMovingAverageFunction _signal;

		public MovingAverageConvergenceDivergenceFunction(int ema1, int ema2, int smoothingFactor)
		{
			this._ema1 = new ExponentialMovingAverageFunction(ema1);
			this._ema2 = new ExponentialMovingAverageFunction(ema2);
			this._signal = new ExponentialMovingAverageFunction(smoothingFactor);
		}

		public override decimal? Next(decimal value)
		{
			decimal? ema1 = this._ema1.Next(value);
			decimal? ema2 = this._ema2.Next(value);

			if (ema2 == null || ema1 == null)
			{
				return null;
			}

			decimal macd = ema1.Value - ema2.Value;

			decimal? signal = this._signal.Next(value);
			if (signal == null)
			{
				return null;
			}

			return macd - signal;
		}

		public override void Clear()
		{
			this._ema1.Clear();
			this._ema2.Clear();
			this._signal.Clear();
		}

	}
}
