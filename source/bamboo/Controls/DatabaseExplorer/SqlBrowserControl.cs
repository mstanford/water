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
	/// Summary description for SqlBrowserControl.
	/// </summary>
	public class SqlBrowserControl : System.Windows.Forms.UserControl
	{
		private SqlDatabaseTreeView sqlDatabaseTreeView;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ListView listView;
		private System.ComponentModel.IContainer components;

		public SqlBrowserControl()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SqlBrowserControl));
			this.sqlDatabaseTreeView = new SqlDatabaseTreeView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.listView = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// sqlDatabaseTreeView
			// 
			this.sqlDatabaseTreeView.Dock = System.Windows.Forms.DockStyle.Left;
			this.sqlDatabaseTreeView.Location = new System.Drawing.Point(0, 0);
			this.sqlDatabaseTreeView.Name = "sqlDatabaseTreeView";
			this.sqlDatabaseTreeView.Size = new System.Drawing.Size(280, 480);
			this.sqlDatabaseTreeView.TabIndex = 0;
			this.sqlDatabaseTreeView.SelectionChanged += new System.EventHandler(this.sqlDatabaseTreeView_SelectionChanged);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(280, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 480);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// imageList
			// 
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Magenta;
			// 
			// listView
			// 
			this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView.Location = new System.Drawing.Point(283, 0);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(357, 480);
			this.listView.SmallImageList = this.imageList;
			this.listView.TabIndex = 2;
			this.listView.View = System.Windows.Forms.View.List;
			// 
			// SqlBrowserControl
			// 
			this.Controls.Add(this.listView);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.sqlDatabaseTreeView);
			this.Name = "SqlBrowserControl";
			this.Size = new System.Drawing.Size(640, 480);
			this.ResumeLayout(false);

		}
		#endregion

		public void Connect(string connectionString)
		{
			this.Cursor = Cursors.WaitCursor;

			this.sqlDatabaseTreeView.LoadDatabase(connectionString);

			this.Cursor = Cursors.Default;
		}

		private void sqlDatabaseTreeView_SelectionChanged(object sender, System.EventArgs e)
		{
			System.Windows.Forms.TreeNode selectedNode = this.sqlDatabaseTreeView.SelectedNode;
			if(selectedNode != null)
			{
				if(selectedNode is TablesTreeNode)
				{
					this.listView.View = System.Windows.Forms.View.List;
					this.listView.Columns.Clear();
					this.listView.Clear();

					TablesTreeNode tablesTreeNode = (TablesTreeNode)selectedNode;
					Bamboo.Mssql.TableCollection tables = (Bamboo.Mssql.TableCollection)tablesTreeNode.Tag;
					this.ShowTables(tables);
				}
				else if(selectedNode is TableTreeNode)
				{
					this.listView.View = System.Windows.Forms.View.List;
					this.listView.Columns.Clear();
					this.listView.Clear();

					TableTreeNode tableTreeNode = (TableTreeNode)selectedNode;
					Bamboo.Mssql.Table table = (Bamboo.Mssql.Table)tableTreeNode.Tag;
					this.ShowTableColumns(table.Columns);
				}
				else if(selectedNode is ColumnsTreeNode)
				{
					this.listView.View = System.Windows.Forms.View.List;
					this.listView.Columns.Clear();
					this.listView.Clear();

					ColumnsTreeNode columnsTreeNode = (ColumnsTreeNode)selectedNode;
					Bamboo.Mssql.TableColumnCollection columns = (Bamboo.Mssql.TableColumnCollection)columnsTreeNode.Tag;
					this.ShowTableColumns(columns);
				}
				else if(selectedNode is ColumnTreeNode)
				{
					this.listView.Clear();
					this.listView.View = System.Windows.Forms.View.Details;

					ColumnTreeNode columnTreeNode = (ColumnTreeNode)selectedNode;
					Bamboo.Mssql.TableColumn column = (Bamboo.Mssql.TableColumn)columnTreeNode.Tag;
					this.ShowTableColumn(column);
				}
				else if(selectedNode is ViewsTreeNode)
				{
					this.listView.View = System.Windows.Forms.View.List;
					this.listView.Columns.Clear();
					this.listView.Clear();

					ViewsTreeNode viewsTreeNode = (ViewsTreeNode)selectedNode;
					Bamboo.Mssql.ViewCollection views = (Bamboo.Mssql.ViewCollection)viewsTreeNode.Tag;
					this.ShowViews(views);
				}
				else if(selectedNode is ViewTreeNode)
				{
					this.listView.View = System.Windows.Forms.View.List;
					this.listView.Columns.Clear();
					this.listView.Clear();

					ViewTreeNode viewTreeNode = (ViewTreeNode)selectedNode;
					Bamboo.Mssql.View view = (Bamboo.Mssql.View)viewTreeNode.Tag;
					this.ShowViewColumns(view.Columns);
				}
				else if(selectedNode is ViewColumnsTreeNode)
				{
					this.listView.View = System.Windows.Forms.View.List;
					this.listView.Columns.Clear();
					this.listView.Clear();

					ViewColumnsTreeNode viewColumnsTreeNode = (ViewColumnsTreeNode)selectedNode;
					Bamboo.Mssql.ViewColumnCollection columns = (Bamboo.Mssql.ViewColumnCollection)viewColumnsTreeNode.Tag;
					this.ShowViewColumns(columns);
				}
				else if(selectedNode is ViewColumnTreeNode)
				{
					this.listView.Clear();
					this.listView.View = System.Windows.Forms.View.Details;

					ViewColumnTreeNode viewColumnTreeNode = (ViewColumnTreeNode)selectedNode;
					Bamboo.Mssql.ViewColumn column = (Bamboo.Mssql.ViewColumn)viewColumnTreeNode.Tag;
					this.ShowViewColumn(column);
				}
				else if(selectedNode is ProceduresTreeNode)
				{
					this.listView.View = System.Windows.Forms.View.List;
					this.listView.Columns.Clear();
					this.listView.Clear();

					ProceduresTreeNode proceduresTreeNode = (ProceduresTreeNode)selectedNode;
					Bamboo.Mssql.ProcedureCollection procedures = (Bamboo.Mssql.ProcedureCollection)proceduresTreeNode.Tag;
					this.ShowProcudures(procedures);
				}
				else if(selectedNode is ProcedureTreeNode)
				{
					this.listView.Clear();
					this.listView.View = System.Windows.Forms.View.Details;

					ProcedureTreeNode procedureTreeNode = (ProcedureTreeNode)selectedNode;
					Bamboo.Mssql.Procedure procedure = (Bamboo.Mssql.Procedure)procedureTreeNode.Tag;
					this.ShowProcudure(procedure);
				}
				else
				{
					this.listView.Clear();
				}
			}
		}

		private void ShowTables(Bamboo.Mssql.TableCollection tables)
		{
			foreach(Bamboo.Mssql.Table table in tables)
			{
				this.listView.Items.Add(new System.Windows.Forms.ListViewItem(table.Name, Images.Table));
			}
		}

		private void ShowTableColumns(Bamboo.Mssql.TableColumnCollection columns)
		{
			foreach(Bamboo.Mssql.TableColumn column in columns)
			{
				this.listView.Items.Add(new System.Windows.Forms.ListViewItem(column.Name, Images.Column));
			}
		}

		private void ShowTableColumn(Bamboo.Mssql.TableColumn column)
		{
			this.listView.Columns.Add("", 250, System.Windows.Forms.HorizontalAlignment.Left);
			this.listView.Columns.Add("", 250, System.Windows.Forms.HorizontalAlignment.Left);

			this.listView.Items.Add(new System.Windows.Forms.ListViewItem(new string[] { "Name", column.Name }));
			this.listView.Items.Add(new System.Windows.Forms.ListViewItem(new string[] { "Datatype", column.Datatype }));
			if(column.Length > 0)
			{
				this.listView.Items.Add(new System.Windows.Forms.ListViewItem(new string[] { "Length", column.Length.ToString() }));
			}
			this.listView.Items.Add(new System.Windows.Forms.ListViewItem(new string[] { "Null", column.IsNullable.ToString() }));
			this.listView.Items.Add(new System.Windows.Forms.ListViewItem(new string[] { "PrimaryKey", column.IsPrimaryKey.ToString() }));
			this.listView.Items.Add(new System.Windows.Forms.ListViewItem(new string[] { "ForeignKey", column.IsForeignKey.ToString() }));
		}

		private void ShowViews(Bamboo.Mssql.ViewCollection views)
		{
			foreach(Bamboo.Mssql.View view in views)
			{
				this.listView.Items.Add(new System.Windows.Forms.ListViewItem(view.Name, Images.View));
			}
		}

		private void ShowViewColumns(Bamboo.Mssql.ViewColumnCollection columns)
		{
			foreach(Bamboo.Mssql.ViewColumn column in columns)
			{
				this.listView.Items.Add(new System.Windows.Forms.ListViewItem(column.Name, Images.Column));
			}
		}

		private void ShowViewColumn(Bamboo.Mssql.ViewColumn column)
		{
			this.listView.Columns.Add("", 250, System.Windows.Forms.HorizontalAlignment.Left);
			this.listView.Columns.Add("", 250, System.Windows.Forms.HorizontalAlignment.Left);

			this.listView.Items.Add(new System.Windows.Forms.ListViewItem(new string[] { "Name", column.Name }));
			this.listView.Items.Add(new System.Windows.Forms.ListViewItem(new string[] { "Datatype", column.Datatype }));
			if(column.Length > 0)
			{
				this.listView.Items.Add(new System.Windows.Forms.ListViewItem(new string[] { "Length", column.Length.ToString() }));
			}
		}

		private void ShowProcudures(Bamboo.Mssql.ProcedureCollection procedures)
		{
			foreach(Bamboo.Mssql.Procedure procedure in procedures)
			{
				this.listView.Items.Add(new System.Windows.Forms.ListViewItem(procedure.Name, Images.Procedure));
			}
		}

		private void ShowProcudure(Bamboo.Mssql.Procedure procedure)
		{
			this.listView.Columns.Add("", 500, System.Windows.Forms.HorizontalAlignment.Left);

			System.IO.StringReader reader = new System.IO.StringReader(procedure.Definition);
			string line;
			while((line = reader.ReadLine()) != null)
			{
				this.listView.Items.Add(new System.Windows.Forms.ListViewItem(new string[] { line }));
			}
		}

	}
}
