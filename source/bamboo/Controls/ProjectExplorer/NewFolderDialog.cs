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

namespace bamboo.Controls.ProjectExplorer
{
	/// <summary>
	/// Summary description for NewFolderDialog.
	/// </summary>
	public class NewFolderDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxFolderName;
		private System.Windows.Forms.Button buttonCreate;
		private System.Windows.Forms.Button buttonCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NewFolderDialog()
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
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonCreate = new System.Windows.Forms.Button();
			this.textBoxFolderName = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(264, 40);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 13;
			this.buttonCancel.Text = "Cancel";
			// 
			// buttonCreate
			// 
			this.buttonCreate.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonCreate.Enabled = false;
			this.buttonCreate.Location = new System.Drawing.Point(176, 40);
			this.buttonCreate.Name = "buttonCreate";
			this.buttonCreate.TabIndex = 12;
			this.buttonCreate.Text = "Create";
			// 
			// textBoxFolderName
			// 
			this.textBoxFolderName.Location = new System.Drawing.Point(8, 8);
			this.textBoxFolderName.Name = "textBoxFolderName";
			this.textBoxFolderName.Size = new System.Drawing.Size(328, 20);
			this.textBoxFolderName.TabIndex = 11;
			this.textBoxFolderName.Text = "";
			this.textBoxFolderName.TextChanged += new System.EventHandler(this.textBoxFolderName_TextChanged);
			// 
			// NewFolderDialog
			// 
			this.AcceptButton = this.buttonCreate;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(346, 71);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonCreate);
			this.Controls.Add(this.textBoxFolderName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewFolderDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Folder";
			this.ResumeLayout(false);

		}
		#endregion

		private void textBoxFolderName_TextChanged(object sender, System.EventArgs e)
		{
			if(this.textBoxFolderName.Text.Equals(String.Empty))
			{
				this.buttonCreate.Enabled = false;
			}
			else
			{
				this.buttonCreate.Enabled = true;
			}

		}

		public string FolderName
		{
			get { return this.textBoxFolderName.Text; }
			set { this.textBoxFolderName.Text = value; }
		}

	}
}
