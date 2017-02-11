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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace bamboo.Controls.DatabaseExplorer
{
	/// <summary>
	/// Summary description for 
	/// </summary>
	public class SqlDatabaseTreeView : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.TreeView treeView;
		private System.ComponentModel.IContainer components;

		public SqlDatabaseTreeView()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SqlDatabaseTreeView));
			this.treeView = new System.Windows.Forms.TreeView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.ImageList = this.imageList;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.Size = new System.Drawing.Size(336, 432);
			this.treeView.TabIndex = 0;
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			// 
			// imageList
			// 
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Magenta;
			// 
			// SqlDatabaseTreeView
			// 
			this.Controls.Add(this.treeView);
			this.Name = "SqlDatabaseTreeView";
			this.Size = new System.Drawing.Size(336, 432);
			this.ResumeLayout(false);

		}
		#endregion

		public void LoadDatabase(string connectionString)
		{
			this.treeView.Nodes.Clear();

			DatabaseTreeNode databaseTreeNode = new DatabaseTreeNode(connectionString);
			this.treeView.Nodes.Add(databaseTreeNode);
		}

		private void treeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			this.OnSelectionChanged(this, System.EventArgs.Empty);
		}

		public System.Windows.Forms.TreeNode SelectedNode
		{
			get { return this.treeView.SelectedNode; }
		}

		public event System.EventHandler SelectionChanged;

		private void OnSelectionChanged(object sender, System.EventArgs e)
		{
			if(SelectionChanged != null)
			{
				SelectionChanged(sender, e);
			}
		}

	}
}
