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
	/// Summary description for Patcher.
	/// </summary>
	public class Patcher
	{

		private Patcher()
		{
		}

		public static System.IO.TextReader Patch(System.IO.TextReader source, DiffList diffList)
		{
			System.Collections.ArrayList sourceList = new System.Collections.ArrayList();

			string line;

			while((line = source.ReadLine()) != null)
			{
				sourceList.Add(line);
			}

			System.Collections.ArrayList results = Patch(sourceList, diffList);

			System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
			System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(memoryStream);

			int i = 0;
			foreach(string result in results)
			{
				if(i == (results.Count - 1))
				{
					streamWriter.Write(result);
				}
				else
				{
					streamWriter.WriteLine(result);
				}
				i++;
			}
			streamWriter.Flush();

			memoryStream.Position = 0;

			System.IO.StreamReader streamReader = new System.IO.StreamReader(memoryStream);
			return streamReader;
		}

		public static System.Collections.ArrayList Patch(System.Collections.IList target, DiffList diffList)
		{
			System.Collections.ArrayList results = new System.Collections.ArrayList();

			diffList = Differ.Compress(diffList);

			int length = target.Count;
			foreach(DiffItem item in diffList)
			{
				switch(item.Status)
				{
					case DiffStatus.Deleted :
					{
						length--;
						break;
					}
					case DiffStatus.Inserted :
					{
						length++;
						break;
					}
				}
			}

			int diffIndex = 0;
			DiffItem diffItem = Next(diffList, diffIndex);

			int targetLineNumber = 0;
			int line = 0;

			for(int i = 0; i < length; i++)
			{
				if(diffItem != null && diffItem.Position == targetLineNumber && diffItem.Status == DiffStatus.Deleted)
				{
					//Skip a target line.
					targetLineNumber++;

					diffIndex++;
					diffItem = Next(diffList, diffIndex);
				}


				if(diffItem != null && diffItem.Position == line && diffItem.Status == DiffStatus.Inserted)
				{
					results.Add(diffItem.Value);
					line++;

					diffIndex++;
					diffItem = Next(diffList, diffIndex);

					continue;
				}


				results.Add(target[targetLineNumber]);
				targetLineNumber++;
				line++;
			}
	
			return results;
		}

		private static DiffItem Next(DiffList diffList, int diffIndex)
		{
			if(diffIndex < diffList.Count)
			{
				return diffList[diffIndex];
			}
			return null;
		}

	}
}
