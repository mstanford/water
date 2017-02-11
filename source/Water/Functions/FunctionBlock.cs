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

namespace Water.Functions
{
	/// <summary>
	/// Summary description for FunctionBlock.
	/// </summary>
	public class FunctionBlock : Water.Blocks.Block
	{

		public FunctionBlock()
		{
		}

		public override string Name
		{
			get { return "function"; }
		}

		public override bool IsRecursive
		{
			get { return false; }
		}

		public override void Evaluate(Water.List expressions, Water.List statements)
		{
			if(!(expressions[0] is Water.Identifier))
			{
				throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
			}
			string name = ((Water.Identifier)expressions[0]).Value;

			string namespace_ = (string)Water.Environment.Identify("_Namespace");
			if(namespace_ != null && namespace_.Length > 0)
			{
				name = namespace_ + "." + name;
			}

			Water.Functions.Function function = new Water.Functions.Function(name);

			for(int i = 1; i < expressions.Count; i++)
			{
				if(expressions[i] is Water.Identifier)
				{
					Water.Identifier identifier = (Water.Identifier)expressions[i];
					function.Parameters.Add(identifier);
				}
				else if((expressions[i] is Water.Quote))
				{
					Water.Quote quote = (Water.Quote)expressions[i];
					Water.Identifier identifier = (Water.Identifier)quote.Expression;
					function.Parameters.Add(new Water.Identifier("'" + identifier.Value));
				}
				else
				{
					throw new Water.Error("'" + expressions[i] + "' is not a valid parameter name.");
				}
			}

			foreach(Water.Statement statement in statements)
			{
				function.Statements.Add(statement);
			}

			bool hasOptional = HasOptional(function.Parameters);
			bool hasRest = HasRest(function.Parameters);
			if(hasOptional && hasRest)
			{
				throw new Water.Error("Cannot have both optional and rest parameters: " + name);
			}
			else if(hasRest)
			{
				CheckRestLegal(name, function.Parameters);
			}

			Water.Environment.DefineConstant(function.Name, function);
		}

		private void CheckRestLegal(string name, Water.List parameters)
		{
			int count = 0;
			foreach(Water.Identifier parameter in parameters)
			{
				if(Function.IsRest(parameter))
				{
					if(count > 0)
					{
						throw new Water.Error("Only one rest parameter allowed in function " + name);
					}
					count++;
				}
				else
				{
					if(count > 0)
					{
						throw new Water.Error("The rest parameter must appear after required parameters in function " + name);
					}
				}
			}
		}

		private bool HasOptional(Water.List parameters)
		{
			foreach(Water.Identifier parameter in parameters)
			{
				if(Function.IsOptional(parameter))
				{
					return true;
				}
			}
			return false;
		}

		private bool HasRest(Water.List parameters)
		{
			foreach(Water.Identifier parameter in parameters)
			{
				if(Function.IsRest(parameter))
				{
					return true;
				}
			}
			return false;
		}

	}
}
