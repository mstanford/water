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
	/// Summary description for NewOperator.
	/// </summary>
	public class NewOperator : Water.Operator
	{

		public NewOperator()
		{
		}

		public override object Evaluate(Water.List expressions)
		{
			if(expressions.Count == 0)
			{
				throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
			}


			System.Type type = (System.Type)Water.Evaluator.Evaluate(expressions[0]);

			object[] parameters = new object[expressions.Count - 1];
			for(int i = 1; i < expressions.Count; i++)
			{
				parameters[i - 1] = Water.Evaluator.Evaluate(expressions[i]);
				if(parameters[i - 1] == null)
				{
					//TODO DELETE throw new Water.Error("Expression was null: " + expressions[i].ToString());
				}
			}

			try
			{
				return Activator.CreateInstance(type, parameters);
			}
			catch(System.MissingMethodException exception)
			{
				throw new System.Exception("Invalid arguments passed to constructor on '" + type.FullName + "'.", exception);
			}
		}

		public override string ToString()
		{
			return GetType().FullName;
		}

	}
}
