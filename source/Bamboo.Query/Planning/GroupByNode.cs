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
	/// Summary description for GroupByNode.
	/// </summary>
	public class GroupByNode : Node
	{
		private Bamboo.Query.Planning.Node _node;
		private System.Collections.IEnumerable _indexes;
		private System.Collections.IEnumerable _aggregators;
		private System.Collections.IEnumerable _columnCoordinates;

		public GroupByNode(Bamboo.Query.Planning.Node node, System.Collections.IList groupBy, System.Collections.IList select)
		{
			this._node = node;

			int[] indexes = GetIndexes(groupBy, this._node.ColumnCoordinates);
			Bamboo.Query.Aggregators.Aggregator[] aggregators = GetAggregators(select);

			this._indexes = indexes;
			this._aggregators = aggregators;

			ColumnCoordinate[] columnCoordinates = new ColumnCoordinate[groupBy.Count + aggregators.Length];
			int i;
			for (i = 0; i < groupBy.Count; i++)
			{
				Bamboo.Query.Query.Column column = (Bamboo.Query.Query.Column)groupBy[i];
				columnCoordinates[i] = new ColumnCoordinate(column.Identifier, i);
			}
			foreach (Bamboo.Query.Query.Column column in select)
			{
				if (column.Identifier.IndexOf("(") != -1)
				{
					string identifier = column.Identifier;
					string function = identifier.Substring(0, identifier.IndexOf("("));
					int start = identifier.IndexOf("(") + 1;
					string columnIdentifier = identifier.Substring(start, identifier.Length - (start + 1));
					columnCoordinates[i] = new ColumnCoordinate(function + "(" + columnIdentifier + ")", i);
					i++;
				}
			}
			this._columnCoordinates = columnCoordinates;
		}

		public override string Name
		{
			get { return "GROUP"; }
		}

		public override System.Collections.IEnumerable ColumnCoordinates
		{
			get { return this._columnCoordinates; }
		}

		public Bamboo.Query.Planning.Node Node
		{
			get { return this._node; }
		}

		public System.Collections.IEnumerable Indexes
		{
			get { return this._indexes; }
		}

		public System.Collections.IEnumerable Aggregators
		{
			get { return this._aggregators; }
		}

		public override Bamboo.Query.Iterators.Iterator CreateIterator()
		{
			return new Bamboo.Query.Iterators.HashGroupIterator(this._node.CreateIterator(), this._indexes, this._aggregators);
		}

		private Bamboo.Query.Aggregators.Aggregator[] GetAggregators(System.Collections.IList select)
		{
			// Measure
			int count = 0;
			foreach (Bamboo.Query.Query.Column column in select)
			{
				if (column.Identifier.IndexOf("(") != -1)
				{
					count++;
				}
			}

			// Create
			Bamboo.Query.Aggregators.Aggregator[] aggregators = new Bamboo.Query.Aggregators.Aggregator[count];
			int i = 0;
			foreach (Bamboo.Query.Query.Column column in select)
			{
				if (column.Identifier.IndexOf("(") != -1)
				{
					aggregators[i] = ToAggregator(column);
					i++;
				}
			}
			return aggregators;
		}

		private Bamboo.Query.Aggregators.Aggregator ToAggregator(Bamboo.Query.Query.Column column)
		{
			string identifier = column.Identifier;
			string aggregator = identifier.Substring(0, identifier.IndexOf("("));
			int start = identifier.IndexOf("(") + 1;
			string columnIdentifier = identifier.Substring(start, identifier.Length - (start + 1));
			switch (aggregator.ToUpper())
			{
				case "SUM":
					{
						return new Bamboo.Query.Aggregators.SumDoubleAggregator(GetIndex(new Bamboo.Query.Query.Column(columnIdentifier), this._node.ColumnCoordinates));
					}
				case "SUMINT":
					{
						return new Bamboo.Query.Aggregators.SumIntAggregator(GetIndex(new Bamboo.Query.Query.Column(columnIdentifier), this._node.ColumnCoordinates));
					}
				case "SUMDOUBLE":
					{
						return new Bamboo.Query.Aggregators.SumDoubleAggregator(GetIndex(new Bamboo.Query.Query.Column(columnIdentifier), this._node.ColumnCoordinates));
					}
				case "COUNT":
					{
						return new Bamboo.Query.Aggregators.CountAggregator(GetIndex(new Bamboo.Query.Query.Column(columnIdentifier), this._node.ColumnCoordinates));
					}
				case "INC":
					{
						return new Bamboo.Query.Aggregators.IncrementAggregator();
					}
				default:
					{
						throw new System.Exception("Unknown function: " + aggregator);
					}
			}
		}

	}
}
