// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005-2008 Swampware, Inc.
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
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace bamboo.Controls.ProjectExplorer
{
	/// <summary>
	/// Summary description for AddReferenceDialog.
	/// </summary>
	public class AddReferenceDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.ListView listViewReferences;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageProjects;
		private System.Windows.Forms.ListView listViewProjects;
		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.ColumnHeader columnHeaderAssembly;
		private System.Windows.Forms.ColumnHeader columnHeaderVersion;
		private System.Windows.Forms.TabPage tabPageAssemblies;
		private System.Windows.Forms.ListView listViewAssemblies;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddReferenceDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listViewReferences = new System.Windows.Forms.ListView();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageAssemblies = new System.Windows.Forms.TabPage();
			this.listViewAssemblies = new System.Windows.Forms.ListView();
			this.columnHeaderAssembly = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderVersion = new System.Windows.Forms.ColumnHeader();
			this.tabPageProjects = new System.Windows.Forms.TabPage();
			this.listViewProjects = new System.Windows.Forms.ListView();
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.tabControl.SuspendLayout();
			this.tabPageAssemblies.SuspendLayout();
			this.tabPageProjects.SuspendLayout();
			this.SuspendLayout();
			// 
			// listViewReferences
			// 
			this.listViewReferences.FullRowSelect = true;
			this.listViewReferences.Location = new System.Drawing.Point(8, 274);
			this.listViewReferences.Name = "listViewReferences";
			this.listViewReferences.Size = new System.Drawing.Size(640, 104);
			this.listViewReferences.TabIndex = 4;
			this.listViewReferences.View = System.Windows.Forms.View.List;
			this.listViewReferences.ItemActivate += new System.EventHandler(this.listViewReferences_ItemActivate);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(568, 394);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Text = "Cancel";
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(480, 394);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 5;
			this.buttonOK.Text = "OK";
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageAssemblies);
			this.tabControl.Controls.Add(this.tabPageProjects);
			this.tabControl.Location = new System.Drawing.Point(8, 8);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(640, 226);
			this.tabControl.TabIndex = 7;
			// 
			// tabPageAssemblies
			// 
			this.tabPageAssemblies.Controls.Add(this.listViewAssemblies);
			this.tabPageAssemblies.Location = new System.Drawing.Point(4, 22);
			this.tabPageAssemblies.Name = "tabPageAssemblies";
			this.tabPageAssemblies.Size = new System.Drawing.Size(632, 200);
			this.tabPageAssemblies.TabIndex = 0;
			this.tabPageAssemblies.Text = "Assemblies";
			// 
			// listViewAssemblies
			// 
			this.listViewAssemblies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								 this.columnHeaderAssembly,
																								 this.columnHeaderVersion});
			this.listViewAssemblies.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewAssemblies.FullRowSelect = true;
			this.listViewAssemblies.Location = new System.Drawing.Point(0, 0);
			this.listViewAssemblies.Name = "listViewAssemblies";
			this.listViewAssemblies.Size = new System.Drawing.Size(632, 200);
			this.listViewAssemblies.TabIndex = 5;
			this.listViewAssemblies.View = System.Windows.Forms.View.Details;
			this.listViewAssemblies.ItemActivate += new System.EventHandler(this.listViewAssemblies_ItemActivate);
			// 
			// columnHeaderAssembly
			// 
			this.columnHeaderAssembly.Text = "Assembly";
			this.columnHeaderAssembly.Width = 300;
			// 
			// columnHeaderVersion
			// 
			this.columnHeaderVersion.Text = "Version";
			this.columnHeaderVersion.Width = 100;
			// 
			// tabPageProjects
			// 
			this.tabPageProjects.Controls.Add(this.listViewProjects);
			this.tabPageProjects.Location = new System.Drawing.Point(4, 22);
			this.tabPageProjects.Name = "tabPageProjects";
			this.tabPageProjects.Size = new System.Drawing.Size(632, 200);
			this.tabPageProjects.TabIndex = 1;
			this.tabPageProjects.Text = "Projects";
			// 
			// listViewProjects
			// 
			this.listViewProjects.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewProjects.FullRowSelect = true;
			this.listViewProjects.Location = new System.Drawing.Point(0, 0);
			this.listViewProjects.Name = "listViewProjects";
			this.listViewProjects.Size = new System.Drawing.Size(632, 200);
			this.listViewProjects.TabIndex = 5;
			this.listViewProjects.View = System.Windows.Forms.View.List;
			this.listViewProjects.ItemActivate += new System.EventHandler(this.listViewProjects_ItemActivate);
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.Location = new System.Drawing.Point(8, 245);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.TabIndex = 8;
			this.buttonBrowse.Text = "Browse";
			this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// AddReferenceDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(656, 432);
			this.Controls.Add(this.buttonBrowse);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.listViewReferences);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddReferenceDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add Reference";
			this.Load += new System.EventHandler(this.AddReferenceDialog_Load);
			this.tabControl.ResumeLayout(false);
			this.tabPageAssemblies.ResumeLayout(false);
			this.tabPageProjects.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private System.Guid _projectGuid = System.Guid.Empty;

		public System.Guid ProjectGuid
		{
			get { return this._projectGuid; }
			set { this._projectGuid = value; }
		}

		private void AddReferenceDialog_Load(object sender, System.EventArgs e)
		{
//			if(this.DesignMode)
//			{
//				return;
//			}
//
//			//Load Assemblies.
//			Bamboo.Framework.Services.AssemblyService assemblyService = (Bamboo.Framework.Services.AssemblyService)Bamboo.Application.GetInstance().GetService(typeof(Bamboo.Framework.Services.AssemblyService));
//
//			foreach(Bamboo.Framework.Services.Assembly assembly in assemblyService.Assemblies)
//			{
//				ListViewItem listViewItem = new ListViewItem(assembly.Name);
//				listViewItem.SubItems.Add(assembly.Version);
//				listViewItem.Tag = assembly;
//				this.listViewAssemblies.Items.Add(listViewItem);
//			}
//
//			//Load Projects.
//			Bamboo.ProjectExplorer.Services.ProjectTrackingService projectTrackingService = (Bamboo.ProjectExplorer.Services.ProjectTrackingService)Bamboo.Application.GetInstance().GetService(typeof(Bamboo.ProjectExplorer.Services.ProjectTrackingService));
//
//			foreach(TreeNodes.Projects.ProjectTreeNode treeNode in projectTrackingService.Projects)
//			{
//				if(treeNode.ProjectGuid != this.ProjectGuid)
//				{
//					ListViewItem listViewItem = new ListViewItem(treeNode.Text);
//					listViewItem.Tag = treeNode.ProjectGuid;
//					this.listViewProjects.Items.Add(listViewItem);
//				}
//			}
//
		}

		private void buttonBrowse_Click(object sender, System.EventArgs e)
		{
//			string filename = this.FileDialogService.ShowOpenFileDialog("Assemblies (*.dll)|*.dll|All files|*.*", "File");
//
//			if(filename.Length != 0)
//			{
//				System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
//				string assemblyName = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
//
//
//
//				FileReference reference = new FileReference(assemblyName, filename);
//
//				this._references.Add(reference);
//
//				ListViewItem listViewItem = new ListViewItem(reference.Name);
//				listViewItem.Tag = reference;
//				this.listViewReferences.Items.Add(listViewItem);
//			}
		}

		private List<Reference> _references = new List<Reference>();

		/// <summary>
		/// Add a file reference to the list of references.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewAssemblies_ItemActivate(object sender, System.EventArgs e)
		{
//			ListViewItem selectedItem = this.listViewAssemblies.SelectedItems[0];
//
//			Bamboo.Framework.Services.Assembly assembly = (Bamboo.Framework.Services.Assembly)selectedItem.Tag;
//
//			FileReference reference = new FileReference(assembly.Name, assembly.Path);
//
//			this._references.Add(reference);
//
//			ListViewItem listViewItem = new ListViewItem(reference.Name);
//			listViewItem.Tag = reference;
//			this.listViewReferences.Items.Add(listViewItem);
		}

		/// <summary>
		/// Add a project reference to the list of references.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewProjects_ItemActivate(object sender, System.EventArgs e)
		{
			ListViewItem selectedItem = this.listViewProjects.SelectedItems[0];

			ProjectReference reference = new ProjectReference(selectedItem.Text, (System.Guid)selectedItem.Tag);
		
			this._references.Add(reference);

			ListViewItem listViewItem = new ListViewItem(reference.Name);
			listViewItem.Tag = reference;
			this.listViewReferences.Items.Add(listViewItem);
		}

		/// <summary>
		/// Remove the reference from the list.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewReferences_ItemActivate(object sender, System.EventArgs e)
		{
			ListViewItem selectedItem = this.listViewReferences.SelectedItems[0];

			Reference reference = (Reference)selectedItem.Tag;
			
			this._references.Remove(reference);

			this.listViewReferences.Items.Remove(selectedItem);
		}

		public List<Reference> References
		{
			get { return this._references; }
		}

	}
}
