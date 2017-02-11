// ------------------------------------------------------------------------------
// 
// Copyright (c) 2007 Swampware, Inc.
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
	/// Summary description for CsvGenerator.
	/// </summary>
	public class CsvGenerator
	{

		public static void Generate(System.Collections.IList table, System.IO.TextWriter writer)
		{
			bool first = true;
            foreach (System.Collections.IList row in table)
			{
				first = true;
				foreach (object cell in row)
				{
					if(first)
					{
						first = false;
					}
					else
					{
						writer.Write(",");
					}

					if (cell != null && (!cell.Equals("")))
					{
						writer.Write("\"" + Escape(cell.ToString()) + "\"");
					}
				}
				writer.WriteLine("");
			}
			writer.Flush();
		}

		private static string Escape(string s)
		{
			return s.Replace("\"", "\"\"");
		}

		private CsvGenerator()
		{
		}

	}
}
