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

namespace bamboo.Controls.CommandWindow
{
	/// <summary>
	/// Summary description for CommandWindowControl.
	/// </summary>
	public class CommandWindowControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.RichTextBox richTextBox;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CommandWindowControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();



			this.Text = "Command Window";

			this.Tag = "View.CommandWindow";
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
			this.richTextBox = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// richTextBox
			// 
			this.richTextBox.DetectUrls = false;
			this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBox.Location = new System.Drawing.Point(0, 0);
			this.richTextBox.Name = "richTextBox";
			this.richTextBox.Size = new System.Drawing.Size(600, 240);
			this.richTextBox.TabIndex = 0;
			this.richTextBox.Text = "";
			this.richTextBox.WordWrap = false;
			this.richTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox_KeyDown);
			// 
			// CommandWindowControl
			// 
			this.Controls.Add(this.richTextBox);
			this.Name = "CommandWindowControl";
			this.Size = new System.Drawing.Size(600, 240);
			this.Load += new System.EventHandler(this.CommandWindowControl_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private CommandHistory _commandHistory = new CommandHistory();
		private int _index = 0;

		private void richTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Enter)
			{
				e.Handled = true;
				this._index = 0;

				string line = this.CurrentLine;

				this._commandHistory.Add(line);

				this.richTextBox.AppendText(System.Environment.NewLine);

				this.Execute(line);

				this.WritePrompt();
			}
			else if (e.KeyCode == System.Windows.Forms.Keys.Back)
			{
				if (this.CurrentLine.Length == 0)
				{
					//Don't erase prompt.
					e.Handled = true;
				}
			}
			else if (e.KeyCode == System.Windows.Forms.Keys.Up)
			{
				if (this._index < this._commandHistory.Count)
				{
					this.CurrentLine = this._commandHistory[this._index];
					this._index++;
				}
				e.Handled = true;
			}
			else if (e.KeyCode == System.Windows.Forms.Keys.Down)
			{
				if (this._index != 0 && this._index <= this._commandHistory.Count)
				{
					this._index--;
					this.CurrentLine = this._commandHistory[this._index];
				}
				e.Handled = true;
			}
		}

		private void CommandWindowControl_Load(object sender, System.EventArgs e)
		{
			if (this.DesignMode)
			{
				return;
			}

			this.WritePrompt();
			this.richTextBox.SelectionStart = this.richTextBox.Text.Length;
		}

		private string CurrentLine
		{
			get
			{
				string currentLine = this.richTextBox.Lines[this.richTextBox.Lines.Length - 1];
				return currentLine.Substring(1);
			}
			set
			{
				int length = this.CurrentLine.Length;
				this.richTextBox.Select(this.richTextBox.Text.Length - length, length);
				this.richTextBox.SelectedText = value;
			}
		}

		public void WritePrompt()
		{
			this.richTextBox.AppendText(">");
		}

		public void WriteLine(string text)
		{
			this.richTextBox.AppendText(text);
			this.richTextBox.AppendText(System.Environment.NewLine);
			this.richTextBox.Focus();
			this.richTextBox.ScrollToCaret();
		}

		private void Execute(string text)
		{
			System.IO.StringWriter writer = new System.IO.StringWriter();
			Water.Environment.Output = writer;

			Water.StatementParser parser = new Water.StatementParser(new System.IO.StringReader(text));

			Water.Interpreter.Interpret(parser.Parse(), Water.Environment.Output);

			WriteLine(writer.ToString());
		}

	}
}
