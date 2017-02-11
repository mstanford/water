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
	public class DictionaryHttpProcessor : HttpProcessor
	{
		private Dictionary<string, HttpProcessor> _processors = new Dictionary<string, HttpProcessor>();

		public DictionaryHttpProcessor(Bamboo.DataStructures.BufferPool bufferPool)
			: base(bufferPool)
		{
		}

		public void Add(string name, HttpProcessor processor)
		{
			this._processors.Add(name.ToLower(), processor);
		}

		public override void ProcessHttp(HttpRequest request, HttpResponse response)
		{
			string path = request.Path.ToLower();

			if (this._processors.ContainsKey(path))
			{
				this._processors[path].ProcessHttp(request, response);
			}
			else
			{
				response.Status = 404;
				response.ContentType = "text/html";

				response.Writer.Write("<html>");
				response.Writer.Write("<head>");
				response.Writer.Write("<title>404 Not Found</title>");
				response.Writer.Write("</head>");
				response.Writer.Write("<body>");
				response.Writer.Write("<h1>404 Not Found</h1>");
				response.Writer.Write("<p>The requested URL was not found on this server.</p>");
				response.Writer.Write("</body>");
				response.Writer.Write("</html>");
			}
		}

	}
}
