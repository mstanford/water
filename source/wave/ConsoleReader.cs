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

namespace wave
{
	/// <summary>
	/// Summary description for ConsoleReader.
	/// </summary>
	public class ConsoleReader : Water.TextReader
	{
		private Water.TextReader _reader;

		public ConsoleReader(string s)
		{
			this._reader = new Water.TextReader(new System.IO.StringReader(s + "\n"));
        }

		/// <summary>
		/// For this to work, we must only parse one statement at a time.  
		/// This way we can parse multiline block statements.
		/// </summary>
		private void CheckReader()
		{
			if(this._reader == null || this._reader.Peek() == -1)
			{
				this._reader = new Water.TextReader(new System.IO.StringReader(System.Console.ReadLine() + "\n"));
			}
		}

		public override string Filename
		{
			get { return this._reader.Filename; }
		}

		public override int LineNumber
		{
			get { return this._reader.LineNumber; }
		}

		public override int ColumnNumber
		{
			get { return this._reader.ColumnNumber; }
		}

		public override int Peek()
		{
			this.CheckReader();
			return this._reader.Peek();
		}

		public override int Read()
		{
			this.CheckReader();
			return this._reader.Read();
		}

		public override void Close()
		{
			this._reader.Close();
		}

	}
}
