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
	public class ParserGenerator
	{

		public static void Generate(string name, string nspace, Bamboo.Parsing.Grammars.Grammar grammar, Surf.Set FIRST, Surf.Set FOLLOW, Surf.Set PREDICT, System.IO.TextWriter writer)
		{
			writer.WriteLine("//");
			writer.WriteLine("// AUTOGENERATED " + System.DateTime.Now + "");
			writer.WriteLine("//");
			writer.WriteLine("using System;");
			writer.WriteLine("");
			writer.WriteLine("namespace " + nspace + "");
			writer.WriteLine("{");
			writer.WriteLine("	public class " + name + "Parser");
			writer.WriteLine("	{");
			writer.WriteLine("		private " + name + "Tokenizer _tokenizer = new " + name + "Tokenizer();");

			//TODO use a state variable.
			writer.WriteLine("		private " + name + "TextReader _reader;");
			writer.WriteLine("		private " + name + "Token _token;");

			writer.WriteLine("");
			writer.WriteLine("		public " + name + "Parser(" + name + "TextReader reader)");
			writer.WriteLine("		{");
			writer.WriteLine("			this._reader = reader;");
			writer.WriteLine("			this._token = this._tokenizer.Tokenize(this._reader);");
			writer.WriteLine("		}");
			writer.WriteLine("");
			writer.WriteLine("		public " + name + "Node Parse()");
			writer.WriteLine("		{");
			writer.WriteLine("			return Parse" + grammar.Productions[0].Nonterminal + "();");
			writer.WriteLine("		}");
			writer.WriteLine("");

			foreach (string nonterminal in grammar.Nonterminals)
			{
				writer.WriteLine("		private " + name + "Node Parse" + nonterminal + "()");
				writer.WriteLine("		{");
				writer.WriteLine("			" + name + "Node node = new " + name + "Node(" + name + "NodeType." + nonterminal + ");");
				writer.WriteLine("");
				writer.WriteLine("			switch (this._token.Type)");
				writer.WriteLine("			{");
				bool hasDefault = false;
				foreach (Surf.Tuple tuple in PREDICT)
				{
					int n = (int)tuple[0];
					Bamboo.Parsing.Grammars.Production production = (Bamboo.Parsing.Grammars.Production)tuple[1];
					Surf.Set predict = (Surf.Set)tuple[2];

					if (production.Nonterminal.Equals(nonterminal))
					{
						List<Bamboo.Parsing.Grammars.Expression> expressions = ToList(production.Expression);

						//predict.Count == 0 && 
						if (expressions.Count == 1 && ((Bamboo.Parsing.Grammars.Symbol)expressions[0]).Token.Equals("EPSILON"))
						{
							hasDefault = true;
							writer.WriteLine("				default:");
							writer.WriteLine("					{");
							writer.WriteLine("						return node;");
							writer.WriteLine("					}");
						}
						else
						{
							if (predict.Count == 1 && predict[0].Equals("EOF"))
							{
								writer.WriteLine("				case " + name + "TokenType._EOF_:");
								writer.WriteLine("					{");
								writer.WriteLine("						return node;");
								writer.WriteLine("					}");
							}
							else
							{
								foreach (string symbol in predict)
								{
									writer.WriteLine("				case " + name + "TokenType." + symbol + ":");
								}
								writer.WriteLine("					{");
								if (!((Bamboo.Parsing.Grammars.Symbol)expressions[0]).Token.Equals("EPSILON"))
								{
									for (int i = 0; i < expressions.Count; i++)
									{
										writer.WriteLine("						node.Nodes.Add(Parse" + ((Bamboo.Parsing.Grammars.Symbol)expressions[i]).Token + "());");
									}
								}
								writer.WriteLine("						return node;");
								writer.WriteLine("					}");
							}
						}
					}
				}
				if (!hasDefault)
				{
					writer.WriteLine("				default:");
					writer.WriteLine("					{");
					writer.WriteLine("						throw new System.Exception(\"Syntax error.\");");
					writer.WriteLine("					}");
				}

				writer.WriteLine("			}");
				writer.WriteLine("		}");
				writer.WriteLine("");
			}

			foreach (string terminal in grammar.Terminals)
			{
				if (!terminal.Equals("EPSILON"))
				{
					writer.WriteLine("		private " + name + "Node Parse" + terminal + "()");
					writer.WriteLine("		{");
					writer.WriteLine("			" + name + "Node node = new " + name + "Node(" + name + "NodeType." + terminal + ", this._token.Value);");
					writer.WriteLine("			this._token = this._tokenizer.Tokenize(this._reader);");
					writer.WriteLine("			return node;");
					writer.WriteLine("		}");
					writer.WriteLine("");
				}
			}

			writer.WriteLine("	}");
			writer.WriteLine("}");
		}

		private static List<Bamboo.Parsing.Grammars.Expression> ToList(Bamboo.Parsing.Grammars.Expression expression)
		{
			List<Bamboo.Parsing.Grammars.Expression> list = new List<Bamboo.Parsing.Grammars.Expression>();
			if (expression is Bamboo.Parsing.Grammars.Symbol)
			{
				Bamboo.Parsing.Grammars.Symbol symbol = (Bamboo.Parsing.Grammars.Symbol)expression;
				list.Add(symbol);
			}
			else if (expression is Bamboo.Parsing.Grammars.Concatenation)
			{
				Bamboo.Parsing.Grammars.Concatenation concatenation = (Bamboo.Parsing.Grammars.Concatenation)expression;
				foreach (Bamboo.Parsing.Grammars.Symbol symbol in concatenation.Expressions)
				{
					list.Add(symbol);
				}
			}
			else
			{
				throw new System.Exception("INVALID");
			}
			return list;
		}

		private ParserGenerator()
		{
		}

	}
}
