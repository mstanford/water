//
// AUTOGENERATED 2/26/2009 12:33:15 PM
//
using System;

namespace Bamboo.Css
{
	public abstract class CssEvaluator
	{

		public CssEvaluator()
		{
		}

		public object Evaluate(CssNode node)
		{
			switch (node.Type)
			{
				case CssNodeType.Declaration:
					{
						if(node.Nodes.Count == 4 && node.Nodes[0].Type == CssNodeType.IDENTIFIER && node.Nodes[1].Type == CssNodeType.COLON && node.Nodes[2].Type == CssNodeType.ValueList && node.Nodes[3].Type == CssNodeType.SEMICOLON)
						{
							return EvaluateIDENTIFIERCOLONValueListSEMICOLON(node);
						}
						throw new System.Exception("Invalid expression.");
					}
				case CssNodeType.DeclarationList:
					{
						if(node.Nodes.Count == 2 && node.Nodes[0].Type == CssNodeType.Declaration && node.Nodes[1].Type == CssNodeType.DeclarationList)
						{
							return EvaluateDeclarationDeclarationList(node);
						}
						throw new System.Exception("Invalid expression.");
					}
				case CssNodeType.Rule:
					{
						if(node.Nodes.Count == 4 && node.Nodes[0].Type == CssNodeType.Selector && node.Nodes[1].Type == CssNodeType.LEFT_CURLY_BRACE && node.Nodes[2].Type == CssNodeType.DeclarationList && node.Nodes[3].Type == CssNodeType.RIGHT_CURLY_BRACE)
						{
							return EvaluateSelectorLEFT_CURLY_BRACEDeclarationListRIGHT_CURLY_BRACE(node);
						}
						throw new System.Exception("Invalid expression.");
					}
				case CssNodeType.RuleList:
					{
						if(node.Nodes.Count == 2 && node.Nodes[0].Type == CssNodeType.Rule && node.Nodes[1].Type == CssNodeType.RuleList)
						{
							return EvaluateRuleRuleList(node);
						}
						throw new System.Exception("Invalid expression.");
					}
				case CssNodeType.Selector:
					{
						if(node.Nodes.Count == 2 && node.Nodes[0].Type == CssNodeType.IDENTIFIER && node.Nodes[1].Type == CssNodeType.SelectorTail)
						{
							return EvaluateIDENTIFIERSelectorTail(node);
						}
						throw new System.Exception("Invalid expression.");
					}
				case CssNodeType.SelectorTail:
					{
						if(node.Nodes.Count == 5 && node.Nodes[0].Type == CssNodeType.LEFT_BRACKET && node.Nodes[1].Type == CssNodeType.IDENTIFIER && node.Nodes[2].Type == CssNodeType.EQUALS && node.Nodes[3].Type == CssNodeType.STRING && node.Nodes[4].Type == CssNodeType.RIGHT_BRACKET)
						{
							return EvaluateLEFT_BRACKETIDENTIFIEREQUALSSTRINGRIGHT_BRACKET(node);
						}
						throw new System.Exception("Invalid expression.");
					}
				case CssNodeType.Value:
					{
						if(node.Nodes.Count == 1 && node.Nodes[0].Type == CssNodeType.IDENTIFIER)
						{
							return EvaluateIDENTIFIER(node.Nodes[0].Value);
						}
						if(node.Nodes.Count == 1 && node.Nodes[0].Type == CssNodeType.STRING)
						{
							return EvaluateSTRING(node.Nodes[0].Value);
						}
						if(node.Nodes.Count == 1 && node.Nodes[0].Type == CssNodeType.INTEGER)
						{
							return EvaluateINTEGER(node.Nodes[0].Value);
						}
						if(node.Nodes.Count == 1 && node.Nodes[0].Type == CssNodeType.FLOAT)
						{
							return EvaluateFLOAT(node.Nodes[0].Value);
						}
						if(node.Nodes.Count == 1 && node.Nodes[0].Type == CssNodeType.BOOLEAN)
						{
							return EvaluateBOOLEAN(node.Nodes[0].Value);
						}
						throw new System.Exception("Invalid expression.");
					}
				case CssNodeType.ValueList:
					{
						if(node.Nodes.Count == 2 && node.Nodes[0].Type == CssNodeType.Value && node.Nodes[1].Type == CssNodeType.ValueList)
						{
							return EvaluateValueValueList(node);
						}
						throw new System.Exception("Invalid expression.");
					}
				case CssNodeType.BOOLEAN:
					{
						return EvaluateBOOLEAN(node.Value);
					}
				case CssNodeType.COLON:
					{
						return EvaluateCOLON(node.Value);
					}
				case CssNodeType.EQUALS:
					{
						return EvaluateEQUALS(node.Value);
					}
				case CssNodeType.FLOAT:
					{
						return EvaluateFLOAT(node.Value);
					}
				case CssNodeType.IDENTIFIER:
					{
						return EvaluateIDENTIFIER(node.Value);
					}
				case CssNodeType.INTEGER:
					{
						return EvaluateINTEGER(node.Value);
					}
				case CssNodeType.LEFT_BRACKET:
					{
						return EvaluateLEFT_BRACKET(node.Value);
					}
				case CssNodeType.LEFT_CURLY_BRACE:
					{
						return EvaluateLEFT_CURLY_BRACE(node.Value);
					}
				case CssNodeType.RIGHT_BRACKET:
					{
						return EvaluateRIGHT_BRACKET(node.Value);
					}
				case CssNodeType.RIGHT_CURLY_BRACE:
					{
						return EvaluateRIGHT_CURLY_BRACE(node.Value);
					}
				case CssNodeType.SEMICOLON:
					{
						return EvaluateSEMICOLON(node.Value);
					}
				case CssNodeType.STRING:
					{
						return EvaluateSTRING(node.Value);
					}
				default:
					{
						throw new System.Exception("Invalid expression.");
					}
			}
		}

		protected virtual object EvaluateIDENTIFIERCOLONValueListSEMICOLON(CssNode node)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateDeclarationDeclarationList(CssNode node)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateSelectorLEFT_CURLY_BRACEDeclarationListRIGHT_CURLY_BRACE(CssNode node)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateRuleRuleList(CssNode node)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateIDENTIFIERSelectorTail(CssNode node)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateLEFT_BRACKETIDENTIFIEREQUALSSTRINGRIGHT_BRACKET(CssNode node)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateIDENTIFIER(string value)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateSTRING(string value)
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

		protected virtual object EvaluateBOOLEAN(string value)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateValueValueList(CssNode node)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateCOLON(string value)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateEQUALS(string value)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateLEFT_BRACKET(string value)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateLEFT_CURLY_BRACE(string value)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateRIGHT_BRACKET(string value)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateRIGHT_CURLY_BRACE(string value)
		{
			throw new System.Exception("Implement.");
		}

		protected virtual object EvaluateSEMICOLON(string value)
		{
			throw new System.Exception("Implement.");
		}

	}
}