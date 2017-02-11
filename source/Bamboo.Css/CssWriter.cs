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

namespace Bamboo.Css
{
	public class CssWriter
	{
		private System.IO.Stream _stream;
		private System.IO.StreamWriter _writer;

		public CssWriter(System.IO.Stream stream)
		{
			this._stream = stream;
			this._writer = new System.IO.StreamWriter(stream);
		}

		public void Write(StyleSheet styleSheet)
		{
			bool first = true;
			foreach (StyleRule styleRule in styleSheet.Rules)
			{
				if (first)
				{
					first = false;
				}
				else
				{
					this._writer.WriteLine();
				}
				this._writer.Write(styleRule.Name);
				if (styleRule is KeyValueStyleRule)
				{
					KeyValueStyleRule keyValueStyleRule = (KeyValueStyleRule)styleRule;
					this._writer.Write("[");
					this._writer.Write(keyValueStyleRule.Key);
					this._writer.Write("=");
					this._writer.Write("\"" + keyValueStyleRule.Value + "\"");
					this._writer.Write("]");
				}
				this._writer.WriteLine();
				this._writer.WriteLine("{");
				foreach (StyleDeclaration styleDeclaration in styleRule.Declarations)
				{
					this._writer.Write("\t" + styleDeclaration.Name + ": ");
					if (styleDeclaration.Value is string)
					{
						string s = (string)styleDeclaration.Value;
						if (s.IndexOf(" ") != -1)
						{
							this._writer.Write("\"" + s + "\"");
						}
						else
						{
							this._writer.Write(s);
						}
					}
					else if (styleDeclaration.Value is bool)
					{
						this._writer.Write(styleDeclaration.Value.ToString().ToLower());
					}
					else if (styleDeclaration.Value is Css.Font)
					{
						Css.Font font = (Css.Font)styleDeclaration.Value;
						this._writer.Write("\"" + font.Name + "\"");
						this._writer.Write(" ");
						this._writer.Write(font.Size);
						if (font.Bold)
						{
							this._writer.Write(" ");
							this._writer.Write("bold");
						}
					}
					else
					{
						this._writer.Write(styleDeclaration.Value);
					}

					//bool first2 = true;
					//foreach (object value in styleDeclaration.Values)
					//{
					//    if (first2)
					//    {
					//        first2 = false;
					//    }
					//    else
					//    {
					//        this._writer.Write(" ");
					//    }
					//    if (value is string)
					//    {
					//        string s = (string)value;
					//        if (s.IndexOf(" ") != -1)
					//        {
					//            this._writer.Write("\"" + value + "\"");
					//        }
					//        else
					//        {
					//            this._writer.Write(value);
					//        }
					//    }
					//    else if (value is bool)
					//    {
					//        this._writer.Write(value.ToString().ToLower());
					//    }
					//    else
					//    {
					//        this._writer.Write(value);
					//    }
					//}
					this._writer.WriteLine(";");
				}
				this._writer.WriteLine("}");
			}
			this._writer.Flush();
		}

		public void Close()
		{
			this._stream.Close();
		}

	}
}
