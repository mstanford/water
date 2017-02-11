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
	/// Summary description for OrderByNode.
	/// </summary>
	public class OrderByNode : Node
	{
		private Bamboo.Query.Planning.Node _node;
		private System.Collections.IList _expressions;
		private int[] _indexes;
		private bool[] _ascendingOrder;

		public OrderByNode(Bamboo.Query.Planning.Node node, Bamboo.Query.Query.OrderByClause orderBy)
		{
			this._node = node;
			this._expressions = orderBy.Expressions;

			System.Collections.ArrayList columns = new System.Collections.ArrayList(this._expressions.Count);
			this._ascendingOrder = new bool[this._expressions.Count];
			for (int i = 0; i < this._expressions.Count; i++)
			{
				Bamboo.Query.Query.OrderByExpression orderByExpression = (Bamboo.Query.Query.OrderByExpression)this._expressions[i];
				this._ascendingOrder[i] = orderByExpression.Ascending;
				columns.Add(orderByExpression.Column);
			}

			this._indexes = GetIndexes(columns, this._node.ColumnCoordinates);
		}

		public override string Name
		{
			get { return "ORDER"; }
		}

		public override System.Collections.IEnumerable ColumnCoordinates
		{
			get { return this._node.ColumnCoordinates; }
		}

		public Bamboo.Query.Planning.Node Node
		{
			get { return this._node; }
		}

		public System.Collections.IList Expressions
		{
			get { return this._expressions; }
		}

		public int[] Indexes
		{
			get { return this._indexes; }
		}

		public bool[] AscendingOrder
		{
			get { return this._ascendingOrder; }
		}

		public override Bamboo.Query.Iterators.Iterator CreateIterator()
		{
			return new Bamboo.Query.Iterators.OrderIterator(this._node.CreateIterator(), new Bamboo.Query.RowComparer(this._indexes, this._ascendingOrder));
		}

	}
}
