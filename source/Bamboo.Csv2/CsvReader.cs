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
	public class CsvReader : CsvEvaluator
	{
		private System.IO.TextReader _reader;
		private List<object> _row = new List<object>();

		public CsvReader(System.IO.TextReader reader)
		{
			this._reader = reader;
		}

		public CsvReader(System.IO.Stream stream)
		{
			this._reader = new System.IO.StreamReader(stream);
		}

		public Bamboo.DataStructures.Table Read()
		{
			return Read(null);
		}

		private Bamboo.DataStructures.Table Read(Bamboo.DataStructures.Table table)
		{
			CsvParser parser = new CsvParser(new CsvTextReader(this._reader));

			while (this._reader.Peek() != -1)
			{
				Evaluate(parser.Parse());
				Bamboo.DataStructures.Tuple row = new Bamboo.DataStructures.Tuple(this._row.ToArray());
				this._row.Clear();
				if (table == null)
				{
					table = new Bamboo.DataStructures.Table(row, new List<Bamboo.DataStructures.Tuple>());
				}
				else
				{
					table.Rows.Add(row);
				}
			}

			return table;
		}

		public void Close()
		{
			this._reader.Close();
		}

		protected override object EvaluateCellListRowEnd(CsvNode node)
		{
			Evaluate(node.Nodes[0]);
			return null;
		}

		protected override object EvaluateCellCellListTail(CsvNode node)
		{
			this._row.Add(Evaluate(node.Nodes[0]));
			if (node.Nodes[1].Nodes.Count > 0)
			{
				Evaluate(node.Nodes[1]);
			}
			return null;
		}

		protected override object EvaluateCOMMACellCellListTail(CsvNode node)
		{
			if (node.Nodes[1].Nodes.Count == 0)
			{
				this._row.Add(null);
			}
			else
			{
				this._row.Add(Evaluate(node.Nodes[1]));
			}
			if (node.Nodes[2].Nodes.Count > 0)
			{
				Evaluate(node.Nodes[2]);
			}
			return null;
		}

		protected override object EvaluateBOOLEAN(string value)
		{
			return bool.Parse(value);
		}

		protected override object EvaluateINTEGER(string value)
		{
			return int.Parse(value);
		}

		protected override object EvaluateFLOAT(string value)
		{
			return decimal.Parse(value);
		}

		protected override object EvaluateSTRING(string value)
		{
			return value;
		}

		protected override object EvaluateQUOTED_STRING(string value)
		{
			return value.Substring(1, value.Length - 2).Trim();
		}

	}
}
