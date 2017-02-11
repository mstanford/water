// ------------------------------------------------------------------------------
// 
// Copyright (c) 2007 Swampware, Inc.
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
	/// Summary description for DictionaryHttpProcessor.
	/// </summary>
	public class DictionaryHttpProcessor : HttpProcessor
	{
		private System.Collections.Hashtable _processors = new System.Collections.Hashtable(new System.Collections.CaseInsensitiveHashCodeProvider(), new System.Collections.CaseInsensitiveComparer());
		private HttpProcessor _defaultProcessor = null;

		public DictionaryHttpProcessor()
		{
		}

		protected System.Collections.Hashtable Processors
		{
			get { return this._processors; }
		}

		public void Add(string path, HttpProcessor processor)
		{
			this._processors.Add(path, processor);
		}

		public void Default(HttpProcessor processor)
		{
			this._defaultProcessor = processor;
		}

		public bool Exists(string path)
        {
            return this._processors.ContainsKey(path);
		}

		public void Remove(string path)
		{
			this._processors.Remove(path);
		}

		public override void Process(Bamboo.WebServer.HttpRequest request, Bamboo.WebServer.HttpResponse response)
		{
			//TODO check containskey instead.
			HttpProcessor processor = (HttpProcessor)this._processors[request.Path];
			if(processor != null)
			{
				processor.Process(request, response);
				return;
			}
			else if (this._defaultProcessor != null)
			{
				this._defaultProcessor.Process(request, response);
				return;
			}
			else
			{
				response.Reset();

				response.Status = 404;
				response.Headers.Add("Content-Type", "text/plain");
				response.WriteLine("Page not found.");
			}
        }

	}
}
