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

namespace Water.Conditionals
{
	/// <summary>
	/// Summary description for IfBlock.
	/// </summary>
	public class IfBlock : Water.Blocks.Block
	{

		public IfBlock()
		{
		}

		public override string Name
		{
			get { return "if"; }
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

			Water.Dictionary block = new Water.Dictionary();
			block["else_if"] = new Water.List();

			Water.List if_block = new Water.List();
			block["if"] = if_block;
			Water.List current_block = if_block;

			foreach(Water.Statement block_statement in statements)
			{
				Water.List statement = (Water.List)block_statement.Expression;
				if(statement.First() is Water.Identifier)
				{
					string tag = ((Water.Identifier)statement.First()).Value;

					if(tag.Equals("if"))
					{
						current_block.Add(block_statement);
					}
					else if(statement.Count == 2 && tag.Equals("else_if"))
					{
						Water.Dictionary else_if_block = new Water.Dictionary();
						//TODO use generics
						((Water.List)block["else_if"]).Add(else_if_block);

						else_if_block["Expressions"] = statement.NotFirst().First();

						Water.List else_if_block_statements = new Water.List();
						else_if_block["Statements"] = else_if_block_statements;
						current_block = else_if_block_statements;
					}
					else if(statement.Count == 1 && tag.Equals("else")) // && depth == 0
					{
						Water.List else_block = new Water.List();
						block["else"] = else_block;
						current_block = else_block;
					}
					else if(statement.Count == 1 && tag.Equals("end_if"))
					{
						current_block.Add(block_statement);
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

			if(Water.Evaluator.EvaluateBoolean(expressions[0]))
			{
				//TODO use generics
				Water.Interpreter.Interpret(new StatementIterator((Water.List)block["if"], false));
				return;
			}
			//TODO use generics
			foreach(Water.Dictionary elseIf in (Water.List)block["else_if"])
			{
				if(Water.Evaluator.EvaluateBoolean(elseIf["Expressions"]))
				{
					//TODO use generics
					Water.Interpreter.Interpret(new StatementIterator((Water.List)elseIf["Statements"], false));
					return;
				}
			}
			if(block["else"] != null)
			{
				//TODO use generics
				Water.Interpreter.Interpret(new StatementIterator((Water.List)block["else"], false));
				return;
			}
		}

	}
}
