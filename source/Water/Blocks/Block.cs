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

namespace Water.Blocks
{
	/// <summary>
	/// Summary description for Block.
	/// </summary>
	public abstract class Block : Water.Operator
	{

		public Block()
		{
		}

		public abstract string Name { get; }

		public abstract bool IsRecursive { get; }

		public override object Evaluate(Water.List expressions)
		{
			Evaluate(expressions.NotLast(), (Water.List)expressions.Last());

			return null;
		}

		public abstract void Evaluate(Water.List expressions, Water.List statements);

		public static void EvaluateBlock(Water.List statements, bool allowBreaks)
		{
			Water.Interpreter.Interpret(new StatementIterator(statements, allowBreaks));
		}

		public override string ToString()
		{
			return "BLOCK " + this.Name;
		}

	}
}
