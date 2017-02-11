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
	/// Summary description for QueryPlan.
	/// </summary>
	public class Planner
	{

		public static Bamboo.Query.Planning.Node Plan(object query, Bamboo.Query.Database database)
		{
			return NaivePlan(query, database);
		}

		// This is without query optimization.
		private static Bamboo.Query.Planning.Node NaivePlan(object query, Bamboo.Query.Database database)
		{
			if (query is Bamboo.Query.Query.UnionStatement)
			{
				Bamboo.Query.Query.UnionStatement unionStatement = (Bamboo.Query.Query.UnionStatement)query;

				return new UnionNode(NaivePlan(unionStatement.A, database), NaivePlan(unionStatement.B, database));
			}
			else if(query is Bamboo.Query.Query.SelectStatement)
			{
				Bamboo.Query.Query.SelectStatement selectStatement = (Bamboo.Query.Query.SelectStatement)query;

				System.Collections.Hashtable tables = database.Lookup(GetNames(selectStatement));

				// FROM
				string name = selectStatement.From;
				Bamboo.Query.Planning.Node node = new Bamboo.Query.Planning.TableScanNode(name, (Bamboo.Query.Table)tables[name]);

				// JOIN
				foreach(Bamboo.Query.Query.JoinClause joinClause in selectStatement.Joins)
				{
					name = GetTableName(joinClause.RightColumn);
					node = new Bamboo.Query.Planning.JoinNode(node, new Bamboo.Query.Planning.TableScanNode(name, (Bamboo.Query.Table)tables[name]), joinClause.LeftColumn, joinClause.RightColumn);
				}

				// WHERE
				if(selectStatement.Where != null)
				{
					node = new Bamboo.Query.Planning.WhereNode(node, selectStatement.Where);
				}

				// GROUP BY
				if(selectStatement.GroupBy.Count > 0)
				{
					node = new Bamboo.Query.Planning.GroupByNode(node, selectStatement.GroupBy, selectStatement.Select);
				}

				// SELECT
				node = new Bamboo.Query.Planning.SelectNode(node, selectStatement.Select);

				// ORDER BY
				if (selectStatement.OrderBy != null)
				{
					node = new Bamboo.Query.Planning.OrderByNode(node, selectStatement.OrderBy);
				}

				return node;
			}
			else
			{
				throw new System.Exception("Invalid query.");
			}
		}

		private static string[] GetNames(Bamboo.Query.Query.SelectStatement selectStatement)
		{
			string[] names = new string[1 + selectStatement.Joins.Count];
			names[0] = selectStatement.From;
			System.Collections.IList joins = selectStatement.Joins;
			for (int i = 0; i < joins.Count; i++)
			{
				names[i + 1] = GetTableName(((Bamboo.Query.Query.JoinClause)joins[i]).RightColumn);
			}
			return names;
		}

		private static string GetTableName(Bamboo.Query.Query.Column column)
		{
			string identifier = column.Identifier;
			if (identifier.IndexOf(".") != -1)
			{
				return identifier.Substring(0, identifier.IndexOf("."));
			}
			else
			{
				return "";
			}
		}

		private Planner()
		{
		}

	}
}
