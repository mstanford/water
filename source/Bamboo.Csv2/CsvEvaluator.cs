//
// AUTOGENERATED 7/17/2009 5:09:41 PM
//
using System;

namespace Bamboo.Csv2
{
	public abstract class CsvEvaluator
	{

		public CsvEvaluator()
		{
		}

		public object Evaluate(CsvNode node)
		{
			switch (node.Type)
			{
				case CsvNodeType.Cell:
					{
						if(node.Nodes.Count == 1 && node.Nodes[0].Type == CsvNodeType.BOOLEAN)
						{
							return EvaluateBOOLEAN(node.Nodes[0].Value);
						}
						if(node.Nodes.Count == 1 && node.Nodes[0].Type == CsvNodeType.INTEGER)
						{
							return EvaluateINTEGER(node.Nodes[0].Value);
						}
						if(node.Nodes.Count == 1 && node.Nodes[0].Type == CsvNodeType.FLOAT)
						{
							return EvaluateFLOAT(node.Nodes[0].Value);
						}
						if(node.Nodes.Count == 1 && node.Nodes[0].Type == CsvNodeType.STRING)
						{
							return EvaluateSTRING(node.Nodes[0].Value);
						}
						if(node.Nodes.Count == 1 && node.Nodes[0].Type == CsvNodeType.QUOTED_STRING)
						{
							return EvaluateQUOTED_STRING(node.Nodes[0].Value);
						}
						throw new System.Exception("Invalid expression.");
					}
				case CsvNodeType.CellList:
					{
						if(node.Nodes.Count == 2 && node.Nodes[0].Type == CsvNodeType.Cell && node.Nodes[1].Type == CsvNodeType.CellListTail)
						{
							return EvaluateCellCellListTail(node);
						}
						throw new System.Exception("Invalid expression.");
					}
				case CsvNodeType.CellListTail:
					{
						if(node.Nodes.Count == 3 && node.Nodes[0].Type == CsvNodeType.COMMA && node.Nodes[1].Type == CsvNodeType.Cell && node.Nodes[2].Type == CsvNodeType.CellListTail)
						{
							return EvaluateCOMMACellCellListTail(node);
						}
						throw new System.Exception("Invalid expression.");
					}
				case CsvNodeType.Row:
					{
						if(node.Nodes.Count == 2 && node.Nodes[0].Type == CsvNodeType.CellList && node.Nodes[1].Type == CsvNodeType.RowEnd)
						{
							return EvaluateCellListRowEnd(node);
						}
						throw new System.Exception("Invalid expression.");
					}
				case CsvNodeType.RowEnd:
					{
						if(node.Nodes.Count == 1 && node.Nodes[0].Type == CsvNodeType.NEWLINE)
						{
							return EvaluateNEWLINE(node.Nodes[0].Value);
						}
						if(node.Nodes.Count == 1 && node.Nodes[0].Type == CsvNodeType.EOF)
						{
							return EvaluateEOF(node.Nodes[0].Value);
						}
						throw new System.Exception("Invalid expression.");
					}
				case CsvNodeType.BOOLEAN:
					{
						return EvaluateBOOLEAN(node.Value);
					}
				case CsvNodeType.COMMA:
					{
						return EvaluateCOMMA(node.Value);
					}
				case CsvNodeType.EOF:
					{
						return EvaluateEOF(node.Value);
					}
				case CsvNodeType.FLOAT:
					{
						return EvaluateFLOAT(node.Value);
					}
				case CsvNodeType.INTEGER:
					{
						return EvaluateINTEGER(node.Value);
					}
				case CsvNodeType.NEWLINE:
					{
						return EvaluateNEWLINE(node.Value);
					}
				case CsvNodeType.QUOTED_STRING:
					{
						return EvaluateQUOTED_STRING(node.Value);
					}
				case CsvNodeType.STRING:
					{
						return EvaluateSTRING(node.Value);
					}
				default:
					{
						throw new System.Exception("Invalid expression.");
					}
			}
		}

		protected virtual object EvaluateBOOLEAN(string value)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateINTEGER(string value)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateFLOAT(string value)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateSTRING(string value)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateQUOTED_STRING(string value)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateCellCellListTail(CsvNode node)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateCOMMACellCellListTail(CsvNode node)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateCellListRowEnd(CsvNode node)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateNEWLINE(string value)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateEOF(string value)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateCOMMA(string value)
		{
			throw new System.Exception("Implement.");
		}

	}
}