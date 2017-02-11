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
	public class Algorithms
	{

		public static FiniteAutomaton CreateFA(Bamboo.Parsing.RegularExpressions.Expression expression)
		{
			FiniteAutomaton nfa = NFA(expression);



			List<System.Collections.BitArray> alphabet = Alphabet.Create(expression);

			char[] alphabet_index = new char[128];
			for (int i = 0; i < alphabet.Count; i++)
			{
				System.Collections.BitArray bitArray = alphabet[i];

				for (int j = 0; j < bitArray.Count; j++)
				{
					if (bitArray[j])
					{
						if (alphabet_index[j] != 0)
						{
							throw new System.Exception("Already assigned.");
						}
						alphabet_index[j] = (char)i;
					}
				}
			}



			Surf.Set alphabet2 = new Surf.Set();
			foreach (char ch in nfa.Alphabet)
			{
				alphabet2.Add(alphabet_index[ch]);
			}
			Surf.Set transitions2 = new Surf.Set();
			foreach (Transition transition in nfa.Transitions)
			{
				if (transition.Epsilon)
				{
					transitions2.Add(transition);
				}
				else
				{
					transitions2.Add(new Transition(transition.FromState, alphabet_index[transition.Character], transition.ToState));
				}
			}

			FiniteAutomaton nfa2 = new FiniteAutomaton();
			nfa2.States = nfa.States;
			nfa2.StartState = nfa.StartState;
			nfa2.FinalStates = nfa.FinalStates;
			nfa2.Tokens = nfa.Tokens;
			nfa2.Alphabet = new char[alphabet2.Count];
			for (int i = 0; i < alphabet2.Count; i++)
			{
				nfa2.Alphabet[i] = (char)alphabet2[i];
			}
			nfa2.Transitions = new Transition[transitions2.Count];
			for (int i = 0; i < transitions2.Count; i++)
			{
				nfa2.Transitions[i] = (Transition)transitions2[i];
			}



			FiniteAutomaton dfa = DFA(nfa2);



			FiniteAutomaton fa2 = new FiniteAutomaton();
			fa2.States = dfa.States;
			fa2.StartState = dfa.StartState;
			fa2.FinalStates = dfa.FinalStates;
			fa2.Tokens = dfa.Tokens;

			List<char> alphabet3 = new List<char>();
			foreach (char ch in dfa.Alphabet)
			{
				System.Collections.BitArray bitArray = alphabet[ch];
				for (int i = 0; i < bitArray.Count; i++)
				{
					if (bitArray[i])
					{
						alphabet3.Add((char)i);
					}
				}
			}
			fa2.Alphabet = alphabet3.ToArray();

			List<Bamboo.Parsing.FiniteAutomata.Transition> transitions = new List<Transition>();
			foreach (Bamboo.Parsing.FiniteAutomata.Transition transition in dfa.Transitions)
			{
				if (transition.Epsilon)
				{
					transitions.Add(transition);
				}
				else
				{
					System.Collections.BitArray bitArray = alphabet[transition.Character];
					for (int i = 0; i < bitArray.Count; i++)
					{
						if (bitArray[i])
						{
							transitions.Add(new Bamboo.Parsing.FiniteAutomata.Transition(transition.FromState, (char)i, transition.ToState));
						}
					}
				}
			}
			fa2.Transitions = transitions.ToArray();

			return fa2;
		}

		#region NFA

		//
		// Thompson's Construction
		//

		public static FiniteAutomaton NFA(Bamboo.Parsing.RegularExpressions.Expression expression)
		{
			System.Console.WriteLine("START NFA     " + System.DateTime.Now.ToString());

			FiniteAutomaton fa = NFA(expression, new Counter());

			System.Console.WriteLine("END   NFA     " + System.DateTime.Now.ToString());
			return fa;
		}

		private static FiniteAutomaton NFA(Bamboo.Parsing.RegularExpressions.Expression expression, Counter counter)
		{
			if (expression is Bamboo.Parsing.RegularExpressions.Literal)
			{
				Bamboo.Parsing.RegularExpressions.Literal literal = (Bamboo.Parsing.RegularExpressions.Literal)expression;

				Surf.Set states = new Surf.Set();
				Surf.Set alphabet = new Surf.Set();
				Surf.Set transitions = new Surf.Set();
				int startState;
				Surf.Set finalStates = new Surf.Set();
				Surf.Set tokens = new Surf.Set();

				startState = counter.Next();
				int currentState = startState;

				char[] ach = literal.Value.ToCharArray();
				for (int i = 0; i < ach.Length; i++)
				{
					char ch = ach[i];

					states.Add(currentState);
					alphabet.Add(ch);

					int nextState = counter.Next();
					transitions.Add(Transition(currentState, ch, nextState));
					currentState = nextState;
				}

				states.Add(currentState);
				finalStates.Add(currentState);
				if (literal.TokenType.Length > 0)
				{
					foreach (int state in finalStates)
					{
						tokens.Add(new Surf.Tuple(new object[] { state, literal.TokenType }));
					}
				}

				return new FiniteAutomaton(states, alphabet, transitions, startState, finalStates, tokens);
			}
			else if (expression is Bamboo.Parsing.RegularExpressions.Concatenation)
			{
				Bamboo.Parsing.RegularExpressions.Concatenation concatenation = (Bamboo.Parsing.RegularExpressions.Concatenation)expression;

				Surf.Set states = new Surf.Set();
				Surf.Set alphabet = new Surf.Set();
				Surf.Set transitions = new Surf.Set();
				int startState;
				Surf.Set finalStates = new Surf.Set();
				Surf.Set tokens = new Surf.Set();

				FiniteAutomaton a = NFA(concatenation.A, counter);
				FiniteAutomaton b = NFA(concatenation.B, counter);

				states = states.Union(new Surf.Set(a.States));
				states = states.Union(new Surf.Set(b.States));

				alphabet = alphabet.Union(new Surf.Set(a.Alphabet));
				alphabet = alphabet.Union(new Surf.Set(b.Alphabet));

				transitions = transitions.Union(Set(a.Transitions));
				transitions = transitions.Union(Set(b.Transitions));
//TODO
//				transitions.Add(Transition((int)a.FinalStates[0], b.StartState));
				foreach (int aFinalState in a.FinalStates)
				{
					transitions.Add(Transition(aFinalState, b.StartState));
				}

				startState = a.StartState;

				finalStates = new Surf.Set(b.FinalStates);
				if (concatenation.TokenType.Length > 0)
				{
					foreach (int state in finalStates)
					{
						tokens.Add(new Surf.Tuple(new object[] { state, concatenation.TokenType }));
					}
				}
				foreach (Token token in a.Tokens)
				{
					tokens.Add(new Surf.Tuple(new object[] { token.Number, token.Name }));
				}
				foreach (Token token in b.Tokens)
				{
					tokens.Add(new Surf.Tuple(new object[] { token.Number, token.Name }));
				}

				return new FiniteAutomaton(states, alphabet, transitions, startState, finalStates, tokens);
			}
			else if (expression is Bamboo.Parsing.RegularExpressions.Alternation)
			{
				Bamboo.Parsing.RegularExpressions.Alternation alternation = (Bamboo.Parsing.RegularExpressions.Alternation)expression;

				Surf.Set states = new Surf.Set();
				Surf.Set alphabet = new Surf.Set();
				Surf.Set transitions = new Surf.Set();
				int startState;
				Surf.Set finalStates = new Surf.Set();
				Surf.Set tokens = new Surf.Set();

				startState = counter.Next();
				int finalState = counter.Next();

				FiniteAutomaton a = NFA(alternation.A, counter);
				FiniteAutomaton b = NFA(alternation.B, counter);

				states = states.Union(new Surf.Set(a.States));
				states = states.Union(new Surf.Set(b.States));
				states.Add(startState);
				states.Add(finalState);

				alphabet = alphabet.Union(new Surf.Set(a.Alphabet));
				alphabet = alphabet.Union(new Surf.Set(b.Alphabet));

				transitions = transitions.Union(Set(a.Transitions));
				transitions = transitions.Union(Set(b.Transitions));
				transitions.Add(Transition(startState, a.StartState));
				transitions.Add(Transition(startState, b.StartState));
//TODO
//	            transitions.Add(Transition((int)a.FinalStates[0], finalState)); 
				foreach (int aFinalState in a.FinalStates)
				{
					transitions.Add(Transition(aFinalState, finalState));
				}
//TODO
//	            transitions.Add(Transition((int)b.FinalStates[0], finalState));
				foreach (int bFinalState in b.FinalStates)
				{
					transitions.Add(Transition(bFinalState, finalState));
				}

				finalStates.Add(finalState);
				if (alternation.TokenType.Length > 0)
				{
					foreach (int state in finalStates)
					{
						tokens.Add(new Surf.Tuple(new object[] { state, alternation.TokenType }));
					}
				}
				foreach (Token token in a.Tokens)
				{
					tokens.Add(new Surf.Tuple(new object[] { token.Number, token.Name }));
				}
				foreach (Token token in b.Tokens)
				{
					tokens.Add(new Surf.Tuple(new object[] { token.Number, token.Name }));
				}

				return new FiniteAutomaton(states, alphabet, transitions, startState, finalStates, tokens);
			}
			else if (expression is Bamboo.Parsing.RegularExpressions.Repitition)
			{
				Bamboo.Parsing.RegularExpressions.Repitition repitition = (Bamboo.Parsing.RegularExpressions.Repitition)expression;

				Surf.Set states = new Surf.Set();
				Surf.Set alphabet = new Surf.Set();
				Surf.Set transitions = new Surf.Set();
				int startState;
				Surf.Set finalStates = new Surf.Set();
				Surf.Set tokens = new Surf.Set();

				startState = counter.Next();
				int finalState = counter.Next();

				FiniteAutomaton a = NFA(repitition.Expression, counter);

				states = states.Union(new Surf.Set(a.States));
				states.Add(startState);
				states.Add(finalState);

				alphabet = alphabet.Union(new Surf.Set(a.Alphabet));

				transitions = transitions.Union(Set(a.Transitions));
	            transitions.Add(Transition(startState, a.StartState));
				transitions.Add(Transition(startState, finalState));
//TODO
//	            transitions.Add(Transition((int)a.FinalStates[0], finalState));
				foreach (int aFinalState in a.FinalStates)
				{
					transitions.Add(Transition(aFinalState, finalState));
				}
//TODO
//	            transitions.Add(Transition((int)a.FinalStates[0], a.StartState));
				foreach (int aFinalState in a.FinalStates)
				{
					transitions.Add(Transition(aFinalState, a.StartState));
				}

				finalStates.Add(finalState);
				if (repitition.TokenType.Length > 0)
				{
					foreach (int state in finalStates)
					{
						tokens.Add(new Surf.Tuple(new object[] { state, repitition.TokenType }));
					}
				}
				foreach (Token token in a.Tokens)
				{
					tokens.Add(new Surf.Tuple(new object[] { token.Number, token.Name }));
				}

				return new FiniteAutomaton(states, alphabet, transitions, startState, finalStates, tokens);
			}
			else if (expression is Bamboo.Parsing.RegularExpressions.Optional)
			{
				Bamboo.Parsing.RegularExpressions.Optional optional = (Bamboo.Parsing.RegularExpressions.Optional)expression;

				Surf.Set states = new Surf.Set();
				Surf.Set alphabet = new Surf.Set();
				Surf.Set transitions = new Surf.Set();
				int startState;
				Surf.Set finalStates = new Surf.Set();
				Surf.Set tokens = new Surf.Set();

				startState = counter.Next();
				int finalState = counter.Next();

				FiniteAutomaton a = NFA(optional.Expression, counter);

				states = states.Union(new Surf.Set(a.States));
				states.Add(startState);
				states.Add(finalState);

				alphabet = alphabet.Union(new Surf.Set(a.Alphabet));

				transitions = transitions.Union(Set(a.Transitions));
				transitions.Add(Transition(startState, a.StartState));
				transitions.Add(Transition(startState, finalState));
//TODO
//				transitions.Add(Transition((int)a.FinalStates[0], finalState));
				foreach (int aFinalState in a.FinalStates)
				{
					transitions.Add(Transition(aFinalState, finalState));
				}

				finalStates.Add(finalState);
				if (optional.TokenType.Length > 0)
				{
					foreach (int state in finalStates)
					{
						tokens.Add(new Surf.Tuple(new object[] { state, optional.TokenType }));
					}
				}
				foreach (Token token in a.Tokens)
				{
					tokens.Add(new Surf.Tuple(new object[] { token.Number, token.Name }));
				}

				return new FiniteAutomaton(states, alphabet, transitions, startState, finalStates, tokens);
			}
			else
			{
				throw new System.Exception("Unknown expression type: " + expression.GetType().FullName);
			}
		}

		#endregion

		#region DFA

		//
		// Subset Construction
		//

		public static FiniteAutomaton DFA(Bamboo.Parsing.FiniteAutomata.FiniteAutomaton nfa)
		{
			return DFA(nfa, new Counter());
		}

		public static FiniteAutomaton DFA(Bamboo.Parsing.FiniteAutomata.FiniteAutomaton nfa, Counter counter)
		{
			System.Console.WriteLine("START DFA     " + System.DateTime.Now.ToString());

			Surf.Set epsilonCache = new Surf.Set();
			Surf.Set moveCache = new Surf.Set();

			Surf.Set nfa_transitions = Set(nfa.Transitions);

			Dictionary<string, int> nfa_transitions_lookup = new Dictionary<string, int>();
			foreach (Surf.Tuple transition in nfa_transitions)
			{
				Surf.Tuple input = (Surf.Tuple)transition[0];

				if (input.Count == 2)
				{
					int state = (int)input[0];
					char ch = (char)input[1];

					nfa_transitions_lookup.Add("" + state + "::" + ch, (int)transition[1]);
				}
			}

			Surf.Set q0 = EpsilonClosure(new Surf.Set(new object[] { nfa.StartState }), nfa_transitions, epsilonCache);
			Surf.Set Q = new Surf.Set(); // A set whose element are sets of states that are subsets of N.
			Q.Add(q0);
			Surf.Set T = new Surf.Set();
			Stack<Surf.Set> worklist = new Stack<Surf.Set>();
			worklist.Push(q0);

			while (worklist.Count > 0)
			{
				Surf.Set q = worklist.Pop();

				foreach (char character in nfa.Alphabet)
				{
					Surf.Set t = EpsilonClosure(Move(q, character, nfa_transitions_lookup), nfa_transitions, epsilonCache);

					if (t.Count > 0)
					{
						T.Add(new Surf.Tuple(new object[] { new Surf.Tuple(new object[] { q, character }), t }));

						if (!Q.Contains(t))
						{
							Q.Add(t);
							worklist.Push(t);
						}
					}
				}
			}



			Surf.Set states = new Surf.Set();
			Surf.Set alphabet = new Surf.Set();
			Surf.Set transitions = new Surf.Set();
			int startState;
			Surf.Set finalStates = new Surf.Set();
			Surf.Set tokens = new Surf.Set();

			Surf.Set D = new Surf.Set();
			foreach (Surf.Set q in Q)
			{
				D.Add(new Surf.Tuple(new object[] { q, counter.Next() }));
			}



			foreach (Surf.Set q in Q)
			{
				states.Add(D.Apply(q));
			}
			alphabet = new Surf.Set(nfa.Alphabet);
			foreach (Surf.Tuple t in T)
			{
				Surf.Tuple input = (Surf.Tuple)t[0];
				int fromState = (int)D.Apply(input[0]);
				char character = (char)input[1];
				int toState = (int)D.Apply(t[1]);
				transitions.Add(new Surf.Tuple(new object[] { new Surf.Tuple(new object[] { fromState, character }), toState }));
			}
			startState = (int)states[0];
			foreach (int state in nfa.FinalStates)
			{
				foreach (Surf.Tuple d in D)
				{
					Surf.Set q = (Surf.Set)d[0];
					if (q.Contains(state))
					{
						finalStates.Add(d[1]);
						foreach (int q_state in q)
						{
							string token = Lookup(nfa.Tokens, (int)q_state);
							if (token.Length > 0)
							{
								tokens.Add(new Surf.Tuple(new object[] { d[1], token }));
								break;
							}
						}
					}
				}
			}

			System.Console.WriteLine("END   DFA     " + System.DateTime.Now.ToString());
			//foreach (Token token in nfa.Tokens)
			//{
			//    System.Console.WriteLine(token.Name + " " + token.Number);
			//}
			//System.Console.WriteLine();
			//Surf.Printer.Print(tokens, System.Console.Out);
			//System.Console.WriteLine();
			//System.Console.WriteLine();
			return Minimize(Reorder(new FiniteAutomaton(states, alphabet, transitions, startState, finalStates, tokens)));
		}

		private static Surf.Set EpsilonClosure(Surf.Set states, Surf.Set transitions, Surf.Set epsilonCache)
		{
			Surf.Tuple key = Tuple(states);
			if (epsilonCache.IsDefined(key))
			{
				return (Surf.Set)epsilonCache.Apply(key);
			}

			//System.Console.WriteLine("START EpsilonClosure     " + System.DateTime.Now.ToString());
			Surf.Set epsilonClosure = new Surf.Set();

			Stack<int> worklist = new Stack<int>();

			foreach (int state in states)
			{
				epsilonClosure.Add(state);
				worklist.Push(state);
			}

			while (worklist.Count > 0)
			{
				int state = worklist.Pop();

				foreach (Surf.Tuple transition in transitions)
				{
					Surf.Tuple input = (Surf.Tuple)transition[0];

					if (input.Count == 1 && (int)input[0] == state)
					{
						int toState = (int)transition[1];
						if (!epsilonClosure.Contains(toState))
						{
							epsilonClosure.Add(toState);
							worklist.Push(toState);
						}
					}
				}
			}

			epsilonCache.Add(new Surf.Tuple(new object[] { key, epsilonClosure }));
			//System.Console.WriteLine("END   EpsilonClosure     " + System.DateTime.Now.ToString());
			return epsilonClosure;
		}

		private static Surf.Set Move(Surf.Set states, char ch, Dictionary<string, int> transitions)
		{
			//System.Console.WriteLine("START Move     " + System.DateTime.Now.ToString());
			Surf.Set move = new Surf.Set();

			foreach (int state in states)
			{
				if (transitions.ContainsKey("" + state + "::" + ch))
				{
					move.Add(transitions["" + state + "::" + ch]);
				}
				//foreach (Surf.Tuple transition in transitions)
				//{
				//    Surf.Tuple input = (Surf.Tuple)transition[0];

				//    if (input.Count == 2 && (int)input[0] == state && (char)input[1] == ch)
				//    {
				//        move.Add(transition[1]);
				//    }
				//}
			}

			//System.Console.WriteLine("END   Move     " + System.DateTime.Now.ToString());
			return move;
		}

		// Hopcroft's Algorithm
		public static FiniteAutomaton Minimize(FiniteAutomaton dfa)
		{
			System.Console.WriteLine("START Minimize     " + System.DateTime.Now.ToString());

			Surf.Set dfa_transitions = Set(dfa.Transitions);

			// Split final states and non-final states.
			Surf.Set worklist = new Surf.Set();
			Surf.Set P = new Surf.Set();

			Surf.Set f = SplitFinalStates(dfa.Tokens);
			foreach (Surf.Set f2 in f)
			{
				worklist.Add(f2);
				P.Add(f2);
			}
			Surf.Set nonFinalStates = new Surf.Set(dfa.States).Difference(new Surf.Set(dfa.FinalStates));
			worklist.Add(nonFinalStates);
			P.Add(nonFinalStates);

			// While there are more states to split.
			while (worklist.Count > 0)
			{
				Surf.Set p = (Surf.Set)worklist[0];
				worklist.Remove(0);

				Surf.Set t = Split(p, dfa_transitions, P);

				if (t.Count > 1)
				{
					int i = 0;
					foreach (Surf.Set p2 in P)
					{
						if (p2.Equals(p))
						{
							P.Remove(i);
							break;
						}
						i++;
					}
					foreach (Surf.Set t2 in t)
					{
						worklist.Add(t2);
						P.Add(t2);
					}
				}
			}
			/*
			// Split final states and non-final states.
			Surf.Set P = new Surf.Set();
			Surf.Set f = SplitFinalStates(dfa.Tokens);
			foreach (Surf.Set f2 in f)
			{
				P.Add(f2);
			}
			P.Add(new Surf.Set(dfa.States).Difference(new Surf.Set(dfa.FinalStates)));

			// While there are more states to split.
			bool isChanging = true;
			while (isChanging)
			{
				isChanging = false;

				Surf.Set T = new Surf.Set();
				foreach (Surf.Set p in P)
				{
					Surf.Set t = Split(p, dfa_transitions, P);

					if (t.Count > 1)
					{
						isChanging = true;
					}
					foreach (Surf.Set t2 in t)
					{
						T.Add(t2);
					}
				}
				P = T;
			}
			*/



			Surf.Set states = new Surf.Set();
			Surf.Set alphabet = new Surf.Set();
			Surf.Set transitions = new Surf.Set();
			int startState;
			Surf.Set finalStates = new Surf.Set();
			Surf.Set tokens = new Surf.Set();

			Surf.Set P_map = PartitionMap(P);

			foreach (int state in dfa.States)
			{
				states.Add(P_map.Apply(state));
			}
			alphabet = new Surf.Set(dfa.Alphabet);
			foreach (Surf.Tuple transition in dfa_transitions)
			{
				Surf.Tuple input = (Surf.Tuple)transition[0];
				int fromState = (int)P_map.Apply(input[0]);
				char character = (char)input[1];
				int toState = (int)P_map.Apply(transition[1]);
				transitions.Add(new Surf.Tuple(new object[] { new Surf.Tuple(new object[] { fromState, character }), toState }));
			}
			startState = (int)P_map.Apply(dfa.StartState);
			foreach (int state in dfa.FinalStates)
			{
				finalStates.Add(P_map.Apply(state));
				if (!tokens.IsDefined(P_map.Apply(state)))
				{
					tokens.Add(new Surf.Tuple(new object[] { P_map.Apply(state), Lookup(dfa.Tokens, state) }));
				}
			}

			System.Console.WriteLine("END   Minimize     " + System.DateTime.Now.ToString());
			return Reorder(new FiniteAutomaton(states, alphabet, transitions, startState, finalStates, tokens));
		}

		private static Surf.Set SplitFinalStates(Token[] tokens)
		{
			Surf.Set fn = new Surf.Set();
			foreach (Token token in tokens)
			{
				fn.Add(new Surf.Tuple(new object[] {token.Name, token.Number }));
			}
			return fn.Nest().Range();
		}

		private static Surf.Set Split(Surf.Set S, Surf.Set transitions, Surf.Set P2)
		{
			//System.Console.WriteLine("START Split     " + System.DateTime.Now.ToString());

			Surf.Set alphabet = new Surf.Set();
			foreach (int state in S)
			{
				foreach (Surf.Tuple transition in transitions)
				{
					Surf.Tuple input = (Surf.Tuple)transition[0];
					int fromState = (int)input[0];
					char character = (char)input[1];
					int toState = (int)transition[1];

					if (fromState == state)
					{
						alphabet.Add(character);
					}
				}
			}

			foreach (char character in alphabet)
			{
			    Surf.Set s = Split(S, character, transitions, P2);
			    if (s.Count > 1)
			    {
					//System.Console.WriteLine("END   Split     " + System.DateTime.Now.ToString());
					return s;
			    }
			}

			//System.Console.WriteLine("END   Split     " + System.DateTime.Now.ToString());
			return new Surf.Set(new object[] { S });
		}

		private static Surf.Set Split(Surf.Set S, char character, Surf.Set transitions, Surf.Set P2)
		{
			//System.Console.WriteLine("START Split2     " + System.DateTime.Now.ToString());

			Surf.Set f_nP = PartitionMap(P2);

			Surf.Set partition_map = new Surf.Set();

			foreach (int fromState in S)
			{
				object key = new Surf.Tuple(new object[] { fromState, character });
				if (transitions.IsDefined(key))
				{
					object value = transitions.Apply(key);
					int toState = (int)value;
					int partition = (int)f_nP.Apply(toState);
					partition_map.Add(new Surf.Tuple(new object[] { partition, fromState }));
				}
				else
				{
					partition_map.Add(new Surf.Tuple(new object[] { -1, fromState }));
				}
			}

			//System.Console.WriteLine("END   Split2     " + System.DateTime.Now.ToString());
			return partition_map.Nest().Range();
		}

		private static Surf.Set PartitionMap(Surf.Set T)
		{
			// Label the partition.
			Surf.Set R = new Surf.Set();
			int i = 0;
			foreach (Surf.Set t in T)
			{
				if (t.Count > 0)
				{
					R.Add(new Surf.Tuple(new object[] { i, t }));
					i++;
				}
			}

			return R.Unnest().Transpose();
		}

		private static FiniteAutomaton Reorder(Bamboo.Parsing.FiniteAutomata.FiniteAutomaton fa)
		{
			System.Console.WriteLine("START Reorder     " + System.DateTime.Now.ToString());

			Surf.Set stateMap = new Surf.Set();

			GetToStates(fa.StartState, fa.Transitions, stateMap, new Counter());



			Surf.Set states = new Surf.Set();
			Surf.Set alphabet = new Surf.Set();
			Surf.Set transitions = new Surf.Set();
			int startState;
			Surf.Set finalStates = new Surf.Set();
			Surf.Set tokens = new Surf.Set();

			for(int i = 0; i < fa.States.Length; i++)
			{
				states.Add(stateMap.Apply(fa.States[i]));
			}
			alphabet = new Surf.Set(fa.Alphabet);
			for (int i = 0; i < fa.Transitions.Length; i++)
			{
				if (fa.Transitions[i].Epsilon)
				{
					transitions.Add(new Surf.Tuple(new object[] { new Surf.Tuple(new object[] { stateMap.Apply(fa.Transitions[i].FromState) }), stateMap.Apply(fa.Transitions[i].ToState) }));
				}
				else
				{
					transitions.Add(new Surf.Tuple(new object[] { new Surf.Tuple(new object[] { stateMap.Apply(fa.Transitions[i].FromState), fa.Transitions[i].Character }), stateMap.Apply(fa.Transitions[i].ToState) }));
				}
			}
			startState = (int)stateMap.Apply(fa.StartState);
			for (int i = 0; i < fa.FinalStates.Length; i++)
			{
				finalStates.Add(stateMap.Apply(fa.FinalStates[i]));
				tokens.Add(new Surf.Tuple(new object[] { stateMap.Apply(fa.FinalStates[i]), Lookup(fa.Tokens, fa.FinalStates[i]) }));
			}

			System.Console.WriteLine("END   Reorder     " + System.DateTime.Now.ToString());
			return new FiniteAutomaton(states, alphabet, transitions, startState, finalStates, tokens);
		}

		private static void GetToStates(int fromState, Transition[] transitions, Surf.Set stateMap, Counter counter)
		{
			if (stateMap.IsDefined(fromState))
			{
				return;
			}

			stateMap.Add(new Surf.Tuple(new object[] { fromState, counter.Next() }));

			foreach (Transition transition in transitions)
			{
				if (fromState == transition.FromState)
				{
					GetToStates(transition.ToState, transitions, stateMap, counter);
				}
			}
		}

		#endregion

		private static Surf.Tuple Transition(int fromState, int toState)
		{
			#region Create a transition.

			return new Surf.Tuple(new object[] { 
				new Surf.Tuple(new object[] { fromState }), 
				toState });

			#endregion
		}

		private static Surf.Tuple Transition(int fromState, char character, int toState)
		{
			#region Create a transition.

			return new Surf.Tuple(new object[] { 
				new Surf.Tuple(new object[] { fromState, character }), 
				toState });

			#endregion
		}

		private static Surf.Set Set(Transition[] transitions)
		{
			#region Convert a Transition[] to a transition relation.

			Surf.Set fn = new Surf.Set();
			for (int i = 0; i < transitions.Length; i++)
			{
				Transition transition = transitions[i];
				if (transition.Epsilon)
				{
					fn.Add(new Surf.Tuple(new object[] { new Surf.Tuple(new object[] { transition.FromState } ), transition.ToState }));
				}
				else
				{
					fn.Add(new Surf.Tuple(new object[] { new Surf.Tuple(new object[] { transition.FromState, transition.Character } ), transition.ToState }));
				}
			}
			return fn;

			#endregion
		}

		private static Surf.Tuple Tuple(Surf.Set a)
		{
			Surf.Tuple b = new Surf.Tuple();
			foreach (object item in a)
			{
				b.Add(item);
			}
			return b;
		}

		private static string Lookup(Token[] tokens, int state)
		{
			foreach (Token token in tokens)
			{
				if (token.Number == state)
				{
					return token.Name;
				}
			}
			return "";
		}

		private Algorithms()
		{
		}

	}
}
