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

namespace Bamboo.CSharp.Compilers
{
	/// <summary>
	/// Summary description for CompilerParameters.
	/// </summary>
	public class CompilerParameters
	{
		private string _applicationIcon = String.Empty;
		private string _assembly = String.Empty;
		private bool _debug = false;
		private string _define = String.Empty;
		private string _doc = String.Empty;
		private string _lib = String.Empty;
		private string _target = "library";
		private string _platform = "x86";
		private System.Collections.Specialized.StringCollection _references = new System.Collections.Specialized.StringCollection();
		private System.Collections.Specialized.StringCollection _resources = new System.Collections.Specialized.StringCollection();
		private System.Collections.Specialized.StringCollection _sources = new System.Collections.Specialized.StringCollection();
		private Bamboo.CSharp.Compilers.ErrorCollection _errors = new Bamboo.CSharp.Compilers.ErrorCollection();
		private System.Collections.Specialized.StringCollection _output = new System.Collections.Specialized.StringCollection();

		public CompilerParameters()
		{
		}

		public string ApplicationIcon
		{
			get { return this._applicationIcon; }
			set { this._applicationIcon = value; }
		}

		public string Assembly
		{
			get { return this._assembly; }
			set { this._assembly = value; }
		}

		public bool Debug
		{
			get { return this._debug; }
			set { this._debug = value; }
		}

		public string Define
		{
			get { return this._define; }
			set { this._define = value; }
		}

		public string Doc
		{
			get { return this._doc; }
			set { this._doc = value; }
		}

		public string Lib
		{
			get { return this._lib; }
			set { this._lib = value; }
		}

		public string Target
		{
			get { return this._target; }
			set { this._target = value; }
		}

		public string Platform
		{
			get { return this._platform; }
			set { this._platform = value; }
		}

		public System.Collections.Specialized.StringCollection References
		{
			get { return this._references; }
		}

		public System.Collections.Specialized.StringCollection Resources
		{
			get { return this._resources; }
		}

		public System.Collections.Specialized.StringCollection Sources
		{
			get { return this._sources; }
		}

		public Bamboo.CSharp.Compilers.ErrorCollection Errors
		{
			get { return this._errors; }
		}

		public System.Collections.Specialized.StringCollection Output
		{
			get { return this._output; }
		}

	}
}
