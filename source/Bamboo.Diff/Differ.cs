// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005-2006 Swampware, Inc.
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

namespace Bamboo.Diff
{
	/// <summary>
	/// Summary description for Differ.
	/// </summary>
	public class Differ
	{

		private Differ()
		{
		}

		public static DiffList Diff(System.IO.TextReader source, System.IO.TextReader target)
		{
			System.Collections.ArrayList sourceList = new System.Collections.ArrayList();
			System.Collections.ArrayList targetList = new System.Collections.ArrayList();

			string line;

			while((line = source.ReadLine()) != null)
			{
				sourceList.Add(line);
			}

			while((line = target.ReadLine()) != null)
			{
				targetList.Add(line);
			}

			return Diff(sourceList, targetList);
		}

		public static DiffList Diff(System.Collections.IList source, System.Collections.IList target)
		{
			DiffList results = new DiffList();

			int i = 0;
			int j = 0;
			int k = 0;

			while(!(i == source.Count && j == target.Count))
			{
				//End of target.
				if(j == target.Count)
				{
					results.Add(new DiffItem(DiffStatus.Deleted, i, source[i]));
					i++;
					continue;
				}

				//End of source.
				if(i == source.Count)
				{
					results.Add(new DiffItem(DiffStatus.Inserted, j, target[j]));
					j++;
					continue;
				}

				//End of source.
				if((i+k) == source.Count)
				{
					results.Add(new DiffItem(DiffStatus.Deleted, i, source[i]));
					results.Add(new DiffItem(DiffStatus.Inserted, j, target[j]));
					i++;
					j++;
					k = 0;
					continue;
				}

				//End of target.
				if((j+k) == target.Count)
				{
					results.Add(new DiffItem(DiffStatus.Deleted, i, source[i]));
					results.Add(new DiffItem(DiffStatus.Inserted, j, target[j]));
					i++;
					j++;
					k = 0;
					continue;
				}

				bool match_i = false;
				bool match_j = false;

				if(source[i+k].Equals(target[j]))
				{
					match_i = true;
				}
				if(source[i].Equals(target[j+k]))
				{
					match_j = true;
				}

				if(match_i && match_j)
				{
					results.Add(new DiffItem(DiffStatus.Unchanged, i, source[i]));
					i++;
					j++;
					k = 0;
				}
				else if(match_i)
				{
					results.Add(new DiffItem(DiffStatus.Deleted, i, source[i]));
					i++;
					k = 0;
				}
				else if(match_j)
				{
					results.Add(new DiffItem(DiffStatus.Inserted, j, target[j]));
					j++;
					k = 0;
				}
				else
				{
					k++;
				}
			}

			return results;
		}

		public static DiffList Compress(DiffList diffList)
		{
			DiffList result = new DiffList();

			foreach(DiffItem diffItem in diffList)
			{
				if(diffItem.Status != DiffStatus.Unchanged)
				{
					result.Add(diffItem);
				}
			}

			return result;
		}

	}
}
