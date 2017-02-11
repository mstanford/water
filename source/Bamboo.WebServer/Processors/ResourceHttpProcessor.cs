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
	/// Summary description for ResourceHttpProcessor.
	/// </summary>
	public class ResourceHttpProcessor : HttpProcessor
	{
		private string _contentType;
		private byte[] _content;
		private string _etag;

		public ResourceHttpProcessor(string contentType, string content)
			: this(contentType, System.Text.Encoding.UTF8.GetBytes(content))
		{
		}

		public ResourceHttpProcessor(string contentType, byte[] content)
		{
			this._contentType = contentType;
			this._content = content;
			this._etag = CreateChecksum(content);
		}

		private static string CreateChecksum(byte[] bytes)
		{
			System.Security.Cryptography.SHA1CryptoServiceProvider sha1CryptoServiceProvider = new System.Security.Cryptography.SHA1CryptoServiceProvider();
			byte[] hashBytes = sha1CryptoServiceProvider.ComputeHash(bytes);
			return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
		}

		public string ContentType
		{
			get { return this._contentType; }
		}

		public byte[] Content
		{
			get { return this._content; }
		}

		public string ETag
		{
			get { return this._etag; }
		}

		public override void Process(Bamboo.WebServer.HttpRequest request, Bamboo.WebServer.HttpResponse response)
		{
			if(request.Method.ToUpper().Equals("GET"))
			{
				response.Headers.Add("Content-Type", this._contentType);
				response.Headers.Add("ETag", this._etag);
				response.Output.Write(this._content, 0, this._content.Length);
			}
			else
			{
				throw new System.Exception("Invalid method: " + request.Method);
			}
		}

	}
}
