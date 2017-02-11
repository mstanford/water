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
	public class FollowBuilder
	{
		private static readonly Surf.Set EPSILON = new Surf.Set(new object[] { "EPSILON" });

		private Bamboo.Parsing.Grammars.Grammar grammar;
		private Surf.Set first;
		private Surf.Set follow;

		public FollowBuilder(Bamboo.Parsing.Grammars.Grammar grammar, Surf.Set FIRST)
		{
			this.grammar = grammar;
			this.first = FIRST;
			this.follow = new Surf.Set();
		}

		public Surf.Set Build()
		{
			foreach (string symbol in grammar.Terminals)
				follow.Add(new Surf.Tuple(new object[] { symbol, new Surf.Set() }));

			foreach (string symbol in grammar.Nonterminals)
				follow.Add(new Surf.Tuple(new object[] { symbol, new Surf.Set() }));

			// I hope this is fixed-point.
			for (int z = 0; z < grammar.Productions.Count; z++)
			{
				foreach (Bamboo.Parsing.Grammars.Production production in grammar.Productions)
				{
					string A = production.Nonterminal;
					List<Bamboo.Parsing.Grammars.Expression> B = ToList(production.Expression);
					int k = B.Count - 1;

					Union(FOLLOW(B[k]), FOLLOW(A));

					Surf.Set trailer = FOLLOW(A);
					for (int i = k; i >= 1; i--)
					{
						if (FIRST(B[i]).Contains("EPSILON"))
						{
							Union(FOLLOW(B[i - 1]), FIRST(B[i]).Difference(EPSILON));
							Union(FOLLOW(B[i - 1]), trailer);
						}
						else
						{
							Union(FOLLOW(B[i - 1]), FIRST(B[i]));
							trailer = new Surf.Set();
						}
					}
				}
			}

			return follow;
		}

		private Surf.Set FIRST(Bamboo.Parsing.Grammars.Expression expression)
		{
			return (Surf.Set)first.Apply(((Bamboo.Parsing.Grammars.Symbol)expression).Token);
		}

		private Surf.Set FOLLOW(Bamboo.Parsing.Grammars.Expression expression)
		{
			return FOLLOW(((Bamboo.Parsing.Grammars.Symbol)expression).Token);
		}

		private Surf.Set FOLLOW(string symbol)
		{
			if (!follow.IsDefined(symbol))
			{
				return new Surf.Set();
			}
			return (Surf.Set)follow.Apply(symbol);
		}

		private static void Union(Surf.Set a, Surf.Set b)
		{
			foreach (object item in b)
			{
				a.Add(item);
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

	}
}
