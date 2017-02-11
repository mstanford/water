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

namespace Bamboo.Query
{
	/// <summary>
	/// Summary description for Evaluator.
	/// </summary>
	public class Evaluator
	{

		public static Bamboo.Query.Table Evaluate(string query, Bamboo.Query.Database database)
		{
			return Evaluate(Bamboo.Query.Query.QueryParser.Parse(query), database);
		}

		public static Bamboo.Query.Table Evaluate(object query, Bamboo.Query.Database database)
		{
			Bamboo.Query.Table T = new Bamboo.Query.Table(GetColumns(query));
			Bamboo.Query.Planning.Node plan = Bamboo.Query.Planning.Planner.Plan(query, database);
			Bamboo.Query.Iterators.Iterator R = plan.CreateIterator();
			R.Open();
			System.Collections.IList r;
			while ((r = R.GetNext()) != null)
			{
				T.AddRow(r);
			}
			R.Close();
			return T;
		}

		private static System.Collections.ICollection GetColumns(object query)
		{
			if (query is Bamboo.Query.Query.UnionStatement)
			{
				Bamboo.Query.Query.UnionStatement unionStatement = (Bamboo.Query.Query.UnionStatement)query;

				return GetColumns(unionStatement.A);
			}
			else if(query is Bamboo.Query.Query.SelectStatement)
			{
				Bamboo.Query.Query.SelectStatement selectStatement = (Bamboo.Query.Query.SelectStatement)query;

				System.Collections.ArrayList columns = new System.Collections.ArrayList(selectStatement.Select.Count);
				foreach (Bamboo.Query.Query.Column column in selectStatement.Select)
				{
					columns.Add(GetColumnName(column));
				}
				return columns;
			}
			else
			{
				throw new System.Exception("Invalid query.");
			}
		}

		private static string GetColumnName(Bamboo.Query.Query.Column column)
		{
			if (column.Alias != null && column.Alias.Length > 0)
			{
				return column.Alias;
			}
			else if (column.Identifier.IndexOf("(") != -1)
			{
				string identifier = column.Identifier;
				string function = identifier.Substring(0, identifier.IndexOf("("));
				int start = identifier.IndexOf("(") + 1;
				string columnIdentifier = identifier.Substring(start, identifier.Length - (start + 1));

				if (columnIdentifier.IndexOf(".") != -1)
				{
					return columnIdentifier.Substring(columnIdentifier.LastIndexOf(".") + 1);
				}
				else
				{
					return columnIdentifier;
				}
			}
			else
			{
				if (column.Identifier.IndexOf(".") != -1)
				{
					return column.Identifier.Substring(column.Identifier.LastIndexOf(".") + 1);
				}
				else
				{
					return column.Identifier;
				}
			}
		}

		private Evaluator()
		{
		}

	}
}
