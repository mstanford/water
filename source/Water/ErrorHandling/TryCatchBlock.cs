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

namespace Water.ErrorHandling
{
	/// <summary>
	/// Summary description for TryCatchBlock.
	/// </summary>
	public class TryCatchBlock : Water.Blocks.Block
	{

		public TryCatchBlock()
		{
		}

		public override string Name
		{
			get { return "try"; }
		}

		public override bool IsRecursive
		{
			get { return true; }
		}

		public override void Evaluate(Water.List expressions, Water.List statements)
		{
			if(expressions.Count != 0)
			{
//TODO				throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
			}

			string exception_name = null;

			Water.Dictionary block = new Water.Dictionary(); // TODO block = GroupBlock(tags);

			Water.List try_block = new Water.List();
			block["try"] = try_block;
			Water.List current_block = try_block;

			foreach(Water.Statement block_statement in statements)
			{
				//TODO use generics
				Water.List statement = (Water.List)block_statement.Expression;
				if(statement.First() is Water.Identifier)
				{
					string tag = ((Water.Identifier)statement.First()).Value;

					if(statement.Count == 2 && tag.Equals("catch")) // && depth == 0
					{
						//TODO check the number of expressions.

						Water.List catch_block = new Water.List();
						block["catch"] = catch_block;
						current_block = catch_block;

						exception_name = ((Water.Identifier)statement[1]).Value;
					}
					else
					{
						current_block.Add(block_statement);
					}
				}
				else
				{
					current_block.Add(block_statement);
				}
			}

			if(exception_name == null)
			{
				//TODO throw exception.
			}

			try
			{
				//TODO use generics
				Water.Interpreter.Interpret(new StatementIterator((Water.List)block["try"], false));
			}
			catch(System.Reflection.TargetInvocationException exception)
			{
				Water.Environment.Push();
				Water.Environment.DefineVariable(exception_name, exception.InnerException);
				//TODO use generics
				Water.Interpreter.Interpret(new StatementIterator((Water.List)block["catch"], false));
				Water.Environment.Pop();
			}
			catch(System.Exception exception)
			{
				Water.Environment.Push();
				Water.Environment.DefineVariable(exception_name, exception);
				//TODO use generics
				Water.Interpreter.Interpret(new StatementIterator((Water.List)block["catch"], false));
				Water.Environment.Pop();
			}
		}

	}
}
