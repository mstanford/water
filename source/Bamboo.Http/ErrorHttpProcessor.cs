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
	public class ErrorHttpProcessor : HttpProcessor
	{
		private HttpProcessor _processor;
		private Bamboo.DataStructures.BufferPool _bufferPool;

		public ErrorHttpProcessor(HttpProcessor processor, Bamboo.DataStructures.BufferPool bufferPool)
			: base(bufferPool)
		{
			this._processor = processor;
			this._bufferPool = bufferPool;
		}

		public override void ProcessHttp(HttpRequest request, HttpResponse response)
		{
			try
			{
				this._processor.ProcessHttp(request, response);
			}
			catch (System.Exception exception)
			{
				response.Writer.Flush();
				response.ContentLength = (int)response.Content.Length;
				if (response.ContentLength > 0)
				{
					response.Content.Position = 0;
				}



				//TODO log error



				response.Content.Close();

				response.Content = new Bamboo.DataStructures.MemoryStream(this._bufferPool);
				response.Writer = new System.IO.StreamWriter(response.Content);

				response.Status = 500;
				response.ContentType = "text/html";

				response.Writer.Write("<html>");
				response.Writer.Write("<head>");
				response.Writer.Write("<title>500 Internal Server Error</title>");
				response.Writer.Write("</head>");
				response.Writer.Write("<body>");
				response.Writer.Write("<h1>500 Internal Server Error</h1>");
				response.Writer.Write("<p>The requested URL caused an error on this server.</p>");
				response.Writer.Write("</body>");
				response.Writer.Write("</html>");
			}
		}

	}
}
