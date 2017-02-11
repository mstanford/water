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
	/// Summary description for SqlConnectionDialog.
	/// </summary>
	public class SqlConnectionDialog : System.Windows.Forms.Form
	{
		#region PLUMBING

		private System.Windows.Forms.Label labelPassword;
		private System.Windows.Forms.Label labelLogin;
		private System.Windows.Forms.Label labelConnect;
		private System.Windows.Forms.RadioButton optSQL;
		private System.Windows.Forms.RadioButton optWindows;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtServer;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label labelDatabase;
		private System.Windows.Forms.TextBox txtDatabase;
		private System.Windows.Forms.TextBox txtLogin;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label labelSQL;
		private System.Windows.Forms.Label labelServer;
		private System.Windows.Forms.Button buttonSelectDatabase;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SqlConnectionDialog()
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
			this.labelPassword = new System.Windows.Forms.Label();
			this.labelLogin = new System.Windows.Forms.Label();
			this.labelConnect = new System.Windows.Forms.Label();
			this.optSQL = new System.Windows.Forms.RadioButton();
			this.optWindows = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtServer = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.labelDatabase = new System.Windows.Forms.Label();
			this.txtDatabase = new System.Windows.Forms.TextBox();
			this.txtLogin = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.labelSQL = new System.Windows.Forms.Label();
			this.buttonSelectDatabase = new System.Windows.Forms.Button();
			this.labelServer = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelPassword
			// 
			this.labelPassword.BackColor = System.Drawing.SystemColors.Control;
			this.labelPassword.Location = new System.Drawing.Point(88, 144);
			this.labelPassword.Name = "labelPassword";
			this.labelPassword.Size = new System.Drawing.Size(72, 16);
			this.labelPassword.TabIndex = 16;
			this.labelPassword.Text = "Password:";
			// 
			// labelLogin
			// 
			this.labelLogin.BackColor = System.Drawing.SystemColors.Control;
			this.labelLogin.Location = new System.Drawing.Point(88, 120);
			this.labelLogin.Name = "labelLogin";
			this.labelLogin.Size = new System.Drawing.Size(72, 16);
			this.labelLogin.TabIndex = 15;
			this.labelLogin.Text = "Login name:";
			// 
			// labelConnect
			// 
			this.labelConnect.BackColor = System.Drawing.SystemColors.Control;
			this.labelConnect.Location = new System.Drawing.Point(16, 56);
			this.labelConnect.Name = "labelConnect";
			this.labelConnect.Size = new System.Drawing.Size(88, 16);
			this.labelConnect.TabIndex = 14;
			this.labelConnect.Text = "Connect using:";
			// 
			// optSQL
			// 
			this.optSQL.BackColor = System.Drawing.SystemColors.Control;
			this.optSQL.Location = new System.Drawing.Point(32, 96);
			this.optSQL.Name = "optSQL";
			this.optSQL.Size = new System.Drawing.Size(160, 16);
			this.optSQL.TabIndex = 2;
			this.optSQL.Text = "SQL Server authentication";
			this.optSQL.CheckedChanged += new System.EventHandler(this.optSQL_CheckedChanged);
			// 
			// optWindows
			// 
			this.optWindows.BackColor = System.Drawing.SystemColors.Control;
			this.optWindows.Checked = true;
			this.optWindows.Location = new System.Drawing.Point(32, 72);
			this.optWindows.Name = "optWindows";
			this.optWindows.Size = new System.Drawing.Size(144, 16);
			this.optWindows.TabIndex = 1;
			this.optWindows.TabStop = true;
			this.optWindows.Text = "Windows authentication";
			this.optWindows.CheckedChanged += new System.EventHandler(this.optWindows_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox2.Location = new System.Drawing.Point(8, 40);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(352, 7);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox1.Location = new System.Drawing.Point(8, 168);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(352, 7);
			this.groupBox1.TabIndex = 17;
			this.groupBox1.TabStop = false;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(200, 224);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 11;
			this.btnOK.Text = "OK";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(288, 224);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "Cancel";
			// 
			// txtServer
			// 
			this.txtServer.Location = new System.Drawing.Point(176, 8);
			this.txtServer.Name = "txtServer";
			this.txtServer.Size = new System.Drawing.Size(184, 20);
			this.txtServer.TabIndex = 0;
			this.txtServer.Text = "";
			// 
			// groupBox3
			// 
			this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox3.Location = new System.Drawing.Point(8, 208);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(352, 7);
			this.groupBox3.TabIndex = 19;
			this.groupBox3.TabStop = false;
			// 
			// labelDatabase
			// 
			this.labelDatabase.BackColor = System.Drawing.SystemColors.Control;
			this.labelDatabase.Location = new System.Drawing.Point(56, 184);
			this.labelDatabase.Name = "labelDatabase";
			this.labelDatabase.Size = new System.Drawing.Size(72, 16);
			this.labelDatabase.TabIndex = 18;
			this.labelDatabase.Text = "Database:";
			// 
			// txtDatabase
			// 
			this.txtDatabase.Location = new System.Drawing.Point(144, 184);
			this.txtDatabase.Name = "txtDatabase";
			this.txtDatabase.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtDatabase.Size = new System.Drawing.Size(184, 20);
			this.txtDatabase.TabIndex = 5;
			this.txtDatabase.Text = "";
			// 
			// txtLogin
			// 
			this.txtLogin.Location = new System.Drawing.Point(176, 120);
			this.txtLogin.Name = "txtLogin";
			this.txtLogin.ReadOnly = true;
			this.txtLogin.Size = new System.Drawing.Size(184, 20);
			this.txtLogin.TabIndex = 3;
			this.txtLogin.Text = "";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(176, 144);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.ReadOnly = true;
			this.txtPassword.Size = new System.Drawing.Size(184, 20);
			this.txtPassword.TabIndex = 4;
			this.txtPassword.Text = "";
			// 
			// labelSQL
			// 
			this.labelSQL.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelSQL.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.labelSQL.Location = new System.Drawing.Point(0, 0);
			this.labelSQL.Name = "labelSQL";
			this.labelSQL.Size = new System.Drawing.Size(80, 40);
			this.labelSQL.TabIndex = 20;
			this.labelSQL.Text = "SQL";
			// 
			// buttonSelectDatabase
			// 
			this.buttonSelectDatabase.Location = new System.Drawing.Point(336, 184);
			this.buttonSelectDatabase.Name = "buttonSelectDatabase";
			this.buttonSelectDatabase.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectDatabase.TabIndex = 21;
			this.buttonSelectDatabase.Text = "...";
			this.buttonSelectDatabase.Click += new System.EventHandler(this.buttonSelectDatabase_Click);
			// 
			// labelServer
			// 
			this.labelServer.BackColor = System.Drawing.SystemColors.Control;
			this.labelServer.Location = new System.Drawing.Point(88, 10);
			this.labelServer.Name = "labelServer";
			this.labelServer.Size = new System.Drawing.Size(72, 16);
			this.labelServer.TabIndex = 22;
			this.labelServer.Text = "Server:";
			// 
			// SqlConnectionDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(370, 256);
			this.Controls.Add(this.labelServer);
			this.Controls.Add(this.buttonSelectDatabase);
			this.Controls.Add(this.labelSQL);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtLogin);
			this.Controls.Add(this.txtDatabase);
			this.Controls.Add(this.txtServer);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.labelDatabase);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.labelPassword);
			this.Controls.Add(this.labelLogin);
			this.Controls.Add(this.labelConnect);
			this.Controls.Add(this.optSQL);
			this.Controls.Add(this.optWindows);
			this.Controls.Add(this.groupBox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SqlConnectionDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Connect To SQL";
			this.Load += new System.EventHandler(this.SqlConnectionDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void optWindows_CheckedChanged(object sender, System.EventArgs e)
		{
			this.txtLogin.ReadOnly = true;
			this.txtPassword.ReadOnly = true;
		}

		private void optSQL_CheckedChanged(object sender, System.EventArgs e)
		{
			this.txtLogin.ReadOnly = false;
			this.txtPassword.ReadOnly = false;
		}

		#endregion

		private void SqlConnectionDialog_Load(object sender, System.EventArgs e)
		{
			if(this.DesignMode)
			{
				return;
			}

		}

		private void buttonSelectDatabase_Click(object sender, System.EventArgs e)
		{
			SqlDatabaseList form = new SqlDatabaseList();
			form.Server = this.Server;
			form.Windows = this.Windows;
			form.Login = this.Login;
			form.Password = this.Password;
			System.Windows.Forms.DialogResult dialogResult = form.ShowDialog();

			if(dialogResult != System.Windows.Forms.DialogResult.OK)
			{
				return;
			}

			this.txtDatabase.Text = form.Database;
		}

		private bool Sql
		{
			get { return this.optSQL.Checked; }
		}

		private bool Windows
		{
			get { return this.optWindows.Checked; }
		}

		private string Server
		{
			get { return this.txtServer.Text; }
		}

		private string Database
		{
			get { return this.txtDatabase.Text; }
		}

		private string Login
		{
			get { return this.txtLogin.Text; }
		}

		private string Password
		{
			get { return this.txtPassword.Text; }
		}

		public string ConnectionName
		{
			get { return this.txtDatabase.Text; }
		}

		public Bamboo.Mssql.ConnectionString ConnectionString
		{
			get
			{
				Bamboo.Mssql.ConnectionString connectionString = new Bamboo.Mssql.ConnectionString();
				connectionString.TrustedConnection = this.Windows;
				connectionString.Server = this.Server;
				connectionString.Database = this.Database;
				connectionString.Username = this.Login;
				connectionString.Password = this.Password;
				return connectionString;
			}
		}

	}
}
