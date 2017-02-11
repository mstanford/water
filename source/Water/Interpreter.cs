// ------------------------------------------------------------------------------
// 
// Copyright (c) 2008 Swampware, Inc.
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
	/// Summary description for Interpreter.
	/// </summary>
	public class Interpreter
	{
		public static bool Trace = false;

		public static void Interpret(Iterator iterator)
		{
			System.IO.TextWriter output = Water.Environment.Output;

			Water.Environment.Push();

			Water.Statement statement;
			while ((statement = iterator.Next()) != null)
			{
				Interpret(statement, output);
			}

			Water.Environment.Pop();
		}

		public static void Interpret(Statement statement, System.IO.TextWriter output)
		{
			Water.Environment.DefineVariable("_Statement", statement);

			if (Trace)
			{
				if (Water.StatementParser.IsBlock(statement.Expression))
				{
					output.WriteLine(Water.Generator.Generate(statement.Expression.NotLast()));
				}
				else
				{
					output.WriteLine(Water.Generator.Generate(statement.Expression));
				}
			}

			object result = Water.Evaluator.Evaluate(statement.Expression);
			if (result != null)
			{
				output.WriteLine(Water.Generator.Generate(result, "", ""));
			}
		}

		private Interpreter()
		{
		}

	}
}
