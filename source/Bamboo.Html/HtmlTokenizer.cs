//
// AUTOGENERATED 3/2/2009 1:21:50 PM
//
using System;

namespace Bamboo.Html
{
	public class HtmlTokenizer
	{
		private System.Text.StringBuilder _stringBuilder = new System.Text.StringBuilder();

		public HtmlTokenizer()
		{
		}

		public HtmlToken Tokenize(HtmlTextReader reader)
		{
			int n;
			char ch;

			_stringBuilder.Length = 0;

			//
			// Trim whitespace
			//
			while ((n = reader.Peek()) != -1)
			{
				ch = (char)n;
				switch (ch)
				{
					case ' ':
					case '\t':
					case '\r':
					case '\n':
						{
							reader.Read();
							break;
						}
					default:
						{
							goto s0;
						}
				}
			}

		s0:
			n = reader.Peek();
			if (n == -1)
			{
				if (_stringBuilder.Length == 0)
				{
					return new HtmlToken(HtmlTokenType._EOF_);
				}
				else
				{
					return new HtmlToken(HtmlTokenType._ERROR_);
				}
			}
			ch = (char)n;
			switch (ch)
			{
				case '\"':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s1;
					}
				case '<':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s4;
					}
				case '>':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s6;
					}
				case '=':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s7;
					}
				case '!':
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '(':
				case ')':
				case '*':
				case '+':
				case ',':
				case '-':
				case '.':
				case ':':
				case ';':
				case '?':
				case '@':
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
				case 'G':
				case 'H':
				case 'I':
				case 'J':
				case 'K':
				case 'L':
				case 'M':
				case 'N':
				case 'O':
				case 'P':
				case 'Q':
				case 'R':
				case 'S':
				case 'T':
				case 'U':
				case 'V':
				case 'W':
				case 'X':
				case 'Y':
				case 'Z':
				case '[':
				case '\\':
				case ']':
				case '^':
				case '_':
				case '`':
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
				case 'g':
				case 'h':
				case 'i':
				case 'j':
				case 'k':
				case 'l':
				case 'm':
				case 'n':
				case 'o':
				case 'p':
				case 'q':
				case 'r':
				case 's':
				case 't':
				case 'u':
				case 'v':
				case 'w':
				case 'x':
				case 'y':
				case 'z':
				case '{':
				case '|':
				case '}':
				case '~':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s8;
					}
				case '/':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s9;
					}
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s10;
					}
				case '0':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s12;
					}
				default:
					{
						_stringBuilder.Append(ch);
						goto se;
					}
			}

		s1:
			n = reader.Peek();
			if (n == -1)
			{
				if (_stringBuilder.Length == 0)
				{
					return new HtmlToken(HtmlTokenType._EOF_);
				}
				else
				{
					return new HtmlToken(HtmlTokenType._ERROR_);
				}
			}
			ch = (char)n;
			switch (ch)
			{
				case '\t':
				case '\n':
				case '\r':
				case ' ':
				case '!':
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '(':
				case ')':
				case '*':
				case '+':
				case ',':
				case '-':
				case '.':
				case '/':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
				case ':':
				case ';':
				case '<':
				case '=':
				case '>':
				case '?':
				case '@':
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
				case 'G':
				case 'H':
				case 'I':
				case 'J':
				case 'K':
				case 'L':
				case 'M':
				case 'N':
				case 'O':
				case 'P':
				case 'Q':
				case 'R':
				case 'S':
				case 'T':
				case 'U':
				case 'V':
				case 'W':
				case 'X':
				case 'Y':
				case 'Z':
				case '[':
				case ']':
				case '^':
				case '_':
				case '`':
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
				case 'g':
				case 'h':
				case 'i':
				case 'j':
				case 'k':
				case 'l':
				case 'm':
				case 'n':
				case 'o':
				case 'p':
				case 'q':
				case 'r':
				case 's':
				case 't':
				case 'u':
				case 'v':
				case 'w':
				case 'x':
				case 'y':
				case 'z':
				case '{':
				case '|':
				case '}':
				case '~':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s1;
					}
				case '\"':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s2;
					}
				case '\\':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s3;
					}
				default:
					{
						_stringBuilder.Append(ch);
						goto se;
					}
			}

		s2:
			return new HtmlToken(HtmlTokenType.QUOTED_STRING, _stringBuilder.ToString());

		s3:
			n = reader.Peek();
			if (n == -1)
			{
				if (_stringBuilder.Length == 0)
				{
					return new HtmlToken(HtmlTokenType._EOF_);
				}
				else
				{
					return new HtmlToken(HtmlTokenType._ERROR_);
				}
			}
			ch = (char)n;
			switch (ch)
			{
				case '\t':
				case '\n':
				case '\r':
				case ' ':
				case '!':
				case '\"':
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '(':
				case ')':
				case '*':
				case '+':
				case ',':
				case '-':
				case '.':
				case '/':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
				case ':':
				case ';':
				case '<':
				case '=':
				case '>':
				case '?':
				case '@':
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
				case 'G':
				case 'H':
				case 'I':
				case 'J':
				case 'K':
				case 'L':
				case 'M':
				case 'N':
				case 'O':
				case 'P':
				case 'Q':
				case 'R':
				case 'S':
				case 'T':
				case 'U':
				case 'V':
				case 'W':
				case 'X':
				case 'Y':
				case 'Z':
				case '[':
				case '\\':
				case ']':
				case '^':
				case '_':
				case '`':
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
				case 'g':
				case 'h':
				case 'i':
				case 'j':
				case 'k':
				case 'l':
				case 'm':
				case 'n':
				case 'o':
				case 'p':
				case 'q':
				case 'r':
				case 's':
				case 't':
				case 'u':
				case 'v':
				case 'w':
				case 'x':
				case 'y':
				case 'z':
				case '{':
				case '|':
				case '}':
				case '~':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s1;
					}
				default:
					{
						_stringBuilder.Append(ch);
						goto se;
					}
			}

		s4:
			n = reader.Peek();
			if (n == -1)
			{
				if (_stringBuilder.Length == 0)
				{
					return new HtmlToken(HtmlTokenType._EOF_);
				}
				else
				{
					return new HtmlToken(HtmlTokenType.LESS_THAN, _stringBuilder.ToString());
				}
			}
			ch = (char)n;
			switch (ch)
			{
				case '/':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s5;
					}
				default:
					{
						return new HtmlToken(HtmlTokenType.LESS_THAN, _stringBuilder.ToString());
					}
			}

		s5:
			return new HtmlToken(HtmlTokenType.LESS_THAN_FORWARD_SLASH, _stringBuilder.ToString());

		s6:
			return new HtmlToken(HtmlTokenType.GREATER_THAN, _stringBuilder.ToString());

		s7:
			return new HtmlToken(HtmlTokenType.EQUALS, _stringBuilder.ToString());

		s8:
			n = reader.Peek();
			if (n == -1)
			{
				if (_stringBuilder.Length == 0)
				{
					return new HtmlToken(HtmlTokenType._EOF_);
				}
				else
				{
					return new HtmlToken(HtmlTokenType.STRING, _stringBuilder.ToString());
				}
			}
			ch = (char)n;
			switch (ch)
			{
				case '!':
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '(':
				case ')':
				case '*':
				case '+':
				case ',':
				case '-':
				case '.':
				case '/':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
				case ':':
				case ';':
				case '?':
				case '@':
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
				case 'G':
				case 'H':
				case 'I':
				case 'J':
				case 'K':
				case 'L':
				case 'M':
				case 'N':
				case 'O':
				case 'P':
				case 'Q':
				case 'R':
				case 'S':
				case 'T':
				case 'U':
				case 'V':
				case 'W':
				case 'X':
				case 'Y':
				case 'Z':
				case '[':
				case '\\':
				case ']':
				case '^':
				case '_':
				case '`':
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
				case 'g':
				case 'h':
				case 'i':
				case 'j':
				case 'k':
				case 'l':
				case 'm':
				case 'n':
				case 'o':
				case 'p':
				case 'q':
				case 'r':
				case 's':
				case 't':
				case 'u':
				case 'v':
				case 'w':
				case 'x':
				case 'y':
				case 'z':
				case '{':
				case '|':
				case '}':
				case '~':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s8;
					}
				default:
					{
						return new HtmlToken(HtmlTokenType.STRING, _stringBuilder.ToString());
					}
			}

		s9:
			n = reader.Peek();
			if (n == -1)
			{
				if (_stringBuilder.Length == 0)
				{
					return new HtmlToken(HtmlTokenType._EOF_);
				}
				else
				{
					return new HtmlToken(HtmlTokenType.FORWARD_SLASH, _stringBuilder.ToString());
				}
			}
			ch = (char)n;
			switch (ch)
			{
				case '!':
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '(':
				case ')':
				case '*':
				case '+':
				case ',':
				case '-':
				case '.':
				case '/':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
				case ':':
				case ';':
				case '?':
				case '@':
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
				case 'G':
				case 'H':
				case 'I':
				case 'J':
				case 'K':
				case 'L':
				case 'M':
				case 'N':
				case 'O':
				case 'P':
				case 'Q':
				case 'R':
				case 'S':
				case 'T':
				case 'U':
				case 'V':
				case 'W':
				case 'X':
				case 'Y':
				case 'Z':
				case '[':
				case '\\':
				case ']':
				case '^':
				case '_':
				case '`':
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
				case 'g':
				case 'h':
				case 'i':
				case 'j':
				case 'k':
				case 'l':
				case 'm':
				case 'n':
				case 'o':
				case 'p':
				case 'q':
				case 'r':
				case 's':
				case 't':
				case 'u':
				case 'v':
				case 'w':
				case 'x':
				case 'y':
				case 'z':
				case '{':
				case '|':
				case '}':
				case '~':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s8;
					}
				default:
					{
						return new HtmlToken(HtmlTokenType.FORWARD_SLASH, _stringBuilder.ToString());
					}
			}

		s10:
			n = reader.Peek();
			if (n == -1)
			{
				if (_stringBuilder.Length == 0)
				{
					return new HtmlToken(HtmlTokenType._EOF_);
				}
				else
				{
					return new HtmlToken(HtmlTokenType.INTEGER, _stringBuilder.ToString());
				}
			}
			ch = (char)n;
			switch (ch)
			{
				case '!':
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '(':
				case ')':
				case '*':
				case '+':
				case ',':
				case '-':
				case '/':
				case ':':
				case ';':
				case '?':
				case '@':
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
				case 'G':
				case 'H':
				case 'I':
				case 'J':
				case 'K':
				case 'L':
				case 'M':
				case 'N':
				case 'O':
				case 'P':
				case 'Q':
				case 'R':
				case 'S':
				case 'T':
				case 'U':
				case 'V':
				case 'W':
				case 'X':
				case 'Y':
				case 'Z':
				case '[':
				case '\\':
				case ']':
				case '^':
				case '_':
				case '`':
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
				case 'g':
				case 'h':
				case 'i':
				case 'j':
				case 'k':
				case 'l':
				case 'm':
				case 'n':
				case 'o':
				case 'p':
				case 'q':
				case 'r':
				case 's':
				case 't':
				case 'u':
				case 'v':
				case 'w':
				case 'x':
				case 'y':
				case 'z':
				case '{':
				case '|':
				case '}':
				case '~':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s8;
					}
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s10;
					}
				case '.':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s11;
					}
				default:
					{
						return new HtmlToken(HtmlTokenType.INTEGER, _stringBuilder.ToString());
					}
			}

		s11:
			n = reader.Peek();
			if (n == -1)
			{
				if (_stringBuilder.Length == 0)
				{
					return new HtmlToken(HtmlTokenType._EOF_);
				}
				else
				{
					return new HtmlToken(HtmlTokenType.FLOAT, _stringBuilder.ToString());
				}
			}
			ch = (char)n;
			switch (ch)
			{
				case '!':
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '(':
				case ')':
				case '*':
				case '+':
				case ',':
				case '-':
				case '.':
				case '/':
				case ':':
				case ';':
				case '?':
				case '@':
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
				case 'G':
				case 'H':
				case 'I':
				case 'J':
				case 'K':
				case 'L':
				case 'M':
				case 'N':
				case 'O':
				case 'P':
				case 'Q':
				case 'R':
				case 'S':
				case 'T':
				case 'U':
				case 'V':
				case 'W':
				case 'X':
				case 'Y':
				case 'Z':
				case '[':
				case '\\':
				case ']':
				case '^':
				case '_':
				case '`':
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
				case 'g':
				case 'h':
				case 'i':
				case 'j':
				case 'k':
				case 'l':
				case 'm':
				case 'n':
				case 'o':
				case 'p':
				case 'q':
				case 'r':
				case 's':
				case 't':
				case 'u':
				case 'v':
				case 'w':
				case 'x':
				case 'y':
				case 'z':
				case '{':
				case '|':
				case '}':
				case '~':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s8;
					}
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s11;
					}
				default:
					{
						return new HtmlToken(HtmlTokenType.FLOAT, _stringBuilder.ToString());
					}
			}

		s12:
			n = reader.Peek();
			if (n == -1)
			{
				if (_stringBuilder.Length == 0)
				{
					return new HtmlToken(HtmlTokenType._EOF_);
				}
				else
				{
					return new HtmlToken(HtmlTokenType.INTEGER, _stringBuilder.ToString());
				}
			}
			ch = (char)n;
			switch (ch)
			{
				case '!':
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '(':
				case ')':
				case '*':
				case '+':
				case ',':
				case '-':
				case '/':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
				case ':':
				case ';':
				case '?':
				case '@':
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
				case 'G':
				case 'H':
				case 'I':
				case 'J':
				case 'K':
				case 'L':
				case 'M':
				case 'N':
				case 'O':
				case 'P':
				case 'Q':
				case 'R':
				case 'S':
				case 'T':
				case 'U':
				case 'V':
				case 'W':
				case 'X':
				case 'Y':
				case 'Z':
				case '[':
				case '\\':
				case ']':
				case '^':
				case '_':
				case '`':
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
				case 'g':
				case 'h':
				case 'i':
				case 'j':
				case 'k':
				case 'l':
				case 'm':
				case 'n':
				case 'o':
				case 'p':
				case 'q':
				case 'r':
				case 's':
				case 't':
				case 'u':
				case 'v':
				case 'w':
				case 'x':
				case 'y':
				case 'z':
				case '{':
				case '|':
				case '}':
				case '~':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s8;
					}
				case '.':
					{
						reader.Read();
						_stringBuilder.Append(ch);
						goto s11;
					}
				default:
					{
						return new HtmlToken(HtmlTokenType.INTEGER, _stringBuilder.ToString());
					}
			}

		se:
			return new HtmlToken(HtmlTokenType._ERROR_, _stringBuilder.ToString());

		}

	}
}
