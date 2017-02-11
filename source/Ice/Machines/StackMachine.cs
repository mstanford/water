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
	public class StackMachine : Machine
	{
		private System.Collections.Stack _stack = new System.Collections.Stack();

		#region Stack

		public void push(object value)
		{
			_stack.Push(value);
		}

		public void swap()
		{
			object value1 = _stack.Pop();
			object value2 = _stack.Pop();
			_stack.Push(value1);
			_stack.Push(value2);
		}

		public void dump()
		{
			System.Console.WriteLine();
			System.Console.WriteLine("STACK");
			foreach (object item in this._stack)
			{
				System.Console.WriteLine(item.ToString());
			}
			System.Console.WriteLine();
		}

		#endregion

		#region Relational

		public void eq()
		{
			_stack.Push(_stack.Pop().Equals(_stack.Pop()));
		}

		public void ge()
		{
			_stack.Push(Evaluate("ge", _stack.Pop(), _stack.Pop()));
		}

		public bool ge(int a, int b)
		{
			return a >= b;
		}

		public bool ge(double a, double b)
		{
			return a >= b;
		}

		public bool ge(double a, int b)
		{
			return a >= b;
		}

		public bool ge(int a, double b)
		{
			return a >= b;
		}

		public void gt()
		{
			_stack.Push(Evaluate("gt", _stack.Pop(), _stack.Pop()));
		}

		public bool gt(int a, int b)
		{
			return a > b;
		}

		public bool gt(double a, double b)
		{
			return a > b;
		}

		public bool gt(double a, int b)
		{
			return a > b;
		}

		public bool gt(int a, double b)
		{
			return a > b;
		}

		public void le()
		{
			_stack.Push(Evaluate("le", _stack.Pop(), _stack.Pop()));
		}

		public bool le(int a, int b)
		{
			return a <= b;
		}

		public bool le(double a, double b)
		{
			return a <= b;
		}

		public bool le(double a, int b)
		{
			return a <= b;
		}

		public bool le(int a, double b)
		{
			return a <= b;
		}

		public void lt()
		{
			_stack.Push(Evaluate("lt", _stack.Pop(), _stack.Pop()));
		}

		public bool lt(int a, int b)
		{
			return a < b;
		}

		public bool lt(double a, double b)
		{
			return a < b;
		}

		public bool lt(double a, int b)
		{
			return a < b;
		}

		public bool lt(int a, double b)
		{
			return a < b;
		}

		public void ne()
		{
			_stack.Push(!_stack.Pop().Equals(_stack.Pop()));
		}

		#endregion

		#region Arithmetic

		public void abs()
		{
			_stack.Push(Evaluate("abs", _stack.Pop()));
		}

		public int abs(int a)
		{
			return Math.Abs(a);
		}

		public double abs(double a)
		{
			return Math.Abs(a);
		}

		public void add()
		{
			_stack.Push(Evaluate("add", _stack.Pop(), _stack.Pop()));
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

		public void div()
		{
			_stack.Push(Evaluate("div", _stack.Pop(), _stack.Pop()));
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

		public void mod()
		{
			_stack.Push(Evaluate("mod", _stack.Pop(), _stack.Pop()));
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

		public void mul()
		{
			_stack.Push(Evaluate("mul", _stack.Pop(), _stack.Pop()));
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

		public void neg()
		{
			_stack.Push(Evaluate("neg", _stack.Pop()));
		}

		public int neg(int a)
		{
			return 0 - a;
		}

		public double neg(double a)
		{
			return 0 - a;
		}

		public void sub()
		{
			_stack.Push(Evaluate("subtract", _stack.Pop(), _stack.Pop()));
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

		#region Boolean

		public void and()
		{
			_stack.Push(Evaluate("and", _stack.Pop(), _stack.Pop()));
		}

		public bool and(bool a, bool b)
		{
			return a && b;
		}

		public void or()
		{
			_stack.Push(Evaluate("or", _stack.Pop(), _stack.Pop()));
		}

		public bool or(bool a, bool b)
		{
			return a || b;
		}

		public void not()
		{
			_stack.Push(Evaluate("not", _stack.Pop()));
		}

		public bool not(bool a)
		{
			return !a;
		}

		#endregion

		public void jmp(int address)
		{
			InstructionStream.Jump(address);
		}

		public void noop()
		{
		}

	}
}
