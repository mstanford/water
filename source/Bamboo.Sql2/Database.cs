// ------------------------------------------------------------------------------
// 
// Copyright (c) 2009 Swampware, Inc.
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
using System.Collections.Generic;
using System.Text;

namespace Bamboo.Sql2
{
	public abstract class Database
	{

		public Database()
		{
		}

		public abstract Bamboo.DataStructures.Table ReadTable(string name);

		public abstract Query ReadView(string name);

		public static Bamboo.DataStructures.Table Resolve(Database database, string name)
		{
			Bamboo.DataStructures.Table table = database.ReadTable(name);
			if (table == null)
			{
				Query view = database.ReadView(name);
				if (view != null)
				{
					table = Evaluate(database, view);
				}
			}
			return table;
		}

		public static Bamboo.DataStructures.Table Evaluate(Database database, object statement)
		{
			if (statement is SelectStatement)
			{
				Iterators.Iterator iterator = CreateIterator(database, (SelectStatement)statement);
				if (iterator == null)
				{
					return null;
				}
				return iterator.ToTable();
			}
			else if (statement is UnionStatement)
			{
				Iterators.Iterator iterator = CreateIterator(database, (UnionStatement)statement);
				if (iterator == null)
				{
					return null;
				}
				return iterator.ToTable();
			}
			else
			{
				throw new System.Exception("Invalid statement.");
			}
		}

		private static Iterators.Iterator CreateIterator(Database database, SelectStatement select)
		{
			// From
			if (select.From.Count > 1)
			{
				throw new System.Exception("Multiple tables in FROM not supported.");
			}
			string tableName = TableName(select.From[0]);
			Bamboo.DataStructures.Table table = Resolve(database, tableName);
			Iterators.Iterator iterator = new Iterators.TableIterator(table);

			// Join
			if (select.Joins != null)
			{
				foreach (object join in select.Joins)
				{
					if (join is InnerJoin)
					{
						iterator = new Iterators.InnerJoinIterator(iterator, (InnerJoin)join);
					}
					else if (join is LeftOuterJoin)
					{
						iterator = new Iterators.LeftOuterJoinIterator(iterator, (LeftOuterJoin)join);
					}
					else if (join is RightOuterJoin)
					{
						iterator = new Iterators.RightOuterJoinIterator(iterator, (RightOuterJoin)join);
					}
					else
					{
						throw new System.Exception("Invalid join: " + join.GetType().FullName);
					}
				}
			}

			// Where
			if (select.Where != null)
			{
				iterator = new Iterators.WhereIterator(iterator, CreatePredicate(select.Where, iterator));
			}

			// Group
			if (select.GroupBy != null)
			{
				iterator = new Iterators.GroupByIterator(iterator, GetOrdinals(select.GroupBy, iterator.Columns()));
			}

			// Order
			if (select.OrderBy != null)
			{
				iterator = new Iterators.OrderByIterator(iterator, select.OrderBy);
			}

			// Select
			List<Iterators.ColumnIterator> columnIterators = new List<Iterators.ColumnIterator>();
			foreach (object column in select.Columns)
			{
				if (column is Identifier && ((Identifier)column).Value.Equals("*"))
				{
					if (select.From.Count == 1)
					{
						for (int i = 0; i < table.Columns.Count; i++)
						{
							columnIterators.Add(new Iterators.ColumnIterator(GetOrdinal(table.Columns[i], iterator.Columns()), (string)table.Columns[i]));
						}
					}
					else
					{
						throw new System.Exception("Multiple tables in FROM not supported.");
					}
				}
				else
				{
					columnIterators.Add(CreateColumnIterator(column, iterator));
				}
			}
			iterator = new Iterators.SelectIterator(iterator, columnIterators);

			return iterator;
		}

		private static Iterators.ColumnIterator CreateColumnIterator(object column, Iterators.Iterator iterator)
		{
			if (column is Identifier)
			{
				return new Iterators.ColumnIterator(GetOrdinal(column, iterator.Columns()), ((Identifier)column).Value);
			}
			else if (column is Alias)
			{
				Alias alias = (Alias)column;
				return new Iterators.AliasIterator(CreateColumnIterator(alias.Name, iterator), alias.As.Value);
			}
			else if (column is Function)
			{
				Function function = (Function)column;
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				stringBuilder.Append(function.Name);
				stringBuilder.Append("(");
				for (int i = 0; i < function.Operands.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(function.Operands[i].ToString());
				}
				stringBuilder.Append(")");

				Functions.Function fn = Functions.Function.Create(function.Name.Value, cdr(function.Operands));

				return new Iterators.FunctionIterator(GetOrdinal(function.Operands[0], iterator.Columns()), stringBuilder.ToString(), fn);
			}
			else
			{
				throw new System.Exception();
			}
		}

		private static Iterators.Iterator CreateIterator(Database database, UnionStatement union)
		{
			return new Iterators.UnionIterator(CreateIterator(database, union.A), CreateIterator(database, union.B));
		}

		private static Iterators.Iterator CreateIterator(Database database, object query)
		{
			if (query is SelectStatement)
			{
				return CreateIterator(database, (SelectStatement)query);
			}
			else if (query is UnionStatement)
			{
				return CreateIterator(database, (UnionStatement)query);
			}
			else
			{
				throw new System.Exception("Invalid query.");
			}
		}

		private static Predicates.Predicate CreatePredicate(Predicate predicate, Iterators.Iterator iterator)
		{
			switch (predicate.Operator.ToUpper())
			{
				case "AND":
					{
						return new Predicates.AndPredicate(CreatePredicate((Predicate)predicate.A, iterator), CreatePredicate((Predicate)predicate.B, iterator));
					}
				case "=":
					{
						return new Predicates.EqualsPredicate(GetOrdinal(predicate.A, iterator.Columns()), predicate.B);
					}
				case "!=":
				case "<>":
					{
						return new Predicates.NotEqualsPredicate(GetOrdinal(predicate.A, iterator.Columns()), predicate.B);
					}
				case "NOT":
					{
						return new Predicates.NotPredicate(CreatePredicate((Predicate)predicate.A, iterator));
					}
				case "OR":
					{
						return new Predicates.OrPredicate(CreatePredicate((Predicate)predicate.A, iterator), CreatePredicate((Predicate)predicate.B, iterator));
					}
				default:
					{
						throw new System.Exception("Invalid predicate: " + predicate.Operator);
					}
			}
		}

		private static string TableName(object obj)
		{
			if (obj is Identifier)
			{
				string name = ((Identifier)obj).Value;
				if (name.StartsWith("[") && name.EndsWith("]"))
				{
					name = name.Substring(1, name.Length - 2);
				}
				return name;
			}
			else if (obj is Alias)
			{
				Alias alias = (Alias)obj;
				string name = ((Identifier)alias.Name).Value;
				if (name.StartsWith("[") && name.EndsWith("]"))
				{
					name = name.Substring(1, name.Length - 2);
				}
				return name;
			}
			else
			{
				throw new System.Exception("Invalid.");
			}
		}

		//TODO DELETE
		//private static string ColumnName(object obj)
		//{
		//    if (obj is Identifier)
		//    {
		//        return ((Identifier)obj).Value;
		//    }
		//    else if (obj is Alias)
		//    {
		//        Alias alias = (Alias)obj;
		//        return ((Identifier)alias.Name).Value;
		//    }
		//        //TODO DELETE
		//    //else if (obj is Function)
		//    //{
		//    //    Function function = (Function)obj;
		//    //    System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		//    //    stringBuilder.Append(function.Name);
		//    //    stringBuilder.Append("(");
		//    //    for (int i = 0; i < function.Operands.Length; i++)
		//    //    {
		//    //        if (i > 0)
		//    //        {
		//    //            stringBuilder.Append(", ");
		//    //        }
		//    //        stringBuilder.Append(function.Operands[i].ToString());
		//    //    }
		//    //    stringBuilder.Append(")");
		//    //    return stringBuilder.ToString();
		//    //}
		//    else
		//    {
		//        throw new System.Exception("Invalid.");
		//    }
		//}

		private static int[] GetOrdinals(List<Identifier> columns, List<string> underlyingColumns)
		{
			int[] ordinals = new int[columns.Count];
			for (int i = 0; i < ordinals.Length; i++)
			{
				ordinals[i] = GetOrdinal(columns[i], underlyingColumns);
			}
			return ordinals;
		}

		private static int[] GetOrdinals(List<object> columns, List<string> underlyingColumns)
		{
			int[] ordinals = new int[columns.Count];
			for (int i = 0; i < ordinals.Length; i++)
			{
				ordinals[i] = GetOrdinal(columns[i], underlyingColumns);
			}
			return ordinals;
		}

		private static int GetOrdinal(object column, List<string> underlyingColumns)
		{
			if (column is Identifier)
			{
				return GetOrdinal(((Identifier)column).Value, underlyingColumns);
			}
			else if (column is Alias)
			{
				return GetOrdinal(((Alias)column).Name, underlyingColumns);
			}
			else if (column is Function)
			{
				return GetOrdinal(((Function)column).Operands[0], underlyingColumns);
			}
			else if (column is string)
			{
				string columnName = (string)column;
				for (int i = 0; i < underlyingColumns.Count; i++)
				{
					if (columnName.Equals(underlyingColumns[i]))
					{
						return i;
					}
				}
				throw new System.Exception("Invalid.");
			}
			else
			{
				throw new System.Exception("Invalid.");
			}
		}

		private static List<object> cdr(List<object> l)
		{
			List<object> m = new List<object>();
			for (int i = 1; i < l.Count; i++)
			{
				m.Add(l[i]);
			}
			return m;
		}

	}
}
