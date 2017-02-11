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

namespace Bamboo.Query.Query
{
	public class SelectStatement
	{
		private System.Collections.IList _select = new System.Collections.ArrayList(0);
		private string _from = String.Empty;
		private System.Collections.IList _joins = new System.Collections.ArrayList(0);
		private Bamboo.Query.Query.WhereClause _where = null;
		private System.Collections.IList _groupBy = new System.Collections.ArrayList(0);
		private Bamboo.Query.Query.OrderByClause _orderBy = null;

		public SelectStatement()
		{
		}

		public System.Collections.IList Select
		{
			get { return this._select; }
			set { this._select = value; }
		}

		public string From
		{
			get { return this._from; }
			set { this._from = value; }
		}

		public System.Collections.IList Joins
		{
			get { return this._joins; }
			set { this._joins = value; }
		}

		public Bamboo.Query.Query.WhereClause Where
		{
			get { return this._where; }
			set { this._where = value; }
		}

		public System.Collections.IList GroupBy
		{
			get { return this._groupBy; }
			set { this._groupBy = value; }
		}

		public Bamboo.Query.Query.OrderByClause OrderBy
		{
			get { return this._orderBy; }
			set { this._orderBy = value; }
		}

	}
}
