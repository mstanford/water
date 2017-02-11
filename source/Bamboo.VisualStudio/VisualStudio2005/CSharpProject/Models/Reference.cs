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

namespace Bamboo.VisualStudio.VisualStudio2005.CSharpProject.Models
{
	/// <summary>
	/// Summary description for Reference.
	/// </summary>
	public class Reference
	{
		private System.String _name = String.Empty;
		private System.String _include = String.Empty;
		private System.String _hintPath = String.Empty;
		private System.String _specificVersion = String.Empty;

		public Reference()
		{
		}

		public System.String Name
		{
			get { return this._name; }
			set { this._name = value; }
		}

		public System.String Include
		{
			get { return this._include; }
			set { this._include = value; }
		}

		public System.String HintPath
		{
			get { return this._hintPath; }
			set { this._hintPath = value; }
		}

		public System.String SpecificVersion
		{
			get { return this._specificVersion; }
			set { this._specificVersion = value; }
		}

	}
}
