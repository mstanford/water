// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006-2008 Swampware, Inc.
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

namespace Water.Operators
{
	/// <summary>
	/// Summary description for ConstantOperator.
	/// </summary>
	public class ConstantOperator : Water.Operator
	{

		public ConstantOperator()
		{
		}

		public override object Evaluate(Water.List expressions)
		{
			if(expressions.Count != 2)
			{
				throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
			}



			if(!(expressions[0] is Identifier))
			{
				throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
			}
			string name = ((Identifier)expressions[0]).Value;

			object value = Water.Evaluator.Evaluate(expressions[1]);



			Water.Environment.DefineConstant(name, value);



			return null;
		}

	}
}
