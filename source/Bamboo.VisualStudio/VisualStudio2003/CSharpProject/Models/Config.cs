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
	/// Summary description for Config.
	/// </summary>
	public class Config
	{
		private System.String _name = String.Empty;
		private System.String _allowunsafeblocks = String.Empty;
		private System.String _baseaddress = String.Empty;
		private System.String _checkforoverflowunderflow = String.Empty;
		private System.String _configurationoverridefile = String.Empty;
		private System.String _defineconstants = String.Empty;
		private System.String _documentationfile = String.Empty;
		private System.String _debugsymbols = String.Empty;
		private System.String _filealignment = String.Empty;
		private System.String _incrementalbuild = String.Empty;
		private System.String _nostdlib = String.Empty;
		private System.String _nowarn = String.Empty;
		private System.String _optimize = String.Empty;
		private System.String _outputpath = String.Empty;
		private System.String _registerforcominterop = String.Empty;
		private System.String _removeintegerchecks = String.Empty;
		private System.String _treatwarningsaserrors = String.Empty;
		private System.String _warninglevel = String.Empty;

		public Config()
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

		public System.String AllowUnsafeBlocks
		{
			get { return this._allowunsafeblocks; }
			set
			{
				this._allowunsafeblocks = value;
				this.OnAllowUnsafeBlocksChanged();
			}
		}

		public System.String BaseAddress
		{
			get { return this._baseaddress; }
			set
			{
				this._baseaddress = value;
				this.OnBaseAddressChanged();
			}
		}

		public System.String CheckForOverflowUnderflow
		{
			get { return this._checkforoverflowunderflow; }
			set
			{
				this._checkforoverflowunderflow = value;
				this.OnCheckForOverflowUnderflowChanged();
			}
		}

		public System.String ConfigurationOverrideFile
		{
			get { return this._configurationoverridefile; }
			set
			{
				this._configurationoverridefile = value;
				this.OnConfigurationOverrideFileChanged();
			}
		}

		public System.String DefineConstants
		{
			get { return this._defineconstants; }
			set
			{
				this._defineconstants = value;
				this.OnDefineConstantsChanged();
			}
		}

		public System.String DocumentationFile
		{
			get { return this._documentationfile; }
			set
			{
				this._documentationfile = value;
				this.OnDocumentationFileChanged();
			}
		}

		public System.String DebugSymbols
		{
			get { return this._debugsymbols; }
			set
			{
				this._debugsymbols = value;
				this.OnDebugSymbolsChanged();
			}
		}

		public System.String FileAlignment
		{
			get { return this._filealignment; }
			set
			{
				this._filealignment = value;
				this.OnFileAlignmentChanged();
			}
		}

		public System.String IncrementalBuild
		{
			get { return this._incrementalbuild; }
			set
			{
				this._incrementalbuild = value;
				this.OnIncrementalBuildChanged();
			}
		}

		public System.String NoStdLib
		{
			get { return this._nostdlib; }
			set
			{
				this._nostdlib = value;
				this.OnNoStdLibChanged();
			}
		}

		public System.String NoWarn
		{
			get { return this._nowarn; }
			set
			{
				this._nowarn = value;
				this.OnNoWarnChanged();
			}
		}

		public System.String Optimize
		{
			get { return this._optimize; }
			set
			{
				this._optimize = value;
				this.OnOptimizeChanged();
			}
		}

		public System.String OutputPath
		{
			get { return this._outputpath; }
			set
			{
				this._outputpath = value;
				this.OnOutputPathChanged();
			}
		}

		public System.String RegisterForComInterop
		{
			get { return this._registerforcominterop; }
			set
			{
				this._registerforcominterop = value;
				this.OnRegisterForComInteropChanged();
			}
		}

		public System.String RemoveIntegerChecks
		{
			get { return this._removeintegerchecks; }
			set
			{
				this._removeintegerchecks = value;
				this.OnRemoveIntegerChecksChanged();
			}
		}

		public System.String TreatWarningsAsErrors
		{
			get { return this._treatwarningsaserrors; }
			set
			{
				this._treatwarningsaserrors = value;
				this.OnTreatWarningsAsErrorsChanged();
			}
		}

		public System.String WarningLevel
		{
			get { return this._warninglevel; }
			set
			{
				this._warninglevel = value;
				this.OnWarningLevelChanged();
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

		public event System.EventHandler AllowUnsafeBlocksChanged;

		protected void OnAllowUnsafeBlocksChanged()
		{
			if(AllowUnsafeBlocksChanged != null)
			{
				AllowUnsafeBlocksChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler BaseAddressChanged;

		protected void OnBaseAddressChanged()
		{
			if(BaseAddressChanged != null)
			{
				BaseAddressChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler CheckForOverflowUnderflowChanged;

		protected void OnCheckForOverflowUnderflowChanged()
		{
			if(CheckForOverflowUnderflowChanged != null)
			{
				CheckForOverflowUnderflowChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler ConfigurationOverrideFileChanged;

		protected void OnConfigurationOverrideFileChanged()
		{
			if(ConfigurationOverrideFileChanged != null)
			{
				ConfigurationOverrideFileChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler DefineConstantsChanged;

		protected void OnDefineConstantsChanged()
		{
			if(DefineConstantsChanged != null)
			{
				DefineConstantsChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler DocumentationFileChanged;

		protected void OnDocumentationFileChanged()
		{
			if(DocumentationFileChanged != null)
			{
				DocumentationFileChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler DebugSymbolsChanged;

		protected void OnDebugSymbolsChanged()
		{
			if(DebugSymbolsChanged != null)
			{
				DebugSymbolsChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler FileAlignmentChanged;

		protected void OnFileAlignmentChanged()
		{
			if(FileAlignmentChanged != null)
			{
				FileAlignmentChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler IncrementalBuildChanged;

		protected void OnIncrementalBuildChanged()
		{
			if(IncrementalBuildChanged != null)
			{
				IncrementalBuildChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler NoStdLibChanged;

		protected void OnNoStdLibChanged()
		{
			if(NoStdLibChanged != null)
			{
				NoStdLibChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler NoWarnChanged;

		protected void OnNoWarnChanged()
		{
			if(NoWarnChanged != null)
			{
				NoWarnChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler OptimizeChanged;

		protected void OnOptimizeChanged()
		{
			if(OptimizeChanged != null)
			{
				OptimizeChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler OutputPathChanged;

		protected void OnOutputPathChanged()
		{
			if(OutputPathChanged != null)
			{
				OutputPathChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler RegisterForComInteropChanged;

		protected void OnRegisterForComInteropChanged()
		{
			if(RegisterForComInteropChanged != null)
			{
				RegisterForComInteropChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler RemoveIntegerChecksChanged;

		protected void OnRemoveIntegerChecksChanged()
		{
			if(RemoveIntegerChecksChanged != null)
			{
				RemoveIntegerChecksChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler TreatWarningsAsErrorsChanged;

		protected void OnTreatWarningsAsErrorsChanged()
		{
			if(TreatWarningsAsErrorsChanged != null)
			{
				TreatWarningsAsErrorsChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler WarningLevelChanged;

		protected void OnWarningLevelChanged()
		{
			if(WarningLevelChanged != null)
			{
				WarningLevelChanged(this, System.EventArgs.Empty);
			}
		}

	}
}
