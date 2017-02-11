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

namespace Bamboo.Html
{
	public class HtmlReader : Html.HtmlEvaluator
	{
		private System.IO.TextReader _reader;
		private Stack<Element> _elements = new Stack<Element>();
		private Element _element = null;

		public HtmlReader(System.IO.TextReader reader)
		{
			this._reader = reader;
		}

		public Element Read(string s)
		{
			Bamboo.Html.HtmlParser parser = new Bamboo.Html.HtmlParser(new HtmlTextReader(this._reader));

			while (this._reader.Peek() != -1)
			{
				Evaluate(parser.Parse());
			}

			return this._element;
		}

		public void Close()
		{
			this._reader.Close();
		}

		protected override object EvaluateContent(HtmlNode node)
		{
			if (this._element != null)
			{
				this._elements.Peek().Content.Add(Evaluate(node.Nodes[0]));
			}

			return null;
		}

		protected override object EvaluateStartTag(HtmlNode node)
		{
			Element element = new Element((string)Evaluate(node.Nodes[1]));
			HtmlNode attributeListNode = node.Nodes[2];
			while (attributeListNode.Nodes.Count > 0)
			{
				Attribute attribute = (Attribute)Evaluate(attributeListNode.Nodes[0]);
				element.Attributes.Add(attribute);
				attributeListNode = attributeListNode.Nodes[1];
			}

			if (this._element == null)
			{
				this._element = element;
			}
			else
			{
				this._elements.Peek().Elements.Add(element);
			}

			this._elements.Push(element);

			return null;
		}

		protected override object EvaluateEndTag(HtmlNode node)
		{
			string name = (string)Evaluate(node.Nodes[1]);

			while(!this._elements.Peek().Name.Equals(name))
			{
				this._elements.Pop();
			}
			this._elements.Pop();

			return null;
		}

		protected override object EvaluateSTRINGAttributeTail(HtmlNode node)
		{
			string name = (string)Evaluate(node.Nodes[0]);
			string value = (node.Nodes[1].Nodes.Count == 0) ? null : Evaluate(node.Nodes[1].Nodes[1]).ToString();
			Attribute attribute = new Attribute(name, value);
			return attribute;
		}

		protected override object EvaluateINTEGER(string value)
		{
			return Int32.Parse(value);
		}

		protected override object EvaluateFLOAT(string value)
		{
			return Single.Parse(value);
		}

		protected override object EvaluateQUOTED_STRING(string value)
		{
			return value.Substring(1, value.Length - 2);
		}

		protected override object EvaluateSTRING(string value)
		{
			return value;
		}

	}
}
