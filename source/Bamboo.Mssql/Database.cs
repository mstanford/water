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
	/// Summary description for Database.
	/// </summary>
	public class Database
	{
		private string _name = String.Empty;
		private TableCollection _tables = new TableCollection();
		private RelationshipCollection _relationships = new RelationshipCollection();
		private ViewCollection _views = new ViewCollection();
		private ProcedureCollection _procedures = new ProcedureCollection();

		public Database()
		{
		}

		public string Name
		{
			get { return this._name; }
			set { this._name = value; }
		}

		public TableCollection Tables
		{
			get { return this._tables; }
			set { this._tables = value; }
		}

		public RelationshipCollection Relationships
		{
			get { return this._relationships; }
			set { this._relationships = value; }
		}

		public ViewCollection Views
		{
			get { return this._views; }
			set { this._views = value; }
		}

		public ProcedureCollection Procedures
		{
			get { return this._procedures; }
			set { this._procedures = value; }
		}

	}
}
	
