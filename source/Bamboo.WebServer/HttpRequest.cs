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
	/// Summary description for HttpRequest.
	/// </summary>
	public class HttpRequest : System.IDisposable
	{
		private string _localAddress;
		private int _localPort;
		private string _remoteAddress;
		private int _remotePort;
		private string _method;
        private string _pathAndQuery;
        private string _path;
        private string _query;
		private System.Collections.Hashtable _headers = new System.Collections.Hashtable(0, new System.Collections.CaseInsensitiveHashCodeProvider(), new System.Collections.CaseInsensitiveComparer());
		private System.Collections.Hashtable _cookies = new System.Collections.Hashtable(0, new System.Collections.CaseInsensitiveHashCodeProvider(), new System.Collections.CaseInsensitiveComparer());
		private System.Collections.Hashtable _queryString = new System.Collections.Hashtable(0, new System.Collections.CaseInsensitiveHashCodeProvider(), new System.Collections.CaseInsensitiveComparer());
		private System.Collections.Hashtable _form = new System.Collections.Hashtable(0, new System.Collections.CaseInsensitiveHashCodeProvider(), new System.Collections.CaseInsensitiveComparer());
		private System.Collections.Hashtable _files = new System.Collections.Hashtable(0, new System.Collections.CaseInsensitiveHashCodeProvider(), new System.Collections.CaseInsensitiveComparer());
		private System.IO.Stream _input = new System.IO.MemoryStream();

        public HttpRequest()
		{
        }

		public string LocalAddress
		{
			get { return this._localAddress; }
			set { this._localAddress = value; }
		}

		public int LocalPort
		{
			get { return this._localPort; }
			set { this._localPort = value; }
		}

		public string RemoteAddress
		{
			get { return this._remoteAddress; }
			set { this._remoteAddress = value; }
		}

		public int RemotePort
		{
			get { return this._remotePort; }
			set { this._remotePort = value; }
		}

		public string Method
		{
			get { return this._method; }
			set { this._method = value; }
		}

		public string PathAndQuery
		{
			get { return this._pathAndQuery; }
            set { this._pathAndQuery = value; }
        }

        public string Path
        {
            get { return this._path; }
            set { this._path = value; }
        }

        public string Query
        {
            get { return this._query; }
            set { this._query = value; }
        }

        public System.Collections.Hashtable Headers
		{
			get { return this._headers; }
            set { this._headers = value; }
        }

		public System.Collections.Hashtable Cookies
		{
			get { return this._cookies; }
            set { this._cookies = value; }
        }

		public System.Collections.Hashtable QueryString
        {
            get { return this._queryString; }
            set { this._queryString = value; }
        }

		public System.Collections.Hashtable Form
		{
			get { return this._form; }
            set { this._form = value; }
        }

		public System.Collections.Hashtable Files
		{
			get { return this._files; }
			set { this._files = value; }
		}

		public System.IO.Stream Input
        {
            get { return this._input; }
            set { this._input = value; }
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
				if (this._queryString != null)
				{
					this._queryString.Clear();
				}
				if (this._form != null)
				{
					this._form.Clear();
				}
				if (this._files != null)
				{
					foreach (System.IO.Stream stream in this._files.Values)
					{
						stream.Close();
					}
					this._files.Clear();
				}
				if (this._input != null)
				{
					this._input.Close();
				}

				this._localAddress = null;
				this._remoteAddress = null;
				this._method = null;
				this._pathAndQuery = null;
				this._path = null;
				this._query = null;
				this._headers = null;
				this._cookies = null;
				this._queryString = null;
				this._form = null;
				this._files = null;
				this._input = null;
			}
			isDisposed = true;
		}

		#endregion

	}
}
