// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005-2006 Swampware, Inc.
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

namespace Bamboo.VisualStudio.ProjectDependencies
{
	/// <summary>
	/// Summary description for Project.
	/// </summary>
	public class Project
	{
		private string _name = String.Empty;
		private object _tag = null;
		private System.Guid _guid;
		private ProjectDependencyCollection _dependencies = new ProjectDependencyCollection();

		public Project()
		{
		}

		public string Name
		{
			get { return this._name; }
			set { this._name = value; }
		}

		public object Tag
		{
			get { return this._tag; }
			set { this._tag = value; }
		}

		public System.Guid Guid
		{
			get { return this._guid; }
			set { this._guid = value; }
		}

		public ProjectDependencyCollection Dependencies
		{
			get { return this._dependencies; }
			set { this._dependencies = value; }
		}

	}
}
