using System;
using System.Collections.Generic;
using System.Text;

namespace Bamboo.Parsing.FiniteAutomata
{
	public class Alphabet
	{

		public static List<System.Collections.BitArray> Create2(Bamboo.Parsing.RegularExpressions.Expression expression)
		{
			return Partition(Extract(expression, true));
		}

		private static List<System.Collections.BitArray> Partition(List<System.Collections.BitArray> bitArrays)
		{
			System.Collections.BitArray empty = new System.Collections.BitArray(bitArrays[0].Count);

			for (int i = 0; i < bitArrays.Count; i++)
			{
				System.Collections.BitArray bitArray = bitArrays[i];

				for (int j = (i + 1); j < bitArrays.Count; j++)
				{
					System.Collections.BitArray bitArray2 = bitArrays[j];

					System.Collections.BitArray bitArray3 = And(bitArray, bitArray2);
					if (!Equals(bitArray3, empty))
					{
						bitArrays.RemoveAt(j);
						bitArrays.RemoveAt(i);
						i--;

						if (!Contains(bitArrays, bitArray3))
						{
							bitArrays.Add(bitArray3);
						}

						System.Collections.BitArray bitArray4 = Xor(bitArray3, bitArray);
						if (!Equals(bitArray4, empty))
						{
							if (!Contains(bitArrays, bitArray4))
							{
								bitArrays.Add(bitArray4);
							}
						}

						System.Collections.BitArray bitArray5 = Xor(bitArray3, bitArray2);
						if (!Equals(bitArray5, empty))
						{
							if (!Contains(bitArrays, bitArray5))
							{
								bitArrays.Add(bitArray5);
							}
						}

						break;
					}
				}
			}

			return bitArrays;
		}

		private static List<System.Collections.BitArray> Extract(Bamboo.Parsing.RegularExpressions.Expression expression, bool first) //TODO this sucks.
		{
			if (expression is Bamboo.Parsing.RegularExpressions.Literal)
			{
				Bamboo.Parsing.RegularExpressions.Literal literal = (Bamboo.Parsing.RegularExpressions.Literal)expression;

				List<System.Collections.BitArray> bitArrays = new List<System.Collections.BitArray>();
				char[] ach = literal.Value.ToCharArray();
				for (int i = 0; i < ach.Length; i++)
				{
					char ch = ach[i];
					int n = (int)ch;

					System.Collections.BitArray bitArray = new System.Collections.BitArray(128);
					bitArray.Set(n, true);
					if (!Contains(bitArrays, bitArray))
					{
						bitArrays.Add(bitArray);
					}
				}
				return bitArrays;
			}
			else if (expression is Bamboo.Parsing.RegularExpressions.Concatenation)
			{
				Bamboo.Parsing.RegularExpressions.Concatenation concatenation = (Bamboo.Parsing.RegularExpressions.Concatenation)expression;

				List<System.Collections.BitArray> bitArrays = new List<System.Collections.BitArray>();
				foreach (System.Collections.BitArray bitArray in Extract(concatenation.A, false))
				{
					if (!Contains(bitArrays, bitArray))
					{
						bitArrays.Add(bitArray);
					}
				}
				foreach (System.Collections.BitArray bitArray in Extract(concatenation.B, false))
				{
					if (!Contains(bitArrays, bitArray))
					{
						bitArrays.Add(bitArray);
					}
				}
				return bitArrays;
			}
			else if (expression is Bamboo.Parsing.RegularExpressions.Alternation)
			{
				Bamboo.Parsing.RegularExpressions.Alternation alternation = (Bamboo.Parsing.RegularExpressions.Alternation)expression;

				List<System.Collections.BitArray> bitArraysA = Extract(alternation.A, first);
				List<System.Collections.BitArray> bitArraysB = Extract(alternation.B, first);

				//TODO this sucks.
				if (!first && bitArraysA.Count == 1 && bitArraysB.Count == 1)
				{
					List<System.Collections.BitArray> bitArrays = new List<System.Collections.BitArray>();
					bitArrays.Add(bitArraysA[0].Or(bitArraysB[0]));
					return bitArrays;
				}
				else
				{
					List<System.Collections.BitArray> bitArrays = new List<System.Collections.BitArray>();
					foreach (System.Collections.BitArray bitArray in bitArraysA)
					{
						if (!Contains(bitArrays, bitArray))
						{
							bitArrays.Add(bitArray);
						}
					}
					foreach (System.Collections.BitArray bitArray in bitArraysB)
					{
						if (!Contains(bitArrays, bitArray))
						{
							bitArrays.Add(bitArray);
						}
					}
					return bitArrays;
				}
			}
			else if (expression is Bamboo.Parsing.RegularExpressions.Repitition)
			{
				Bamboo.Parsing.RegularExpressions.Repitition repitition = (Bamboo.Parsing.RegularExpressions.Repitition)expression;

				return Extract(repitition.Expression, false);
			}
			else if (expression is Bamboo.Parsing.RegularExpressions.Optional)
			{
				Bamboo.Parsing.RegularExpressions.Optional optional = (Bamboo.Parsing.RegularExpressions.Optional)expression;

				return Extract(optional.Expression, false);
			}
			else
			{
				throw new System.Exception("Unknown expression type: " + expression.GetType().FullName);
			}
		}

		private static bool Contains(List<System.Collections.BitArray> bitArrays, System.Collections.BitArray bitArray)
		{
			foreach (System.Collections.BitArray bitArray2 in bitArrays)
			{
				if (Equals(bitArray, bitArray2))
				{
					return true;
				}
			}
			return false;
		}

		private static bool Equals(System.Collections.BitArray bitArray, System.Collections.BitArray bitArray2)
		{
			if (bitArray.Count != bitArray2.Count)
			{
				return false;
			}
			for (int i = 0; i < bitArray.Count; i++)
			{
				if (bitArray[i] != bitArray2[i])
				{
					return false;
				}
			}
			return true;
		}

		private static System.Collections.BitArray And(System.Collections.BitArray bitArray, System.Collections.BitArray bitArray2)
		{
			//System.Collections.BitArray bitArray3 = new System.Collections.BitArray(bitArray);
			//System.Collections.BitArray bitArray4 = new System.Collections.BitArray(bitArray2);
			//return new System.Collections.BitArray(bitArray3).And(bitArray4);

			System.Collections.BitArray bitArray3 = new System.Collections.BitArray(bitArray.Count);
			for (int i = 0; i < bitArray.Count; i++)
			{
				if (bitArray[i] && bitArray2[i])
				{
					bitArray3.Set(i, true);
				}
			}
			return bitArray3;
		}

		private static System.Collections.BitArray Xor(System.Collections.BitArray bitArray, System.Collections.BitArray bitArray2)
		{
			//System.Collections.BitArray bitArray3 = new System.Collections.BitArray(bitArray);
			//System.Collections.BitArray bitArray4 = new System.Collections.BitArray(bitArray2);
			//return new System.Collections.BitArray(bitArray3).Xor(bitArray4);

			System.Collections.BitArray bitArray3 = new System.Collections.BitArray(bitArray.Count);
			for (int i = 0; i < bitArray.Count; i++)
			{
				if (bitArray[i] ^ bitArray2[i])
				{
					bitArray3.Set(i, true);
				}
			}
			return bitArray3;
		}

		private Alphabet()
		{
		}

	}
}
