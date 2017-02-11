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

namespace Surf
{
	public class Comparer : System.Collections.IComparer
	{

		public Comparer()
		{
		}

		public int Compare(object a, object b)
		{
			System.Collections.ArrayList a_list = ToList(a);
			System.Collections.ArrayList b_list = ToList(b);

			if (a_list.Count >= b_list.Count)
			{
				return CompareList(a_list, b_list);
			}
			else
			{
				return (0 - CompareList(b_list, a_list));
			}
		}

		private int CompareList(System.Collections.ArrayList a_list, System.Collections.ArrayList b_list)
		{
			for (int i = 0; i < b_list.Count; i++)
			{
				System.IComparable a = (System.IComparable)a_list[i];
				System.IComparable b = (System.IComparable)b_list[i];
				int c = a.CompareTo(b);
				if (c != 0)
				{
					return c;
				}
			}
			if (a_list.Count == b_list.Count)
			{
				return 0;
			}
			return 1;
		}

		private System.Collections.ArrayList ToList(object a)
		{
			System.Collections.ArrayList a_list = new System.Collections.ArrayList();

			if (a is Tuple)
			{
				Tuple tuple = (Tuple)a;

				foreach (object item in tuple)
				{
					a_list.Add(item);
				}
			}
			else if (a is Set)
			{
				Set set = (Set)a;

				foreach (object item in set)
				{
					a_list.Add(item);
				}
			}
			else
			{
				a_list.Add(a);
			}

			return a_list;
		}

	}
}
