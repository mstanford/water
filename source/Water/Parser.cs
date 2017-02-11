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
	/// Summary description for Parser.
	/// </summary>
	public class Parser
	{

		public static object Parse(System.IO.TextReader reader)
		{
			return Parse(new Water.TextReader(reader));
		}

		public static object Parse(Water.TextReader reader)
		{
			#region Parse

			TrimWhitespace(reader);

			if(reader.Peek() == -1)
			{
				return null;
			}

			if(IsComment(reader))
			{
				ParseComment(reader);

				// Try it again.
				return Parse(reader);
			}

			return ParseExpression(reader);

			#endregion
		}

		public static object Parse(string s)
		{
			System.IO.StringReader reader = new System.IO.StringReader(s);
			object value = Parse(reader);
			reader.Close();
			return value;
		}

		public static object ParseExpression(Water.TextReader reader)
		{
			#region ParseExpression

			char ch = (char)reader.Peek();

			switch(ch)
			{
				case '(' :
				{
					return ParseList(reader);
				}
				case '{' :
				{
					return ParseDictionary(reader);
				}
				case '"' :
				{
					return ParseString(reader);
				}
				case '[' :
				{
					return ParseBytes(reader);
				}
				case '\'' :
				{
					return ParseQuote(reader);
				}
				case ',' :
				{
					return ParseComma(reader);
				}
				default :
				{
					object atom = ParseAtom(reader);
                    return atom;
				}
			}

			#endregion
		}

		//
		// Lists
		//

		public static Water.List ParseList(Water.TextReader reader)
		{
			#region ParseList

			Water.List list = new Water.List();

			// Read the '('
			reader.Read();

			TrimWhitespace(reader);

			while(reader.Peek() != -1)
			{
				char ch = (char)reader.Peek();

				if(ch == ')')
				{
					reader.Read();
					return list;
				}
				else
				{
					list.Add(Water.Parser.Parse(reader));
				}

				TrimWhitespace(reader);
			}

			throw new Water.Error("Parser error: Invalid list.");

			#endregion
		}

		//
		// Dictionaries
		//

		private static Water.Dictionary ParseDictionary(Water.TextReader reader)
		{
			#region ParseDictionary

			Water.Dictionary dict = new Water.Dictionary();

			// Read the '{'
			reader.Read();

			TrimWhitespace(reader);

			while(reader.Peek() != -1)
			{
				char ch = (char)reader.Peek();

				if(ch == '}')
				{
					//Exit state
					reader.Read();
					return dict;
				}
				else
				{
					ParseDictEntry(reader, dict);
				}

				TrimWhitespace(reader);
			}

			throw new Water.Error("Parser error: End of dictionary expected.");

			#endregion
		}

		private static void ParseDictEntry(Water.TextReader reader, Water.Dictionary dict)
		{
			#region ParseDictEntry

			TrimWhitespace(reader);

			string name = ParseName(reader);

			TrimWhitespace(reader);

			if(reader.Peek() == ':')
			{
				reader.Read();
			}
			else
			{
				throw new Water.Error("Parser error: Invalid dictionary entry name: " + name);
			}

			TrimWhitespace(reader);

			dict.Add(name, Parse(reader));

			#endregion
		}

		private static string ParseName(Water.TextReader reader)
		{
			#region ParseName

			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

			while(reader.Peek() != -1)
			{
				char ch = (char)reader.Peek();

				if(ch == ':')
				{
					return stringBuilder.ToString();
				}
				else
				{
					stringBuilder.Append(ch);
					reader.Read();
				}
			}

			throw new Water.Error("Parser error: " + stringBuilder.ToString());

			#endregion
		}

		//
		// Strings
		//

		private static string ParseString(Water.TextReader reader)
		{
			#region ParseString

			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

			// Read the '\"'
			reader.Read();

			bool isEscaped = false;
			while(reader.Peek() != -1)
			{
				char ch = (char)reader.Read();

				if(isEscaped)
				{
					switch(ch)
					{
						case '\\' :
						{
							stringBuilder.Append("\\");
							break;
						}
						case '"' :
						{
							stringBuilder.Append("\"");
							break;
						}
						case '\'' :
						{
							stringBuilder.Append("\'");
							break;
						}
						case '0' :
						{
							stringBuilder.Append("\0");
							break;
						}
						case 'a' :
						{
							stringBuilder.Append("\a");
							break;
						}
						case 'b' :
						{
							stringBuilder.Append("\b");
							break;
						}
						case 'f' :
						{
							stringBuilder.Append("\f");
							break;
						}
						case 'n' :
						{
							stringBuilder.Append("\n");
							break;
						}
						case 'r' :
						{
							stringBuilder.Append("\r");
							break;
						}
						case 't' :
						{
							stringBuilder.Append("\t");
							break;
						}
						case 'v' :
						{
							stringBuilder.Append("\v");
							break;
						}
						default :
						{
							throw new Water.Error("Unknown escape sequence: \\" + ch);
						}
					}

					isEscaped = false;
				}
				else if(ch == '\\')
				{
					isEscaped = true;
				}
				else if(!isEscaped && ch == '"')
				{
					return stringBuilder.ToString();
				}
				else
				{
					stringBuilder.Append(ch);
				}
			}

			throw new Water.Error("Parser error: End of double-quoted string expected.  " + stringBuilder.ToString());

			#endregion
		}

		//
		// Binary
		//

		private static byte[] ParseBytes(Water.TextReader reader)
		{
			#region ParseBytes

			System.IO.StringWriter writer = new System.IO.StringWriter();

			// Read the '['
			reader.Read();

			while(reader.Peek() != -1)
			{
				char ch = (char)reader.Read();

				if(ch == ']')
				{
					byte[] bytes = System.Convert.FromBase64String(writer.ToString());
					writer.Close();
					return bytes;
				}

				writer.Write(ch);
			}

			throw new Water.Error("Invalid bytestring.");

			#endregion
		}

		//
		// Quote-prefixed expressions
		//

		private static Water.Quote ParseQuote(Water.TextReader reader)
		{
			#region ParseQuote

			// Read the '\''
			reader.Read();

			return new Water.Quote(Parse(reader));

			#endregion
		}

		//
		// Comma-prefixed expressions
		//

		private static Water.Comma ParseComma(Water.TextReader reader)
		{
			#region ParseComma

			// Read the ','
			reader.Read();

			return new Water.Comma(Parse(reader));

			#endregion
		}

		//
		// Comment
		//

		public static bool IsComment(Water.TextReader reader)
		{
			#region IsComment

			char ch = (char)reader.Peek();

			return IsComment(ch);

			#endregion
		}

		private static bool IsComment(char ch)
		{
			#region IsComment

			if(ch == ';')
			{
				return true;
			}
			else
			{
				return false;
			}

			#endregion
		}

		public static void ParseComment(Water.TextReader reader)
		{
			#region ParseComment

            while (reader.Peek() != -1)
            {
                if (IsNewline((char)reader.Peek()))
                {
                    return;
                }
                else
                {
                    reader.Read();
                }
            }

			#endregion
		}

		//
		// Atoms
		//

		private static object ParseAtom(Water.TextReader reader)
		{
			#region ParseAtom

			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

			while(reader.Peek() != -1)
			{
				char ch = (char)reader.Peek();

				if(IsWhitespace(ch))
				{
					break;
				}
				else if(ch == '[')
				{
					ParseIndexer(reader, stringBuilder);
				}
				else if(ch == '(')
				{
					return Coerce(stringBuilder.ToString());
				}
				else if(ch == ')')
				{
					return Coerce(stringBuilder.ToString());
				}
				else if(ch == '}')
				{
					return Coerce(stringBuilder.ToString());
				}
				else
				{
					stringBuilder.Append(ch);
					reader.Read();
				}
			}

			return Coerce(stringBuilder.ToString());

			#endregion
		}

		private static void ParseIndexer(Water.TextReader reader, System.Text.StringBuilder stringBuilder)
		{
			#region ParseIndexer

			stringBuilder.Append((char)reader.Read());

			while(reader.Peek() != -1)
			{
				char ch = (char)reader.Peek();

				if(ch == ']')
				{
					stringBuilder.Append(ch);
					reader.Read();
					return;
				}
				else
				{
					stringBuilder.Append(ch);
					reader.Read();
				}
			}

			throw new Water.Error("Parser error: End of indexer expected.");

			#endregion
		}

		private static object Coerce(string s)
		{
			#region Coerce

			if (s == null || s.Length == 0)
			{
				throw new Water.Error("Token is empty.");
			}

			if (s.ToLower() == "null")
			{
				return null;
			}

			if(s.ToLower() == "true")
			{
				return true;
			}
			else if(s.ToLower() == "false")
			{
				return false;
			}

#if NET11
			try
			{
				int n = Int32.Parse(s);
				return n;
			}
			catch (System.Exception exception)
			{
				string thisIsAHackToIgnoreCompilerWarning = exception.Message;
			}
#else
			int n;
			if (Int32.TryParse(s, out n))
			{
				return n;
			}
#endif

#if NET11
			try
			{
				double d = System.Double.Parse(s);
				return d;
			}
			catch (System.Exception exception)
			{
				string thisIsAHackToIgnoreCompilerWarning = exception.Message;
			}
#else
			double d;
			if (Double.TryParse(s, out d))
			{
				return d;
			}
#endif

			return new Water.Identifier(s);

			#endregion
		}

		//
		// Whitespace
		//

		public static void TrimWhitespace(Water.TextReader reader)
		{
			#region TrimWhitespace

			while(reader.Peek() != -1)
			{
				if(!IsWhitespace((char)reader.Peek()))
				{
					return;
				}
				reader.Read();
			}

			#endregion
		}

		private bool IsWhitespace(Water.TextReader reader)
		{
			#region IsWhitespace

			char ch = (char)reader.Peek();

			return IsWhitespace(ch);

			#endregion
		}

		private static bool IsWhitespace(char ch)
		{
			#region IsWhitespace

			if(ch == ' ')
			{
				return true;
			}
			else if(ch == '\t')
			{
				return true;
			}
			else if(ch == '\r')
			{
				return true;
			}
			else if(ch == '\n')
			{
				return true;
			}
			else
			{
				return false;
			}

			#endregion
		}

		public static void TrimSpace(Water.TextReader reader)
		{
			#region TrimSpace

			while(reader.Peek() != -1)
			{
				if(!IsSpace((char)reader.Peek()))
				{
					return;
				}
				reader.Read();
			}

			#endregion
		}

		private bool IsSpace(Water.TextReader reader)
		{
			#region IsSpace

			char ch = (char)reader.Peek();

			return IsSpace(ch);

			#endregion
		}

		private static bool IsSpace(char ch)
		{
			#region IsSpace

			if(ch == ' ')
			{
				return true;
			}
			else if(ch == '\t')
			{
				return true;
			}
			else
			{
				return false;
			}

			#endregion
		}

		public static void TrimNewline(Water.TextReader reader)
		{
			#region TrimNewline

			while(reader.Peek() != -1)
			{
				if(!IsNewline((char)reader.Peek()))
				{
					return;
				}
				reader.Read();
			}

			#endregion
		}

		public static bool IsNewline(Water.TextReader reader)
		{
			#region IsNewline

			char ch = (char)reader.Peek();

			return IsNewline(ch);

			#endregion
		}

		private static bool IsNewline(char ch)
		{
			#region IsNewline

			if(ch == '\r')
			{
				return true;
			}
			else if(ch == '\n')
			{
				return true;
			}
			else
			{
				return false;
			}

			#endregion
		}

		private Parser()
		{
		}

	}
}
