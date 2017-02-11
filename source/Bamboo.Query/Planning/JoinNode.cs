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
	/// Summary description for JoinNode.
	/// </summary>
	public class JoinNode : Node
	{
		private Bamboo.Query.Planning.Node _leftNode;
		private Bamboo.Query.Planning.Node _rightNode;
		private int _leftRowIndex;
		private int _rightRowIndex;
		private System.Collections.IEnumerable _columnCoordinates;

		public JoinNode(Bamboo.Query.Planning.Node leftNode, Bamboo.Query.Planning.Node rightNode, Bamboo.Query.Query.Column leftColumn, Bamboo.Query.Query.Column rightColumn)
		{
			this._leftNode = leftNode;
			this._rightNode = rightNode;
			this._leftRowIndex = GetIndex(leftColumn, this._leftNode.ColumnCoordinates);
			this._rightRowIndex = GetIndex(rightColumn, this._rightNode.ColumnCoordinates);

			System.Collections.IEnumerable leftNodeColumnCoordinates = this._leftNode.ColumnCoordinates;
			System.Collections.IEnumerable rightNodeColumnCoordinates = this._rightNode.ColumnCoordinates;

			int length = 0;
			foreach (ColumnCoordinate columnCoordinate in leftNodeColumnCoordinates)
			{
				object ignore = columnCoordinate;
				length++;
			}
			foreach (ColumnCoordinate columnCoordinate in rightNodeColumnCoordinates)
			{
				object ignore = columnCoordinate;
				length++;
			}

			ColumnCoordinate[] columnCoordinates = new ColumnCoordinate[length];
			int i = 0;
			foreach (ColumnCoordinate columnCoordinate in leftNodeColumnCoordinates)
			{
				columnCoordinates[i] = new ColumnCoordinate(columnCoordinate.Name, i);
				i++;
			}
			foreach (ColumnCoordinate columnCoordinate in rightNodeColumnCoordinates)
			{
				columnCoordinates[i] = new ColumnCoordinate(columnCoordinate.Name, i);
				i++;
			}
			this._columnCoordinates = columnCoordinates;
		}

		public override string Name
		{
			get { return "JOIN"; }
		}

		public override System.Collections.IEnumerable ColumnCoordinates
		{
			get { return this._columnCoordinates; }
		}

		public Bamboo.Query.Planning.Node LeftNode
		{
			get { return this._leftNode; }
		}

		public Bamboo.Query.Planning.Node RightNode
		{
			get { return this._rightNode; }
		}

		public int LeftRowIndex
		{
			get { return this._leftRowIndex; }
		}

		public int RightRowIndex
		{
			get { return this._rightRowIndex; }
		}

		public override Bamboo.Query.Iterators.Iterator CreateIterator()
		{
			return new Bamboo.Query.Iterators.HashJoinIterator(this.LeftNode.CreateIterator(), this.RightNode.CreateIterator(), this._leftRowIndex, this._rightRowIndex);
		}

	}
}
