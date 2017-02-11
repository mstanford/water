//
// AUTOGENERATED 2/26/2009 12:33:15 PM
//
using System;

namespace Bamboo.Css
{
	public class CssNodePrinter
	{

		public static void Print(CssNode node, System.IO.TextWriter writer)
		{
			Print(node, writer, 0);
		}

		private static void Print(CssNode node, System.IO.TextWriter writer, int indentationLevel)
		{
			switch(node.Type)
			{
				case CssNodeType.Declaration :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("Declaration:");
						foreach(CssNode node2 in node.Nodes)
						{
							Print(node2, writer, indentationLevel + 1);
						}
						break;
					}
				case CssNodeType.DeclarationList :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("DeclarationList:");
						foreach(CssNode node2 in node.Nodes)
						{
							Print(node2, writer, indentationLevel + 1);
						}
						break;
					}
				case CssNodeType.Rule :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("Rule:");
						foreach(CssNode node2 in node.Nodes)
						{
							Print(node2, writer, indentationLevel + 1);
						}
						break;
					}
				case CssNodeType.RuleList :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("RuleList:");
						foreach(CssNode node2 in node.Nodes)
						{
							Print(node2, writer, indentationLevel + 1);
						}
						break;
					}
				case CssNodeType.Selector :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("Selector:");
						foreach(CssNode node2 in node.Nodes)
						{
							Print(node2, writer, indentationLevel + 1);
						}
						break;
					}
				case CssNodeType.SelectorTail :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("SelectorTail:");
						foreach(CssNode node2 in node.Nodes)
						{
							Print(node2, writer, indentationLevel + 1);
						}
						break;
					}
				case CssNodeType.Value :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("Value:");
						foreach(CssNode node2 in node.Nodes)
						{
							Print(node2, writer, indentationLevel + 1);
						}
						break;
					}
				case CssNodeType.ValueList :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("ValueList:");
						foreach(CssNode node2 in node.Nodes)
						{
							Print(node2, writer, indentationLevel + 1);
						}
						break;
					}
				case CssNodeType.BOOLEAN :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("BOOLEAN: " + node.Value);
						break;
					}
				case CssNodeType.COLON :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("COLON: " + node.Value);
						break;
					}
				case CssNodeType.EPSILON :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("EPSILON: " + node.Value);
						break;
					}
				case CssNodeType.EQUALS :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("EQUALS: " + node.Value);
						break;
					}
				case CssNodeType.FLOAT :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("FLOAT: " + node.Value);
						break;
					}
				case CssNodeType.IDENTIFIER :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("IDENTIFIER: " + node.Value);
						break;
					}
				case CssNodeType.INTEGER :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("INTEGER: " + node.Value);
						break;
					}
				case CssNodeType.LEFT_BRACKET :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("LEFT_BRACKET: " + node.Value);
						break;
					}
				case CssNodeType.LEFT_CURLY_BRACE :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("LEFT_CURLY_BRACE: " + node.Value);
						break;
					}
				case CssNodeType.RIGHT_BRACKET :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("RIGHT_BRACKET: " + node.Value);
						break;
					}
				case CssNodeType.RIGHT_CURLY_BRACE :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("RIGHT_CURLY_BRACE: " + node.Value);
						break;
					}
				case CssNodeType.SEMICOLON :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("SEMICOLON: " + node.Value);
						break;
					}
				case CssNodeType.STRING :
					{
						for(int i = 0; i < indentationLevel; i++)
						{
							System.Console.Write("   ");
						}
						System.Console.WriteLine("STRING: " + node.Value);
						break;
					}
				default:
					{
						throw new System.Exception("Invalid node type.");
					}
			}
		}

		private CssNodePrinter()
		{
		}

	}
}