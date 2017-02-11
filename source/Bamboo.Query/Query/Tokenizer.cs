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
	/// <summary>
	/// Summary description for Tokenizer.
	/// </summary>
	public class Tokenizer
	{
		private System.IO.TextReader _reader;

		public Tokenizer(System.IO.TextReader reader)
		{
			this._reader = reader;
		}

		public string Next()
		{
			TrimWhitespace(this._reader);

			int n;
			while ((n = this._reader.Peek()) != -1)
			{
				char ch = (char)n;

				switch (ch)
				{
					case '.':
						{
							this._reader.Read();
							return ".";
						}
					case '(':
					{
						this._reader.Read();
						return "(";
					}
					case ')':
					{
						this._reader.Read();
						return ")";
					}
					case '[':
					{
						this._reader.Read();
						return ReadBlock(this._reader);
					}
					case '\'':
					{
						this._reader.Read();
						return ReadStringLiteral(this._reader);
					}
					default:
					{
						string token = ReadSymbol(this._reader);
						if (token.Length > 0)
						{
							return token;
						}
						else
						{
							return Next();
						}
					}
				}
			}

			return null;
		}

		private static void TrimWhitespace(System.IO.TextReader reader)
		{
			int n;
			while ((n = reader.Peek()) != -1)
			{
				char ch = (char)n;

				if (!IsWhitespace(ch))
				{
					return;
				}
				reader.Read();
			}
		}

		private static bool IsWhitespace(char ch)
		{
			if (ch == ' ')
			{
				return true;
			}
			else if (ch == '\t')
			{
				return true;
			}
			else if (ch == '\r')
			{
				return true;
			}
			else if (ch == '\n')
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private static string ReadBlock(System.IO.TextReader reader)
		{
			System.Text.StringBuilder tokenBuilder = new System.Text.StringBuilder();

			while (true)
			{
				char ch = (char)reader.Read();
				if (ch == ']')
				{
					return tokenBuilder.ToString();
				}
				else
				{
					tokenBuilder.Append(ch);
				}
			}
		}

		private static string ReadStringLiteral(System.IO.TextReader reader)
		{
			System.Text.StringBuilder tokenBuilder = new System.Text.StringBuilder();

			while (true)
			{
				char ch = (char)reader.Read();
				if (ch == '\'')
				{
					return tokenBuilder.ToString();
				}
				else
				{
					tokenBuilder.Append(ch);
				}
			}
		}

		private static string ReadSymbol(System.IO.TextReader reader)
		{
			System.Text.StringBuilder tokenBuilder = new System.Text.StringBuilder();

			int n;
			while ((n = reader.Peek()) != -1)
			{
				char ch = (char)n;

				if (ch == '(')
				{
					reader.Read();
					return tokenBuilder.ToString() + "(" + ReadFunction(reader) + ")";
				}
				else if (ch == ' ')
				{
					reader.Read();

					if (tokenBuilder.Length > 0)
					{
						return tokenBuilder.ToString();
					}
				}
				else if (ch == '.')
				{
					if (tokenBuilder.Length > 0)
					{
						return tokenBuilder.ToString();
					}
				}
				else if (ch == '\r')
				{
					reader.Read();

					if (tokenBuilder.Length > 0)
					{
						return tokenBuilder.ToString();
					}
				}
				else if (ch == '\n')
				{
					reader.Read();

					if (tokenBuilder.Length > 0)
					{
						return tokenBuilder.ToString();
					}
				}
				else if (ch == ',')
				{
					if (tokenBuilder.Length == 0)
					{
						reader.Read();
						return ",";
					}
					else
					{
						//if (reader.Peek() != -1 && (char)reader.Peek() == ' ')
						//{
						//    reader.Read();
						//}
						return tokenBuilder.ToString();
					}
				}
				else
				{
					reader.Read();
					tokenBuilder.Append(ch);
				}
			}

			return tokenBuilder.ToString();
		}

		private static string ReadFunction(System.IO.TextReader reader)
		{
			System.Text.StringBuilder tokenBuilder = new System.Text.StringBuilder();

			int n;
			while ((n = reader.Peek()) != -1)
			{
				char ch = (char)reader.Read();
				if (ch == ')')
				{
					return tokenBuilder.ToString();
				}
				else
				{
					tokenBuilder.Append(ch);
				}
			}

			throw new System.Exception("Invalid function: " + tokenBuilder.ToString());
		}

	}
}
