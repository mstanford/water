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

namespace Bamboo.Parsing.Generators.CSharp
{
	public class TokenGenerator
	{

		public static void Generate(string name, string nspace, Bamboo.Parsing.FiniteAutomata.FiniteAutomaton finiteAutomaton, System.IO.TextWriter writer)
		{
			writer.WriteLine("//");
			writer.WriteLine("// AUTOGENERATED " + System.DateTime.Now + "");
			writer.WriteLine("//");
			writer.WriteLine("using System;");
			writer.WriteLine("");
			writer.WriteLine("namespace " + nspace + "");
			writer.WriteLine("{");
			writer.WriteLine("	public class " + name + "Token");
			writer.WriteLine("	{");
			writer.WriteLine("		public int Type;");
			writer.WriteLine("		public string Value;");
			writer.WriteLine("");
			writer.WriteLine("		public " + name + "Token(int type)");
			writer.WriteLine("		{");
			writer.WriteLine("			Type = type;");
			writer.WriteLine("			Value = null;");
			writer.WriteLine("		}");
			writer.WriteLine("");
			writer.WriteLine("		public " + name + "Token(int type, string value)");
			writer.WriteLine("		{");
			writer.WriteLine("			Type = type;");
			writer.WriteLine("			Value = value;");
			writer.WriteLine("		}");
			writer.WriteLine("");
			writer.WriteLine("	}");
			writer.WriteLine("}");
		}

		private TokenGenerator()
		{
		}

	}
}
