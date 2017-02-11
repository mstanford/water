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

namespace Water
{
	/// <summary>
	/// Environment represents the current execution environment.
	/// </summary>
	public class Environment
	{
		private static System.IO.TextWriter _output = System.Console.Out;
		private static bool _break = false;
		private static bool _return = false;
		private static object _returnValue = null;
		private static Water.Dictionary _constants = new Water.Dictionary();
		private static Water.List _variables = new Water.List();
		private static int _stackDepth = 0;
		private static Water.Dictionary _includes = new Water.Dictionary();

		public static System.IO.TextWriter Output
		{
			get { return _output; }
			set { _output = value; }
		}

		public static bool Break
		{
			get { return _break; }
			set { _break = value; }
		}

		public static bool Return
		{
			get { return _return; }
			set { _return = value; }
		}

		public static object ReturnValue
		{
			get { return _returnValue; }
			set { _returnValue = value; }
		}

		public static Water.Dictionary Constants
		{
			get { return _constants; }
		}

		public static Water.List Variables
		{
			get { return _variables; }
		}

		public static Water.Dictionary Includes
		{
			get { return _includes; }
		}

		public static void DefineConstant(string name, object value)
		{
			if (IsConstant(name))
			{
				throw new Water.Error("Constant \"" + name + "\" is already defined.");
			}

			if(value == null)
			{
				throw new Water.Error("Cannot define \"" + name + "\" as a null value.");
			}

			_constants.Add(name, value);
		}

		public static void UndefineConstant(string name)
		{
			if (!IsConstant(name))
			{
				throw new Water.Error("Constant \"" + name + "\" is not defined.");
			}

			_constants.Remove(name);
		}

		public static bool IsConstant(string name)
		{
			if(_constants[name] != null)
			{
				return true;
			}

			return false;
		}

		public static void DefineVariable(string name, object value)
		{
			if (IsConstant(name))
			{
				throw new Water.Error("Constant \"" + name + "\" is already defined.");
			}

			Water.Dictionary frame = (Water.Dictionary)_variables[_stackDepth - 1];
			if (frame.IsDefined(name))
			{
				frame.Remove(name);
			}
			frame.Add(name, value);
		}

		public static void RedefineVariable(string name, object value)
		{
			if (IsConstant(name))
			{
				throw new Water.Error("Constant \"" + name + "\" is already defined.");
			}

			Water.Dictionary frame;
			for (int i = (_stackDepth - 1); i >= 1; i--)
			{
				frame = (Water.Dictionary)_variables[i];

				if (frame.IsDefined(name))
				{
					frame.Remove(name);
					frame.Add(name, value);
					return;
				}
			}

			throw new Water.Error("Variable is not defined: " + name);
		}
		/// <summary>
		/// This looks up an identifier in the environment.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static object Identify(string name)
		{
			if(_constants[name] != null)
			{
				return _constants[name];
			}

			Water.Dictionary frame;

			// Check locals.
			for(int i = (_stackDepth - 1); i >= 1; i--)
			{
				frame = (Water.Dictionary)_variables[i];
				if(frame.IsDefined(name))
				{
					return frame[name];
				}
			}

			// Check globals.
			frame = (Water.Dictionary)_variables[0];
			if(frame.IsDefined(name))
			{
				return frame[name];
			}

			return null;
		}

		/// <summary>
		/// Push a stack frame.
		/// </summary>
		public static void Push()
		{
			Water.Dictionary frame;
			if (_variables.Count == _stackDepth)
			{
				frame = new Water.Dictionary();
				_variables.Add(frame);
			}
			else
			{
				frame = (Water.Dictionary)_variables[_stackDepth];
			}
			_stackDepth++;
		}

		/// <summary>
		/// Pop a stack frame.
		/// </summary>
		public static void Pop()
		{
			Water.Dictionary frame = (Water.Dictionary)_variables[_stackDepth - 1];
			frame.Clear();

			_stackDepth--;
		}

		public static void Reset()
		{
			_output = System.Console.Out;
			_break = false;
			_return = false;
			_returnValue = null;
			_constants = new Water.Dictionary();
			_variables = new Water.List();
			_stackDepth = 0;
			_includes = new Water.Dictionary();
		}

		private Environment()
		{
		}

	}
}
