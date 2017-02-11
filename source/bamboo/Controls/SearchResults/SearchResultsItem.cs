// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005 Swampware, Inc.
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

namespace bamboo.Controls.SearchResults
{
	/// <summary>
	/// Summary description for SearchResultsItem.
	/// </summary>
	public class SearchResultsItem : System.Windows.Forms.ListViewItem
	{
		private string _fileName = String.Empty;
		private int _line = -1;
		private int _column = -1;
		private string _text = String.Empty;

		private SearchResultsItem()
		{
		}

		public SearchResultsItem(string fileName, int line, int column, string text)
		{
			this.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));

			this._fileName = fileName;
			this._line = line;
			this._column = column;
			this._text = text;

			this.Text = new System.IO.FileInfo(fileName).Name;
			this.SubItems.Add(line.ToString());
			this.SubItems.Add(column.ToString());
			this.SubItems.Add(text.Replace("\t", "    "));
		}

		public string FileName
		{
			get { return this._fileName; }
		}

		public int Line
		{
			get { return this._line; }
		}

		public int Column
		{
			get { return this._column; }
		}

		public string ResultText
		{
			get { return this._text; }
		}

	}
}
