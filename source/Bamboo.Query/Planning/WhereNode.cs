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
	/// Summary description for WhereNode.
	/// </summary>
	public class WhereNode : Node
	{
		private Bamboo.Query.Planning.Node _node;
		private object _predicate;

		public WhereNode(Bamboo.Query.Planning.Node node, Bamboo.Query.Query.WhereClause whereClause)
		{
			this._node = node;
			this._predicate = whereClause.Predicate;
		}

		public override string Name
		{
			get { return "WHERE"; }
		}

		public override System.Collections.IEnumerable ColumnCoordinates
		{
			get { return this._node.ColumnCoordinates; }
		}

		public Bamboo.Query.Planning.Node Node
		{
			get { return this._node; }
		}

		public object Predicate
		{
			get { return this._predicate; }
		}

		public override Bamboo.Query.Iterators.Iterator CreateIterator()
		{
			return new Bamboo.Query.Iterators.WhereIterator(this._node.CreateIterator(), CreatePredicate(this._predicate));
		}

		private Bamboo.Query.Predicates.Predicate CreatePredicate(object predicate)
		{
			if (predicate is Bamboo.Query.Query.AndPredicate)
			{
				Bamboo.Query.Query.AndPredicate andPredicate = (Bamboo.Query.Query.AndPredicate)predicate;

				return new Bamboo.Query.Predicates.AndPredicate(CreatePredicate(andPredicate.A), CreatePredicate(andPredicate.B));
			}
			else if (predicate is Bamboo.Query.Query.EqualsPredicate)
			{
				Bamboo.Query.Query.EqualsPredicate equalsPredicate = (Bamboo.Query.Query.EqualsPredicate)predicate;

				return new Bamboo.Query.Predicates.EqualsPredicate(GetIndex(equalsPredicate.Column, this._node.ColumnCoordinates), equalsPredicate.Value);
			}
			else if (predicate is Bamboo.Query.Query.GreaterThanPredicate)
			{
				Bamboo.Query.Query.GreaterThanPredicate greaterThanPredicate = (Bamboo.Query.Query.GreaterThanPredicate)predicate;

				return new Bamboo.Query.Predicates.GreaterThanPredicate(GetIndex(greaterThanPredicate.Column, this._node.ColumnCoordinates), greaterThanPredicate.Value);
			}
			else if (predicate is Bamboo.Query.Query.LessThanPredicate)
			{
				Bamboo.Query.Query.LessThanPredicate lessThanPredicate = (Bamboo.Query.Query.LessThanPredicate)predicate;

				return new Bamboo.Query.Predicates.LessThanPredicate(GetIndex(lessThanPredicate.Column, this._node.ColumnCoordinates), lessThanPredicate.Value);
			}
			else if (predicate is Bamboo.Query.Query.NotEqualsPredicate)
			{
				Bamboo.Query.Query.NotEqualsPredicate notEqualsPredicate = (Bamboo.Query.Query.NotEqualsPredicate)predicate;

				return new Bamboo.Query.Predicates.NotEqualsPredicate(GetIndex(notEqualsPredicate.Column, this._node.ColumnCoordinates), notEqualsPredicate.Value);
			}
			else if (predicate is Bamboo.Query.Query.OrPredicate)
			{
				Bamboo.Query.Query.OrPredicate orPredicate = (Bamboo.Query.Query.OrPredicate)predicate;

				return new Bamboo.Query.Predicates.OrPredicate(CreatePredicate(orPredicate.A), CreatePredicate(orPredicate.B));
			}
			else
			{
				throw new System.Exception("Invalid filter expression: " + predicate.GetType().FullName);
			}
		}

	}
}
