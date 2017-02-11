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
	public class Transition : System.IComparable
	{
		public int FromState;
		public char Character;
		public bool Epsilon;
		public int ToState;

		public Transition(int fromState, int toState)
		{
			FromState = fromState;
			ToState = toState;
			Epsilon = true;
		}

		public Transition(int fromState, char character, int toState)
		{
			FromState = fromState;
			Character = character;
			ToState = toState;
			Epsilon = false;
		}

		public override bool Equals(object obj)
		{
			return (this.CompareTo(obj) == 0);
		}

		public override int GetHashCode()
		{
			string key = this.FromState + "," + this.Character + "," + this.Epsilon + "," + this.ToState;
			return key.GetHashCode();
		}

		public int CompareTo(object obj)
		{
			Transition a = this;
			Transition b = (Transition)obj;

			int comparison = a.ToState.CompareTo(b.ToState);
			if (comparison != 0)
			{
				return comparison;
			}

			comparison = a.Character.CompareTo(b.Character);
			if (comparison != 0)
			{
				return comparison;
			}

			comparison = a.FromState.CompareTo(b.FromState);
			if (comparison != 0)
			{
				return comparison;
			}

			return 0;
		}

	}
}
