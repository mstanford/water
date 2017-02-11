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
	/// Summary description for TextReader.
	/// </summary>
	public class TextReader
	{
		private System.IO.TextReader _reader;
		private System.Text.StringBuilder _buffer = new System.Text.StringBuilder();
		private string _filename = null;
		private int _lineNumber = 1;
		private int _columnNumber = 1;

		protected TextReader()
		{
		}

		public TextReader(string s)
		{
			this._reader = new System.IO.StringReader(s);
		}

		public TextReader(System.IO.TextReader reader)
		{
			this._reader = reader;
		}

		public TextReader(System.IO.TextReader reader, string filename)
		{
			this._reader = reader;
			this._filename = filename;
		}

		public virtual string Filename
		{
			get { return this._filename; }
		}

		public virtual int LineNumber
		{
			get { return this._lineNumber; }
		}

		public virtual int ColumnNumber
		{
			get { return this._columnNumber; }
		}

		public virtual int Peek()
		{
			#region Peek

			return this._reader.Peek();

			#endregion
		}

		public virtual int Read()
		{
			#region Read

			int n = this._reader.Read();
			if(n == (int)'\n')
			{
				this._lineNumber++;
				this._columnNumber = 1;
			}
			else
			{
				this._columnNumber++;
			}
			return n;

			#endregion
		}

        public virtual void Close()
		{
			#region Close

			this._reader.Close();

			#endregion
		}

	}
}
