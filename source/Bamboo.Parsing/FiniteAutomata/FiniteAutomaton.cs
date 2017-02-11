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

namespace Bamboo.Parsing.FiniteAutomata
{
	public class FiniteAutomaton
	{
		public int[] States;
		public char[] Alphabet;
		public Transition[] Transitions;
		public int StartState;
		public int[] FinalStates;
		public Token[] Tokens;

		public FiniteAutomaton()
		{
		}

		public FiniteAutomaton(Surf.Set states, Surf.Set alphabet, Surf.Set transitions, int startState, Surf.Set finalStates, Surf.Set tokens)
		{
			int[] states2 = new int[states.Count];
			for (int i = 0; i < states2.Length; i++)
			{
				states2[i] = (int)states[i];
			}

			char[] alphabet2 = new char[alphabet.Count];
			for (int i = 0; i < alphabet2.Length; i++)
			{
				alphabet2[i] = (char)alphabet[i];
			}

			Transition[] transitions2 = new Transition[transitions.Count];
			for (int i = 0; i < transitions2.Length; i++)
			{
				Surf.Tuple tuple = (Surf.Tuple)transitions[i];
				Surf.Tuple input = (Surf.Tuple)tuple[0];
				int toState = (int)tuple[1];

				Transition transition;
				if (input.Count == 1)
				{
					int fromState = (int)input[0];
					transition = new Transition(fromState, toState);
				}
				else if (input.Count == 2)
				{
					int fromState = (int)input[0];
					char character = (char)input[1];
					transition = new Transition(fromState, character, toState);
				}
				else
				{
					throw new System.Exception("UNEXPECTED.");
				}
				transitions2[i] = transition;
			}

			int[] finalStates2 = new int[finalStates.Count];
			for (int i = 0; i < finalStates2.Length; i++)
			{
				finalStates2[i] = (int)finalStates[i];
			}

			Token[] tokens2 = new Token[tokens.Count];
			for (int i = 0; i < tokens2.Length; i++)
			{
				Surf.Tuple tuple = (Surf.Tuple)tokens[i];

				int number = (int)tuple[0];
				string name = (string)tuple[1];

				tokens2[i] = new Token(name, number);
			}

			this.States = states2;
			this.Alphabet = alphabet2;
			this.Transitions = transitions2;
			this.StartState = startState;
			this.FinalStates = finalStates2;
			this.Tokens = tokens2;
		}

	}
}
