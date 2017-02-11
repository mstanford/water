// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006-2008 Swampware, Inc.
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

namespace Water
{
	/// <summary>
	/// Summary description for StatementParser.
	/// </summary>
	public class StatementParser
	{
		private Water.TextReader _reader;

		public StatementParser(System.IO.TextReader reader)
		{
			this._reader = new Water.TextReader(reader);
		}

		public StatementParser(Water.TextReader reader)
		{
			this._reader = reader;
		}

		public Water.Statement Parse()
		{
			Water.Parser.TrimWhitespace(this._reader);

			if(this._reader.Peek() == -1)
			{
				return null;
			}

			char ch = (char)this._reader.Peek();

			if(ch == ';')
			{
				Water.Parser.ParseComment(this._reader);

				// Try it again.
				return Parse();
			}
			else if(ch == '(')
			{
				string file = this._reader.Filename;
				int line = this._reader.LineNumber;
				int column = this._reader.ColumnNumber;
				Water.List expressions = Water.Parser.ParseList(this._reader);

				return new Water.Statement(file, line, column, expressions);
			}
			else
			{
				return ParseStatement(this._reader);
			}
		}

		public static Water.Statement Parse(string s)
		{
			System.IO.StringReader reader = new System.IO.StringReader(s);
			StatementParser statementParser = new StatementParser(reader);
			Water.Statement value = statementParser.Parse();
			reader.Close();
			return value;
		}

		private Water.Statement ParseStatement(Water.TextReader reader)
		{
			Water.Parser.TrimWhitespace(reader);

			if(reader.Peek() == -1)
			{
				return null;
			}

			char ch = (char)reader.Peek();

			if(ch == ';')
			{
				Water.Parser.ParseComment(reader);

				// Try it again.
				return ParseStatement(reader);
			}

			string file = reader.Filename;
			int line = reader.LineNumber;
			int column = reader.ColumnNumber;
			Water.List expressions = new Water.List();

			while(reader.Peek() != -1)
			{
				if(Water.Parser.IsNewline(reader))
				{
//TODO DELETE					Water.Parser.TrimNewline(reader);
					break;
				}

				if(Water.Parser.IsComment(reader))
				{
					Water.Parser.ParseComment(reader);
					break;
				}

				expressions.Add(Water.Parser.ParseExpression(reader));

				Water.Parser.TrimSpace(reader);
			}

			if(expressions.Count == 0)
			{
				// Try it again.
				return ParseStatement(reader);
			}

			Water.Blocks.Block block = GetBlock(expressions);
			if(block != null)
			{
				Water.Statement statement = new Water.Statement(file, line, column, expressions);

				expressions.Add(
					ParseBlockStatements(
					reader, 
					block.Name, 
					"end_" + block.Name, 
					block.IsRecursive, 
					statement));

				return statement;
			}

			return new Water.Statement(file, line, column, expressions);
		}

		private Water.List ParseBlockStatements(Water.TextReader reader, string name, string end, bool allowRecursion, Water.Statement start_statement)
		{
			Water.List block_statements = new Water.List();

			while(reader.Peek() != -1)
			{
				Water.Parser.TrimWhitespace(reader);

				Water.Statement statement = ParseStatement(reader);
				if(statement != null)
				{
					if (statement.Expression.Count == 1 && statement.Expression[0].ToString().Equals(end))
					{
						return block_statements;
					}

					block_statements.Add(statement);
				}
			}

//TODO DELETE			Water.Environment.Set("_Statement", start_statement);
			throw new Water.Error("\"" + end + "\" expected.");
		}

		public static Water.Blocks.Block GetBlock(Water.List list)
		{
			if(list == null || list.Count == 0)
			{
				return null;
			}

			if(!(list[0] is Water.Identifier))
			{
				return null;
			}

			Water.Identifier identifier = (Water.Identifier)list[0];

			object block = Water.Environment.Identify(identifier.Value);

			if(block == null)
			{
				return null;
			}

			if(!(block is Water.Blocks.Block))
			{
				return null;
			}

			return (Water.Blocks.Block)block;
		}

		public static bool IsBlock(Water.List list)
		{
			if(list == null || list.Count == 0)
			{
				return false;
			}
			if(!(list[0] is Water.Identifier))
			{
				return false;
			}
			Water.Identifier identifier = (Water.Identifier)list[0];
			object block = Water.Environment.Identify(identifier.Value);
			if(block == null)
			{
				return false;
			}
			if(!(block is Water.Blocks.Block))
			{
				return false;
			}
			return true;
		}

	}
}
