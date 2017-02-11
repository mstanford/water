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
	/// Summary description for SqlQueryResultsControl.
	/// </summary>
	public class SqlQueryResultsControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.DataGrid dataGrid;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SqlQueryResultsControl()
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
			this.dataGrid = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid
			// 
			this.dataGrid.CaptionBackColor = System.Drawing.SystemColors.Control;
			this.dataGrid.CaptionForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid.DataMember = "";
			this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid.Location = new System.Drawing.Point(0, 0);
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.ParentRowsBackColor = System.Drawing.SystemColors.Window;
			this.dataGrid.SelectionBackColor = System.Drawing.SystemColors.Window;
			this.dataGrid.SelectionForeColor = System.Drawing.SystemColors.WindowText;
			this.dataGrid.Size = new System.Drawing.Size(712, 632);
			this.dataGrid.TabIndex = 1;
			// 
			// SqlQueryResultsTool
			// 
			this.Controls.Add(this.dataGrid);
			this.Name = "SqlQueryResultsTool";
			this.Size = new System.Drawing.Size(600, 400);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private System.Data.DataSet _dataSet = null;
		private System.Data.SqlClient.SqlDataAdapter _dataAdapter = null;

		public void Execute(string connectionString, string query)
		{
			if(this._dataSet != null)
			{
				this._dataAdapter.Update(this._dataSet);
			}
			this._dataSet = null;



			this._dataAdapter = new System.Data.SqlClient.SqlDataAdapter(query, connectionString);
			this._dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
			System.Data.SqlClient.SqlCommandBuilder commandBuilder = new System.Data.SqlClient.SqlCommandBuilder(this._dataAdapter);
			try
			{
				this._dataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();
				this._dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
				this._dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
			}
			catch(InvalidOperationException exception)
			{
				string s = exception.Message;

				//TODO throw an application specific exception.
				MessageBox.Show("No primary key is defined for this query.\nThe results are Read-Only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

				this.dataGrid.ReadOnly = true;
			}

			this._dataSet = new DataSet();
			this._dataAdapter.Fill(this._dataSet);

			this.dataGrid.DataSource = this._dataSet.Tables[0];
			this._dataSet.Tables[0].RowChanged += new DataRowChangeEventHandler(DataTable_RowChanged);
			this._dataSet.Tables[0].RowDeleted += new DataRowChangeEventHandler(DataTable_RowDeleted);
		}

		private void DataTable_RowChanged(object sender, DataRowChangeEventArgs e)
		{
			this._dataAdapter.Update(this._dataSet);
		}

		private void DataTable_RowDeleted(object sender, DataRowChangeEventArgs e)
		{
			this._dataAdapter.Update(this._dataSet);
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			this._dataAdapter.Update(this._dataSet);
		}

		//TODO
//		public void SaveResults(string filename)
//		{
//			this._dataSet.WriteXml(filename);
//		}

	}
}
