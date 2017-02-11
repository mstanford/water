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
	/// Summary description for LoadMethodOperator.
	/// </summary>
	public class LoadMethodOperator : Water.Operator
	{

		public LoadMethodOperator()
		{
		}

		public override object Evaluate(Water.List expressions)
		{
			if(expressions.Count == 4)
			{
				if(!(expressions[0] is Water.Identifier))
				{
					throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
				}
				string name = ((Water.Identifier)expressions[0]).Value;


				string type_name = ((Water.Identifier)expressions[1]).Value;
				string assembly_name = ((Water.Identifier)expressions[2]).Value;


				if(!(expressions[3] is Water.Identifier))
				{
					throw new Water.Error("args[3] was not a valid method name.");
				}
				string method = ((Water.Identifier)expressions[3]).Value;


				LoadMethod(name, type_name, assembly_name, method);
				return null;
			}
			else if(expressions.Count == 3)
			{
				if(!(expressions[0] is Water.Identifier))
				{
					throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
				}
				string name = ((Water.Identifier)expressions[0]).Value;


				System.Type type = (System.Type)Water.Evaluator.Evaluate(expressions[1]);


				if(!(expressions[2] is Water.Identifier))
				{
					throw new Water.Error("args[2] was not a valid method name.");
				}
				string method = ((Water.Identifier)expressions[2]).Value;


				LoadMethod(name, type, method);
				return null;
			}
			else
			{
				throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
			}
		}

		public static void LoadMethod(string name, string type_name, string assembly_name, string method_name)
		{
			System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadWithPartialName(assembly_name);
			if(assembly == null)
			{
				throw new Water.Error("Cannot load assembly: " + assembly_name);
			}

			System.Type type = assembly.GetType(type_name);
			if(type == null)
			{
				throw new Water.Error("Cannot load type: " + type_name);
			}

			LoadMethod(name, type, method_name);
		}

		private static void LoadMethod(string name, System.Type type, string method_name)
		{
			Water.Method methodStub = new Water.Method(type, method_name);

			Water.Environment.DefineConstant(name, methodStub);
		}

		public override string ToString()
		{
			return GetType().FullName;
		}

	}
}
