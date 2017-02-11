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

namespace bamboo.Controls.SearchResults
{
	/// <summary>
	/// Summary description for SearchResultsControl.
	/// </summary>
	public class SearchResultsControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ColumnHeader columnHeaderColumn;
		private System.Windows.Forms.ColumnHeader columnHeaderText;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader columnHeaderFile;
		private System.Windows.Forms.ColumnHeader columnHeaderLine;
//		private System.ComponentModel.IContainer components;

		public SearchResultsControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();



			this.Text = "Search Results";

			this.Tag = "View.SearchResults";
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
//				if(components != null)
//				{
//					components.Dispose();
//				}
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
			this.columnHeaderColumn = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderText = new System.Windows.Forms.ColumnHeader();
			this.listView = new System.Windows.Forms.ListView();
			this.columnHeaderFile = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderLine = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// columnHeaderColumn
			// 
			this.columnHeaderColumn.Text = "Column";
			// 
			// columnHeaderText
			// 
			this.columnHeaderText.Text = "Text";
			this.columnHeaderText.Width = 200;
			// 
			// listView
			// 
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.columnHeaderFile,
																					   this.columnHeaderLine,
																					   this.columnHeaderColumn,
																					   this.columnHeaderText});
			this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView.FullRowSelect = true;
			this.listView.Location = new System.Drawing.Point(0, 0);
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(496, 208);
			this.listView.TabIndex = 1;
			this.listView.View = System.Windows.Forms.View.Details;
			this.listView.ItemActivate += new System.EventHandler(this.listView_ItemActivate);
			// 
			// columnHeaderFile
			// 
			this.columnHeaderFile.Text = "File";
			this.columnHeaderFile.Width = 200;
			// 
			// columnHeaderLine
			// 
			this.columnHeaderLine.Text = "Line";
			this.columnHeaderLine.Width = 200;
			// 
			// SearchResultsControl
			// 
			this.Controls.Add(this.listView);
			this.Name = "SearchResultsControl";
			this.Size = new System.Drawing.Size(496, 208);
			this.ResumeLayout(false);

		}
		#endregion

		public void Clear()
		{
			this.listView.Items.Clear();
		}

		public void AddResult(string fileName, int line, int column, string text)
		{
			this.Add(new SearchResultsItem(fileName, line, column, text));
		}

		private void Add(SearchResultsItem item)
		{
			this.listView.Items.Add(item);
		}

		private void listView_ItemActivate(object sender, System.EventArgs e)
		{
			SearchResultsItem searchResultsItem = (SearchResultsItem)this.listView.SelectedItems[0];

			this.OnItemActivate(this, new ItemActivateEventArgs(searchResultsItem.FileName, searchResultsItem.Line, searchResultsItem.Column, searchResultsItem.Text));
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
