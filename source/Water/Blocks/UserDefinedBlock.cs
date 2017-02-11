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

namespace Water.Blocks
{
	/// <summary>
	/// Summary description for UserDefinedBlock.
	/// </summary>
	public class UserDefinedBlock : Water.Blocks.Block
	{
		private string _name;
		private Water.List _parameters;
		private Water.List _statements;

		public UserDefinedBlock(string name, Water.List parameters, Water.List statements)
		{
			this._name = name;
			this._parameters = parameters;
			this._statements = statements;
		}

		public override string Name
		{
			get { return this._name; }
		}

		public override bool IsRecursive
		{
			get { return false; }
		}

		public override void Evaluate(Water.List expressions, Water.List statements)
		{
			//Push a stack frame
			Water.Environment.Push();


			// Evaluate local variables.
			Water.List expressions_to_eval = expressions.Copy();
			Water.List values = new Water.List();
			bool hasRest = false;
			foreach (Water.Identifier parameter in this._parameters)
			{
				if (hasRest)
				{
					throw new Water.Error("Cannot have parameters after a rest parameter in function " + this._name);
				}

				if (IsRest(parameter))
				{
					//Add the rest.
					Water.List rest = new Water.List();
					foreach (Water.Identifier expression in expressions_to_eval)
					{
						rest.Add(expression);
					}
					expressions_to_eval.Clear();
					values.Add(rest);
					hasRest = true;
				}
				else if (IsOptional(parameter))
				{
					//If it doesn't exist add null.
					if (expressions_to_eval.Count == 0)
					{
						values.Add(null);
					}
					else
					{
						Water.Identifier value = (Water.Identifier)expressions_to_eval.First();
						values.Add(value);

						expressions_to_eval = expressions_to_eval.NotFirst();
					}
				}
				else if (IsUnevaluated(parameter))
				{
					values.Add(expressions_to_eval.First());

					expressions_to_eval = expressions_to_eval.NotFirst();
				}
				else
				{
					//Throw exception if null.
					if (expressions_to_eval.Count == 0)
					{
						throw new Water.Error("Parameter is required: " + parameter.Value);
					}
					else
					{
						Water.Identifier value = (Water.Identifier)expressions_to_eval.First();
						if (value == null)
						{
							throw new Water.Error("Parameter is required: " + parameter.Value);
						}
						values.Add(value);

						expressions_to_eval = expressions_to_eval.NotFirst();
					}
				}
			}
			if (expressions_to_eval.Count > 0)
			{
				throw new Water.Error("Too many expressions passed to function: " + this._name);
			}


			// Push local variables.
			int i = 0;
			foreach (Water.Identifier parameter in this._parameters)
			{
				if (Water.Environment.IsConstant(parameter.Value))
				{
					throw new Water.Error("Constant \"" + parameter.Value + "\" is already defined.");
				}
				if (IsOptional(parameter))
				{
					string name = GetOptional(parameter);
					Water.Environment.DefineVariable(name, values[i]);
				}
				else if (IsRest(parameter))
				{
					string name = GetRest(parameter);
					Water.Environment.DefineVariable(name, values[i]);
				}
				else if (IsUnevaluated(parameter))
				{
					string name = GetUnevaluated(parameter);
					Water.Environment.DefineVariable(name, values[i]);
				}
				else
				{
					Water.Environment.DefineVariable(parameter.Value, values[i]);
				}
				i++;
			}


			Water.Environment.DefineVariable("_Statements", statements);


			//Evaluate each statement.
			Water.Interpreter.Interpret(new StatementIterator(this._statements, false));


			//Pop a stack frame.
			Water.Environment.Pop();
		}

		private bool IsRest(Water.Identifier parameter)
		{
			return parameter.Value.StartsWith("*");
			//return parameter.Value.EndsWith("*");
		}

		private string GetRest(Water.Identifier parameter)
		{
			return parameter.Value.Substring(1);
			//return parameter.Value.Substring(0, parameter.Value.Length - 1);
		}

		private bool IsOptional(Water.Identifier parameter)
		{
			return parameter.Value.StartsWith("?");
			//return parameter.Value.EndsWith("?");
		}

		private string GetOptional(Water.Identifier parameter)
		{
			return parameter.Value.Substring(1);
			//return parameter.Value.Substring(0, parameter.Value.Length - 1);
		}

		private bool IsUnevaluated(Water.Identifier parameter)
		{
			return parameter.Value.StartsWith("'");
		}

		private string GetUnevaluated(Water.Identifier parameter)
		{
			return parameter.Value.Substring(1);
		}

	}
}
