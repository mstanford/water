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
	/// Summary description for Item.
	/// </summary>
	public class Item
	{
		private System.String _type = String.Empty;
		private System.String _include = String.Empty;
		private System.String _subType = String.Empty;
		private System.String _autoGen = String.Empty;
		private System.String _dependentUpon = String.Empty;
		private System.String _designTimeSharedInput = String.Empty;
		private System.String _generator = String.Empty;
		private System.String _lastGenOutput = String.Empty;
		private System.String _designTime = String.Empty;

		public Item()
		{
		}

		public System.String Type
		{
			get { return this._type; }
			set { this._type = value; }
		}

		public System.String Include
		{
			get { return this._include; }
			set { this._include = value; }
		}

		public System.String SubType
		{
			get { return this._subType; }
			set { this._subType = value; }
		}

		public System.String AutoGen
		{
			get { return this._autoGen; }
			set { this._autoGen = value; }
		}

		public System.String DependentUpon
		{
			get { return this._dependentUpon; }
			set { this._dependentUpon = value; }
		}

		public System.String DesignTimeSharedInput
		{
			get { return this._designTimeSharedInput; }
			set { this._designTimeSharedInput = value; }
		}

		public System.String Generator
		{
			get { return this._generator; }
			set { this._generator = value; }
		}

		public System.String LastGenOutput
		{
			get { return this._lastGenOutput; }
			set { this._lastGenOutput = value; }
		}

		public System.String DesignTime
		{
			get { return this._designTime; }
			set { this._designTime = value; }
		}

	}
}
