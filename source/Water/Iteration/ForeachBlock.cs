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
	/// Summary description for ForeachBlock.
	/// </summary>
	public class ForeachBlock : Water.Blocks.Block
	{

		public ForeachBlock()
		{
		}

		public override string Name
		{
			get { return "foreach"; }
		}

		public override bool IsRecursive
		{
			get { return true; }
		}

		public override void Evaluate(Water.List expressions, Water.List statements)
		{
			if(expressions.Count != 3)
			{
				throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
			}

			string itemName = ((Water.Identifier)expressions[0]).Value;
			System.Collections.IEnumerable list = (System.Collections.IEnumerable)Water.Evaluator.Evaluate(expressions[2]);

			// Copy list to allow modification.
			System.Collections.ArrayList list2 = new System.Collections.ArrayList();
			foreach(object item in list)
			{
				list2.Add(item);
			}

			bool first = true;
			bool last = false;
			int i = 1;
			foreach(object item in list2)
			{
				if(i == list2.Count)
				{
					last = true;
				}

				//Push a stack frame
				Water.Environment.Push();

				Water.Environment.DefineVariable("first", first);
				Water.Environment.DefineVariable("last", last);
				Water.Environment.DefineVariable(itemName, item);

				if(first)
				{
					first = false;
				}

				Water.Interpreter.Interpret(new StatementIterator(statements, true));

				//Pop a stack frame.
				Water.Environment.Pop();

				if(Water.Environment.Break)
				{
					break;
				}

				i++;
			}

			if(Water.Environment.Break)
			{
				Water.Environment.Break = false;
			}
		}

	}
}
