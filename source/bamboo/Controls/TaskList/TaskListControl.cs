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

namespace bamboo.Controls.TaskList
{
	/// <summary>
	/// Summary description for TaskListControl.
	/// </summary>
	public class TaskListControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ColumnHeader columnHeaderLine;
		private System.Windows.Forms.ColumnHeader columnHeaderColumn;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ColumnHeader columnHeaderFile;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader columnHeaderType;
		private System.Windows.Forms.ColumnHeader columnHeaderDescription;
		private System.ComponentModel.IContainer components;

		public TaskListControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();



			this.Text = "Task List";

			this.Tag = "View.TaskList";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TaskListControl));
			this.columnHeaderLine = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderColumn = new System.Windows.Forms.ColumnHeader();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.columnHeaderFile = new System.Windows.Forms.ColumnHeader();
			this.listView = new System.Windows.Forms.ListView();
			this.columnHeaderType = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderDescription = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// columnHeaderLine
			// 
			this.columnHeaderLine.Text = "Line";
			// 
			// columnHeaderColumn
			// 
			this.columnHeaderColumn.Text = "Column";
			// 
			// imageList
			// 
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// columnHeaderFile
			// 
			this.columnHeaderFile.Text = "File";
			this.columnHeaderFile.Width = 200;
			// 
			// listView
			// 
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.columnHeaderType,
																					   this.columnHeaderDescription,
																					   this.columnHeaderFile,
																					   this.columnHeaderLine,
																					   this.columnHeaderColumn});
			this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView.FullRowSelect = true;
			this.listView.Location = new System.Drawing.Point(0, 0);
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(496, 208);
			this.listView.SmallImageList = this.imageList;
			this.listView.TabIndex = 1;
			this.listView.View = System.Windows.Forms.View.Details;
			this.listView.ItemActivate += new System.EventHandler(this.listView_ItemActivate);
			// 
			// columnHeaderType
			// 
			this.columnHeaderType.Text = "!";
			this.columnHeaderType.Width = 20;
			// 
			// columnHeaderDescription
			// 
			this.columnHeaderDescription.Text = "Description";
			this.columnHeaderDescription.Width = 300;
			// 
			// TaskListControl
			// 
			this.Controls.Add(this.listView);
			this.Name = "TaskListControl";
			this.Size = new System.Drawing.Size(496, 208);
			this.ResumeLayout(false);

		}
		#endregion

		public void Clear()
		{
			this.listView.Items.Clear();
		}

		public void AddError(string description, string fileName, int line, int column)
		{
			this.Add(new TaskListItem(TaskListImage.Error, description, fileName, line, column));
		}

		public void AddWarning(string description, string fileName, int line, int column)
		{
			this.Add(new TaskListItem(TaskListImage.Warning, description, fileName, line, column));
		}

		public void AddInformation(string description, string fileName, int line, int column)
		{
			this.Add(new TaskListItem(TaskListImage.Information, description, fileName, line, column));
		}

		private void Add(TaskListItem item)
		{
			this.listView.Items.Add(item);
		}

		private void listView_ItemActivate(object sender, System.EventArgs e)
		{
			TaskListItem taskListItem = (TaskListItem)this.listView.SelectedItems[0];

			this.OnItemActivate(this, new ItemActivateEventArgs(taskListItem.FileName, taskListItem.Line, taskListItem.Column));
		}

		public event ItemActivateEventHandler ItemActivate;

		private void OnItemActivate(object sender, ItemActivateEventArgs e)
		{
			if(ItemActivate != null)
			{
				ItemActivate(sender, e);
			}
		}

	}
}
