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
	/// Summary description for SelectNode.
	/// </summary>
	public class SelectNode : Node
	{
		private Bamboo.Query.Planning.Node _node;
		private int[] _indexes;
		private System.Collections.IEnumerable _columnCoordinates;

		public SelectNode(Bamboo.Query.Planning.Node node, System.Collections.IList select)
		{
			this._node = node;
			this._indexes = GetIndexes(select, this._node.ColumnCoordinates);

			ColumnCoordinate[] columnCoordinates = new ColumnCoordinate[select.Count];
			for (int i = 0; i < columnCoordinates.Length; i++)
			{
				Bamboo.Query.Query.Column column = (Bamboo.Query.Query.Column)select[i];
				columnCoordinates[i] = new ColumnCoordinate(column.Identifier, i);
			}
			this._columnCoordinates = columnCoordinates;
		}

		public override string Name
		{
			get { return "SELECT"; }
		}

		public override System.Collections.IEnumerable ColumnCoordinates
		{
			get { return this._columnCoordinates; }
		}

		public Bamboo.Query.Planning.Node Node
		{
			get { return this._node; }
		}

		public int[] Indexes
		{
			get { return this._indexes; }
		}

		public override Bamboo.Query.Iterators.Iterator CreateIterator()
		{
			return new Bamboo.Query.Iterators.SelectIterator(this.Node.CreateIterator(), this._indexes);
		}

	}
}
