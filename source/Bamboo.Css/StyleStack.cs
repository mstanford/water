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
	public class StyleStack
	{
		private Bamboo.DataStructures.Stack<StyleSheet> _styleSheets = new Bamboo.DataStructures.Stack<StyleSheet>();
		private Bamboo.DataStructures.Stack<string> _tags = new Bamboo.DataStructures.Stack<string>();

		public StyleStack()
		{
		}

		public object this[string name]
		{
		    get
		    {
				for (int i = (this._tags.Count - 1); i >= 0; i--)
				{
					string tag = this._tags[i];

					for (int j = (this._styleSheets.Count - 1); j >= 0; j--)
					{
						foreach (StyleRule styleRule in this._styleSheets[j].Rules)
						{
							if (styleRule.Name.Equals(tag))
							{
								foreach (StyleDeclaration styleDeclaration in styleRule.Declarations)
								{
									if (styleDeclaration.Name.Equals(name))
									{
										return styleDeclaration.Value;
									}
								}
							}
						}
					}
				}

				throw new System.Exception("Style is not defined: " + name);
		    }
		}

		public void PushStyleSheet(StyleSheet styleSheet)
		{
			this._styleSheets.Push(styleSheet);
		}

		public void PopStyleSheet()
		{
			this._styleSheets.Pop();
		}

		public void PushTag(string tag)
		{
			this._tags.Push(tag);
		}

		public void PopTag()
		{
			this._tags.Pop();
		}

	}
}
