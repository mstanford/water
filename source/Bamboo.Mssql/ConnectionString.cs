// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005 Swampware, Inc.
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

namespace Bamboo.Mssql
{
	/// <summary>
	/// Summary description for ConnectionString.
	/// </summary>
	public class ConnectionString
	{
		private string _server = String.Empty;
		private string _database = String.Empty;
		private string _username = String.Empty;
		private string _password = String.Empty;
		private bool _trustedconnection = true;

		public ConnectionString()
		{
		}

		public string Server
		{
			get { return this._server; }
			set { this._server = value; }
		}

		public string Database
		{
			get { return this._database; }
			set { this._database = value; }
		}

		public string Username
		{
			get { return this._username; }
			set { this._username = value; }
		}

		public string Password
		{
			get { return this._password; }
			set { this._password = value; }
		}

		public bool TrustedConnection
		{
			get { return this._trustedconnection; }
			set { this._trustedconnection = value; }
		}

		public override string ToString()
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

			if( this._trustedconnection )
			{
				stringBuilder.Append("Server=" + this.Server + ";");
				stringBuilder.Append("Database=" + this.Database + ";");
				stringBuilder.Append("Trusted_Connection=True;");
			}
			else
			{
				stringBuilder.Append("Server=" + this.Server + ";");
				stringBuilder.Append("Database=" + this.Database + ";");
				stringBuilder.Append("User ID=" + this.Username + ";");
				stringBuilder.Append("Password=" + this.Password + ";");
			}

			return stringBuilder.ToString();
		}

		public static implicit operator string(ConnectionString connectionString)
		{
			return connectionString.ToString();
		}

	}
}
