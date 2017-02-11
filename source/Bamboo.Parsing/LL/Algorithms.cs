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

namespace Bamboo.Parsing.LL
{
	public class Algorithms
	{
		private static readonly Surf.Set EPSILON = new Surf.Set(new object[] { "EPSILON" });

		#region FIRST

		public static Surf.Set First(Bamboo.Parsing.Grammars.Grammar grammar)
		{
			Surf.Set FIRST = new Surf.Set();

			foreach (string symbol in grammar.Terminals)
				FIRST.Add(new Surf.Tuple(new object[] { symbol, new Surf.Set(new object[] { symbol }) }));

			foreach (string symbol in grammar.Nonterminals)
				FIRST.Add(new Surf.Tuple(new object[] { symbol, new Surf.Set() }));

			List<Bamboo.Parsing.Grammars.Production> remaining = Copy(grammar.Productions);
			while (remaining.Count > 0)
			{
				for (int i = 0; i < remaining.Count; i++)
				{
					Bamboo.Parsing.Grammars.Production production = remaining[i];
					if (CanResolve(production.Expression, remaining))
					{
						First(production.Nonterminal, production.Expression, FIRST);
						remaining.Remove(production);
						break;
					}
				}
			}

			return FIRST;
		}

		private static void First(string A, Bamboo.Parsing.Grammars.Expression expression, Surf.Set FIRST)
		{
			List<Bamboo.Parsing.Grammars.Expression> B = ToList(expression);

			Surf.Set FIRST_A = (Surf.Set)FIRST.Apply(A);

			Union(FIRST_A, ((Surf.Set)FIRST.Apply(((Bamboo.Parsing.Grammars.Symbol)B[0]).Token)).Difference(EPSILON));

			int i = 0;
			while (((Surf.Set)FIRST.Apply(((Bamboo.Parsing.Grammars.Symbol)B[0]).Token)).Contains("EPSILON") && i < (B.Count - 1))
			{
				Union(FIRST_A, ((Surf.Set)FIRST.Apply(((Bamboo.Parsing.Grammars.Symbol)B[i + 1]).Token)).Difference(EPSILON));
				i++;
			}

			if (i == (B.Count - 1) && ((Surf.Set)FIRST.Apply(((Bamboo.Parsing.Grammars.Symbol)B[B.Count - 1]).Token)).Contains("EPSILON"))
			{
				Union(FIRST_A, EPSILON);
			}
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

		private static bool CanResolve(Bamboo.Parsing.Grammars.Expression expression, List<Bamboo.Parsing.Grammars.Production> remaining)
		{
			if (expression is Bamboo.Parsing.Grammars.Symbol)
			{
				Bamboo.Parsing.Grammars.Symbol symbol = (Bamboo.Parsing.Grammars.Symbol)expression;
				foreach (Bamboo.Parsing.Grammars.Production production in remaining)
				{
					if (production.Nonterminal.Equals(symbol.Token))
					{
						return false;
					}
				}
				return true;
			}
			else if (expression is Bamboo.Parsing.Grammars.Concatenation)
			{
				Bamboo.Parsing.Grammars.Concatenation concatenation = (Bamboo.Parsing.Grammars.Concatenation)expression;
				return CanResolve(concatenation.Expressions[0], remaining);
			}
			else
			{
				throw new System.Exception("INVALID");
			}
		}

		private static List<Bamboo.Parsing.Grammars.Production> Copy(List<Bamboo.Parsing.Grammars.Production> productions)
		{
			List<Bamboo.Parsing.Grammars.Production> productions2 = new List<Bamboo.Parsing.Grammars.Production>();
			foreach (Bamboo.Parsing.Grammars.Production production in productions)
			{
				productions2.Add(production);
			}
			return productions2;
		}

		#endregion

		#region FOLLOW

		public static Surf.Set Follow(Bamboo.Parsing.Grammars.Grammar grammar, Surf.Set FIRST)
		{
			FollowBuilder followBuilder = new FollowBuilder(grammar, FIRST);
			return followBuilder.Build();
		}

		#endregion

		#region PREDICT

		public static Surf.Set Predict(Bamboo.Parsing.Grammars.Grammar grammar, Surf.Set FIRST, Surf.Set FOLLOW)
		{
			Surf.Set PREDICT = new Surf.Set();

			int i = 0;
			foreach (Bamboo.Parsing.Grammars.Production production in grammar.Productions)
			{
				Surf.Set PREDICT_i = ((Surf.Set)FIRST.Apply(GetFirstSymbol(production.Expression))).Difference(EPSILON);
				if (((Surf.Set)FIRST.Apply(GetFirstSymbol(production.Expression))).Contains("EPSILON"))
				{
					Union(PREDICT_i, (Surf.Set)FOLLOW.Apply(production.Nonterminal));
				}

				PREDICT.Add(new Surf.Tuple(new object[] { i, production, PREDICT_i }));
				i++;
			}
			AssertDisjoint(PREDICT, grammar.Nonterminals);
			return PREDICT;
		}

		private static void AssertDisjoint(Surf.Set PREDICT, Surf.Set nonterminals)
		{
			Dictionary<string, List<Surf.Set>> lookup = new Dictionary<string, List<Surf.Set>>();
			foreach (string nonterminal in nonterminals)
			{
				lookup.Add(nonterminal, new List<Surf.Set>());
			}
			foreach (Surf.Tuple tuple in PREDICT)
			{
				Bamboo.Parsing.Grammars.Production production = (Bamboo.Parsing.Grammars.Production)tuple[1];
				Surf.Set predict = (Surf.Set)tuple[2];
				lookup[production.Nonterminal].Add(predict);
			}
			foreach (List<Surf.Set> sets in lookup.Values)
			{
				for (int i = 0; i < sets.Count; i++)
				{
					for (int j = (i + 1); j < sets.Count; j++)
					{
						if (sets[i].Intersection(sets[j]).Count > 0)
						{
							System.Console.WriteLine("PREDICT");
							foreach (Surf.Tuple tuple in PREDICT)
							{
								int n = (int)tuple[0];
								Bamboo.Parsing.Grammars.Production production = (Bamboo.Parsing.Grammars.Production)tuple[1];
								Surf.Set predict = (Surf.Set)tuple[2];

								System.Console.Write(n + "	" + production.Nonterminal + "		");
								Surf.Printer.Print(predict, System.Console.Out);
								System.Console.WriteLine();
							}
							System.Console.WriteLine();

							throw new System.Exception("Ambiguous grammar.  Predict-predict conflict.");
						}
					}
				}
			}
		}

		private static string GetFirstSymbol(Bamboo.Parsing.Grammars.Expression expression)
		{
			if (expression is Bamboo.Parsing.Grammars.Symbol)
			{
				Bamboo.Parsing.Grammars.Symbol symbol = (Bamboo.Parsing.Grammars.Symbol)expression;
				return symbol.Token;
			}
			else if (expression is Bamboo.Parsing.Grammars.Concatenation)
			{
				Bamboo.Parsing.Grammars.Concatenation concatenation = (Bamboo.Parsing.Grammars.Concatenation)expression;
				return GetFirstSymbol(concatenation.Expressions[0]);
			}
			else
			{
				throw new System.Exception("INVALID");
			}
		}

		#endregion

		private static void Union(Surf.Set a, Surf.Set b)
		{
			foreach (object item in b)
			{
				a.Add(item);
			}
		}

		private Algorithms()
		{
		}

	}
}
