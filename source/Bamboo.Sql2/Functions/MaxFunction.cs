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
	public class MaxFunction : Function
	{
		private int _period;
		private Bamboo.DataStructures.Queue<decimal> _previous;

		public MaxFunction(int period)
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

			decimal max = Max(this._previous);
			this._previous.Dequeue();
			return max;
		}

		private static decimal Max(Bamboo.DataStructures.Queue<decimal> values)
		{
			decimal max = values[0];
			for (int i = 1; i < values.Count; i++)
			{
				if (values[i] > max)
				{
					max = values[i];
				}
			}
			return max;
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
