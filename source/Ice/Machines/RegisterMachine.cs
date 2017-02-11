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

namespace Ice.Machines
{
	public class RegisterMachine : Machine
	{
		private object[] _registers;
		private int _cmp;

		public RegisterMachine(int registers)
		{
			_registers = new object[registers];
		}

		public int Register(Identifier name)
		{
			return ((int)Char.Parse(name.Value) - 65);
		}

		#region Memory

		public void mov(Identifier destination, Identifier source)
		{
			this._registers[Register(destination)] = this._registers[Register(source)];
		}

		public void mov(Identifier destination, bool source)
		{
			this._registers[Register(destination)] = source;
		}

		public void mov(Identifier destination, int source)
		{
			this._registers[Register(destination)] = source;
		}

		public void mov(Identifier destination, double source)
		{
			this._registers[Register(destination)] = source;
		}

		public void mov(Identifier destination, char source)
		{
			this._registers[Register(destination)] = source;
		}

		public void mov(Identifier destination, string source)
		{
			this._registers[Register(destination)] = source;
		}

		#endregion

		#region Arithmetic

		public void add(Identifier destination, Identifier source)
		{
			this._registers[Register(destination)] = Evaluate("add", this._registers[Register(destination)], this._registers[Register(source)]);
		}

		public void add(Identifier destination, int source)
		{
			this._registers[Register(destination)] = Evaluate("add", this._registers[Register(destination)], source);
		}

		public void add(Identifier destination, double source)
		{
			this._registers[Register(destination)] = Evaluate("add", this._registers[Register(destination)], source);
		}

		public int add(int a, int b)
		{
			return a + b;
		}

		public double add(double a, double b)
		{
			return a + b;
		}

		public double add(double a, int b)
		{
			return a + b;
		}

		public double add(int a, double b)
		{
			return a + b;
		}

		public void div(Identifier destination, Identifier source)
		{
			this._registers[Register(destination)] = Evaluate("div", this._registers[Register(destination)], this._registers[Register(source)]);
		}

		public void div(Identifier destination, int source)
		{
			this._registers[Register(destination)] = Evaluate("div", this._registers[Register(destination)], source);
		}

		public void div(Identifier destination, double source)
		{
			this._registers[Register(destination)] = Evaluate("div", this._registers[Register(destination)], source);
		}

		public int div(int a, int b)
		{
			return a / b;
		}

		public double div(double a, double b)
		{
			return a / b;
		}

		public double div(double a, int b)
		{
			return a / b;
		}

		public double div(int a, double b)
		{
			return a / b;
		}

		public void mod(Identifier destination, Identifier source)
		{
			this._registers[Register(destination)] = Evaluate("mod", this._registers[Register(destination)], this._registers[Register(source)]);
		}

		public void mod(Identifier destination, int source)
		{
			this._registers[Register(destination)] = Evaluate("mod", this._registers[Register(destination)], source);
		}

		public void mod(Identifier destination, double source)
		{
			this._registers[Register(destination)] = Evaluate("mod", this._registers[Register(destination)], source);
		}

		public int mod(int a, int b)
		{
			return a % b;
		}

		public double mod(double a, double b)
		{
			return a % b;
		}

		public double mod(double a, int b)
		{
			return a % b;
		}

		public double mod(int a, double b)
		{
			return a % b;
		}

		public void mul(Identifier destination, Identifier source)
		{
			this._registers[Register(destination)] = Evaluate("mul", this._registers[Register(destination)], this._registers[Register(source)]);
		}

		public void mul(Identifier destination, int source)
		{
			this._registers[Register(destination)] = Evaluate("mul", this._registers[Register(destination)], source);
		}

		public void mul(Identifier destination, double source)
		{
			this._registers[Register(destination)] = Evaluate("mul", this._registers[Register(destination)], source);
		}

		public int mul(int a, int b)
		{
			return a * b;
		}

		public double mul(double a, double b)
		{
			return a * b;
		}

		public double mul(double a, int b)
		{
			return a * b;
		}

		public double mul(int a, double b)
		{
			return a * b;
		}

		public void neg(Identifier destination)
		{
			this._registers[Register(destination)] = Evaluate("neg", this._registers[Register(destination)]);
		}

		public int neg(int a)
		{
			return 0 - a;
		}

		public double neg(double a)
		{
			return 0 - a;
		}

		public void sub(Identifier destination, Identifier source)
		{
			this._registers[Register(destination)] = Evaluate("sub", this._registers[Register(destination)], this._registers[Register(source)]);
		}

		public void sub(Identifier destination, int source)
		{
			this._registers[Register(destination)] = Evaluate("sub", this._registers[Register(destination)], source);
		}

		public void sub(Identifier destination, double source)
		{
			this._registers[Register(destination)] = Evaluate("sub", this._registers[Register(destination)], source);
		}

		public int sub(int a, int b)
		{
			return a - b;
		}

		public double sub(double a, double b)
		{
			return a - b;
		}

		public double sub(double a, int b)
		{
			return a - b;
		}

		public double sub(int a, double b)
		{
			return a - b;
		}

		#endregion

		#region Bitwise

		public void and(Identifier destination, Identifier source)
		{
			this._registers[Register(destination)] = Evaluate("and", this._registers[Register(destination)], this._registers[Register(source)]);
		}

		public void and(Identifier destination, int source)
		{
			this._registers[Register(destination)] = Evaluate("and", this._registers[Register(destination)], source);
		}

		public int and(int a, int b)
		{
			return a & b;
		}

		public void or(Identifier destination, Identifier source)
		{
			this._registers[Register(destination)] = Evaluate("or", this._registers[Register(destination)], this._registers[Register(source)]);
		}

		public void or(Identifier destination, int source)
		{
			this._registers[Register(destination)] = Evaluate("or", this._registers[Register(destination)], source);
		}

		public int or(int a, int b)
		{
			return a | b;
		}

		public void xor(Identifier destination, Identifier source)
		{
			this._registers[Register(destination)] = Evaluate("xor", this._registers[Register(destination)], this._registers[Register(source)]);
		}

		public void xor(Identifier destination, int source)
		{
			this._registers[Register(destination)] = Evaluate("xor", this._registers[Register(destination)], source);
		}

		public int xor(int a, int b)
		{
			return a ^ b;
		}

		public void not(Identifier destination)
		{
			this._registers[Register(destination)] = Evaluate("not", this._registers[Register(destination)]);
		}

		public int not(int a)
		{
			return ~a;
		}

		public void shl(Identifier destination, Identifier source)
		{
			this._registers[Register(destination)] = Evaluate("shl", this._registers[Register(destination)], this._registers[Register(source)]);
		}

		public void shl(Identifier destination, int source)
		{
			this._registers[Register(destination)] = Evaluate("shl", this._registers[Register(destination)], source);
		}

		public int shl(int a, int n)
		{
			return a << n;
		}

		public void shr(Identifier destination, Identifier source)
		{
			this._registers[Register(destination)] = Evaluate("shr", this._registers[Register(destination)], this._registers[Register(source)]);
		}

		public void shr(Identifier destination, int source)
		{
			this._registers[Register(destination)] = Evaluate("shr", this._registers[Register(destination)], source);
		}

		public int shr(int a, int n)
		{
			return a >> n;
		}

		#endregion

		#region Compare/Jump

		public void cmp(Identifier destination, Identifier source)
		{
			this._cmp = ((System.IComparable)this._registers[Register(destination)]).CompareTo(this._registers[Register(source)]);
		}

		public void cmp(Identifier destination, bool source)
		{
			this._cmp = ((System.IComparable)this._registers[Register(destination)]).CompareTo(source);
		}

		public void cmp(Identifier destination, int source)
		{
			this._cmp = ((System.IComparable)this._registers[Register(destination)]).CompareTo(source);
		}

		public void cmp(Identifier destination, double source)
		{
			this._cmp = ((System.IComparable)this._registers[Register(destination)]).CompareTo(source);
		}

		public void cmp(Identifier destination, char source)
		{
			this._cmp = ((System.IComparable)this._registers[Register(destination)]).CompareTo(source);
		}

		public void cmp(Identifier destination, string source)
		{
			this._cmp = ((System.IComparable)this._registers[Register(destination)]).CompareTo(source);
		}

		public void jmp(int position)
		{
			InstructionStream.Jump(position);
		}

		public void je(int position)
		{
			if (this._cmp == 0)
			{
				InstructionStream.Jump(position);
			}
		}

		public void jne(int position)
		{
			if (this._cmp != 0)
			{
				InstructionStream.Jump(position);
			}
		}

		public void jg(int position)
		{
			if (this._cmp > 1)
			{
				InstructionStream.Jump(position);
			}
		}

		public void jge(int position)
		{
			if (this._cmp >= 0)
			{
				InstructionStream.Jump(position);
			}
		}

		public void jl(int position)
		{
			if (this._cmp < 0)
			{
				InstructionStream.Jump(position);
			}
		}

		public void jle(int position)
		{
			if (this._cmp <= 0)
			{
				InstructionStream.Jump(position);
			}
		}

		#endregion

		public void print(Identifier address)
		{
			System.Console.WriteLine(this._registers[Register(address)]);
		}

	}
}
