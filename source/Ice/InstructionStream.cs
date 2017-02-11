// ------------------------------------------------------------------------------
// 
// Copyright (c) 2009 Swampware, Inc.
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
using System.Collections.Generic;
using System.Text;

namespace Ice
{
	public class InstructionStream : Evaluator
	{
		private TextReader _reader;
		private Parser _parser;
		private List<Instruction> _instructionTable;
		private int _instructionPointer;

		public InstructionStream(TextReader reader)
		{
			this._reader = reader;
			this._parser = new Parser(reader);
			this._instructionTable = new List<Instruction>();
			this._instructionPointer = 0;
		}

		public void Jump(int address)
		{
			this._instructionPointer = address;
		}

		public Instruction Next()
		{
			while (this._instructionPointer >= this._instructionTable.Count)
			{
				if (this._reader.Peek() == -1)
				{
					return null;
				}
				this._instructionTable.Add((Instruction)Evaluate(this._parser.Parse()));
			}

			Instruction instruction = this._instructionTable[this._instructionPointer];
			this._instructionPointer++;
			return instruction;
		}

		protected override object EvaluateIDENTIFIERInstructionTail(Node node)
		{
			if (
				node.Nodes.Count == 2 &&
				node.Nodes[0].Type == NodeType.IDENTIFIER &&
				node.Nodes[1].Type == NodeType.InstructionTail)
			{
				Identifier opcode = (Identifier)EvaluateIDENTIFIER(node.Nodes[0].Value);
				List<object> operands = (List<object>)Evaluate(node.Nodes[1]);
				return new Instruction(opcode.Value, operands);
			}
			else
			{
				throw new System.Exception("Invalid expression.");
			}
		}

		protected override object EvaluateExpressionExpressionList(Node node)
		{
			if (node.Nodes.Count == 2)
			{
				List<object> operands = new List<object>();
				operands.Add(Evaluate(node.Nodes[0]));
				operands.AddRange((List<object>)Evaluate(node.Nodes[1]));
				return operands;
			}
			else
			{
				throw new System.Exception("Invalid expression.");
			}
		}

		protected override object EvaluateCOMMAExpressionExpressionList(Node node)
		{
			if (node.Nodes.Count == 3 && node.Nodes[0].Type == NodeType.COMMA)
			{
				List<object> operands = new List<object>();
				operands.Add(Evaluate(node.Nodes[1]));
				operands.AddRange((List<object>)Evaluate(node.Nodes[2]));
				return operands;
			}
			else
			{
				throw new System.Exception("Invalid expression.");
			}
		}

		protected override object EvaluateInstructionEnd(Node node)
		{
			return new List<object>();
		}

		protected override object EvaluateBOOLEAN(string value)
		{
			Assert(value.Equals("true") || value.Equals("false"), "Invalid boolean.");
			return Boolean.Parse(value);
		}

		protected override object EvaluateINTEGER(string value)
		{
			return Int32.Parse(value);
		}

		protected override object EvaluateFLOAT(string value)
		{
			return Double.Parse(value);
		}

		protected override object EvaluateCHARACTER(string value)
		{
			return Char.Parse(value.Substring(1, value.Length - 2));
		}

		protected override object EvaluateSTRING(string value)
		{
			return value.Substring(1, value.Length - 2);
		}

		protected override object EvaluateIDENTIFIER(string value)
		{
			return new Identifier(value);
		}

		private static void Assert(bool expression, string message)
		{
			if (!expression)
			{
				throw new System.Exception(message);
			}
		}

	}
}
