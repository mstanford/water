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
	public class TableDrivenTokenizerGenerator
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
			writer.WriteLine("	public class " + name + "Tokenizer");
			writer.WriteLine("	{");
			writer.WriteLine("		private static readonly int[,] TABLE = new int[,] {");

			Surf.Set transitions = new Surf.Set();
			foreach (Bamboo.Parsing.FiniteAutomata.Transition transition in finiteAutomaton.Transitions)
			{
				transitions.Add(new Surf.Tuple(new object[] { new Surf.Tuple(new object[] { transition.FromState, transition.Character }), transition.ToState }));
			}
			foreach (int state in finiteAutomaton.States)
			{
				writer.Write("			{	");
				foreach (object character in finiteAutomaton.Alphabet)
				{
					Surf.Tuple key = new Surf.Tuple(new object[] { state, character });
					if (transitions.IsDefined(key))
					{
						int toState = (int)transitions.Apply(key);
						writer.Write("" + toState + ",		");
					}
					else
					{
						writer.Write("-1,		");
					}
				}
				writer.WriteLine("},");
			}

			writer.WriteLine("		};");
			writer.WriteLine("");
			writer.WriteLine("		private System.Text.StringBuilder _stringBuilder = new System.Text.StringBuilder();");
			writer.WriteLine("");
			writer.WriteLine("		public " + name + "Tokenizer()");
			writer.WriteLine("		{");
			writer.WriteLine("		}");
			writer.WriteLine("");
			writer.WriteLine("		public " + name + "Token Tokenize(" + name + "TextReader reader)");
			writer.WriteLine("		{");
			writer.WriteLine("			int n;");
			writer.WriteLine("			char ch;");
			writer.WriteLine("");
			writer.WriteLine("			_stringBuilder.Length = 0;");
			writer.WriteLine("");
			/*
			//
			// Trim whitespace
			//
			while ((n = reader.Peek()) != -1)
			{
				ch = (char)n;
				switch (ch)
				{
					case ' ':
					case '\t':
					case '\r':
					case '\n':
						{
							break;
						}
					default:
						{
							goto s0;
						}
				}
			}
			*/
			writer.WriteLine("			n = reader.Peek();");
			writer.WriteLine("			ch = (char)0;");
			writer.WriteLine("			int state = 0;");
			writer.WriteLine("");
			writer.WriteLine("			if (n == -1)");
			writer.WriteLine("			{");
			writer.WriteLine("				return new " + name + "Token(" + name + "TokenType._EOF_);");
			writer.WriteLine("			}");
			writer.WriteLine("");
			writer.WriteLine("			while(n != -1 && state != -1)");
			writer.WriteLine("			{");
			writer.WriteLine("				ch = (char)n;");
			writer.WriteLine("");
			writer.WriteLine("				switch(ch)");
			writer.WriteLine("				{");

			int i = 0;
			foreach (char character in finiteAutomaton.Alphabet)
			{
				writer.WriteLine("					case '" + Escape(character) + "':");
				writer.WriteLine("					{");
				writer.WriteLine("						int state2 = TABLE[state, " + i + "];");
				writer.WriteLine("						if(state2 == -1)");
				writer.WriteLine("						{");
				writer.WriteLine("							goto EXIT;");
				writer.WriteLine("						}");
				writer.WriteLine("");
				writer.WriteLine("						state = state2;");
				writer.WriteLine("						_stringBuilder.Append(ch);");
				writer.WriteLine("						reader.Read();");
				writer.WriteLine("						n = reader.Peek();");
				writer.WriteLine("						break;");
				writer.WriteLine("					}");
				i++;
			}

			writer.WriteLine("					default:");
			writer.WriteLine("					{");
			writer.WriteLine("						goto EXIT;");
			writer.WriteLine("					}");
			writer.WriteLine("				}");
			writer.WriteLine("			}");
			writer.WriteLine("");
			writer.WriteLine("		EXIT:");
			writer.WriteLine("			switch(state)");
			writer.WriteLine("			{");

			foreach (int finalState in finiteAutomaton.FinalStates)
			{
				Bamboo.Parsing.FiniteAutomata.Token token = GetToken(finalState, finiteAutomaton.Tokens);

				writer.WriteLine("				case " + finalState + ":");
				writer.WriteLine("				{");
				writer.WriteLine("					return new " + name + "Token(" + name + "TokenType." + token.Name + ", _stringBuilder.ToString());");
				writer.WriteLine("				}");
			}

			writer.WriteLine("				default:");
			writer.WriteLine("				{");
			writer.WriteLine("					if (n > -1)");
			writer.WriteLine("					{");
			writer.WriteLine("						_stringBuilder.Append(ch);");
			writer.WriteLine("					}");
			writer.WriteLine("					return new " + name + "Token(" + name + "TokenType._ERROR_, _stringBuilder.ToString());");
			writer.WriteLine("				}");
			writer.WriteLine("			}");
			writer.WriteLine("		}");
			writer.WriteLine("");
			writer.WriteLine("	}");
			writer.WriteLine("}");
		}

		public static bool IsFinal(int state, int[] finalStates)
		{
			foreach (int finalState in finalStates)
			{
				if (state == finalState)
				{
					return true;
				}
			}
			return false;
		}

		public static bool HasTransitions(int state, FiniteAutomata.Transition[] transitions)
		{
			foreach (FiniteAutomata.Transition transition in transitions)
			{
				if (transition.FromState == state)
				{
					return true;
				}
			}
			return false;
		}

		public static Bamboo.Parsing.FiniteAutomata.Token GetToken(int state, Bamboo.Parsing.FiniteAutomata.Token[] tokens)
		{
			foreach (Bamboo.Parsing.FiniteAutomata.Token token in tokens)
			{
				if (token.Number == state)
				{
					return token;
				}
			}
			throw new System.Exception("Invalid final state.");
		}

		public static string Escape(char character)
		{
			switch (character)
			{
				case '\\':
					{
						return "\\\\";
					}
				case '\"':
					{
						return "\\\"";
					}
				case '\'':
					{
						return "\\\'";
					}
				case '\n':
					{
						return "\\n";
					}
				case '\r':
					{
						return "\\r";
					}
				case '\t':
					{
						return "\\t";
					}
				case '\0':
					{
						return "\\0";
					}
				case '\a':
					{
						return "\\a";
					}
				case '\b':
					{
						return "\\b";
					}
				case '\f':
					{
						return "\\f";
					}
				case '\v':
					{
						return "\\v";
					}
				default:
					{
						return character.ToString();
					}
			}
		}

		private TableDrivenTokenizerGenerator()
		{
		}

	}
}
