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

namespace Bamboo.Http
{
	public class HttpRequest
	{
		public string Method;
		public string Path;
		public string Query;
		public string Host;
		public int ContentLength;
		public string ContentType;
		public Bamboo.DataStructures.MemoryStream Content;

		public HttpRequest(string method, string path, string host)
		{
			this.Method = method;
			int index = path.IndexOf("?");
			if (index == -1)
			{
				this.Path = path;
				this.Query = "";
			}
			else
			{
				this.Path = path.Substring(0, index);
				this.Query = path.Substring(index + 1);
			}
			this.Host = host;
			this.ContentLength = 0;
		}

		public HttpRequest(string method, string path, string host, int contentLength, string contentType, Bamboo.DataStructures.MemoryStream content)
		{
			this.Method = method;
			int index = path.IndexOf("?");
			if (index == -1)
			{
				this.Path = path;
				this.Query = "";
			}
			else
			{
				this.Path = path.Substring(0, index);
				this.Query = path.Substring(index + 1);
			}
			this.Host = host;
			this.ContentLength = contentLength;
			this.ContentType = contentType;
			this.Content = content;
		}

	}
}
