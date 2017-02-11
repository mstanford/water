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

namespace wave
{
	/// <summary>
	/// Summary description for ConsoleIterator.
	/// </summary>
	public class ConsoleIterator : Water.Iterator
	{

		public ConsoleIterator()
		{
		}

		public override Water.Statement Next()
		{
			if (Water.Environment.IsConstant("_Exit") || Water.Environment.IsConstant("_Restart"))
			{
				return null;
			}

			while(true)
			{
				System.Console.WriteLine();
				System.Console.Write(">");

                string s = System.Console.ReadLine();
                if (s.Length > 0)
                {
                    wave.ConsoleReader reader = new wave.ConsoleReader(s);
                    Water.StatementParser parser = new Water.StatementParser(reader);
                    Water.Statement statement = parser.Parse();
                    if (statement != null)
                    {
                        return statement;
                    }
                }
			}
		}

	}
}
