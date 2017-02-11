//
// AUTOGENERATED 12/30/2008 4:01:14 PM
//
using System;

namespace Stream
{
	public class Parser
	{
		private Tokenizer _tokenizer = new Tokenizer();
		private TextReader _reader;
		private Token _token;

		public Parser()
		{
		}

		public Node Parse(TextReader reader)
		{
			this._reader = reader;
			this._token = this._tokenizer.Tokenize(this._reader);

			return ParseExpression();
		}

		private Node ParseExpression()
		{
			Node node = new Node(NodeType.Expression);

			switch (this._token.Type)
			{
				case TokenType.BOOLEAN:
					{
						node.Nodes.Add(ParseBOOLEAN());
						return node;
					}
				case TokenType.INTEGER:
					{
						node.Nodes.Add(ParseINTEGER());
						return node;
					}
				case TokenType.FLOAT:
					{
						node.Nodes.Add(ParseFLOAT());
						return node;
					}
				case TokenType.CHARACTER:
					{
						node.Nodes.Add(ParseCHARACTER());
						return node;
					}
				case TokenType.STRING:
					{
						node.Nodes.Add(ParseSTRING());
						return node;
					}
				case TokenType.IDENTIFIER:
					{
						node.Nodes.Add(ParseIDENTIFIER());
						node.Nodes.Add(ParseExpressionTail());
						return node;
					}
				default:
					{
						throw new System.Exception("Syntax error.");
					}
			}
		}

		private Node ParseExpressionList()
		{
			Node node = new Node(NodeType.ExpressionList);

			switch (this._token.Type)
			{
				case TokenType.BOOLEAN:
				case TokenType.CHARACTER:
				case TokenType.FLOAT:
				case TokenType.IDENTIFIER:
				case TokenType.INTEGER:
				case TokenType.STRING:
					{
						node.Nodes.Add(ParseExpression());
						node.Nodes.Add(ParseExpressionListTail());
						return node;
					}
				default:
					{
						return node;
					}
			}
		}

		private Node ParseExpressionListTail()
		{
			Node node = new Node(NodeType.ExpressionListTail);

			switch (this._token.Type)
			{
				case TokenType.COMMA:
					{
						node.Nodes.Add(ParseCOMMA());
						node.Nodes.Add(ParseExpression());
						node.Nodes.Add(ParseExpressionListTail());
						return node;
					}
				default:
					{
						return node;
					}
			}
		}

		private Node ParseExpressionTail()
		{
			Node node = new Node(NodeType.ExpressionTail);

			switch (this._token.Type)
			{
				case TokenType.ASSIGNMENT:
					{
						node.Nodes.Add(ParseASSIGNMENT());
						node.Nodes.Add(ParseExpression());
						return node;
					}
				case TokenType.EQUALS:
					{
						node.Nodes.Add(ParseEQUALS());
						node.Nodes.Add(ParseExpression());
						return node;
					}
				case TokenType.LEFT_PAREN:
					{
						node.Nodes.Add(ParseLEFT_PAREN());
						node.Nodes.Add(ParseExpressionList());
						node.Nodes.Add(ParseRIGHT_PAREN());
						return node;
					}
				default:
					{
						return node;
					}
			}
		}

		private Node ParseASSIGNMENT()
		{
			Node node = new Node(NodeType.ASSIGNMENT, this._token.Value);
			this._token = this._tokenizer.Tokenize(this._reader);
			return node;
		}

		private Node ParseBOOLEAN()
		{
			Node node = new Node(NodeType.BOOLEAN, this._token.Value);
			this._token = this._tokenizer.Tokenize(this._reader);
			return node;
		}

		private Node ParseCHARACTER()
		{
			Node node = new Node(NodeType.CHARACTER, this._token.Value);
			this._token = this._tokenizer.Tokenize(this._reader);
			return node;
		}

		private Node ParseCOMMA()
		{
			Node node = new Node(NodeType.COMMA, this._token.Value);
			this._token = this._tokenizer.Tokenize(this._reader);
			return node;
		}

		private Node ParseEQUALS()
		{
			Node node = new Node(NodeType.EQUALS, this._token.Value);
			this._token = this._tokenizer.Tokenize(this._reader);
			return node;
		}

		private Node ParseFLOAT()
		{
			Node node = new Node(NodeType.FLOAT, this._token.Value);
			this._token = this._tokenizer.Tokenize(this._reader);
			return node;
		}

		private Node ParseIDENTIFIER()
		{
			Node node = new Node(NodeType.IDENTIFIER, this._token.Value);
			this._token = this._tokenizer.Tokenize(this._reader);
			return node;
		}

		private Node ParseINTEGER()
		{
			Node node = new Node(NodeType.INTEGER, this._token.Value);
			this._token = this._tokenizer.Tokenize(this._reader);
			return node;
		}

		private Node ParseLEFT_PAREN()
		{
			Node node = new Node(NodeType.LEFT_PAREN, this._token.Value);
			this._token = this._tokenizer.Tokenize(this._reader);
			return node;
		}

		private Node ParseRIGHT_PAREN()
		{
			Node node = new Node(NodeType.RIGHT_PAREN, this._token.Value);
			this._token = this._tokenizer.Tokenize(this._reader);
			return node;
		}

		private Node ParseSTRING()
		{
			Node node = new Node(NodeType.STRING, this._token.Value);
			this._token = this._tokenizer.Tokenize(this._reader);
			return node;
		}

	}
}