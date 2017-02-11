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
	/// Summary description for Settings.
	/// </summary>
	public class Settings
	{
		private System.String _applicationicon = String.Empty;
		private System.String _assemblykeycontainername = String.Empty;
		private System.String _assemblyname = String.Empty;
		private System.String _assemblyoriginatorkeyfile = String.Empty;
		private System.String _defaultclientscript = String.Empty;
		private System.String _defaulthtmlpagelayout = String.Empty;
		private System.String _defaulttargetschema = String.Empty;
		private System.String _delaysign = String.Empty;
		private System.String _outputtype = String.Empty;
		private System.String _prebuildevent = String.Empty;
		private System.String _postbuildevent = String.Empty;
		private System.String _rootnamespace = String.Empty;
		private System.String _runpostbuildevent = String.Empty;
		private System.String _startupobject = String.Empty;

		public Settings()
		{
		}

		public System.String ApplicationIcon
		{
			get { return this._applicationicon; }
			set
			{
				this._applicationicon = value;
				this.OnApplicationIconChanged();
			}
		}

		public System.String AssemblyKeyContainerName
		{
			get { return this._assemblykeycontainername; }
			set
			{
				this._assemblykeycontainername = value;
				this.OnAssemblyKeyContainerNameChanged();
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

		public System.String AssemblyOriginatorKeyFile
		{
			get { return this._assemblyoriginatorkeyfile; }
			set
			{
				this._assemblyoriginatorkeyfile = value;
				this.OnAssemblyOriginatorKeyFileChanged();
			}
		}

		public System.String DefaultClientScript
		{
			get { return this._defaultclientscript; }
			set
			{
				this._defaultclientscript = value;
				this.OnDefaultClientScriptChanged();
			}
		}

		public System.String DefaultHTMLPageLayout
		{
			get { return this._defaulthtmlpagelayout; }
			set
			{
				this._defaulthtmlpagelayout = value;
				this.OnDefaultHTMLPageLayoutChanged();
			}
		}

		public System.String DefaultTargetSchema
		{
			get { return this._defaulttargetschema; }
			set
			{
				this._defaulttargetschema = value;
				this.OnDefaultTargetSchemaChanged();
			}
		}

		public System.String DelaySign
		{
			get { return this._delaysign; }
			set
			{
				this._delaysign = value;
				this.OnDelaySignChanged();
			}
		}

		public System.String OutputType
		{
			get { return this._outputtype; }
			set
			{
				this._outputtype = value;
				this.OnOutputTypeChanged();
			}
		}

		public System.String PreBuildEvent
		{
			get { return this._prebuildevent; }
			set
			{
				this._prebuildevent = value;
				this.OnPreBuildEventChanged();
			}
		}

		public System.String PostBuildEvent
		{
			get { return this._postbuildevent; }
			set
			{
				this._postbuildevent = value;
				this.OnPostBuildEventChanged();
			}
		}

		public System.String RootNamespace
		{
			get { return this._rootnamespace; }
			set
			{
				this._rootnamespace = value;
				this.OnRootNamespaceChanged();
			}
		}

		public System.String RunPostBuildEvent
		{
			get { return this._runpostbuildevent; }
			set
			{
				this._runpostbuildevent = value;
				this.OnRunPostBuildEventChanged();
			}
		}

		public System.String StartupObject
		{
			get { return this._startupobject; }
			set
			{
				this._startupobject = value;
				this.OnStartupObjectChanged();
			}
		}


		public event System.EventHandler ApplicationIconChanged;

		protected void OnApplicationIconChanged()
		{
			if(ApplicationIconChanged != null)
			{
				ApplicationIconChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler AssemblyKeyContainerNameChanged;

		protected void OnAssemblyKeyContainerNameChanged()
		{
			if(AssemblyKeyContainerNameChanged != null)
			{
				AssemblyKeyContainerNameChanged(this, System.EventArgs.Empty);
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

		public event System.EventHandler AssemblyOriginatorKeyFileChanged;

		protected void OnAssemblyOriginatorKeyFileChanged()
		{
			if(AssemblyOriginatorKeyFileChanged != null)
			{
				AssemblyOriginatorKeyFileChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler DefaultClientScriptChanged;

		protected void OnDefaultClientScriptChanged()
		{
			if(DefaultClientScriptChanged != null)
			{
				DefaultClientScriptChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler DefaultHTMLPageLayoutChanged;

		protected void OnDefaultHTMLPageLayoutChanged()
		{
			if(DefaultHTMLPageLayoutChanged != null)
			{
				DefaultHTMLPageLayoutChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler DefaultTargetSchemaChanged;

		protected void OnDefaultTargetSchemaChanged()
		{
			if(DefaultTargetSchemaChanged != null)
			{
				DefaultTargetSchemaChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler DelaySignChanged;

		protected void OnDelaySignChanged()
		{
			if(DelaySignChanged != null)
			{
				DelaySignChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler OutputTypeChanged;

		protected void OnOutputTypeChanged()
		{
			if(OutputTypeChanged != null)
			{
				OutputTypeChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler PreBuildEventChanged;

		protected void OnPreBuildEventChanged()
		{
			if(PreBuildEventChanged != null)
			{
				PreBuildEventChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler PostBuildEventChanged;

		protected void OnPostBuildEventChanged()
		{
			if(PostBuildEventChanged != null)
			{
				PostBuildEventChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler RootNamespaceChanged;

		protected void OnRootNamespaceChanged()
		{
			if(RootNamespaceChanged != null)
			{
				RootNamespaceChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler RunPostBuildEventChanged;

		protected void OnRunPostBuildEventChanged()
		{
			if(RunPostBuildEventChanged != null)
			{
				RunPostBuildEventChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler StartupObjectChanged;

		protected void OnStartupObjectChanged()
		{
			if(StartupObjectChanged != null)
			{
				StartupObjectChanged(this, System.EventArgs.Empty);
			}
		}

	}
}
