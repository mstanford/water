// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005-2008 Swampware, Inc.
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
	/// Summary description for XmlReader.
	/// </summary>
	public sealed class XmlReader
	{

		public static Bamboo.Xml.XmlElement Read(System.IO.Stream stream)
		{
			return Read(new System.Xml.XmlTextReader(stream));
		}

		public static Bamboo.Xml.XmlElement Read(System.IO.TextReader textReader)
		{
			return Read(new System.Xml.XmlTextReader(textReader));
		}

		private static Bamboo.Xml.XmlElement Read(System.Xml.XmlTextReader xmlTextReader)
		{
			System.Collections.Stack elementsStack = new System.Collections.Stack();

			while(xmlTextReader.Read())
			{
				switch(xmlTextReader.NodeType)
				{
					case System.Xml.XmlNodeType.Element :
					{
						Bamboo.Xml.XmlElement element = new Bamboo.Xml.XmlElement();
						element.Name = xmlTextReader.LocalName;

						if(xmlTextReader.IsEmptyElement)
						{
							if(elementsStack.Count == 0)
							{
								elementsStack.Push(element);
							}
							else
							{
								Bamboo.Xml.XmlElement parent = (XmlElement)elementsStack.Peek();
								parent.Elements.Add(element);
							}
						}
						else
						{
							if(elementsStack.Count > 0)
							{
								Bamboo.Xml.XmlElement parent = (XmlElement)elementsStack.Peek();
								parent.Elements.Add(element);
							}
							elementsStack.Push(element);
						}

						for(int i = 0; i < xmlTextReader.AttributeCount; i++)
						{
							xmlTextReader.MoveToAttribute(i);

							Bamboo.Xml.XmlAttribute xmlAttribute = new Bamboo.Xml.XmlAttribute();
							xmlAttribute.Name = xmlTextReader.LocalName;
							xmlAttribute.Value = xmlTextReader.Value;

							element.Attributes.Add(xmlAttribute);
						}

						break;
					}
					case System.Xml.XmlNodeType.EndElement :
					{
						if(elementsStack.Count > 1)
						{
							elementsStack.Pop();
						}
						break;
					}
					case System.Xml.XmlNodeType.CDATA :
					case System.Xml.XmlNodeType.Text :
					{
						((XmlElement)elementsStack.Peek()).Value = xmlTextReader.Value;
						break;
					}
				}
			}

			if(elementsStack.Count == 0)
			{
				throw new System.Exception("Document does not contain any elements.");
			}
			else if(elementsStack.Count > 1)
			{
				throw new System.Exception("Document contains more than one root element.");
			}

			return (XmlElement)elementsStack.Pop();
		}

		private XmlReader()
		{
		}

	}
}
