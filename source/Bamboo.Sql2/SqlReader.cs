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
	public class SqlReader : SqlEvaluator
	{
		private System.IO.TextReader _reader;
		private SqlParser _parser;

		public SqlReader(System.IO.Stream stream)
		{
			this._reader = new System.IO.StreamReader(stream);
			this._parser = new SqlParser(new SqlTextReader(this._reader));
		}

		public SqlReader(System.IO.TextReader reader)
		{
			this._reader = reader;
			this._parser = new SqlParser(new SqlTextReader(this._reader));
		}

		public object Read()
		{
			return this.Evaluate(this._parser.Parse());
		}

		public void Close()
		{
			this._reader.Close();
		}

		protected override object EvaluateSelectUnionStatementTail(SqlNode node)
		{
			if (node.Nodes[1].Nodes.Count == 0)
			{
				return this.Evaluate(node.Nodes[0]);
			}
			else
			{
				return new UnionStatement(this.Evaluate(node.Nodes[0]), this.Evaluate(node.Nodes[1].Nodes[1]));
			}
		}

		protected override object EvaluateDELETEFROMIDENTIFIERWhereClauseStatementTail(SqlNode node)
		{
			Identifier table = (Identifier)Evaluate(node.Nodes[2]);
			Predicate where = (Predicate)Evaluate(node.Nodes[3]);
			return new DeleteStatement(table, where);
		}

		protected override object EvaluateINSERTINTOIDENTIFIERLEFT_PARENColumnListRIGHT_PARENVALUESLEFT_PARENValueListRIGHT_PARENStatementTail(SqlNode node)
		{
			Identifier table = (Identifier)Evaluate(node.Nodes[2]);
			Identifier[] columns = (Identifier[])Evaluate(node.Nodes[4]);
			object[] values = (object[])Evaluate(node.Nodes[8]);
			return new InsertStatement(table, columns, values);
		}

		protected override object EvaluateREPLACEINTOIDENTIFIERLEFT_PARENColumnListRIGHT_PARENVALUESLEFT_PARENValueListRIGHT_PARENStatementTail(SqlNode node)
		{
			Identifier table = (Identifier)Evaluate(node.Nodes[2]);
			Identifier[] columns = (Identifier[])Evaluate(node.Nodes[4]);
			object[] values = (object[])Evaluate(node.Nodes[8]);
			return new ReplaceStatement(table, columns, values);
		}

		protected override object EvaluateUPDATEIDENTIFIERSETSetListWhereClauseStatementTail(SqlNode node)
		{
			Identifier table = (Identifier)Evaluate(node.Nodes[1]);
			Predicate[] setList = (Predicate[])Evaluate(node.Nodes[3]);
			if (node.Nodes[4].Nodes.Count > 0)
			{
				Predicate where = (Predicate)Evaluate(node.Nodes[4]);
				return new UpdateStatement(table, setList, where);
			}
			else
			{
				return new UpdateStatement(table, setList);
			}
		}

		protected override object EvaluateIDENTIFIERComparisonOperatorVALUESetListTail(SqlNode node)
		{
			Identifier identifier = (Identifier)Evaluate(node.Nodes[0]);
			string op = (string)Evaluate(node.Nodes[1]);
			object value = Evaluate(node.Nodes[2]);
			Predicate predicate = new Predicate(op, identifier, value);

			if (node.Nodes[3].Nodes.Count == 0)
			{
				return new Predicate[] { predicate };
			}
			else
			{
				return cons(predicate, (Predicate[])Evaluate(node.Nodes[3]));
			}
		}

		protected override object EvaluateCOMMAIDENTIFIERComparisonOperatorVALUESetListTail(SqlNode node)
		{
			Identifier identifier = (Identifier)Evaluate(node.Nodes[1]);
			string op = (string)Evaluate(node.Nodes[2]);
			object value = Evaluate(node.Nodes[3]);
			Predicate predicate = new Predicate(op, identifier, value);

			if (node.Nodes[4].Nodes.Count == 0)
			{
				return new Predicate[] { predicate };
			}
			else
			{
				return cons(predicate, (Predicate[])Evaluate(node.Nodes[4]));
			}
		}

		protected override object EvaluateIDENTIFIERColumnListTail(SqlNode node)
		{
			Identifier column = (Identifier)Evaluate(node.Nodes[0]);
			if (node.Nodes[1].Nodes.Count == 0)
			{
				return new Identifier[] { column };
			}
			else
			{
				Identifier[] columns = (Identifier[])Evaluate(node.Nodes[1]);
				return cons(column, columns);
			}
		}

		protected override object EvaluateCOMMAIDENTIFIERColumnListTail(SqlNode node)
		{
			Identifier column = (Identifier)Evaluate(node.Nodes[1]);
			if (node.Nodes[2].Nodes.Count == 0)
			{
				return new Identifier[] { column };
			}
			else
			{
				Identifier[] columns = (Identifier[])Evaluate(node.Nodes[2]);
				return cons(column, columns);
			}
		}

		protected override object EvaluateVALUEValueListTail(SqlNode node)
		{
			object value = Evaluate(node.Nodes[0]);
			if (node.Nodes[1].Nodes.Count == 0)
			{
				return new object[] { value };
			}
			else
			{
				object[] values = (object[])Evaluate(node.Nodes[1]);
				return cons(value, values);
			}
		}

		protected override object EvaluateCOMMAVALUEValueListTail(SqlNode node)
		{
			object value = Evaluate(node.Nodes[1]);
			if (node.Nodes[2].Nodes.Count == 0)
			{
				return new object[] { value };
			}
			else
			{
				object[] values = (object[])Evaluate(node.Nodes[2]);
				return cons(value, values);
			}
		}

		protected override object EvaluateSELECTSelectListFromJoinListWhereClauseGroupByClauseOrderByClause(SqlNode node)
		{
			List<object> columns = (List<object>)this.Evaluate(node.Nodes[1]);
			List<object> from = (List<object>)this.Evaluate(node.Nodes[2]);
			List<object> joins = (node.Nodes[3].Nodes.Count == 0) ? null : (List<object>)this.Evaluate(node.Nodes[3]);
			Predicate where = (node.Nodes[4].Nodes.Count == 0) ? null : (Predicate)this.Evaluate(node.Nodes[4]);
			List<Identifier> groupBy = (node.Nodes[5].Nodes.Count == 0) ? null : (List<Identifier>)this.Evaluate(node.Nodes[5]);
			List<Sort> orderBy = (node.Nodes[6].Nodes.Count == 0) ? null : (List<Sort>)this.Evaluate(node.Nodes[6]);
			return new SelectStatement(columns, from, joins, where, groupBy, orderBy);
		}

		protected override object EvaluateFROMTableList(SqlNode node)
		{
			List<object> tables = new List<object>();
			object table = this.Evaluate(node.Nodes[1].Nodes[0]);
			if (node.Nodes[1].Nodes[1].Nodes.Count > 0)
			{
				table = new Alias(table, (Identifier)this.Evaluate(node.Nodes[1].Nodes[1]));
			}
			tables.Add(table);
			if (node.Nodes[1].Nodes[2].Nodes.Count > 0)
			{
				foreach (object table2 in (List<object>)this.Evaluate(node.Nodes[1].Nodes[2]))
				{
					tables.Add(table2);
				}
			}
			return tables;
		}

		protected override object EvaluateCOMMAIDENTIFIERTableTailTableListTail(SqlNode node)
		{
			List<object> tables = new List<object>();
			object table = this.Evaluate(node.Nodes[1]);
			if (node.Nodes[2].Nodes.Count > 0)
			{
				table = new Alias(table, (Identifier)this.Evaluate(node.Nodes[2]));
			}
			tables.Add(table);
			if (node.Nodes[3].Nodes.Count > 0)
			{
				foreach (object table2 in (List<object>)this.Evaluate(node.Nodes[3]))
				{
					tables.Add(table2);
				}
			}
			return tables;
		}

		protected override object EvaluateSelectItemSelectListTail(SqlNode node)
		{
			List<object> columns = new List<object>();
			columns.Add(this.Evaluate(node.Nodes[0]));
			if (node.Nodes[1].Nodes.Count > 0)
			{
				foreach (object column in (List<object>)this.Evaluate(node.Nodes[1]))
				{
					columns.Add(column);
				}
			}
			return columns;
		}

		protected override object EvaluateCOMMASelectItemSelectListTail(SqlNode node)
		{
			List<object> columns = new List<object>();
			columns.Add(this.Evaluate(node.Nodes[1]));
			if (node.Nodes[2].Nodes.Count > 0)
			{
				foreach (object column in (List<object>)this.Evaluate(node.Nodes[2]))
				{
					columns.Add(column);
				}
			}
			return columns;
		}

		protected override object EvaluateIDENTIFIERSelectItemTailSelectItemAlias(SqlNode node)
		{
			object column = this.Evaluate(node.Nodes[0]);
			if (node.Nodes[1].Nodes.Count == 3 && node.Nodes[1].Nodes[0].Type == SqlNodeType.LEFT_PAREN && node.Nodes[1].Nodes[2].Type == SqlNodeType.RIGHT_PAREN)
			{
				column = new Function((Identifier)column, (List<object>)this.Evaluate(node.Nodes[1].Nodes[1]));
			}
			if (node.Nodes[2].Nodes.Count == 1)
			{
				column = new Alias(column, (Identifier)this.Evaluate(node.Nodes[2].Nodes[0]));
			}
			else if (node.Nodes[2].Nodes.Count == 2)
			{
				column = new Alias(column, (Identifier)this.Evaluate(node.Nodes[2].Nodes[1]));
			}
			return column;
		}

		protected override object EvaluateVALUEFunctionParameterListTail(SqlNode node)
		{
			List<object> functionParameters = new List<object>();
			functionParameters.Add(this.Evaluate(node.Nodes[0]));
			if (node.Nodes[1].Nodes.Count > 0)
			{
				foreach (object functionParameter in (List<object>)this.Evaluate(node.Nodes[1]))
				{
					functionParameters.Add(functionParameter);
				}
			}
			return functionParameters;
		}

		protected override object EvaluateCOMMAVALUEFunctionParameterListTail(SqlNode node)
		{
			List<object> functionParameters = new List<object>();
			functionParameters.Add(this.Evaluate(node.Nodes[1]));
			if (node.Nodes[2].Nodes.Count > 0)
			{
				foreach (object functionParameter in (List<object>)this.Evaluate(node.Nodes[2]))
				{
					functionParameters.Add(functionParameter);
				}
			}
			return functionParameters;
		}

		protected override object EvaluateINNERJOINIDENTIFIERTableTailONIDENTIFIERComparisonOperatorIDENTIFIERJoinList(SqlNode node)
		{
			List<object> joins = new List<object>();
			object table = this.Evaluate(node.Nodes[2]);
			if (node.Nodes[3].Nodes.Count > 0)
			{
				table = new Alias(table, (Identifier)this.Evaluate(node.Nodes[3]));
			}
			Identifier a = (Identifier)this.Evaluate(node.Nodes[5]);
			string op = (string)this.Evaluate(node.Nodes[6]);
			Identifier b = (Identifier)this.Evaluate(node.Nodes[7]);
			joins.Add(new InnerJoin(table, a, op, b));
			if (node.Nodes[8].Nodes.Count > 0)
			{
				foreach (object join in (List<object>)this.Evaluate(node.Nodes[8]))
				{
					joins.Add(join);
				}
			}
			return joins;
		}

		protected override object EvaluateLEFTOUTERJOINIDENTIFIERTableTailONIDENTIFIERComparisonOperatorIDENTIFIERJoinList(SqlNode node)
		{
			List<object> joins = new List<object>();
			object table = this.Evaluate(node.Nodes[3]);
			if (node.Nodes[4].Nodes.Count > 0)
			{
				table = new Alias(table, (Identifier)this.Evaluate(node.Nodes[4]));
			}
			Identifier a = (Identifier)this.Evaluate(node.Nodes[6]);
			string op = (string)this.Evaluate(node.Nodes[7]);
			Identifier b = (Identifier)this.Evaluate(node.Nodes[8]);
			joins.Add(new LeftOuterJoin(table, a, op, b));
			if (node.Nodes[9].Nodes.Count > 0)
			{
				foreach (object join in (List<object>)this.Evaluate(node.Nodes[9]))
				{
					joins.Add(join);
				}
			}
			return joins;
		}

		protected override object EvaluateRIGHTOUTERJOINIDENTIFIERTableTailONIDENTIFIERComparisonOperatorIDENTIFIERJoinList(SqlNode node)
		{
			List<object> joins = new List<object>();
			object table = this.Evaluate(node.Nodes[3]);
			if (node.Nodes[4].Nodes.Count > 0)
			{
				table = new Alias(table, (Identifier)this.Evaluate(node.Nodes[4]));
			}
			Identifier a = (Identifier)this.Evaluate(node.Nodes[6]);
			string op = (string)this.Evaluate(node.Nodes[7]);
			Identifier b = (Identifier)this.Evaluate(node.Nodes[8]);
			joins.Add(new RightOuterJoin(table, a, op, b));
			if (node.Nodes[9].Nodes.Count > 0)
			{
				foreach (object join in (List<object>)this.Evaluate(node.Nodes[9]))
				{
					joins.Add(join);
				}
			}
			return joins;
		}

		protected override object EvaluateWHEREPredicate(SqlNode node)
		{
			return this.Evaluate(node.Nodes[1]);
		}

		protected override object EvaluateIDENTIFIERPredicateIdentifierTail(SqlNode node)
		{
			Predicate predicate = new Predicate((string)this.Evaluate(node.Nodes[1].Nodes[0]), this.Evaluate(node.Nodes[0]), this.Evaluate(node.Nodes[1].Nodes[1]));
			if (node.Nodes[1].Nodes[0].Value == "BETWEEN")
			{
				predicate = new Predicate("AND", predicate, this.Evaluate(node.Nodes[1].Nodes[3]));
			}
			else if (node.Nodes[1].Nodes[2].Nodes.Count > 0)
			{
				string op = (string)this.Evaluate(node.Nodes[1].Nodes[2].Nodes[0]);
				Predicate predicate2 = (Predicate)this.Evaluate(node.Nodes[1].Nodes[2].Nodes[1]);
				predicate = new Predicate(op, predicate, predicate2);
			}
			return predicate;
		}

		protected override object EvaluateGROUPBYIDENTIFIERGroupByTail(SqlNode node)
		{
			List<Identifier> columns = new List<Identifier>();
			columns.Add((Identifier)this.Evaluate(node.Nodes[2]));
			if (node.Nodes[3].Nodes.Count > 0)
			{
				foreach (Identifier column in (List<Identifier>)this.Evaluate(node.Nodes[3]))
				{
					columns.Add(column);
				}
			}
			return columns;
		}

		protected override object EvaluateCOMMAIDENTIFIERGroupByTail(SqlNode node)
		{
			List<Identifier> columns = new List<Identifier>();
			columns.Add((Identifier)this.Evaluate(node.Nodes[1]));
			if (node.Nodes[2].Nodes.Count > 0)
			{
				foreach (Identifier column in (List<Identifier>)this.Evaluate(node.Nodes[2]))
				{
					columns.Add(column);
				}
			}
			return columns;
		}

		protected override object EvaluateORDERBYIDENTIFIERAscDescOrderByTail(SqlNode node)
		{
			List<Sort> sorts = new List<Sort>();
			Identifier column = (Identifier)this.Evaluate(node.Nodes[2]);
			bool asc = (node.Nodes[3].Nodes.Count == 0) ? true : (bool)this.Evaluate(node.Nodes[3]);
			sorts.Add(new Sort(column, asc));
			if (node.Nodes[4].Nodes.Count > 0)
			{
				foreach (Sort sort in (List<Sort>)this.Evaluate(node.Nodes[4]))
				{
					sorts.Add(sort);
				}
			}
			return sorts;
		}

		protected override object EvaluateCOMMAIDENTIFIERAscDescOrderByTail(SqlNode node)
		{
			List<Sort> sorts = new List<Sort>();
			Identifier column = (Identifier)this.Evaluate(node.Nodes[1]);
			bool asc = (node.Nodes[2].Nodes.Count == 0) ? true : (bool)this.Evaluate(node.Nodes[2]);
			sorts.Add(new Sort(column, asc));
			if (node.Nodes[3].Nodes.Count > 0)
			{
				foreach (Sort sort in (List<Sort>)this.Evaluate(node.Nodes[3]))
				{
					sorts.Add(sort);
				}
			}
			return sorts;
		}

		protected override object EvaluateIDENTIFIER(string value)
		{
			return new Identifier(value);
		}

		protected override object EvaluateEQUALS(string value)
		{
			return value;
		}

		protected override object EvaluateNOT_EQUALS(string value)
		{
			return value;
		}

		protected override object EvaluateGREATER_THAN(string value)
		{
			return value;
		}

		protected override object EvaluateGREATER_THAN_OR_EQUAL(string value)
		{
			return value;
		}

		protected override object EvaluateLESS_THAN(string value)
		{
			return value;
		}

		protected override object EvaluateLESS_THAN_OR_EQUAL(string value)
		{
			return value;
		}

		protected override object EvaluateINTEGER(string value)
		{
			return Int32.Parse(value);
		}

		protected override object EvaluateFLOAT(string value)
		{
			return Decimal.Parse(value);
		}

		protected override object EvaluateQUOTED_STRING(string value)
		{
			return value.Substring(1, value.Length - 2);
		}

		protected override object EvaluateDATE(string value)
		{
			return System.DateTime.Parse(value.Substring(1, value.Length - 2));
		}

		protected override object EvaluateAND(string value)
		{
			return value;
		}

		protected override object EvaluateOR(string value)
		{
			return value;
		}

		protected override object EvaluateNOT(string value)
		{
			return value;
		}

		protected override object EvaluateASC(string value)
		{
			return true;
		}

		protected override object EvaluateDESC(string value)
		{
			return false;
		}

		protected override object EvaluateLIKE(string value)
		{
			return value;
		}

		protected override object EvaluateBETWEEN(string value)
		{
			return value;
		}

		private static object[] cons(object a, object[] b)
		{
			object[] c = new object[b.Length + 1];
			c[0] = a;
			for (int i = 0; i < b.Length; i++)
			{
				c[i + 1] = b[i];
			}
			return c;
		}

		private static Identifier[] cons(Identifier a, Identifier[] b)
		{
			Identifier[] c = new Identifier[b.Length + 1];
			c[0] = a;
			for (int i = 0; i < b.Length; i++)
			{
				c[i + 1] = b[i];
			}
			return c;
		}

		private static Predicate[] cons(Predicate a, Predicate[] b)
		{
			Predicate[] c = new Predicate[b.Length + 1];
			c[0] = a;
			for (int i = 0; i < b.Length; i++)
			{
				c[i + 1] = b[i];
			}
			return c;
		}

	}
}
