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

namespace Bamboo.VisualStudio.VisualStudio2003.CSharpProject.Models
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class Item
	{
		private System.String _relpath = String.Empty;
		private System.String _subtype = String.Empty;
		private System.String _dependentUpon = String.Empty;
		private System.String _buildaction = String.Empty;
		private System.String _generator = String.Empty;
		private System.String _lastGenOutput = String.Empty;
		private System.String _designTime = String.Empty;
		private System.String _autoGen = String.Empty;

		public Item()
		{
		}

		public System.String RelPath
		{
			get { return this._relpath; }
			set
			{
				this._relpath = value;
				this.OnRelPathChanged();
			}
		}

		public System.String SubType
		{
			get { return this._subtype; }
			set
			{
				this._subtype = value;
				this.OnSubTypeChanged();
			}
		}

		public System.String DependentUpon
		{
			get { return this._dependentUpon; }
			set
			{
				this._dependentUpon = value;
			}
		}

		public System.String BuildAction
		{
			get { return this._buildaction; }
			set
			{
				this._buildaction = value;
				this.OnBuildActionChanged();
			}
		}

		public System.String Generator
		{
			get { return this._generator; }
			set
			{
				this._generator = value;
				//this.OnBuildActionChanged();
			}
		}

		public System.String LastGenOutput
		{
			get { return this._lastGenOutput; }
			set
			{
				this._lastGenOutput = value;
				//this.OnBuildActionChanged();
			}
		}

		public System.String DesignTime
		{
			get { return this._designTime; }
			set
			{
				this._designTime = value;
				//this.OnBuildActionChanged();
			}
		}

		public System.String AutoGen
		{
			get { return this._autoGen; }
			set
			{
				this._autoGen = value;
				//this.OnBuildActionChanged();
			}
		}


		public event System.EventHandler RelPathChanged;

		protected void OnRelPathChanged()
		{
			if(RelPathChanged != null)
			{
				RelPathChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler SubTypeChanged;

		protected void OnSubTypeChanged()
		{
			if(SubTypeChanged != null)
			{
				SubTypeChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler BuildActionChanged;

		protected void OnBuildActionChanged()
		{
			if(BuildActionChanged != null)
			{
				BuildActionChanged(this, System.EventArgs.Empty);
			}
		}

	}
}
