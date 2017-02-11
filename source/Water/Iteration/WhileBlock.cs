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

namespace Water.Iteration
{
	/// <summary>
	/// Summary description for WhileBlock.
	/// </summary>
	public class WhileBlock : Water.Blocks.Block
	{

		public WhileBlock()
		{
		}

		public override string Name
		{
			get { return "while"; }
		}

		public override bool IsRecursive
		{
			get { return true; }
		}

		public override void Evaluate(Water.List expressions, Water.List statements)
		{
			if(expressions.Count != 1)
			{
				throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
			}

			while(Water.Evaluator.EvaluateBoolean(expressions[0]) && !Water.Environment.Break)
			{
				Water.Interpreter.Interpret(new StatementIterator(statements, true));
			}

			if(Water.Environment.Break)
			{
				Water.Environment.Break = false;
			}
		}

	}
}
