// ------------------------------------------------------------------------------
// 
// Copyright (c) 2008 Swampware, Inc.
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

namespace bamboo
{
	public class Keys
	{

		public static string KeyToString(System.Windows.Forms.KeyEventArgs e)
		{
			bool alt = e.Alt;
			bool ctrl = e.Control;
			bool shift = e.Shift;
			System.Windows.Forms.Keys keyCode = e.KeyCode;

			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

			if (ctrl) stringBuilder.Append("Ctrl+");
			if (alt) stringBuilder.Append("Alt+");

			switch (keyCode)
			{
				case System.Windows.Forms.Keys.D0:
					{
						if (shift)
							stringBuilder.Append(")");
						else
							stringBuilder.Append("0");
						break;
					}
				case System.Windows.Forms.Keys.D1:
					{
						if (shift)
							stringBuilder.Append("!");
						else
							stringBuilder.Append("1");
						break;
					}
				case System.Windows.Forms.Keys.D2:
					{
						if (shift)
							stringBuilder.Append("@");
						else
							stringBuilder.Append("2");
						break;
					}
				case System.Windows.Forms.Keys.D3:
					{
						if (shift)
							stringBuilder.Append("#");
						else
							stringBuilder.Append("3");
						break;
					}
				case System.Windows.Forms.Keys.D4:
					{
						if (shift)
							stringBuilder.Append("$");
						else
							stringBuilder.Append("4");
						break;
					}
				case System.Windows.Forms.Keys.D5:
					{
						if (shift)
							stringBuilder.Append("%");
						else
							stringBuilder.Append("5");
						break;
					}
				case System.Windows.Forms.Keys.D6:
					{
						if (shift)
							stringBuilder.Append("^");
						else
							stringBuilder.Append("6");
						break;
					}
				case System.Windows.Forms.Keys.D7:
					{
						if (shift)
							stringBuilder.Append("&");
						else
							stringBuilder.Append("7");
						break;
					}
				case System.Windows.Forms.Keys.D8:
					{
						if (shift)
							stringBuilder.Append("*");
						else
							stringBuilder.Append("8");
						break;
					}
				case System.Windows.Forms.Keys.D9:
					{
						if (shift)
							stringBuilder.Append("(");
						else
							stringBuilder.Append("9");
						break;
					}
				case System.Windows.Forms.Keys.OemMinus:
					{
						if (shift)
							stringBuilder.Append("_");
						else
							stringBuilder.Append("-");
						break;
					}
				case System.Windows.Forms.Keys.Oemplus:
					{
						if (shift)
							stringBuilder.Append("+");
						else
							stringBuilder.Append("=");
						break;
					}
				case System.Windows.Forms.Keys.Oemtilde:
					{
						if (shift)
							stringBuilder.Append("~");
						else
							stringBuilder.Append("`");
						break;
					}
				case System.Windows.Forms.Keys.OemOpenBrackets:
					{
						if (shift)
							stringBuilder.Append("{");
						else
							stringBuilder.Append("[");
						break;
					}
				case System.Windows.Forms.Keys.OemCloseBrackets:
					{
						if (shift)
							stringBuilder.Append("}");
						else
							stringBuilder.Append("]");
						break;
					}
				case System.Windows.Forms.Keys.OemPipe:
					{
						if (shift)
							stringBuilder.Append("|");
						else
							stringBuilder.Append("\\");
						break;
					}
				case System.Windows.Forms.Keys.OemSemicolon:
					{
						if (shift)
							stringBuilder.Append(":");
						else
							stringBuilder.Append(";");
						break;
					}
				case System.Windows.Forms.Keys.OemQuotes:
					{
						if (shift)
							stringBuilder.Append(@"""");
						else
							stringBuilder.Append("'");
						break;
					}
				case System.Windows.Forms.Keys.Oemcomma:
					{
						if (shift)
							stringBuilder.Append("<");
						else
							stringBuilder.Append(",");
						break;
					}
				case System.Windows.Forms.Keys.OemPeriod:
					{
						if (shift)
							stringBuilder.Append(">");
						else
							stringBuilder.Append(".");
						break;
					}
				case System.Windows.Forms.Keys.OemQuestion:
					{
						if (shift)
							stringBuilder.Append("?");
						else
							stringBuilder.Append("/");
						break;
					}
				default:
					{
						if (shift) stringBuilder.Append("Shift+");

						stringBuilder.Append(keyCode.ToString());
						break;
					}
			}

			return stringBuilder.ToString();
		}

		private Keys()
		{
		}

	}
}
