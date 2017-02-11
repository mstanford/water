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
using System.Collections.Generic;
using System.Text;

namespace surfer
{
	class Program
	{
		static void Main(string[] args)
		{
			Surf.Interpreter interpreter = new Surf.Interpreter();

			System.Console.WriteLine("SRF");
			System.Console.WriteLine();

			interpreter.Output = System.Console.Out;
			interpreter.Trace = System.Console.Out;

			if (args.Length > 0)
			{
				System.IO.StreamReader streamReader = new System.IO.StreamReader(System.IO.File.OpenRead(args[0]));
				interpreter.Interpret(streamReader);
				streamReader.Close();
			}

			interpreter.Trace = null;

			while (true)
			{
				System.Console.Write(">");
				string line = System.Console.ReadLine();
				if (line.Length > 0)
				{
					interpreter.Interpret(new System.IO.StringReader(line));
				}
				System.Console.WriteLine();
			}
		}
	}
}
