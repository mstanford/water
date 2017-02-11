// ------------------------------------------------------------------------------
// 
// Copyright (c) 2009 Swampware, Inc.
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

namespace Bamboo.Csv2
{
	public class CsvWriter
	{
		private System.IO.TextWriter _writer;

		public CsvWriter(System.IO.Stream stream)
		{
			this._writer = new System.IO.StreamWriter(stream);
		}

		public CsvWriter(System.IO.TextWriter writer)
		{
			this._writer = writer;
		}

		public void Write(Bamboo.DataStructures.Table table)
		{
			bool first = true;
			for(int i = 0; i < table.Columns.Count; i++)
			{
				if (first)
				{
					first = false;
				}
				else
				{
					this._writer.Write(",");
				}
				this._writer.Write(Escape(table.Columns[i]));
			}
			this._writer.Write("\r\n");
			foreach (Bamboo.DataStructures.Tuple row in table.Rows)
			{
				first = true;
				for (int i = 0; i < row.Count; i++)
				{
					if (first)
					{
						first = false;
					}
					else
					{
						this._writer.Write(",");
					}
					object item = row[i];
					if (item != null)
					{
						this._writer.Write(Escape(item));
					}
				}
				this._writer.Write("\r\n");
			}
			this._writer.Flush();
		}

		public void Close()
		{
			this._writer.Close();
		}

		private static object Escape(object o)
		{
			if (o is string)
			{
				string s = (string)o;
				if (s.IndexOf(",") == -1)
				{
					return s;
				}
				else
				{
					return "\"" + s + "\"";
				}
			}
			else
			{
				return o;
			}
		}

	}
}
