// ------------------------------------------------------------------------------
// 
// Copyright (c) 2008 Swampware, Inc.
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

namespace Bamboo.WebServer.Processors
{
	/// <summary>
	/// Summary description for DebugProcessor.
	/// </summary>
	public class DebugProcessor : HttpProcessor
	{
		private HttpProcessor _processor = null;

		public DebugProcessor(HttpProcessor processor)
		{
			this._processor = processor;
		}

		public override void Process(Bamboo.WebServer.HttpRequest request, Bamboo.WebServer.HttpResponse response)
		{
			try
			{
				this._processor.Process(request, response);
			}
			catch (System.Exception exception)
			{
				response.Reset();

				Error(exception);

				response.Status = 500;
				response.Headers.Add("Content-Type", "text/plain");

				response.WriteLine(exception.Message);
				response.WriteLine("");
				response.WriteLine(exception.StackTrace);

				while (exception.InnerException != null)
				{
					response.WriteLine("");
					response.WriteLine("");
					response.WriteLine("");

					exception = exception.InnerException;

					response.WriteLine(exception.Message);
					response.WriteLine("");
					response.WriteLine(exception.StackTrace);
				}
			}
		}

		protected virtual void Error(System.Exception exception)
		{
		}

	}
}
