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
	/// Summary description for XmlElementCollection.
	/// </summary>
	public sealed class XmlElementCollection : System.Collections.CollectionBase
	{

		public XmlElementCollection()
		{
		}

		public XmlElement this[int index]
		{
			get { return (XmlElement)this.List[index]; }
		}

		public XmlElement this[string name]
		{
			get
			{
				foreach (XmlElement element in this.List)
				{
					if (element.Name.Equals(name))
					{
						return element;
					}
				}
				return null;
			}
		}

		public void Add(XmlElement element)
		{
			this.List.Add(element);
		}

	}
}
