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
using System.Collections.Generic;
using System.Text;

namespace Surf
{
	public class Printer
	{

		public static void Print(object value, System.IO.TextWriter writer)
		{
			bool canNewline = true;
			Print(value, writer, ref canNewline);
		}

		private static void Print(object value, System.IO.TextWriter writer, ref bool canNewline)
		{
			if (value is Node)
			{
				Node node = (Node)value;

				new Surf.Generator().Generate(node, writer);
			}
			else if (value is Surf.Bitmap)
			{
				Surf.Bitmap bitmap = (Surf.Bitmap)value;

				writer.Write("[");
				for (int i = 0; i < bitmap.Length; i++)
				{
					writer.Write(bitmap.Get(i) ? "1" : "0");
				}
				writer.Write("]");
			}
			else if (value is Tuple)
			{
				Tuple tuple = (Tuple)value;

				writer.Write("( ");
				for (int i = 0; i < tuple.Count; i++)
				{
					if (i > 0)
					{
						writer.Write(", ");
					}
					Print(tuple[i], writer, ref canNewline);
				}
				writer.Write(" )");
			}
			else if (value is Set)
			{
				Set set = (Set)value;

				bool isRelation = (canNewline && set.Count > 0 && set[0] is Surf.Tuple) ? true : false;
				if (isRelation)
				{
					canNewline = false;
				}

				writer.Write("{ ");
				if (isRelation)
				{
					writer.Write("\n");
				}
				for (int i = 0; i < set.Count; i++)
				{
					if (i > 0)
					{
						writer.Write(", ");
						if (isRelation)
						{
							writer.Write("\n");
						}
					}
					if (isRelation)
					{
						writer.Write("   ");
					}
					Print(set[i], writer, ref canNewline);
				}
				if (isRelation)
				{
					writer.Write("\n");
				}
				writer.Write(" }");
			}
			else if (value is string)
			{
				string s = (string)value;

				writer.Write("\"");
				writer.Write(Escape(s));
				writer.Write("\"");
			}
			else if (value is char)
			{
				char ch = (char)value;

				writer.Write("\'");
				writer.Write(Escape(ch.ToString()));
				writer.Write("\'");
			}
			else if (value == null)
			{
				writer.Write("");
			}
			else
			{
				writer.Write(value.ToString());
			}
		}

		private static string Escape(string input)
		{
			System.IO.StringReader reader = new System.IO.StringReader(input);
			System.IO.StringWriter writer = new System.IO.StringWriter();

			int n;
			while ((n = reader.Read()) != -1)
			{
				char ch = (char)n;
				switch (ch)
				{
					case '\\' :
					case '\'' :
					case '\"' :
					case '\0' :
					case '\a' :
					case '\b' :
					case '\f' :
					case '\n' :
					case '\r' :
					case '\t' :
					case '\v' :
					{
						writer.Write('\\');
						writer.Write(ch);
						break;
					}
					default :
					{
						writer.Write(ch);
						break;
					}
				}
			}
			
			return writer.ToString();
		}

		private Printer()
		{
		}

	}
}
