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
	public class SimpleMovingAverageFunction : Function
	{
		private int _period;
		private Bamboo.DataStructures.Queue<decimal> _previous;

		public SimpleMovingAverageFunction(int period)
		{
			this._period = period;
			this._previous = new Bamboo.DataStructures.Queue<decimal>(period);
		}

		public override decimal? Next(decimal value)
		{
			this._previous.Enqueue(value);

			if (this._previous.Count < this._period)
			{
				return null;
			}

			decimal avg = Avg(this._previous);
			this._previous.Dequeue();
			return avg;
		}

		private static decimal Avg(Bamboo.DataStructures.Queue<decimal> values)
		{
			return Sum(values) / values.Count;
		}

		private static decimal Sum(Bamboo.DataStructures.Queue<decimal> values)
		{
			decimal sum = 0;
			for (int i = 0; i < values.Count; i++)
			{
				sum += values[i];
			}
			return sum;
		}

		public override void Clear()
		{
			while (this._previous.Count > 0)
			{
				this._previous.Dequeue();
			}
		}

	}
}
