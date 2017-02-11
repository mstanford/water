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

namespace Bamboo.Query.Query
{
	public class QueryGenerator
	{

		/// <summary>
		/// Generate the query and return a string.
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public static string Generate(SelectStatement query)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			GenerateSelect(query, stringBuilder);
			GenerateFrom(query, stringBuilder);
			GenerateWhere(query, stringBuilder);
			GenerateGroupBy(query, stringBuilder);
			GenerateOrderBy(query, stringBuilder);
			return stringBuilder.ToString();
		}

		private static void GenerateSelect(SelectStatement query, System.Text.StringBuilder stringBuilder)
		{
			if (query.Select.Count == 0)
			{
				return;
			}

			stringBuilder.Append("SELECT");
			stringBuilder.Append(" ");
			for (int i = 0; i < query.Select.Count; i++)
			{
				Bamboo.Query.Query.Column column = (Bamboo.Query.Query.Column)query.Select[i];

				if (i != 0)
				{
					stringBuilder.Append(", ");
				}

				// TODO Watch out for functions.
				Escape(stringBuilder, column.Identifier);
			}
		}

		private static void GenerateFrom(SelectStatement query, System.Text.StringBuilder stringBuilder)
		{
			if (query.From.Length == 0)
			{
				return;
			}

			stringBuilder.Append(" ");
			stringBuilder.Append("FROM");
			stringBuilder.Append(" ");
			Escape(stringBuilder, query.From);
		}

		private static void GenerateGroupBy(SelectStatement query, System.Text.StringBuilder stringBuilder)
		{
			if (query.GroupBy.Count == 0)
			{
				return;
			}

			stringBuilder.Append(" ");
			stringBuilder.Append("GROUP BY");

			stringBuilder.Append(" ");
			for (int i = 0; i < query.GroupBy.Count; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				Escape(stringBuilder, ((Bamboo.Query.Query.Column)query.GroupBy[i]).Identifier);
			}
		}

		private static void GenerateWhere(SelectStatement query, System.Text.StringBuilder stringBuilder)
		{
			if (query.Where == null)
			{
				return;
			}

			stringBuilder.Append(" ");
			stringBuilder.Append("WHERE");
			stringBuilder.Append(" ");

			GenerateWhere(query.Where, stringBuilder);
		}

		public static void GenerateWhere(Bamboo.Query.Query.WhereClause whereClause, System.Text.StringBuilder stringBuilder)
		{
			bool first = true;
			GenerateWherePredicate(whereClause.Predicate, stringBuilder, ref first);
		}

		private static void GenerateWherePredicate(object predicate, System.Text.StringBuilder stringBuilder, ref bool first)
		{
			if (predicate is Bamboo.Query.Query.EqualsPredicate)
			{
				Bamboo.Query.Query.EqualsPredicate equalsPredicate = (Bamboo.Query.Query.EqualsPredicate)predicate;

				if (first)
				{
					first = false;
				}
				else
				{
					stringBuilder.Append(", ");
				}

				Escape(stringBuilder, equalsPredicate.Column.Identifier);
				stringBuilder.Append(" ");
				stringBuilder.Append("=");
				stringBuilder.Append(" ");
				stringBuilder.Append("'");
				stringBuilder.Append(equalsPredicate.Value);
				stringBuilder.Append("'");
			}
			else if (predicate is Bamboo.Query.Query.OrPredicate)
			{
				Bamboo.Query.Query.OrPredicate orPredicate = (Bamboo.Query.Query.OrPredicate)predicate;

				GenerateWherePredicate(orPredicate.A, stringBuilder, ref first);
				GenerateWherePredicate(orPredicate.B, stringBuilder, ref first);
			}
			else if (predicate is Bamboo.Query.Query.AndPredicate)
			{
				Bamboo.Query.Query.AndPredicate andPredicate = (Bamboo.Query.Query.AndPredicate)predicate;

				GenerateWherePredicate(andPredicate.A, stringBuilder, ref first);
				GenerateWherePredicate(andPredicate.B, stringBuilder, ref first);
			}
			else
			{
				throw new System.Exception("Invalid query.");
			}
		}

		private static void GenerateOrderBy(SelectStatement query, System.Text.StringBuilder stringBuilder)
		{
			if (query.OrderBy == null)
			{
				return;
			}
			if (query.OrderBy.Expressions.Count == 0)
			{
				return;
			}

			stringBuilder.Append(" ");
			stringBuilder.Append("ORDER BY");
			stringBuilder.Append(" ");
			for (int i = 0; i < query.OrderBy.Expressions.Count; i++)
			{
				Bamboo.Query.Query.OrderByExpression orderByExpression = (Bamboo.Query.Query.OrderByExpression)query.OrderBy.Expressions[i];

				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				Escape(stringBuilder, orderByExpression.Column.Identifier);
				if (!orderByExpression.Ascending)
				{
					stringBuilder.Append(" DESC");
				}
			}
		}

		private static void Escape(System.Text.StringBuilder stringBuilder, string value)
		{
			if (value.IndexOf(" ") != -1)
			{
				stringBuilder.Append("[");
				stringBuilder.Append(value);
				stringBuilder.Append("]");
			}
			else
			{
				stringBuilder.Append(value);
			}
		}

		private QueryGenerator()
		{
		}

	}
}
