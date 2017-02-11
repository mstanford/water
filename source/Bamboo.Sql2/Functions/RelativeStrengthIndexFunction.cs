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
	public class RelativeStrengthIndexFunction : Function
	{
		private SimpleMovingAverageFunction _U;
		private SimpleMovingAverageFunction _D;
		private decimal? _previous;

		public RelativeStrengthIndexFunction(int period)
		{
			this._U = new SimpleMovingAverageFunction(period);
			this._D = new SimpleMovingAverageFunction(period);
		}

		public override decimal? Next(decimal value)
		{
			if (this._previous == null)
			{
				this._previous = value;
				return null;
			}

			decimal? U;
			decimal? D;
			if (value > this._previous)
			{
				U = this._U.Next(value - this._previous.Value);
				D = this._D.Next(0);
			}
			else if (value < this._previous)
			{
				U = this._U.Next(0);
				D = this._D.Next(this._previous.Value - value);
			}
			else
			{
				U = this._U.Next(0);
				D = this._D.Next(0);
			}

			this._previous = value;

			if (U == null || D == null)
			{
				return null;
			}

			return 100 * (U / (U + D));
		}

		public override void Clear()
		{
			this._U.Clear();
			this._D.Clear();
			this._previous = null;
		}

	}
}
