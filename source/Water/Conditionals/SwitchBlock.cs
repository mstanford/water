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
	/// Summary description for SwitchBlock.
	/// </summary>
	public class SwitchBlock : Water.Blocks.Block
	{

		public SwitchBlock()
		{
		}

		public override string Name
		{
			get { return "switch"; }
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

			object switch_expression = Water.Evaluator.Evaluate(expressions[0]);

			Water.Dictionary block = new Water.Dictionary();
			block["case"] = new Water.List();

			Water.List current_block = null;

			int depth = 0;
			foreach(Water.Statement block_statement in statements)
			{
				//TODO use generics
				Water.List statement = (Water.List)block_statement.Expression;
				if(statement.Count == 2 && statement.First() is Water.Identifier && ((Water.Identifier)statement.First()).Value.Equals("case"))
				{
					Water.Dictionary case_block = new Water.Dictionary();
					//TODO use generics
					((Water.List)block["case"]).Add(case_block);

					case_block["Expressions"] = statement.NotFirst().First();

					Water.List case_block_statements = new Water.List();
					case_block["Statements"] = case_block_statements;
					current_block = case_block_statements;
				}
				else if(statement.First() is Water.Identifier)
				{
					string tag = ((Water.Identifier)statement.First()).Value;

					if(tag.Equals("switch"))
					{
						current_block.Add(block_statement);
						depth++;
					}
					//TODO No good.
					else if(statement.Count == 1 && tag.Equals("end_switch"))
					{
						current_block.Add(block_statement);
						depth--;
					}
					else if(statement.Count == 1 && tag.Equals("default") && depth == 0)
					{
						Water.List default_block = new Water.List();
						block["default"] = default_block;
						current_block = default_block;
					}
					else
					{
						current_block.Add(block_statement);
					}
				}
				else
				{
					if(current_block == null)
					{
						//TODO file, line, column.
						throw new Water.Error("Invalid statement in switch statement.");
					}
					current_block.Add(block_statement);
				}
			}

			//TODO use generics
			foreach(Water.Dictionary case_ in (Water.List)block["case"])
			{
				if(switch_expression.Equals(Water.Evaluator.Evaluate(case_["Expressions"])))
				{
					//TODO use generics
					Water.Interpreter.Interpret(new StatementIterator((Water.List)case_["Statements"], false));
					return;
				}
			}
			if(block["default"] != null)
			{
				//TODO use generics
				Water.Interpreter.Interpret(new StatementIterator((Water.List)block["default"], false));
				return;
			}
		}

	}
}
