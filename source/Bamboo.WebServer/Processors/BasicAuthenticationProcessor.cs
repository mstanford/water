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
	/// Summary description for BasicAuthentication.
	/// </summary>
	public class BasicAuthenticationProcessor : HttpProcessor
	{
		private string _realm;
		private HttpProcessor _processor;

		public BasicAuthenticationProcessor(string realm, HttpProcessor processor)
		{
			this._realm = realm;
			this._processor = processor;
		}

		public override void Process(HttpRequest request, HttpResponse response)
		{
			try
			{
				string authorization = (string)request.Headers["Authorization"];
				if (authorization == null)
				{
					Unauthorized(this._realm, request, response);
					return;
				}

				int index = authorization.IndexOf(" ");
				string authenticationType = authorization.Substring(0, index).Trim();
				string authenticationCode = System.Text.Encoding.ASCII.GetString(System.Convert.FromBase64String(authorization.Substring(index + 1).Trim()));

				if (!authenticationType.ToLower().Equals("basic"))
				{
					Unauthorized(this._realm, request, response);
					return;
				}

				index = authenticationCode.IndexOf(":");
				string user = authenticationCode.Substring(0, index);
				string password = authenticationCode.Substring(index + 1);
				index = user.IndexOf("\\");
				string domain = user.Substring(0, index);
				string username = user.Substring(index + 1);
				if (!Authenticate(username, password, domain))
				{
					Unauthorized(this._realm, request, response);
					return;
				}

				this._processor.Process(request, response);
				return;
			}
			catch (System.Exception exception)
			{
				string thisIsAHackToIgnoreCompilerWarning = exception.Message;

				Unauthorized(this._realm, request, response);
				return;
			}
		}

		public static void Unauthorized(string realm, HttpRequest request, HttpResponse response)
		{
			response.Status = 401;
			response.Headers.Add("WWW-Authenticate", "Basic realm=\"" + realm + "\"");
		}

		[System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword, int dwLogonType, int dwLogonProvider, out IntPtr phToken);

		[System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public extern static bool CloseHandle(IntPtr handle);

		const int LOGON32_LOGON_NETWORK = 3;
		const int LOGON32_PROVIDER_DEFAULT = 0;

		protected virtual bool Authenticate(string username, string password, string domain)
		{
			IntPtr token;
			bool loggedOn = LogonUser(username, domain, password, LOGON32_LOGON_NETWORK, LOGON32_PROVIDER_DEFAULT, out token);
			if (token != IntPtr.Zero)
			{
				CloseHandle(token);
			}
			return loggedOn;
		}

	}
}
