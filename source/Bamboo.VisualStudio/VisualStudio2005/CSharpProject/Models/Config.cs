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
	/// Summary description for Config.
	/// </summary>
	public class Config
	{
		private System.String _condition = String.Empty;
		private System.String _debugSymbols = String.Empty;
		private System.String _debugType = String.Empty;
		private System.String _optimize = String.Empty;
		private System.String _outputPath = String.Empty;
		private System.String _defineConstants = String.Empty;
		private System.String _errorReport = String.Empty;
		private System.String _warningLevel = String.Empty;

		public Config()
		{
		}

		public System.String Condition
		{
			get { return this._condition; }
			set { this._condition = value; }
		}

		public System.String DebugSymbols
		{
			get { return this._debugSymbols; }
			set { this._debugSymbols = value; }
		}

		public System.String DebugType
		{
			get { return this._debugType; }
			set { this._debugType = value; }
		}

		public System.String Optimize
		{
			get { return this._optimize; }
			set { this._optimize = value; }
		}

		public System.String OutputPath
		{
			get { return this._outputPath; }
			set { this._outputPath = value; }
		}

		public System.String DefineConstants
		{
			get { return this._defineConstants; }
			set { this._defineConstants = value; }
		}

		public System.String ErrorReport
		{
			get { return this._errorReport; }
			set { this._errorReport = value; }
		}

		public System.String WarningLevel
		{
			get { return this._warningLevel; }
			set { this._warningLevel = value; }
		}

	}
}
