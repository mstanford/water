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
	/// Summary description for ReplaceDialog.
	/// </summary>
	public class ReplaceDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxFindText;
		private System.Windows.Forms.CheckBox checkBoxMatchCase;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.TextBox textBoxReplaceText;
		private System.Windows.Forms.Label labelReplaceWith;
		private System.Windows.Forms.Button buttonReplace;
		private System.Windows.Forms.Button buttonReplaceAll;
		private System.Windows.Forms.Label labelFindWhat;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ReplaceDialog(string findCommand, string replaceCommand, string replaceAllCommand)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


			this._findCommand = findCommand;
			this._replaceCommand = replaceCommand;
			this._replaceAllCommand = replaceAllCommand;
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
			this.buttonReplace = new System.Windows.Forms.Button();
			this.checkBoxMatchCase = new System.Windows.Forms.CheckBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.textBoxReplaceText = new System.Windows.Forms.TextBox();
			this.labelReplaceWith = new System.Windows.Forms.Label();
			this.buttonReplaceAll = new System.Windows.Forms.Button();
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
			// buttonReplace
			// 
			this.buttonReplace.Location = new System.Drawing.Point(408, 8);
			this.buttonReplace.Name = "buttonReplace";
			this.buttonReplace.TabIndex = 2;
			this.buttonReplace.Text = "Replace";
			this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
			// 
			// checkBoxMatchCase
			// 
			this.checkBoxMatchCase.Location = new System.Drawing.Point(96, 72);
			this.checkBoxMatchCase.Name = "checkBoxMatchCase";
			this.checkBoxMatchCase.Size = new System.Drawing.Size(104, 20);
			this.checkBoxMatchCase.TabIndex = 6;
			this.checkBoxMatchCase.Text = "Match case";
			this.checkBoxMatchCase.CheckedChanged += new System.EventHandler(this.checkBoxMatchCase_CheckedChanged);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(408, 72);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.TabIndex = 7;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// textBoxReplaceText
			// 
			this.textBoxReplaceText.Location = new System.Drawing.Point(96, 40);
			this.textBoxReplaceText.Name = "textBoxReplaceText";
			this.textBoxReplaceText.Size = new System.Drawing.Size(280, 21);
			this.textBoxReplaceText.TabIndex = 4;
			this.textBoxReplaceText.Text = "";
			this.textBoxReplaceText.TextChanged += new System.EventHandler(this.textBoxReplaceText_TextChanged);
			// 
			// labelReplaceWith
			// 
			this.labelReplaceWith.Location = new System.Drawing.Point(8, 40);
			this.labelReplaceWith.Name = "labelReplaceWith";
			this.labelReplaceWith.Size = new System.Drawing.Size(88, 20);
			this.labelReplaceWith.TabIndex = 3;
			this.labelReplaceWith.Text = "Replace With";
			this.labelReplaceWith.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonReplaceAll
			// 
			this.buttonReplaceAll.Location = new System.Drawing.Point(408, 40);
			this.buttonReplaceAll.Name = "buttonReplaceAll";
			this.buttonReplaceAll.TabIndex = 5;
			this.buttonReplaceAll.Text = "Replace All";
			this.buttonReplaceAll.Click += new System.EventHandler(this.buttonReplaceAll_Click);
			// 
			// ReplaceDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(496, 104);
			this.Controls.Add(this.buttonReplaceAll);
			this.Controls.Add(this.textBoxReplaceText);
			this.Controls.Add(this.textBoxFindText);
			this.Controls.Add(this.labelReplaceWith);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.checkBoxMatchCase);
			this.Controls.Add(this.buttonReplace);
			this.Controls.Add(this.labelFindWhat);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.KeyPreview = true;
			this.Name = "ReplaceDialog";
			this.ShowInTaskbar = false;
			this.Text = "Replace";
			this.TopMost = true;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReplaceDialog_KeyDown);
			this.Load += new System.EventHandler(this.ReplaceDialog_Load);
			this.Closed += new System.EventHandler(this.ReplaceDialog_Closed);
			this.ResumeLayout(false);

		}
		#endregion

		private string _findCommand;
		private string _replaceCommand;
		private string _replaceAllCommand;

		public string FindText
		{
			get { return this.textBoxFindText.Text; }
			set { this.textBoxFindText.Text = value; }
		}

		public string ReplaceText
		{
			get { return this.textBoxReplaceText.Text; }
			set { this.textBoxReplaceText.Text = value; }
		}

		public bool MatchCase
		{
			get { return this.checkBoxMatchCase.Checked; }
			set { this.checkBoxMatchCase.Checked = value; }
		}

		private void ReplaceDialog_Load(object sender, System.EventArgs e)
		{
			this.LoadSettings();
		}

		private void ReplaceDialog_Closed(object sender, System.EventArgs e)
		{
			this.SaveSettings();

			this.Dispose();
		}

		private void buttonReplace_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			Water.Evaluator.Apply(this._replaceCommand, this.FindText, this.ReplaceText);

			this.Cursor = Cursors.Default;
		}

		private void buttonReplaceAll_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			Water.Evaluator.Apply(this._replaceAllCommand, this.FindText, this.ReplaceText);

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
				this.buttonReplace.Enabled = false;
				this.buttonReplaceAll.Enabled = false;
			}
			else
			{
				this.buttonReplace.Enabled = true;
				this.buttonReplaceAll.Enabled = true;
			}

//TODO			SearchHistoryService.FindText = this.textBoxFindText.Text;
		}

		private void textBoxReplaceText_TextChanged(object sender, System.EventArgs e)
		{
//TODO			SearchHistoryService.ReplaceText = this.textBoxReplaceText.Text;
		}

		private void checkBoxMatchCase_CheckedChanged(object sender, System.EventArgs e)
		{
			//TODO save this in SearchHistoryService.
		}

		private void ReplaceDialog_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == System.Windows.Forms.Keys.Escape)
			{
				this.Close();
			}
			else if(e.Control && e.KeyCode == System.Windows.Forms.Keys.F)
			{
				Water.Evaluator.Apply(this._findCommand);
			}
		}

		private void LoadSettings()
		{
			Water.Dictionary Settings = (Water.Dictionary)Water.Environment.Identify("Settings");

			if (Settings.IsDefined("ReplaceDialog.Left"))
			{
				this.Left = (int)Settings["ReplaceDialog.Left"];
			}
			if (Settings.IsDefined("ReplaceDialog.Top"))
			{
				this.Top = (int)Settings["ReplaceDialog.Top"];
			}
		}

		private void SaveSettings()
		{
			Water.Dictionary Settings = (Water.Dictionary)Water.Environment.Identify("Settings");

			Settings["ReplaceDialog.Left"] = this.Left;
			Settings["ReplaceDialog.Top"] = this.Top;
		}

	}
}
