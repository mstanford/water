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
	public class Scraper
	{

		public static Html.Element Scrape(string url, string from, string to, Bamboo.DataStructures.BufferPool bufferPool) 
		{
			try
			{
				System.IO.Stream stream = Bamboo.Http.HttpClient.Get(url, bufferPool);
				System.IO.StreamReader reader = new System.IO.StreamReader(stream);
				string html = reader.ReadToEnd();
				stream.Close();

				int index = html.IndexOf(from);
				html = html.Substring(index);
				index = html.IndexOf(to);
				html = html.Substring(0, index);
				html = "<html>" + html + "</html>";

				Bamboo.Html.HtmlReader htmlReader = new Bamboo.Html.HtmlReader(new System.IO.StringReader(html));
				Bamboo.Html.Element element = htmlReader.Read(html);
				htmlReader.Close();

				return element;
			}
			catch (System.Exception exception)
			{
				//System.IO.StreamWriter writer = System.IO.File.CreateText(@"C:\Documents and Settings\matt\Desktop\abc.html");
				//writer.Write(html);
				//writer.Flush();
				//writer.Close();

				return null;
			}
		}

		private Scraper()
		{
		}

	}
}
