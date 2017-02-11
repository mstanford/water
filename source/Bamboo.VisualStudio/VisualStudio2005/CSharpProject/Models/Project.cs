// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006-2007 Swampware, Inc.
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
	/// Summary description for Project.
	/// </summary>
	public class Project
	{
		private System.String _name = String.Empty;
		private System.String _projecttype = String.Empty;
		private System.String _productversion = String.Empty;
		private System.String _schemaversion = String.Empty;
		private System.Guid _projectguid;
		private Settings _settings = new Settings();
		private ConfigCollection _configs = new ConfigCollection();
		private ReferenceCollection _references = new ReferenceCollection();
		private ProjectReferenceCollection _projectReferences = new ProjectReferenceCollection();
		private ItemCollection _items = new ItemCollection();
		private FolderCollection _folders = new FolderCollection();
		private BootstrapperFile _bootstrapperFile = null;
		private BootstrapperPackage _bootstrapperPackage = null;
		private Import _import = null;

		public Project()
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

		public System.String ProjectType
		{
			get { return this._projecttype; }
			set
			{
				this._projecttype = value;
				this.OnProjectTypeChanged();
			}
		}

		public System.String ProductVersion
		{
			get { return this._productversion; }
			set
			{
				this._productversion = value;
				this.OnProductVersionChanged();
			}
		}

		public System.String SchemaVersion
		{
			get { return this._schemaversion; }
			set
			{
				this._schemaversion = value;
				this.OnSchemaVersionChanged();
			}
		}

		public System.Guid ProjectGuid
		{
			get { return this._projectguid; }
			set
			{
				this._projectguid = value;
				this.OnProjectGuidChanged();
			}
		}

		public Settings Settings
		{
			get { return this._settings; }
			set
			{
				this._settings = value;
				this.OnSettingsChanged();
			}
		}

		public ConfigCollection Configs
		{
			get { return this._configs; }
			set
			{
				this._configs = value;
				this.OnConfigsChanged();
			}
		}

		public ReferenceCollection References
		{
			get { return this._references; }
			set
			{
				this._references = value;
				this.OnReferencesChanged();
			}
		}

		public ProjectReferenceCollection ProjectReferences
		{
			get { return this._projectReferences; }
			set
			{
				this._projectReferences = value;
				this.OnProjectReferencesChanged();
			}
		}

		public ItemCollection Items
		{
			get { return this._items; }
			set
			{
				this._items = value;
				this.OnItemsChanged();
			}
		}

		public FolderCollection Folders
		{
			get { return this._folders; }
			set
			{
				this._folders = value;
				//this.OnItemsChanged();
			}
		}

		public BootstrapperFile BootstrapperFile
		{
			get { return this._bootstrapperFile; }
			set
			{
				this._bootstrapperFile = value;
				//this.OnItemsChanged();
			}
		}

		public BootstrapperPackage BootstrapperPackage
		{
			get { return this._bootstrapperPackage; }
			set
			{
				this._bootstrapperPackage = value;
				//this.OnItemsChanged();
			}
		}

		public Import Import
		{
			get { return this._import; }
			set
			{
				this._import = value;
				//this.OnItemsChanged();
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

		public event System.EventHandler ProjectTypeChanged;

		protected void OnProjectTypeChanged()
		{
			if(ProjectTypeChanged != null)
			{
				ProjectTypeChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler ProductVersionChanged;

		protected void OnProductVersionChanged()
		{
			if(ProductVersionChanged != null)
			{
				ProductVersionChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler SchemaVersionChanged;

		protected void OnSchemaVersionChanged()
		{
			if(SchemaVersionChanged != null)
			{
				SchemaVersionChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler ProjectGuidChanged;

		protected void OnProjectGuidChanged()
		{
			if(ProjectGuidChanged != null)
			{
				ProjectGuidChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler SettingsChanged;

		protected void OnSettingsChanged()
		{
			if(SettingsChanged != null)
			{
				SettingsChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler ConfigsChanged;

		protected void OnConfigsChanged()
		{
			if(ConfigsChanged != null)
			{
				ConfigsChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler ReferencesChanged;

		protected void OnReferencesChanged()
		{
			if(ReferencesChanged != null)
			{
				ReferencesChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler ItemsChanged;

		protected void OnItemsChanged()
		{
			if(ItemsChanged != null)
			{
				ItemsChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler ProjectReferencesChanged;

		protected void OnProjectReferencesChanged()
		{
			if(ProjectReferencesChanged != null)
			{
				ProjectReferencesChanged(this, System.EventArgs.Empty);
			}
		}

	}
}
