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
	/// Summary description for BlockBlock.
	/// </summary>
	public class BlockBlock : Water.Blocks.Block
	{

		public BlockBlock()
		{
		}

		public override string Name
		{
			get { return "block"; }
		}

		public override bool IsRecursive
		{
			get { return false; }
		}

		public override void Evaluate(Water.List expressions, Water.List statements)
		{
			if (!(expressions[0] is Water.Identifier))
			{
				throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
			}

			string name = ((Water.Identifier)expressions[0]).Value;

			Water.Blocks.UserDefinedBlock userDefinedBlock = new Water.Blocks.UserDefinedBlock(name, expressions.NotFirst(), statements);

			Water.Environment.DefineConstant(name, userDefinedBlock);
		}

	}
}
