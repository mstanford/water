// ------------------------------------------------------------------------------
// 
// Copyright (c) 2007-2008 Swampware, Inc.
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

namespace Water
{
	/// <summary>
	/// Summary description for Quote.
	/// </summary>
	public class Quote : Water.Operator
	{
		private object _expression;

		public Quote(object expression)
		{
			this._expression = expression;
		}

		public object Expression
		{
			get { return this._expression; }
		}

		public override object Evaluate(Water.List expressions)
		{
			return CheckQuote(this._expression);
		}

		private object CheckQuote(object o)
		{
			if (o is Water.List)
			{
				Water.List list = (Water.List)o;

				Water.List results = new Water.List();
				foreach (object item in list)
				{
					results.Add(CheckQuote(item));
				}
				return results;
			}
			else if (o is Water.Dictionary)
			{
				Water.Dictionary dictionary = (Water.Dictionary)o;

				Water.Dictionary results = new Water.Dictionary();
				foreach (string key in dictionary)
				{
					results.Add(key, CheckQuote(dictionary[key]));
				}
				return results;
			}
			else if (o is Water.Comma)
			{
				Water.Comma comma = (Water.Comma)o;

				return comma.Evaluate(new Water.List());
			}
            else if (o is System.String)
            {
                System.String s = (System.String)o;

                return Water.Evaluator.EvaluateString(s);
            }
            else
			{
                return o;
			}
		}

	}
}
