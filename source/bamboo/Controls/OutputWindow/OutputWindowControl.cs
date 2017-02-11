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

namespace bamboo.Controls.OutputWindow
{
	/// <summary>
	/// Summary description for OutputWindowControl.
	/// </summary>
	public class OutputWindowControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.RichTextBox richTextBox;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public OutputWindowControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();



			this.Text = "Output Window";

			this.Tag = "View.OutputWindow";
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
			this.richTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox.Location = new System.Drawing.Point(0, 0);
			this.richTextBox.Name = "richTextBox";
			this.richTextBox.ReadOnly = true;
			this.richTextBox.Size = new System.Drawing.Size(600, 240);
			this.richTextBox.TabIndex = 1;
			this.richTextBox.Text = "";
			this.richTextBox.WordWrap = false;
			// 
			// OutputWindowControl
			// 
			this.Controls.Add(this.richTextBox);
			this.Name = "OutputWindowControl";
			this.Size = new System.Drawing.Size(600, 240);
			this.ResumeLayout(false);

		}
		#endregion

		private delegate void SafeClearDelegate();

		public void Clear()
		{
			if(this.InvokeRequired)
			{
				SafeClearDelegate safeClearDelegate = new SafeClearDelegate(this.Clear);

				this.Invoke(safeClearDelegate, new object[] { });
				return;
			}

			this.richTextBox.Clear();
		}

		private delegate void SafeWriteLineDelegate(string text);

		public void WriteLine(string text)
		{
			if(this.InvokeRequired)
			{
				SafeWriteLineDelegate safeWriteLineDelegate = new SafeWriteLineDelegate(this.WriteLine);

				this.Invoke(safeWriteLineDelegate, new object[] { text });
				return;
			}

			this.richTextBox.AppendText(text);
			this.richTextBox.AppendText(System.Environment.NewLine);
			this.richTextBox.Focus();
			this.richTextBox.ScrollToCaret();
		}

	}
}
