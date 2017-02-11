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

namespace Bamboo.Parsing.Generators
{
	public class Operators
	{

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

		private Operators()
		{
		}

	}
}
