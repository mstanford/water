// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005-2007 Swampware, Inc.
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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace bamboo.Controls.AssemblyExplorer
{
	/// <summary>
	/// Summary description for AssemblyExplorerControl.
	/// </summary>
	public class AssemblyExplorerControl : UserControl
	{
		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.Splitter splitter;
		private System.Windows.Forms.Panel panel;
		private System.Windows.Forms.ImageList imageList;
		private System.ComponentModel.IContainer components;

		public AssemblyExplorerControl()
		{
			// This call is required by the Windows.Forms Form Designer.
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AssemblyExplorerControl));
			this.treeView = new System.Windows.Forms.TreeView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.splitter = new System.Windows.Forms.Splitter();
			this.panel = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeView.ImageList = this.imageList;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.Size = new System.Drawing.Size(344, 512);
			this.treeView.TabIndex = 0;
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			// 
			// imageList
			// 
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Magenta;
			// 
			// splitter
			// 
			this.splitter.Location = new System.Drawing.Point(344, 0);
			this.splitter.Name = "splitter";
			this.splitter.Size = new System.Drawing.Size(3, 512);
			this.splitter.TabIndex = 1;
			this.splitter.TabStop = false;
			// 
			// panel
			// 
			this.panel.BackColor = System.Drawing.SystemColors.Window;
			this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel.Location = new System.Drawing.Point(347, 0);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(261, 512);
			this.panel.TabIndex = 2;
			// 
			// AssemblyExplorerControl
			// 
			this.Controls.Add(this.panel);
			this.Controls.Add(this.splitter);
			this.Controls.Add(this.treeView);
			this.Name = "AssemblyExplorerControl";
			this.Size = new System.Drawing.Size(608, 512);
			this.ResumeLayout(false);

		}
		#endregion

		private string _filename;

		public string Filename
		{
			get { return this._filename; }
		}

		public void FileOpen(string filename)
		{
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);

			this.Text = fileInfo.Name;
			this._filename = fileInfo.FullName;

			LoadAssembly();
		}

		private void LoadAssembly()
		{
			this.treeView.Nodes.Clear();

			System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(this._filename);

			AssemblyTreeNode assemblyTreeNode = new AssemblyTreeNode(assembly);
			this.treeView.Nodes.Add(assemblyTreeNode);
		}

		private void treeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			this.panel.Controls.Clear();

			bamboo.Controls.AssemblyExplorer.TreeNode treeNode = (bamboo.Controls.AssemblyExplorer.TreeNode)e.Node;
			if(treeNode == null)
			{
				return;
			}

			System.Windows.Forms.Control detailedView = treeNode.DetailedView;
			if(detailedView != null)
			{
				detailedView.Dock = System.Windows.Forms.DockStyle.Fill;
				this.panel.Controls.Add(detailedView);
			}
		}

	}
}
