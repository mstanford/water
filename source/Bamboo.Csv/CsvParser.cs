// ------------------------------------------------------------------------------
// 
// Copyright (c) 2007-2008 Swampware, Inc.
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

namespace Bamboo.Csv
{
	/// <summary>
	/// Summary description for CsvParser.
	/// </summary>
	public class CsvParser
	{

		public static System.Collections.IList Parse(System.IO.TextReader reader)
		{
			if (reader.Peek() == -1)
			{
				return null;
			}

			System.Collections.ArrayList table = new System.Collections.ArrayList();

			while (reader.Peek() != -1)
			{
				System.Collections.IList row = ParseRow(reader);
				if (row != null && row.Count > 0)
				{
					table.Add(row);
				}
			}

			return table;
		}

		private static System.Collections.IList ParseRow(System.IO.TextReader reader)
		{
			System.Collections.IList row = new System.Collections.ArrayList();

			int n = -1;
			while ((n = reader.Peek()) != -1)
			{
				char ch = (char)n;

				switch (ch)
				{
					case '\"':
						{
							reader.Read();
							row.Add(ParseAtom(ParseQuotedString(reader)));
							break;
						}
					case ',':
						{
							reader.Read();
							row.Add(null);
							break;
						}
					case '\n':
						{
							reader.Read();

							if (row.Count == 0)
							{
								//Try again.
								return ParseRow(reader);
							}
							else
							{
								return row;
							}
						}
					case '\r':
						{
							reader.Read();
							if (reader.Peek() == (int)'\n')
							{
								reader.Read();
							}

							if (row.Count == 0)
							{
								//Try again.
								return ParseRow(reader);
							}
							else
							{
								return row;
							}
						}
					default:
						{
							row.Add(ParseAtom(ParseUnquotedString(reader)));
							break;
						}
				}
			}

			if (row.Count == 0)
			{
				return null;
			}
			else
			{
				return row;
			}
		}

		private static object ParseAtom(string s)
		{
			if (s.Length == 0)
			{
				return null;
			}

			string sToLower = s.ToLower();
			if (sToLower == "true")
			{
				return true;
			}
			else if (sToLower == "false")
			{
				return false;
			}

			int n;
			if (Int32.TryParse(s, out n))
			{
				return n;
			}

			double d;
			if (Double.TryParse(s, out d))
			{
				return d;
			}

			DateTime dt;
			if (DateTime.TryParse(s, out dt))
			{
				return dt;
			}

			return s;
		}

		private static string ParseUnquotedString(System.IO.TextReader reader)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

			int n = -1;
			while ((n = reader.Peek()) != -1)
			{
				switch (n)
				{
					case ',':
						{
							reader.Read();
							return stringBuilder.ToString().Trim();
						}
					case '\n':
						{
							return stringBuilder.ToString().Trim();
						}
					case '\r':
						{
							return stringBuilder.ToString().Trim();
						}
					default:
						{
							char ch = (char)reader.Read();
							stringBuilder.Append(ch);
							break;
						}
				}
			}

			return stringBuilder.ToString().Trim();
		}

		private static string ParseQuotedString(System.IO.TextReader reader)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

			while (reader.Peek() != -1)
			{
				char ch = (char)reader.Read();

				switch (ch)
				{
					case '\"':
						{
							if (reader.Peek() == (int)'\"')
							{
								stringBuilder.Append(EscapeQuotedString(reader));
							}
							else
							{
								if (reader.Peek() == (int)',')
								{
									reader.Read();
								}
								return stringBuilder.ToString();
							}
							break;
						}
					default:
						{
							stringBuilder.Append(ch);
							break;
						}
				}
			}

			throw new System.Exception("Quoted string did not terminate.");
		}

		private static char EscapeQuotedString(System.IO.TextReader reader)
		{
			return (char)reader.Read();
		}

		private CsvParser()
		{
		}

	}
}
