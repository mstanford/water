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

namespace Water.Operators
{
	/// <summary>
	/// Summary description for LoadBlockOperator.
	/// </summary>
	public class LoadBlockOperator : Water.Operator
	{

		public LoadBlockOperator()
		{
		}

		public override object Evaluate(Water.List expressions)
		{
			if(expressions.Count == 2)
			{
				string type_name = ((Water.Identifier)expressions[0]).Value;
				string assembly_name = ((Water.Identifier)expressions[1]).Value;

				this.LoadBlock(type_name, assembly_name);

				return null;
			}
			else if(expressions.Count == 1)
			{
				System.Type type = (System.Type)Water.Evaluator.Evaluate(expressions[0]);

				this.LoadBlock(type);

				return null;
			}
			else
			{
				throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
			}
		}

		private void LoadBlock(string type_name, string assembly_name)
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

			LoadBlock(type);
		}

		private void LoadBlock(System.Type type)
		{
			object instance = Activator.CreateInstance(type);
			if(!(instance is Water.Blocks.Block))
			{
				throw new Water.Error(type.FullName + " is not a Command.");
			}
			Water.Blocks.Block block = (Water.Blocks.Block)instance;

			Water.Environment.DefineConstant(block.Name, block);
		}

		public override string ToString()
		{
			return GetType().FullName;
		}

	}
}
