// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006 Swampware, Inc.
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

namespace Bamboo.Xml
{
	/// <summary>
	/// IndentedTextWriter is a text writer capable of being indented.
	/// </summary>
	public class IndentedTextWriter 
	{
		private System.IO.TextWriter _writer;
		private string _indentationString;
		private bool _indentationsWrittenForThisLine = false;
		private int _indentLevel = 0;

		public IndentedTextWriter(System.IO.TextWriter writer)
		{
			this._writer = writer;
			this._indentationString = "\t";
		}

		public IndentedTextWriter(System.IO.TextWriter writer, string indentationString)
		{
			this._writer = writer;
			this._indentationString = indentationString;
		}

		/// <summary>
		/// Writes a value to the underlying writer.
		/// </summary>
		/// <param name="value"></param>
		public void Write(string value)
		{
			this.WriteIndentations();
			this._writer.Write(value);
		}

		/// <summary>
		/// Writes a value to the underlying writer.
		/// </summary>
		public void WriteLine()
		{
			this.WriteIndentations();
			this._writer.WriteLine();

			this._indentationsWrittenForThisLine = false;
		}

		/// <summary>
		/// Writes a value to the underlying writer.
		/// </summary>
		/// <param name="value"></param>
		public void WriteLine(string value)
		{
			this.WriteIndentations();
			this._writer.WriteLine(value);

			this._indentationsWrittenForThisLine = false;
		}

		/// <summary>
		/// Writes a value to the underlying writer.
		/// </summary>
		public void WriteLineNoTabs()
		{
			this._writer.WriteLine();

			this._indentationsWrittenForThisLine = false;
		}

		/// <summary>
		/// Writes a value to the underlying writer.
		/// </summary>
		/// <param name="value"></param>
		public void WriteLineNoTabs(string value)
		{
			this._writer.WriteLine(value);

			this._indentationsWrittenForThisLine = false;
		}

		public void WriteIndentations()
		{
			if(!this._indentationsWrittenForThisLine)
			{
				for(int i = 0; i < this._indentLevel; i++)
				{
					this._writer.Write(this._indentationString);
				}
				this._indentationsWrittenForThisLine = true;
			}
		}

		public void Indent()
		{
			this._indentLevel++;
		}

		public void Unindent()
		{
			if(this._indentLevel == 0)
			{
				throw new System.Exception("Cannot unindent when there are no indentations.");
			}

			this._indentLevel--;
		}

		public void Reset()
		{
			this._indentLevel = 0;
		}

		/// <summary>
		/// Closes the writer.
		/// </summary>
		public void Close()
		{
			this._writer.Flush();
			this._writer.Close();
		}

	}
}
