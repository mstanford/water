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
	public class CssReader : CssEvaluator
	{
		private System.IO.Stream _stream;
		private System.IO.StreamReader _reader;
		private StyleSheet _styleSheet;
		private StyleRule _styleRule;

		public CssReader(System.IO.Stream stream)
		{
			this._stream = stream;
			this._reader = new System.IO.StreamReader(stream);
		}

		public StyleSheet Read()
		{
			CssParser parser = new CssParser(new CssTextReader(this._reader));

			_styleSheet = new StyleSheet();
			while (this._reader.Peek() != -1)
			{
				Evaluate(parser.Parse());
			}

			return _styleSheet;
		}

		public void Close()
		{
			this._stream.Close();
		}

		protected override object EvaluateSelectorLEFT_CURLY_BRACEDeclarationListRIGHT_CURLY_BRACE(CssNode node)
		{
			string Selector = (string)Evaluate(node.Nodes[0].Nodes[0]);
			if (node.Nodes[0].Nodes[1].Nodes.Count == 0)
			{
				this._styleRule = new StyleRule(Selector);
			}
			else
			{
				string key = (string)Evaluate(node.Nodes[0].Nodes[1].Nodes[1]);
				string value = (string)Evaluate(node.Nodes[0].Nodes[1].Nodes[3]);
				this._styleRule = new KeyValueStyleRule(Selector, key, value);
			}
			this._styleSheet.Rules.Add(this._styleRule);

			// DeclarationList
			if (node.Nodes[2].Nodes.Count > 0)
			{
				Evaluate(node.Nodes[2]);
			}

			return null;
		}

		protected override object EvaluateIDENTIFIERCOLONValueListSEMICOLON(CssNode node)
		{
			string IDENTIFIER = (string)Evaluate(node.Nodes[0]);
			object[] ValueList = (object[])Evaluate(node.Nodes[2]);

			if (ValueList.Length == 1)
			{
				if (IDENTIFIER.Equals("TextAlign"))
				{
					this._styleRule.Declarations.Add(new StyleDeclaration(IDENTIFIER, System.Enum.Parse(typeof(System.Drawing.ContentAlignment), (string)ValueList[0])));
				}
				else
				{
					this._styleRule.Declarations.Add(new StyleDeclaration(IDENTIFIER, ValueList[0]));
				}
			}
			else if (IDENTIFIER.Equals("Font"))
			{
				Css.Font font;
				if (ValueList.Length == 2)
				{
					font = new Css.Font((string)ValueList[0], System.Convert.ToSingle(ValueList[1]));
				}
				else if (ValueList.Length == 3)
				{
					if (ValueList[2].Equals("bold"))
					{
						font = new Css.Font((string)ValueList[0], System.Convert.ToSingle(ValueList[1]), true);
					}
					else
					{
						throw new System.Exception("Invalid font.");
					}
				}
				else
				{
					throw new System.Exception("Invalid font.");
				}
				this._styleRule.Declarations.Add(new StyleDeclaration(IDENTIFIER, font));
			}
			else if (IDENTIFIER.Equals("Colors"))
			{
				string[] colors = new string[ValueList.Length];
				for (int i = 0; i < ValueList.Length; i++)
				{
					colors[i] = (string)ValueList[i];
				}
				this._styleRule.Declarations.Add(new StyleDeclaration(IDENTIFIER, colors));
			}
			else
			{
				throw new System.Exception("Invalid declaration.");
			}

			return null;
		}

		protected override object EvaluateRuleRuleList(CssNode node)
		{
			// Rule
			Evaluate(node.Nodes[0]);

			if (node.Nodes[1].Nodes.Count > 0)
			{
				// RuleList
				Evaluate(node.Nodes[1]);
			}

			return null;
		}

		protected override object EvaluateDeclarationDeclarationList(CssNode node)
		{
			// Declaration
			Evaluate(node.Nodes[0]);

			if (node.Nodes[1].Nodes.Count > 0)
			{
				// DeclarationList
				Evaluate(node.Nodes[1]);
			}

			return null;
		}

		protected override object EvaluateValueValueList(CssNode node)
		{
			object[] values;

			object Value = Evaluate(node.Nodes[0]);

			if (node.Nodes[1].Nodes.Count > 0)
			{
				object[] ValueList = (object[])Evaluate(node.Nodes[1]);
				values = new object[ValueList.Length + 1];
				values[0] = Value;
				System.Array.Copy(ValueList, 0, values, 1, ValueList.Length);
			}
			else
			{
				values = new object[] { Value };
			}

			return values;
		}

		protected override object EvaluateIDENTIFIER(string value)
		{
			return value;
		}

		protected override object EvaluateSTRING(string value)
		{
			return value.Substring(1, value.Length - 2);
		}

		protected override object EvaluateINTEGER(string value)
		{
			return Int32.Parse(value);
		}

		protected override object EvaluateFLOAT(string value)
		{
			return Single.Parse(value);
		}

		protected override object EvaluateBOOLEAN(string value)
		{
			return Boolean.Parse(value);
		}

	}
}
