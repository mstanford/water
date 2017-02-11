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
	public class SqlWriter
	{
		private System.IO.TextWriter _writer;

		public SqlWriter(System.IO.Stream stream)
		{
			this._writer = new System.IO.StreamWriter(stream);
		}

		public SqlWriter(System.IO.TextWriter writer)
		{
			this._writer = writer;
		}

		public void Write(object o)
		{
			Generate(o);
			this._writer.Write(";");
			this._writer.Flush();
		}

		private void Generate(object o)
		{
			if (o is SelectStatement)
			{
				SelectStatement selectStatement = (SelectStatement)o;
				this._writer.Write("SELECT");
				for (int i = 0; i < selectStatement.Columns.Count; i++)
				{
					if (i > 0)
					{
						this._writer.Write(",");
					}
					this._writer.Write(" ");
					Generate(selectStatement.Columns[i]);
				}
				this._writer.Write(" ");
				this._writer.Write("FROM");
				this._writer.Write(" ");
				bool first = true;
				foreach (object table in selectStatement.From)
				{
					if (first)
					{
						first = false;
					}
					else
					{
						this._writer.Write(", ");
					}
					if (table is Alias)
					{
						Alias alias = (Alias)table;
						Generate(alias.Name);
						this._writer.Write(" ");
						Generate(alias.As);
					}
					else
					{
						Generate(table);
					}
				}
				if (selectStatement.Joins != null)
				{
					for (int i = 0; i < selectStatement.Joins.Count; i++)
					{
						Generate(selectStatement.Joins[i]);
					}
				}
				if (selectStatement.Where != null)
				{
					this._writer.Write(" ");
					this._writer.Write("WHERE");
					this._writer.Write(" ");
					Generate(selectStatement.Where);
				}
				if (selectStatement.GroupBy != null)
				{
					this._writer.Write(" ");
					this._writer.Write("GROUP BY");
					for (int i = 0; i < selectStatement.GroupBy.Count; i++)
					{
						if (i > 0)
						{
							this._writer.Write(",");
						}
						this._writer.Write(" ");
						Generate(selectStatement.GroupBy[i]);
					}
				}
				if (selectStatement.OrderBy != null)
				{
					this._writer.Write(" ");
					this._writer.Write("ORDER BY");
					Generate(selectStatement.OrderBy);
				}
			}
			else if (o is UnionStatement)
			{
				UnionStatement unionStatement = (UnionStatement)o;
				Generate(unionStatement.A);
				this._writer.Write(" UNION ");
				Generate(unionStatement.B);
			}
			else if (o is DeleteStatement)
			{
				DeleteStatement deleteStatement = (DeleteStatement)o;
				this._writer.Write("DELETE FROM ");
				Generate(deleteStatement.Table);
				this._writer.Write(" WHERE ");
				Generate(deleteStatement.Where);
			}
			else if (o is InsertStatement)
			{
				InsertStatement insertStatement = (InsertStatement)o;
				this._writer.Write("INSERT INTO ");
				Generate(insertStatement.Table);
				this._writer.Write(" (");
				bool first = true;
				foreach (Identifier identifier in insertStatement.Columns)
				{
					if (first)
					{
						first = false;
					}
					else
					{
						this._writer.Write(", ");
					}
					Generate(identifier);
				}
				this._writer.Write(") VALUES (");
				first = true;
				foreach (object value in insertStatement.Values)
				{
					if (first)
					{
						first = false;
					}
					else
					{
						this._writer.Write(", ");
					}
					Generate(value);
				}
				this._writer.Write(")");
			}
			else if (o is ReplaceStatement)
			{
				ReplaceStatement replaceStatement = (ReplaceStatement)o;
				this._writer.Write("REPLACE INTO ");
				Generate(replaceStatement.Table);
				this._writer.Write(" (");
				bool first = true;
				foreach (Identifier identifier in replaceStatement.Columns)
				{
					if (first)
					{
						first = false;
					}
					else
					{
						this._writer.Write(", ");
					}
					Generate(identifier);
				}
				this._writer.Write(") VALUES (");
				first = true;
				foreach (object value in replaceStatement.Values)
				{
					if (first)
					{
						first = false;
					}
					else
					{
						this._writer.Write(", ");
					}
					Generate(value);
				}
				this._writer.Write(")");
			}
			else if (o is UpdateStatement)
			{
				UpdateStatement updateStatement = (UpdateStatement)o;

				this._writer.Write("UPDATE ");
				Generate(updateStatement.Table);
				this._writer.Write(" SET ");
				bool first = true;
				foreach (Predicate predicate in updateStatement.SetList)
				{
					if (first)
					{
						first = false;
					}
					else
					{
						this._writer.Write(", ");
					}
					Generate(predicate);
				}
				if (updateStatement.Where != null)
				{
					this._writer.Write(" WHERE ");
					Generate(updateStatement.Where);
				}
			}
			else if (o is InnerJoin)
			{
				InnerJoin innerJoin = (InnerJoin)o;
				this._writer.Write(" ");
				this._writer.Write("INNER JOIN");
				this._writer.Write(" ");
				if (innerJoin.Table is Alias)
				{
					Alias alias = (Alias)innerJoin.Table;
					Generate(alias.Name);
					this._writer.Write(" ");
					Generate(alias.As);
				}
				else
				{
					Generate(innerJoin.Table);
				}
				this._writer.Write(" ");
				this._writer.Write("ON");
				this._writer.Write(" ");
				Generate(innerJoin.A);
				this._writer.Write(" ");
				this._writer.Write(innerJoin.Operator);
				this._writer.Write(" ");
				Generate(innerJoin.B);
			}
			else if (o is LeftOuterJoin)
			{
				LeftOuterJoin leftOuterJoin = (LeftOuterJoin)o;
				this._writer.Write(" ");
				this._writer.Write("LEFT OUTER JOIN");
				this._writer.Write(" ");
				if (leftOuterJoin.Table is Alias)
				{
					Alias alias = (Alias)leftOuterJoin.Table;
					Generate(alias.Name);
					this._writer.Write(" ");
					Generate(alias.As);
				}
				else
				{
					Generate(leftOuterJoin.Table);
				}
				this._writer.Write(" ");
				this._writer.Write("ON");
				this._writer.Write(" ");
				Generate(leftOuterJoin.A);
				this._writer.Write(" ");
				this._writer.Write(leftOuterJoin.Operator);
				this._writer.Write(" ");
				Generate(leftOuterJoin.B);
			}
			else if (o is LeftOuterJoin)
			{
				RightOuterJoin rightOuterJoin = (RightOuterJoin)o;
				this._writer.Write(" ");
				this._writer.Write("RIGHT OUTER JOIN");
				this._writer.Write(" ");
				if (rightOuterJoin.Table is Alias)
				{
					Alias alias = (Alias)rightOuterJoin.Table;
					Generate(alias.Name);
					this._writer.Write(" ");
					Generate(alias.As);
				}
				else
				{
					Generate(rightOuterJoin.Table);
				}
				this._writer.Write(" ");
				this._writer.Write("ON");
				this._writer.Write(" ");
				Generate(rightOuterJoin.A);
				this._writer.Write(" ");
				this._writer.Write(rightOuterJoin.Operator);
				this._writer.Write(" ");
				Generate(rightOuterJoin.B);
			}
			else if (o is Predicate)
			{
				Predicate predicate = (Predicate)o;
				Generate(predicate.A);
				this._writer.Write(" ");
				this._writer.Write(predicate.Operator);
				this._writer.Write(" ");
				Generate(predicate.B);
			}
			else if (o is List<Sort>)
			{
				List<Sort> sorts = (List<Sort>)o;
				for (int i = 0; i < sorts.Count; i++)
				{
					if (i > 0)
					{
						this._writer.Write(",");
					}
					this._writer.Write(" ");
					Generate(sorts[i].Column);
					if (!sorts[i].Asc)
					{
						this._writer.Write(" DESC");
					}
				}
			}
			else if (o is Identifier)
			{
				Identifier identifier = (Identifier)o;
				this._writer.Write(identifier.Value);
			}
			else if (o is Function)
			{
				Function function = (Function)o;
				Generate(function.Name);
				this._writer.Write("(");
				for (int i = 0; i < function.Operands.Count; i++)
				{
					if (i > 0)
					{
						this._writer.Write(",");
					}
					this._writer.Write(" ");
					Generate(function.Operands[i]);
				}
				this._writer.Write(")");
			}
			else if (o is Alias)
			{
				Alias alias = (Alias)o;
				Generate(alias.Name);
				this._writer.Write(" AS ");
				Generate(alias.As);
			}
			else if (o is System.DateTime)
			{
				System.DateTime dateTime = (System.DateTime)o;
				this._writer.Write("#");
				if (dateTime.TimeOfDay.Ticks == 0)
				{
					this._writer.Write(dateTime.ToShortDateString());
				}
				else
				{
					this._writer.Write(dateTime.ToString());
				}
				this._writer.Write("#");
			}
			else if (o is string)
			{
				this._writer.Write("'");
				this._writer.Write(o);
				this._writer.Write("'");
			}
			else if (o is int)
			{
				this._writer.Write(o);
			}
			else if (o is decimal)
			{
				this._writer.Write(o);
			}
			else
			{
				throw new System.Exception("Unknown expression: " + o.GetType().FullName);
			}
		}

		public void Close()
		{
			this._writer.Close();
		}

	}
}
