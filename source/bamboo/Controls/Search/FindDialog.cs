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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace bamboo.Controls.Search
{
	/// <summary>
	/// Summary description for FindDialog.
	/// </summary>
	public class FindDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxFindText;
		private System.Windows.Forms.Button buttonFind;
		private System.Windows.Forms.CheckBox checkBoxMatchCase;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Label labelFindWhat;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FindDialog(string findCommand)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


			this._findCommand = findCommand;
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
			this.labelFindWhat = new System.Windows.Forms.Label();
			this.textBoxFindText = new System.Windows.Forms.TextBox();
			this.buttonFind = new System.Windows.Forms.Button();
			this.checkBoxMatchCase = new System.Windows.Forms.CheckBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelFindWhat
			// 
			this.labelFindWhat.Location = new System.Drawing.Point(8, 8);
			this.labelFindWhat.Name = "labelFindWhat";
			this.labelFindWhat.Size = new System.Drawing.Size(80, 20);
			this.labelFindWhat.TabIndex = 0;
			this.labelFindWhat.Text = "Find What";
			this.labelFindWhat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxFindText
			// 
			this.textBoxFindText.Location = new System.Drawing.Point(96, 8);
			this.textBoxFindText.Name = "textBoxFindText";
			this.textBoxFindText.Size = new System.Drawing.Size(280, 21);
			this.textBoxFindText.TabIndex = 1;
			this.textBoxFindText.Text = "";
			this.textBoxFindText.TextChanged += new System.EventHandler(this.textBoxFindText_TextChanged);
			// 
			// buttonFind
			// 
			this.buttonFind.Location = new System.Drawing.Point(408, 8);
			this.buttonFind.Name = "buttonFind";
			this.buttonFind.TabIndex = 2;
			this.buttonFind.Text = "Find";
			this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
			// 
			// checkBoxMatchCase
			// 
			this.checkBoxMatchCase.Location = new System.Drawing.Point(96, 40);
			this.checkBoxMatchCase.Name = "checkBoxMatchCase";
			this.checkBoxMatchCase.Size = new System.Drawing.Size(104, 20);
			this.checkBoxMatchCase.TabIndex = 3;
			this.checkBoxMatchCase.Text = "Match case";
			this.checkBoxMatchCase.CheckedChanged += new System.EventHandler(this.checkBoxMatchCase_CheckedChanged);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(408, 40);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.TabIndex = 4;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// FindDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(496, 72);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.checkBoxMatchCase);
			this.Controls.Add(this.buttonFind);
			this.Controls.Add(this.textBoxFindText);
			this.Controls.Add(this.labelFindWhat);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.KeyPreview = true;
			this.Name = "FindDialog";
			this.ShowInTaskbar = false;
			this.Text = "Find";
			this.TopMost = true;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FindDialog_KeyDown);
			this.Load += new System.EventHandler(this.FindDialog_Load);
			this.Closed += new System.EventHandler(this.FindDialog_Closed);
			this.ResumeLayout(false);

		}
		#endregion

		private string _findCommand;

		public string FindText
		{
			get { return this.textBoxFindText.Text; }
			set { this.textBoxFindText.Text = value; }
		}

		public bool MatchCase
		{
			get { return this.checkBoxMatchCase.Checked; }
			set { this.checkBoxMatchCase.Checked = value; }
		}

		private void FindDialog_Load(object sender, System.EventArgs e)
		{
			this.LoadSettings();
		}

		private void FindDialog_Closed(object sender, System.EventArgs e)
		{
			this.SaveSettings();

			this.Dispose();
		}

		private void buttonFind_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			Water.Evaluator.Apply(this._findCommand, this.FindText);

			this.Cursor = Cursors.Default;
		}

		private void buttonClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void textBoxFindText_TextChanged(object sender, System.EventArgs e)
		{
			if(this.textBoxFindText.Text.Equals(String.Empty))
			{
				this.buttonFind.Enabled = false;
			}
			else
			{
				this.buttonFind.Enabled = true;
			}

//TODO			SearchHistoryService.FindText = this.textBoxFindText.Text;
		}

		private void checkBoxMatchCase_CheckedChanged(object sender, System.EventArgs e)
		{
			//TODO save this in SearchHistoryService.
		}

		private void FindDialog_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == System.Windows.Forms.Keys.Escape)
			{
				this.Close();
			}
		}

		private void LoadSettings()
		{
			Water.Dictionary Settings = (Water.Dictionary)Water.Environment.Identify("Settings");

			if (Settings.IsDefined("FindDialog.Left"))
			{
				this.Left = (int)Settings["FindDialog.Left"];
			}
			if (Settings.IsDefined("FindDialog.Top"))
			{
				this.Top = (int)Settings["FindDialog.Top"];
			}
		}

		private void SaveSettings()
		{
			Water.Dictionary Settings = (Water.Dictionary)Water.Environment.Identify("Settings");

			Settings["FindDialog.Left"] = this.Left;
			Settings["FindDialog.Top"] = this.Top;
		}

	}
}
