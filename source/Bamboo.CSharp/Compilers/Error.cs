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

namespace Bamboo.CSharp.Compilers
{
	/// <summary>
	/// Summary description for Error.
	/// </summary>
	public class Error
	{
		private bool _isWarning;
		private string _file;
		private int _line;
		private int _column;
		private string _code;
		private string _description;
		private string _text;

		public Error(bool isWarning, string file, int line, int column, string code, string description, string text)
		{
			this._isWarning = isWarning;
			this._file = file;
			this._line = line;
			this._column = column;
			this._code = code;
			this._description = description;
			this._text = text;
		}

		public bool IsWarning
		{
			get { return this._isWarning; }
		}

		public string File
		{
			get { return this._file; }
		}

		public int Line
		{
			get { return this._line; }
		}

		public int Column
		{
			get { return this._column; }
		}

		public string Code
		{
			get { return this._code; }
		}

		public string Description
		{
			get { return this._description; }
		}

		public string Text
		{
			get { return this._text; }
		}

	}
}
