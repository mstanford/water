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
	public class HttpResponse
	{
		private const string NEWLINE = "\r\n";

		public int Status;
		public int ContentLength;
		public string ContentType;
		public Bamboo.DataStructures.MemoryStream Content;
		public System.IO.StreamWriter Writer;
		public string RedirectUrl;

		public HttpResponse(Bamboo.DataStructures.BufferPool bufferPool)
		{
			this.Status = 200;
			this.Content = new Bamboo.DataStructures.MemoryStream(bufferPool);
			this.Writer = new System.IO.StreamWriter(this.Content);
		}

		public void Write(string s)
		{
			this.Writer.Write(s);
		}

		public void WriteLine(string s)
		{
			this.Writer.Write(s + NEWLINE);
		}

		public void Redirect(string url)
		{
			this.Status = 302;
			this.RedirectUrl = url;
		}

	}
}
