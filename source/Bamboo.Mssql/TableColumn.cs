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
	/// Summary description for TableColumn.
	/// </summary>
	public class TableColumn
	{
		private string _name = String.Empty;
		private string _datatype = String.Empty;
		private int _length;
		private bool _isnullable = true;
		private bool _isPrimaryKey = false;
		private bool _isForeignKey = false;

		public TableColumn()
		{
		}

		public string Name
		{
			get { return this._name; }
			set { this._name = value; }
		}

		public string Datatype
		{
			get { return this._datatype; }
			set { this._datatype = value; }
		}

		public int Length
		{
			get { return this._length; }
			set { this._length = value; }
		}

		public bool IsNullable
		{
			get { return this._isnullable; }
			set { this._isnullable = value; }
		}

		public bool IsPrimaryKey
		{
			get { return this._isPrimaryKey; }
			set { this._isPrimaryKey = value; }
		}

		public bool IsForeignKey
		{
			get { return this._isForeignKey; }
			set { this._isForeignKey = value; }
		}

	}
}
	
