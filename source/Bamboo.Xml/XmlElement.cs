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
	/// Summary description for XmlElement.
	/// </summary>
	public sealed class XmlElement : System.Collections.IEnumerable
	{
		private string _name = String.Empty;
		private string _value = String.Empty;
		private XmlAttributeCollection _attributes = new XmlAttributeCollection();
		private XmlElementCollection _elements = new XmlElementCollection();

		public XmlElement()
		{
		}

		public string Name
		{
			get { return this._name; }
			set { this._name = value; }
		}

		public string Value
		{
			get { return this._value; }
			set { this._value = value; }
		}

		public object this[string key]
		{
			get
			{
				object value = this._attributes[key];
				if (value != null)
				{
					return value;
				}
				XmlElement element = this._elements[key];
				if (element != null)
				{
					return element;
				}
				throw new System.Exception("Node does not exist: " + key);
			}
		}

		public XmlAttributeCollection Attributes
		{
			get { return this._attributes; }
		}

		public XmlElementCollection Elements
		{
			get { return this._elements; }
		}

		#region IEnumerable Members

		public System.Collections.IEnumerator GetEnumerator()
		{
			return this.Elements.GetEnumerator();
		}

		#endregion

	}
}
