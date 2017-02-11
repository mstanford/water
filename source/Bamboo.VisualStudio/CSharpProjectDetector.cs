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

namespace Bamboo.VisualStudio
{
	/// <summary>
	/// Summary description for CSharpProjectDetector.
	/// </summary>
	public class CSharpProjectDetector
	{

		public static bool Is2003(string path)
		{
			System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
			xmlDocument.Load(path);

			if(xmlDocument["VisualStudioProject"] == null)
			{
				return false;
			}
			if(xmlDocument["VisualStudioProject"]["CSHARP"] == null)
			{
				return false;
			}
			if(xmlDocument["VisualStudioProject"]["CSHARP"].Attributes["ProductVersion"] == null)
			{
				return false;
			}
			return (xmlDocument["VisualStudioProject"]["CSHARP"].Attributes["ProductVersion"].Value.StartsWith("7.1"));
		}

		public static bool Is2005(string path)
		{
			System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
			xmlDocument.Load(path);

			if(xmlDocument["Project"] == null)
			{
				return false;
			}
			if(xmlDocument["Project"]["PropertyGroup"] == null)
			{
				return false;
			}
			if(xmlDocument["Project"]["PropertyGroup"]["ProductVersion"] == null)
			{
				return false;
			}
			return (xmlDocument["Project"]["PropertyGroup"]["ProductVersion"].InnerText.StartsWith("8.0"));
		}

		public CSharpProjectDetector()
		{
		}

	}
}
