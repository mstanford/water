// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006 Swampware, Inc.
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
	/// Summary description for Relationship.
	/// </summary>
	public class Relationship
	{
		private string _primarykeytable = String.Empty;
		private string _primarykeycolumn = String.Empty;
		private string _foreignkeytable = String.Empty;
		private string _foreignkeycolumn = String.Empty;

		public Relationship()
		{
		}

		public string PrimaryKeyTable
		{
			get { return this._primarykeytable; }
			set { this._primarykeytable = value; }
		}

		public string PrimaryKeyColumn
		{
			get { return this._primarykeycolumn; }
			set { this._primarykeycolumn = value; }
		}

		public string ForeignKeyTable
		{
			get { return this._foreignkeytable; }
			set { this._foreignkeytable = value; }
		}

		public string ForeignKeyColumn
		{
			get { return this._foreignkeycolumn; }
			set { this._foreignkeycolumn = value; }
		}

	}
}
	
