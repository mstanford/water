// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005 Swampware, Inc.
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
using System.ComponentModel;
using System.Windows.Forms;

namespace bamboo.Controls.DatabaseExplorer
{
	/// <summary>
	/// Summary description for SqlDatabaseList.
	/// </summary>
	public class SqlDatabaseList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ListBox listBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SqlDatabaseList()
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
			this.listBox = new System.Windows.Forms.ListBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBox
			// 
			this.listBox.Location = new System.Drawing.Point(8, 8);
			this.listBox.Name = "listBox";
			this.listBox.Size = new System.Drawing.Size(288, 225);
			this.listBox.TabIndex = 0;
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(136, 244);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "OK";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(224, 244);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Cancel";
			// 
			// SqlDatabaseList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(304, 278);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.listBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SqlDatabaseList";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Sql Connection Database";
			this.Load += new System.EventHandler(this.SqlDatabaseList_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private string _server;
		private bool _windows;
		private string _login;
		private string _password;

		public string Server
		{
			get { return this._server; }
			set { this._server = value; }
		}

		public bool Windows
		{
			get { return this._windows; }
			set { this._windows = value; }
		}

		public string Login
		{
			get { return this._login; }
			set { this._login = value; }
		}

		public string Password
		{
			get { return this._password; }
			set { this._password = value; }
		}

		private void SqlDatabaseList_Load(object sender, System.EventArgs e)
		{
			if(this.DesignMode)
			{
				return;
			}

			Bamboo.Mssql.ConnectionString connectionString = new Bamboo.Mssql.ConnectionString();
			connectionString.Database = "master";
			connectionString.Server = this.Server;
			connectionString.TrustedConnection = this.Windows;
			connectionString.Username = this.Login;
			connectionString.Password = this.Password;

			System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);
			System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
			command.Connection = connection;
			command.CommandType = System.Data.CommandType.Text;

			//command.CommandType = System.Data.CommandType.StoredProcedure;
			//command.CommandText = "EXEC sp_databases";
			//DATABASE_NAME

			command.CommandText = "SELECT * FROM sysdatabases";
			connection.Open();

			System.Data.SqlClient.SqlDataReader dataReader = command.ExecuteReader();

			while(dataReader.Read())
			{
				this.listBox.Items.Add(dataReader["name"]);
			}

			connection.Close();
		}

		public string Database
		{
			get
			{
				if(this.listBox.SelectedItem != null)
				{
					return this.listBox.SelectedItem.ToString();
				}
				return String.Empty;
			}
		}

	}
}
