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

namespace Bamboo.WebServer
{
	/// <summary>
	/// Summary description for Cookie.
	/// </summary>
	public class Cookie : System.IDisposable
	{
		private string _name;
		private string _value;
		private string _domain;
		private string _expires;
		private string _path;

		public Cookie(string name, string value)
		{
			this._name = name;
			this._value = value;
			this._domain = "";
			this._expires = "";
			this._path = "";
		}

		public Cookie(string name, string value, System.DateTime expires)
		{
			this._name = name;
			this._value = value;
			this._domain = "";
			this._expires = ConvertExpires(expires);
			this._path = "";
		}

		public Cookie(string name, string value, string domain, System.DateTime expires, string path)
		{
			this._name = name;
			this._value = value;
			this._domain = domain;
			this._expires = ConvertExpires(expires);
			this._path = path;
		}

		public string Name
		{
			get { return this._name; }
		}

		public string Value
		{
			get { return this._value; }
		}

		public string Domain
		{
			get { return this._domain; }
		}

		public string Expires
		{
			get { return this._expires; }
		}

		public string Path
		{
			get { return this._path; }
		}

		private static string ConvertExpires(System.DateTime expires)
		{
			System.DateTime utc = expires.ToUniversalTime();
			string s = utc.DayOfWeek + ", " + utc.Day + "-" + GetMonth(utc.Month) + "-" + utc.Year + " " + utc.Hour + ":" + utc.Minute + ":" + utc.Second + " GMT";
			return s;
		}

		private static string GetMonth(int month)
		{
			switch(month)
			{
				case 1:
				{
					return "Jan";
				}
				case 2:
				{
					return "Feb";
				}
				case 3:
				{
					return "Mar";
				}
				case 4:
				{
					return "Apr";
				}
				case 5:
				{
					return "May";
				}
				case 6:
				{
					return "Jun";
				}
				case 7:
				{
					return "Jul";
				}
				case 8:
				{
					return "Aug";
				}
				case 9:
				{
					return "Sep";
				}
				case 10:
				{
					return "Oct";
				}
				case 11:
				{
					return "Nov";
				}
				case 12:
				{
					return "Dec";
				}
				default:
				{
					throw new System.Exception("Invalid month.");
				}
			}
		}

		#region IDisposable Members

		private bool isDisposed = false;

		public void Dispose()
		{
			if (!isDisposed)
			{
				this._name = null;
				this._value = null;
				this._domain = null;
				this._expires = null;
				this._path = null;
			}
			isDisposed = true;
		}

		#endregion

	}
}
