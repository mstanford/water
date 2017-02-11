// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005 Swampware, Inc.
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
	/// Summary description for XmlWriter.
	/// </summary>
	public class XmlWriter
	{

		public static void Write(Bamboo.Xml.XmlElement element, System.IO.Stream stream)
		{
			Write(element, new System.IO.StreamWriter(stream));
		}

		public static void Write(Bamboo.Xml.XmlElement element, System.IO.TextWriter writer)
		{
			WriteElement(element, new Bamboo.Xml.IndentedTextWriter(writer));
			writer.Flush();
		}

		private static void WriteElement(Bamboo.Xml.XmlElement element, Bamboo.Xml.IndentedTextWriter writer)
		{
			writer.Write("<" + element.Name);
			foreach(Bamboo.Xml.XmlAttribute attribute in element.Attributes)
			{
				writer.Write(" " + attribute.Name + "=\"" + attribute.Value + "\"");
			}
			writer.Write(">");
			writer.Write(System.Web.HttpUtility.HtmlEncode(element.Value));
			if(element.Elements.Count > 0)
			{
				writer.WriteLineNoTabs();
				writer.Indent();
				foreach(Bamboo.Xml.XmlElement element2 in element.Elements)
				{
					writer.WriteIndentations();
					WriteElement(element2, writer);
					writer.WriteLineNoTabs();
				}
				writer.Unindent();
			}
			writer.Write("</" + element.Name + ">");
		}

		private XmlWriter()
		{
		}

	}
}
