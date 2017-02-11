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
	/// Summary description for Reference.
	/// </summary>
	public class Reference
	{
		private System.String _name = String.Empty;
		private System.String _assemblyname = String.Empty;
		private System.String _hintpath = String.Empty;
		private System.String _project = String.Empty;
		private System.String _package = String.Empty;
		private System.String _guid = String.Empty;
		private System.String _versionMajor = String.Empty;
		private System.String _versionMinor = String.Empty;
		private System.String _lcid = String.Empty;
		private System.String _wrapperTool = String.Empty;
		private System.String _private = String.Empty;
		private System.String _assemblyFolderKey = String.Empty;

		public Reference()
		{
		}

		public System.String Name
		{
			get { return this._name; }
			set
			{
				this._name = value;
				this.OnNameChanged();
			}
		}

		public System.String AssemblyName
		{
			get { return this._assemblyname; }
			set
			{
				this._assemblyname = value;
				this.OnAssemblyNameChanged();
			}
		}

		public System.String HintPath
		{
			get { return this._hintpath; }
			set
			{
				this._hintpath = value;
				this.OnHintPathChanged();
			}
		}

		public System.String Project
		{
			get { return this._project; }
			set
			{
				this._project = value;
				this.OnProjectChanged();
			}
		}

		public System.String Package
		{
			get { return this._package; }
			set
			{
				this._package = value;
				this.OnPackageChanged();
			}
		}

		public System.String Guid
		{
			get { return this._guid; }
			set
			{
				this._guid = value;
				//this.OnPackageChanged();
			}
		}

		public System.String VersionMajor
		{
			get { return this._versionMajor; }
			set
			{
				this._versionMajor = value;
				//this.OnPackageChanged();
			}
		}

		public System.String VersionMinor
		{
			get { return this._versionMinor; }
			set
			{
				this._versionMinor = value;
				//this.OnPackageChanged();
			}
		}

		public System.String Lcid
		{
			get { return this._lcid; }
			set
			{
				this._lcid = value;
				//this.OnPackageChanged();
			}
		}

		public System.String WrapperTool
		{
			get { return this._wrapperTool; }
			set
			{
				this._wrapperTool = value;
				//this.OnPackageChanged();
			}
		}

		public System.String Private
		{
			get { return this._private; }
			set
			{
				this._private = value;
				//this.OnPackageChanged();
			}
		}

		public System.String AssemblyFolderKey
		{
			get { return this._assemblyFolderKey; }
			set
			{
				this._assemblyFolderKey = value;
				//this.OnPackageChanged();
			}
		}


		public event System.EventHandler NameChanged;

		protected void OnNameChanged()
		{
			if(NameChanged != null)
			{
				NameChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler AssemblyNameChanged;

		protected void OnAssemblyNameChanged()
		{
			if(AssemblyNameChanged != null)
			{
				AssemblyNameChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler HintPathChanged;

		protected void OnHintPathChanged()
		{
			if(HintPathChanged != null)
			{
				HintPathChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler ProjectChanged;

		protected void OnProjectChanged()
		{
			if(ProjectChanged != null)
			{
				ProjectChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler PackageChanged;

		protected void OnPackageChanged()
		{
			if(PackageChanged != null)
			{
				PackageChanged(this, System.EventArgs.Empty);
			}
		}

	}
}
