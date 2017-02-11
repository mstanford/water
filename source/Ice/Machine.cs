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
	public abstract class Machine
	{
		public Ice.InstructionStream InstructionStream;

		private System.Reflection.MethodInfo[] _methods;

		public Machine()
		{
			this._methods = this.GetType().GetMethods();
		}

		public void Run(InstructionStream instructionStream)
		{
			Instruction instruction;
			while ((instruction = instructionStream.Next()) != null)
			{
				Print(instruction);
				Evaluate(instruction);
			}
		}

		public object Evaluate(string mnemonic, params object[] operands)
		{
			return Evaluate(new Instruction(mnemonic, new List<object>(operands)));
		}

		private object Evaluate(Instruction instruction)
		{
			System.Reflection.MethodInfo method = GetMethod(instruction);
			if (method == null)
			{
				System.Console.WriteLine();
				System.Console.Write("INVALID INSTRUCTION: ");
				Print(instruction);

				throw new System.Exception("Invalid instruction.");
			}
			return method.Invoke(this, instruction.Operands);
		}

		private System.Reflection.MethodInfo GetMethod(Instruction instruction)
		{
			foreach (System.Reflection.MethodInfo method in this._methods)
			{
				if (Match(method, instruction))
				{
					return method;
				}
			}
			return null;
		}

		private bool Match(System.Reflection.MethodInfo method, Instruction instruction)
		{
			if (method.Name == instruction.Mnemonic)
			{
				System.Reflection.ParameterInfo[] parameters = method.GetParameters();
				if (parameters.Length == instruction.Operands.Length)
				{
					int i = 0;
					foreach (System.Reflection.ParameterInfo parameter in parameters)
					{
						if (!parameter.ParameterType.IsAssignableFrom(instruction.Operands[i].GetType()))
						{
							return false;
						}
						i++;
					}
					return true;
				}
			}
			return false;
		}

		public void Print(Ice.Instruction instruction)
		{
			System.Console.Write(instruction.Mnemonic + " ");
			bool first = true;
			foreach (object operand in instruction.Operands)
			{
				if (first)
				{
					first = false;
				}
				else
				{
					System.Console.Write(", ");
				}
				System.Console.Write(operand.ToString());
			}
			System.Console.WriteLine();
		}

	}
}
