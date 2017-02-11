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
	public class QueryParser
	{

		public static object Parse(string query)
		{
			System.IO.StringReader reader = new System.IO.StringReader(query);
			object result = Parse(reader);
			reader.Close();
			return result;
		}

		public static object Parse(System.IO.TextReader reader)
		{
			Bamboo.Query.Query.Tokenizer tokenizer = new Bamboo.Query.Query.Tokenizer(reader);

			SelectStatement selectStatement = new SelectStatement();
			UnionStatement unionStatement = null;

			System.Collections.ArrayList selectTokens = new System.Collections.ArrayList();
			System.Collections.ArrayList fromTokens = new System.Collections.ArrayList();
			System.Collections.ArrayList joinsTokens = new System.Collections.ArrayList();
			System.Collections.ArrayList whereTokens = new System.Collections.ArrayList();
			System.Collections.ArrayList groupByTokens = new System.Collections.ArrayList();
			System.Collections.ArrayList orderByTokens = new System.Collections.ArrayList();
			System.Collections.ArrayList currentTokens = null;

			string token;
			while ((token = tokenizer.Next()) != null)
			{
				switch (token.ToUpper())
				{
					case "SELECT":
						{
							currentTokens = selectTokens;
							break;
						}
					case "FROM":
						{
							currentTokens = fromTokens;
							break;
						}
					case "JOIN":
						{
							System.Collections.ArrayList joinTokens = new System.Collections.ArrayList();
							joinsTokens.Add(joinTokens);
							currentTokens = joinTokens;
							break;
						}
					case "WHERE":
						{
							currentTokens = whereTokens;
							break;
						}
					case "GROUP":
						{
							tokenizer.Next(); // BY

							currentTokens = groupByTokens;
							break;
						}
					case "ORDER":
						{
							tokenizer.Next(); // BY

							currentTokens = orderByTokens;
							break;
						}
					case "UNION":
						{
							//DUP
							selectStatement.From = GetFrom(fromTokens);
							selectStatement.Select = GetSelect(selectTokens, selectStatement.From);
							selectStatement.Joins = GetJoins(joinsTokens);
							selectStatement.Where = GetWhere(whereTokens, selectStatement.From);
							selectStatement.GroupBy = GetGroupBy(groupByTokens, selectStatement.From);
							selectStatement.OrderBy = GetOrderBy(orderByTokens, selectStatement.From);

							if (unionStatement != null)
							{
								UnionStatement oldUnionStatement = unionStatement;
								unionStatement = new UnionStatement();
								unionStatement.A = oldUnionStatement;
								selectStatement = new SelectStatement();
								unionStatement.B = selectStatement;
							}
							else
							{
								unionStatement = new UnionStatement();
								unionStatement.A = selectStatement;
								selectStatement = new SelectStatement();
								unionStatement.B = selectStatement;
							}

							selectTokens = new System.Collections.ArrayList();
							fromTokens = new System.Collections.ArrayList();
							joinsTokens = new System.Collections.ArrayList();
							whereTokens = new System.Collections.ArrayList();
							orderByTokens = new System.Collections.ArrayList();
							currentTokens = null;
							break;
						}
					default:
						{
							currentTokens.Add(token);
							break;
						}
				}
			}

			//DUP
			selectStatement.From = GetFrom(fromTokens);
			selectStatement.Select = GetSelect(selectTokens, selectStatement.From);
			selectStatement.Joins = GetJoins(joinsTokens);
			selectStatement.Where = GetWhere(whereTokens, selectStatement.From);
			selectStatement.GroupBy = GetGroupBy(groupByTokens, selectStatement.From);
			selectStatement.OrderBy = GetOrderBy(orderByTokens, selectStatement.From);

			if (unionStatement != null)
			{
				return unionStatement;
			}
			else
			{
				//TODO check columns and add table name here.
				return selectStatement;
			}
		}

		//TODO
		private static Bamboo.Query.Query.Column CreateColumn(string identifier, string from)
		{
			return CreateColumn(identifier, String.Empty, from);
		}

		//TODO
		private static Bamboo.Query.Query.Column CreateColumn(string identifier, string alias, string from)
		{
			if (identifier.IndexOf(".") == -1)
			{
				//TODO do a check.
				//if (joins.Count > 0)
				//{
				//    throw new System.Exception("Invalid column name: " + identifier);
				//}

				if (identifier.IndexOf("(") == -1)
				{
					identifier = from + "." + identifier;
				}
				else
				{
					string function = identifier.Substring(0, identifier.IndexOf("("));
					int start = identifier.IndexOf("(") + 1;
					string columnIdentifier = identifier.Substring(start, identifier.Length - (start + 1));

					identifier = function + "(" + from + "." + columnIdentifier + ")";
				}
			}
			Bamboo.Query.Query.Column column = new Bamboo.Query.Query.Column(identifier, alias);
			return column;
		}

		//TODO
		private static Bamboo.Query.Query.Column CreateJoinColumn(string identifier)
		{
			return new Bamboo.Query.Query.Column(identifier);
		}

		private static System.Collections.IList GetSelect(System.Collections.ArrayList selectTokens, string from)
		{
			System.Collections.ArrayList select = new System.Collections.ArrayList();

			System.Collections.ArrayList tokenBlocks = GetSelectTokenBlocks(selectTokens);
			foreach (System.Collections.ArrayList tokens in tokenBlocks)
			{
				System.Text.StringBuilder Identifier = new System.Text.StringBuilder();
				System.Text.StringBuilder Alias = new System.Text.StringBuilder();
				System.Text.StringBuilder current = Identifier;

				for (int i = 0; i < tokens.Count; i++)
				{
					string token = (string)tokens[i];

					if (token.ToUpper().Equals("AS"))
					{
						current = Alias;
					}
					else
					{
						current.Append(token);
					}
				}

				if (Alias.Length == 0)
				{
					select.Add(CreateColumn(Identifier.ToString(), from));
				}
				else
				{
					select.Add(CreateColumn(Identifier.ToString(), Alias.ToString(), from));
				}
			}

			return select;
		}

		private static System.Collections.ArrayList GetSelectTokenBlocks(System.Collections.ArrayList selectTokens)
		{
			System.Collections.ArrayList tokenBlocks = new System.Collections.ArrayList();

			System.Collections.ArrayList tokenBlock = new System.Collections.ArrayList();
			tokenBlocks.Add(tokenBlock);
			foreach (string token in selectTokens)
			{
				if (token.Equals(","))
				{
					tokenBlock = new System.Collections.ArrayList();
					tokenBlocks.Add(tokenBlock);
				}
				else
				{
					tokenBlock.Add(token);
				}
			}

			return tokenBlocks;
		}

		private static string GetFrom(System.Collections.ArrayList tokens)
		{
			if (tokens.Count == 1)
			{
				return (string)tokens[0];
			}
			else
			{
				throw new System.Exception("Invalid query.");
			}
		}

		private static System.Collections.IList GetJoins(System.Collections.ArrayList joinsTokens)
		{
			System.Collections.ArrayList joins = new System.Collections.ArrayList();
			foreach (System.Collections.ArrayList joinTokens in joinsTokens)
			{
				joins.Add(GetJoin(joinTokens));
			}
			return joins;
		}

		private static Bamboo.Query.Query.JoinClause GetJoin(System.Collections.ArrayList tokens)
		{
			System.Text.StringBuilder Table = new System.Text.StringBuilder();
			System.Text.StringBuilder OnColumn = new System.Text.StringBuilder();
			System.Text.StringBuilder EqualsColumn = new System.Text.StringBuilder();
			System.Text.StringBuilder current = Table;

			for (int i = 0; i < tokens.Count; i++)
			{
				string token = (string)tokens[i];

				if (token.ToUpper().Equals("ON"))
				{
					current = OnColumn;
				}
				else if (token.ToUpper().Equals("="))
				{
					current = EqualsColumn;
				}
				else
				{
					current.Append(token);
				}
			}



			string tableName = Table.ToString();
			//ON
			Bamboo.Query.Query.Column onColumn = CreateJoinColumn(OnColumn.ToString());
			//=
			Bamboo.Query.Query.Column equalsColumn = CreateJoinColumn(EqualsColumn.ToString());

			if (tableName.Equals(GetTableName(onColumn)))
			{
				return new Bamboo.Query.Query.JoinClause(equalsColumn, onColumn);
			}
			else if (tableName.Equals(GetTableName(equalsColumn)))
			{
				return new Bamboo.Query.Query.JoinClause(onColumn, equalsColumn);
			}
			else
			{
				throw new System.Exception("Invalid join.");
			}
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

		private static Bamboo.Query.Query.WhereClause GetWhere(System.Collections.ArrayList tokens, string from)
		{
			object predicate = null;

			Bamboo.Query.Query.TokenReader tokenReader = new Bamboo.Query.Query.TokenReader(tokens);
			string token;
			while ((token = tokenReader.Peek()) != null)
			{
				switch (token)
				{
					case "AND":
						{
							tokenReader.Next();

							if (predicate == null)
							{
								throw new System.Exception("Invalid query.");
							}

							predicate = new Bamboo.Query.Query.AndPredicate(predicate, ParseWherePredicate(tokenReader, from));
							break;
						}
					case "OR":
						{
							tokenReader.Next();

							if (predicate == null)
							{
								throw new System.Exception("Invalid query.");
							}

							predicate = new Bamboo.Query.Query.OrPredicate(predicate, ParseWherePredicate(tokenReader, from));
							break;
						}
					default:
						{
							if (predicate != null)
							{
								throw new System.Exception("Invalid query.");
							}

							predicate = ParseWherePredicate(tokenReader, from);
							break;
						}
				}
			}

			if (predicate == null)
			{
				return null;
			}

			return new Bamboo.Query.Query.WhereClause(predicate);
		}

		private static object ParseWherePredicate(Bamboo.Query.Query.TokenReader tokenReader, string from)
		{
			if (tokenReader.Peek().Equals("("))
			{
				tokenReader.Next();
				return ParseCompoundPredicate(tokenReader, from);
			}

			System.Collections.ArrayList tokens = new System.Collections.ArrayList();

//TODO this needs work.
			string token;
			while ((token = tokenReader.Peek()) != null)
			{
				if (token.Equals("AND"))
				{
					break;
				}
				else if (token.Equals("OR"))
				{
					break;
				}
				else
				{
					tokenReader.Next();

					tokens.Add(token);
				}
			}

			return ParseWherePredicate(tokens, from);
		}

		private static object ParseWherePredicate(System.Collections.ArrayList tokens, string from)
		{
			System.Text.StringBuilder Column = new System.Text.StringBuilder();
			System.Text.StringBuilder Operator = new System.Text.StringBuilder();
			System.Text.StringBuilder Value = new System.Text.StringBuilder();
			System.Text.StringBuilder current = Column;

			for (int i = 0; i < tokens.Count; i++)
			{
				string token = (string)tokens[i];

				if (token.Equals("="))
				{
					Operator.Append(token);

					current = Value;
				}
				else if (token.Equals(">"))
				{
					Operator.Append(token);

					current = Value;
				}
				else if (token.Equals("<"))
				{
					Operator.Append(token);

					current = Value;
				}
				else if (token.Equals("<>"))
				{
					Operator.Append(token);

					current = Value;
				}
				else if (token.Equals("!="))
				{
					Operator.Append(token);

					current = Value;
				}
				else
				{
					current.Append(token);
				}
			}



			object predicate = null;

			string column = Column.ToString();
			string operator_ = Operator.ToString();
			System.IComparable value = ParseAtom(Value.ToString());

			if (operator_.Equals("="))
			{
				predicate = new Bamboo.Query.Query.EqualsPredicate(CreateColumn(column, from), value);
			}
			else if (operator_.Equals(">"))
			{
				predicate = new Bamboo.Query.Query.GreaterThanPredicate(CreateColumn(column, from), value);
			}
			else if (operator_.Equals("<"))
			{
				predicate = new Bamboo.Query.Query.LessThanPredicate(CreateColumn(column, from), value);
			}
			else if (operator_.Equals("<>"))
			{
				predicate = new Bamboo.Query.Query.NotEqualsPredicate(CreateColumn(column, from), value);
			}
			else if (operator_.Equals("!="))
			{
				predicate = new Bamboo.Query.Query.NotEqualsPredicate(CreateColumn(column, from), value);
			}
			else
			{
				throw new System.Exception("Invalid operator: " + operator_);
			}

			return predicate;
		}

		private static object ParseCompoundPredicate(Bamboo.Query.Query.TokenReader tokenReader, string from)
		{
			object predicate = null;

			string token;
			while ((token = tokenReader.Peek()) != null)
			{
				switch (token)
				{
					case ")":
						{
							tokenReader.Next();

							return predicate;
						}
					case "AND":
						{
							tokenReader.Next();

							if (predicate == null)
							{
								throw new System.Exception("Invalid query.");
							}

							predicate = new Bamboo.Query.Query.AndPredicate(predicate, ParseWherePredicate(tokenReader, from));
							break;
						}
					case "OR":
						{
							tokenReader.Next();

							if (predicate == null)
							{
								throw new System.Exception("Invalid query.");
							}

							predicate = new Bamboo.Query.Query.OrPredicate(predicate, ParseWherePredicate(tokenReader, from));
							break;
						}
					default:
						{
							if (predicate != null)
							{
								throw new System.Exception("Invalid query.");
							}

							predicate = ParseWherePredicate(tokenReader, from);
							break;
						}
				}
			}

			if (predicate != null)
			{
				return predicate;
			}

			throw new System.Exception("Invalid query.");
		}

		private static System.Collections.IList GetGroupBy(System.Collections.ArrayList tokens, string from)
		{
			System.Collections.ArrayList groupBy = new System.Collections.ArrayList();

			foreach (string token in tokens)
			{
				if (!token.Equals(","))
				{
					Bamboo.Query.Query.Column column = CreateColumn(token, from);
					groupBy.Add(column);
				}
			}

			return groupBy;
		}

		private static Bamboo.Query.Query.OrderByClause GetOrderBy(System.Collections.ArrayList tokens, string from)
		{
			System.Collections.ArrayList orderByExpressions = new System.Collections.ArrayList(tokens.Count);

			foreach (string token in tokens)
			{
				if (token.Equals("DESC"))
				{
					int index = orderByExpressions.Count - 1;
					Bamboo.Query.Query.OrderByExpression orderByExpression = (Bamboo.Query.Query.OrderByExpression)orderByExpressions[index];
					orderByExpressions.RemoveAt(index);
					orderByExpressions.Add(new Bamboo.Query.Query.OrderByExpression(orderByExpression.Column, false));
				}
				else if (token.Equals(","))
				{
				}
				else
				{
					orderByExpressions.Add(new Bamboo.Query.Query.OrderByExpression(CreateColumn(token, from)));
				}
			}

			if (orderByExpressions.Count == 0)
			{
				return null;
			}
			else
			{
				return new Bamboo.Query.Query.OrderByClause(orderByExpressions);
			}
		}

		private static System.IComparable ParseAtom(string s)
		{
			if (s.Length == 0)
			{
				return null;
			}

			if (s.ToLower() == "true")
			{
				return true;
			}
			else if (s.ToLower() == "false")
			{
				return false;
			}

			try
			{
				int n = Int32.Parse(s);
				return n;
			}
			catch (System.Exception exception)
			{
				string thisIsAHackToIgnoreCompilerWarning = exception.Message;
			}

			try
			{
				double d = System.Double.Parse(s);
				return d;
			}
			catch (System.Exception exception)
			{
				string thisIsAHackToIgnoreCompilerWarning = exception.Message;
			}

			try
			{
				System.DateTime dt = System.DateTime.Parse(s);
				return dt;
			}
			catch (System.Exception exception)
			{
				string thisIsAHackToIgnoreCompilerWarning = exception.Message;
			}

			return s;
		}

		private QueryParser()
		{
		}

	}
}
