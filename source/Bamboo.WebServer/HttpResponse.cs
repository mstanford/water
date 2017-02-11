// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006-2008 Swampware, Inc.
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

namespace Bamboo.WebServer
{
	/// <summary>
	/// Summary description for HttpResponse.
	/// </summary>
	public class HttpResponse : System.IDisposable
	{
		private int _status = 200;
		private System.Collections.Hashtable _headers = new System.Collections.Hashtable(new System.Collections.CaseInsensitiveHashCodeProvider(), new System.Collections.CaseInsensitiveComparer());
		private System.Collections.Hashtable _cookies = new System.Collections.Hashtable(new System.Collections.CaseInsensitiveHashCodeProvider(), new System.Collections.CaseInsensitiveComparer());
		private System.IO.Stream _output = new System.IO.MemoryStream();

		public HttpResponse()
		{
		}

		public int Status
		{
			get { return this._status; }
			set { this._status = value; }
		}

        public System.Collections.Hashtable Cookies
		{
			get { return this._cookies; }
			set { this._cookies = value; }
		}

        public System.Collections.Hashtable Headers
        {
            get { return this._headers; }
            set { this._headers = value; }
        }

		public System.IO.Stream Output
        {
            get { return this._output; }
            set { this._output = value; }
        }

		public void WriteLine(string s)
		{
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes(s + "\r\n");
			this._output.Write(buffer, 0, buffer.Length);
		}

		public void Write(string s)
		{
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes(s);
			this._output.Write(buffer, 0, buffer.Length);
		}

		public void Write(byte[] buffer)
		{
			this._output.Write(buffer, 0, buffer.Length);
		}

		public void Redirect(string url)
		{
			this.Status = 302;
			this.Headers["Location"] = url;
		}

		#region IDisposable Members

		private bool isDisposed = false;

		public void Dispose()
		{
			if (!isDisposed)
			{
				if (this._headers != null)
				{
					this._headers.Clear();
				}
				if (this._cookies != null)
				{
					this._cookies.Clear();
				}
				if (this._output != null)
				{
					this._output.Close();
				}

				this._headers = null;
				this._cookies = null;
				this._output = null;
			}
			isDisposed = true;
		}

		#endregion

		public void Reset()
		{
			this._headers.Clear();
			this._cookies.Clear();

			this._output.Close();
			this._output = new System.IO.MemoryStream();
		}

	}
}
