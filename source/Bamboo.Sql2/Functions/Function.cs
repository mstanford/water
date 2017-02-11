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
	public abstract class Function
	{

		public Function()
		{
		}

		public abstract decimal? Next(decimal value);

		public abstract void Clear();

		public static Function Create(string name, List<object> operands)
		{
			switch (name.ToUpper())
			{
				case "DEMA": return new DoubleExponentialMovingAverageFunction((int)operands[1], (int)operands[2]);
				case "DSMA": return new DoubleSimpleMovingAverageFunction((int)operands[1], (int)operands[2]);
				case "EMA": return new ExponentialMovingAverageFunction((int)operands[1]);
				case "MACD": return new MovingAverageConvergenceDivergenceFunction((int)operands[1], (int)operands[2], (int)operands[3]);
				case "MAX": return new MaxFunction((int)operands[1]);
				case "MIN": return new MinFunction((int)operands[1]);
				case "MOMENTUM": return new MomentumFunction((int)operands[1]);
				case "ROC": return new RateOfChangeFunction((int)operands[1]);
				case "RSI": return new RelativeStrengthIndexFunction((int)operands[1]);
				case "RVI": return new RelativeVolatilityIndexFunction((int)operands[1], (int)operands[2]);
				case "SMA": return new SimpleMovingAverageFunction((int)operands[1]);
				case "STDDEV": return new StandardDeviationFunction((int)operands[1]);
				default: throw new System.Exception();
			}
		}

	}
}
