//
// AUTOGENERATED 7/19/2009 5:57:00 PM
//
using System;

namespace Bamboo.Sql2
{
	public class SqlGenerator
	{

		public SqlGenerator()
		{
		}

		public void Generate(SqlNode node, System.IO.TextWriter writer)
		{
			bool writeWhitespace = false;

			Generate(node, writer, ref writeWhitespace);
		}

		private void Generate(SqlNode node, System.IO.TextWriter writer, ref bool writeWhitespace)
		{
			switch(node.Type)
			{
				case SqlNodeType.AscDesc:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.ColumnList:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.ColumnListTail:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.ComparisonOperator:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.From:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.FunctionParameterList:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.FunctionParameterListTail:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.GroupByClause:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.GroupByTail:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.JoinList:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.LogicalOperator:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.OrderByClause:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.OrderByTail:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.Predicate:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.PredicateIdentifierTail:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.PredicateTail:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.Select:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.SelectItem:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.SelectItemAlias:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.SelectItemTail:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.SelectList:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.SelectListTail:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.SetList:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.SetListTail:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.Statement:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.StatementTail:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.TableList:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.TableListTail:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.TableTail:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.Union:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.VALUE:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.ValueList:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.ValueListTail:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.WhereClause:
					{
						for(int i = 0; i < node.Nodes.Count; i++)
						{
							Generate(node.Nodes[i], writer, ref writeWhitespace);
						}
						break;
					}
				case SqlNodeType.AND:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.AS:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.ASC:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.BETWEEN:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.BY:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.COMMA:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.DATE:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.DELETE:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.DESC:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.EPSILON:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.EQUALS:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.FLOAT:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.FROM:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.GREATER_THAN:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.GREATER_THAN_OR_EQUAL:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.GROUP:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.IDENTIFIER:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.INNER:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.INSERT:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.INTEGER:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.INTO:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.JOIN:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.LEFT:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.LEFT_PAREN:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.LESS_THAN:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.LESS_THAN_OR_EQUAL:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.LIKE:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.NOT:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.NOT_EQUALS:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.ON:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.OR:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.ORDER:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.OUTER:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.QUOTED_STRING:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.REPLACE:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.RIGHT:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.RIGHT_PAREN:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.SELECT:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.SEMICOLON:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.SET:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.UNION:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.UPDATE:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.VALUES:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				case SqlNodeType.WHERE:
					{
						if(writeWhitespace)
						{
							writer.Write(" ");
						}
						writer.Write(node.Value);
						writeWhitespace = true;
						break;
					}
				default:
					{
						throw new System.Exception("Invalid node type.");
					}
			}
		}

	}
}
