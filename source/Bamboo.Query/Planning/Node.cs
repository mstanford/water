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

namespace Bamboo.Query.Planning
{
	/// <summary>
	/// Summary description for Node.
	/// </summary>
	public abstract class Node
	{

		public Node()
		{
		}

		public abstract string Name { get; }

		public abstract System.Collections.IEnumerable ColumnCoordinates { get; }

		public abstract Bamboo.Query.Iterators.Iterator CreateIterator();

		protected int[] GetIndexes(System.Collections.IList columns, System.Collections.IEnumerable columnCoordinates)
		{
			int[] indexes = new int[columns.Count];
			for (int i = 0; i < indexes.Length; i++)
			{
				indexes[i] = GetIndex((Bamboo.Query.Query.Column)columns[i], columnCoordinates);
			}
			return indexes;
		}

		protected int GetIndex(Bamboo.Query.Query.Column column, System.Collections.IEnumerable columnCoordinates)
		{
			//TODO DELETE
			//if (column is Bamboo.Query.Query.FunctionColumn)
			//{
			//    Bamboo.Query.Query.FunctionColumn functionColumn = (Bamboo.Query.Query.FunctionColumn)column;

			//    string name = functionColumn.Function + "(" + functionColumn.Identifier + ")";
			//    foreach (ColumnCoordinate columnCoordinate in columnCoordinates)
			//    {
			//        if (columnCoordinate.Name.Equals(name))
			//        {
			//            return columnCoordinate.Index;
			//        }
			//    }
			//}
			//else
			//{
			foreach (ColumnCoordinate columnCoordinate in columnCoordinates)
			{
				if (columnCoordinate.Name.Equals(column.Identifier))
				{
					return columnCoordinate.Index;
				}
			}
			//}
			throw new System.Exception("Column does not exist: " + column.Identifier);
		}

	}
}
