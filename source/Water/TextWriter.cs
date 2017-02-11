// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006-2007 Swampware, Inc.
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
	/// Summary description for TextWriter.
	/// </summary>
	public class TextWriter
	{
		private System.IO.TextWriter _writer;
		private string _indentationString;
		private string _newlineString;
		private bool _indentationsWrittenForThisLine = false;
		private int _indentLevel = 0;

		public TextWriter(System.IO.TextWriter writer)
		{
			this._writer = writer;
			this._indentationString = "";
			this._newlineString = "";
		}

		public TextWriter(
			System.IO.TextWriter writer, 
			string indentationString, 
			string newlineString)
		{
			this._writer = writer;
			this._indentationString = indentationString;
			this._newlineString = newlineString;
		}

		public string IndentationString
		{
			get { return this._indentationString; }
		}

		public string NewlineString
		{
			get { return this._newlineString; }
		}

		public void Write(string value)
		{
			#region Write

			this.WriteIndentations();
			this._writer.Write(value);

			#endregion
		}

		public void WriteLine(string value)
		{
			#region WriteLine

			this.WriteIndentations();
			this._writer.Write(value);
			this._writer.Write(this._newlineString);

			this._indentationsWrittenForThisLine = false;

			#endregion
		}

		public void WriteIndentations()
		{
			#region WriteIndentations

			if(!this._indentationsWrittenForThisLine)
			{
				for(int i = 0; i < this._indentLevel; i++)
				{
					this._writer.Write(this._indentationString);
				}
				this._indentationsWrittenForThisLine = true;
			}

			#endregion
		}

		public void Indent()
		{
			#region Indent

			this._indentLevel++;

			#endregion
		}

		public void Unindent()
		{
			#region Unindent

			if(this._indentLevel == 0)
			{
				throw new Water.Error("Cannot unindent when there are no indentations.");
			}

			this._indentLevel--;

			#endregion
		}

		public void Flush()
		{
			#region Flush

			this._writer.Flush();

			#endregion
		}

		/// <summary>
		/// Closes the writer.
		/// </summary>
		public void Close()
		{
			#region Close

			this._writer.Flush();
			this._writer.Close();

			#endregion
		}

	}
}
