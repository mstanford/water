// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005-2008 Swampware, Inc.
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

namespace bamboo.Controls.Editor
{
	/// <summary>
	/// Summary description for EditorControl.
	/// </summary>
	public class EditorControl : UserControl
	{
		private Wangdera.Controls.RichTextEditor richTextEditor;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public EditorControl()
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
			this.richTextEditor = new Wangdera.Controls.RichTextEditor();
			this.SuspendLayout();
			// 
			// richTextEditor
			// 
			this.richTextEditor.AcceptsTab = true;
			this.richTextEditor.AllowDrop = true;
			this.richTextEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextEditor.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextEditor.Link2Cursor = System.Windows.Forms.Cursors.Hand;
			this.richTextEditor.Link2Enabled = false;
			this.richTextEditor.Location = new System.Drawing.Point(0, 0);
			this.richTextEditor.Name = "richTextEditor";
			this.richTextEditor.Size = new System.Drawing.Size(528, 448);
			this.richTextEditor.TabIndex = 0;
			this.richTextEditor.Text = "";
			this.richTextEditor.UndoLength = 100;
			this.richTextEditor.WordWrap = false;
			this.richTextEditor.Enter += new System.EventHandler(this.richTextEditor_Enter);
			this.richTextEditor.Leave += new System.EventHandler(this.richTextEditor_Leave);
			this.richTextEditor.TextChanged += new System.EventHandler(this.editorControl_TextChanged);
			// 
			// EditorControl
			// 
			this.Controls.Add(this.richTextEditor);
			this.Name = "EditorControl";
			this.Size = new System.Drawing.Size(528, 448);
			this.ResumeLayout(false);

		}
		#endregion

		private string _filename = null;
		private bool _isDirty = false;
		private SyntaxStyle[] _syntaxStyles = new SyntaxStyle[0];

		public string Filename
		{
			get { return this._filename; }
			set
			{
				this._filename = value;
				this.UpdateTitle();
				this.SyntaxHighlighting();
			}
		}

		public bool IsDirty
		{
			get { return this._isDirty; }
			set
			{
				this._isDirty = value;
				this.UpdateTitle();
			}
		}

		public string EditorText
		{
			get { return this.richTextEditor.Text; }
			set
			{
				this.richTextEditor.Text = value;
				this.richTextEditor.ClearUndo();
			}
		}

		public int Line
		{
			get
			{
				int selectionStart = this.richTextEditor.SelectionStart;
				int line = 1 + this.richTextEditor.GetLineFromCharIndex(selectionStart);

				return line;
			}
		}

		public int Column
		{
			get
			{
				int selectionStart = this.richTextEditor.SelectionStart;
				int line = 1 + this.richTextEditor.GetLineFromCharIndex(selectionStart);
				int endOfLine = 0;
				for (int i = 0; i < (line - 1); i++)
				{
					endOfLine += this.richTextEditor.Lines[i].Length + 1;
				}
				int column = selectionStart - endOfLine + 1;

				return column;
			}
		}

		private void UpdateTitle()
		{
			if (this._filename == null || this._filename.Length == 0)
			{
				if (this._isDirty)
				{
					this.Text = String.Format("{0} {1}", "Untitled", "*");
				}
				else
				{
					this.Text = "Untitled";
				}
			}
			else
			{
				System.IO.FileInfo fileInfo = new System.IO.FileInfo(this._filename);
				if (this._isDirty)
				{
					this.Text = String.Format("{0} {1}", fileInfo.Name, "*");
				}
				else
				{
					this.Text = fileInfo.Name;
				}
			}
		}

		private void SyntaxHighlighting()
		{
			if (this._filename == null || this._filename.Length == 0)
			{
				return;
			}

			int index = this._filename.LastIndexOf(".");
			if (index == -1)
			{
				return;
			}

			string extension = this._filename.Substring(index);

			switch (extension.ToLower())
			{
				case ".cs":
					{
						this._syntaxStyles = new SyntaxStyle[3];
						this._syntaxStyles[0] = new SyntaxStyle(new System.Text.RegularExpressions.Regex("abstract\\s|as\\s|base\\s|bool\\s|break\\s|byte\\s|case\\s|catch\\s|char\\s|checked\\s|class\\s|const\\s|continue\\s|decimal\\s|default\\s|delegate\\s|do\\s|double\\s|else\\s|enum\\s|event\\s|explicit\\s|extern\\s|false\\s|finally\\s|fixed\\s|float\\s|for\\s|foreach\\s|get\\s|goto\\s|if\\s|implicit\\s|in\\s|int\\s|interface\\s|internal|is\\s|lock\\s|long\\s|namespace\\s|new\\s|null\\s|object\\s|operator\\s|out\\s|override\\s|params\\s|private\\s|protected\\s|public\\s|readonly\\s|ref\\s|return\\s|set\\s|sbyte\\s|sealed\\s|short\\s|sizeof\\s|stackalloc\\s|static\\s|string\\s|struct\\s|switch\\s|this(\\.|\\s)*|throw\\s|true\\s|try\\s|typeof\\s|uint\\s|ulong\\s|unchecked\\s|unsafe\\s|ushort\\s|using\\s|virtual\\s|void\\s|volatile\\s|while\\s", System.Text.RegularExpressions.RegexOptions.Compiled), System.Drawing.Color.White, System.Drawing.Color.Blue);
						this._syntaxStyles[1] = new SyntaxStyle(new System.Text.RegularExpressions.Regex(@"(\/\/)[^\r\n]*", System.Text.RegularExpressions.RegexOptions.Compiled), System.Drawing.Color.White, System.Drawing.Color.Green);
						this._syntaxStyles[2] = new SyntaxStyle(new System.Text.RegularExpressions.Regex(@"(\/\*)((\/\*)|(\/\/)|(\*\*)|[^*/])*(\*\/)", System.Text.RegularExpressions.RegexOptions.Compiled), System.Drawing.Color.White, System.Drawing.Color.Green);
						break;
					}
				case ".sql":
					{
						this._syntaxStyles = new SyntaxStyle[3];
						this._syntaxStyles[0] = new SyntaxStyle(new System.Text.RegularExpressions.Regex("ADD\\s|EXCEPT\\s|PERCENT\\s|ALL\\s|EXEC\\s|PLAN\\s|ALTER\\s|EXECUTE\\s|PRECISION\\s|AND\\s|EXISTS\\s|PRIMARY\\s|ANY\\s|EXIT\\s|PRINT\\s|AS\\s|FETCH\\s|PROC\\s|ASC\\s|FILE\\s|PROCEDURE\\s|AUTHORIZATION\\s|FILLFACTOR\\s|PUBLIC\\s|BACKUP\\s|FOR\\s|RAISERROR\\s|BEGIN\\s|FOREIGN\\s|READ\\s|BETWEEN\\s|FREETEXT\\s|READTEXT\\s|BREAK\\s|FREETEXTTABLE\\s|RECONFIGURE\\s|BROWSE\\s|FROM\\s|REFERENCES\\s|BULK\\s|FULL\\s|REPLICATION\\s|BY\\s|FUNCTION\\s|RESTORE\\s|CASCADE\\s|GOTO\\s|RESTRICT\\s|CASE\\s|GRANT\\s|RETURN\\s|CHECK\\s|GROUP\\s|REVOKE\\s|CHECKPOINT\\s|HAVING\\s|RIGHT\\s|CLOSE\\s|HOLDLOCK\\s|ROLLBACK\\s|CLUSTERED\\s|IDENTITY\\s|ROWCOUNT\\s|COALESCE\\s|IDENTITY_INSERT\\s|ROWGUIDCOL\\s|COLLATE\\s|IDENTITYCOL\\s|RULE\\s|COLUMN\\s|IF\\s|SAVE\\s|COMMIT\\s|IN\\s|SCHEMA\\s|COMPUTE\\s|INDEX\\s|SELECT\\s|CONSTRAINT\\s|INNER\\s|SESSION_USER\\s|CONTAINS\\s|INSERT\\s|SET\\s|CONTAINSTABLE\\s|INTERSECT\\s|SETUSER\\s|CONTINUE\\s|INTO\\s|SHUTDOWN\\s|CONVERT\\s|IS\\s|SOME\\s|CREATE\\s|JOIN\\s|STATISTICS\\s|CROSS\\s|KEY\\s|SYSTEM_USER\\s|CURRENT\\s|KILL\\s|TABLE\\s|CURRENT_DATE\\s|LEFT\\s|TEXTSIZE\\s|CURRENT_TIME\\s|LIKE\\s|THEN\\s|CURRENT_TIMESTAMP\\s|LINENO\\s|TO\\s|CURRENT_USER\\s|LOAD\\s|TOP\\s|CURSOR\\s|NATIONAL\\s|\\s|TRAN\\s|DATABASE\\s|NOCHECK\\s|TRANSACTION\\s|DBCC\\s|NONCLUSTERED\\s|TRIGGER\\s|DEALLOCATE\\s|NOT\\s|TRUNCATE\\s|DECLARE\\s|NULL\\s|TSEQUAL\\s|DEFAULT\\s|NULLIF\\s|UNION\\s|DELETE\\s|OF\\s|UNIQUE\\s|DENY\\s|OFF\\s|UPDATE\\s|DESC\\s|OFFSETS\\s|UPDATETEXT\\s|DISK\\s|ON\\s|USE\\s|DISTINCT\\s|OPEN\\s|USER\\s|DISTRIBUTED\\s|OPENDATASOURCE\\s|VALUES\\s|DOUBLE\\s|OPENQUERY\\s|VARYING\\s|DROP\\s|OPENROWSET\\s|VIEW\\s|DUMMY\\s|OPENXML\\s|WAITFOR\\s|DUMP\\s|OPTION\\s|WHEN\\s|ELSE\\s|OR\\s|WHERE\\s|END\\s|ORDER\\s|WHILE\\s|ERRLVL\\s|OUTER\\s|WITH\\s|ESCAPE\\s|OVER\\s|WRITETEXT\\s", System.Text.RegularExpressions.RegexOptions.Compiled), System.Drawing.Color.White, System.Drawing.Color.Blue);
						this._syntaxStyles[1] = new SyntaxStyle(new System.Text.RegularExpressions.Regex(@"(\-\-)[^\r\n]*", System.Text.RegularExpressions.RegexOptions.Compiled), System.Drawing.Color.White, System.Drawing.Color.Green);
						this._syntaxStyles[2] = new SyntaxStyle(new System.Text.RegularExpressions.Regex(@"(\/\*)((\/\*)|(\/\/)|(\*\*)|[^*/])*(\*\/)", System.Text.RegularExpressions.RegexOptions.Compiled), System.Drawing.Color.White, System.Drawing.Color.Green);
						break;
					}
				case ".xml":
					{
						this._syntaxStyles = new SyntaxStyle[2];
						this._syntaxStyles[0] = new SyntaxStyle(new System.Text.RegularExpressions.Regex(@"<[^>]*(<|>)", System.Text.RegularExpressions.RegexOptions.Compiled), System.Drawing.Color.White, System.Drawing.Color.Blue);
						this._syntaxStyles[1] = new SyntaxStyle(new System.Text.RegularExpressions.Regex(@"\s*\w*\s*=\s*(""[^""]*""|'[^']'|[^>]|[^/]*)", System.Text.RegularExpressions.RegexOptions.Compiled), System.Drawing.Color.White, System.Drawing.Color.Red);
						break;
					}
			}
		}

		private void editorControl_TextChanged(object sender, System.EventArgs e)
		{
			this.IsDirty = true;

			string contents = this.richTextEditor.Text;

			Wangdera.Controls.FormattingInstructionCollection instructions = new Wangdera.Controls.FormattingInstructionCollection();

			Wangdera.Controls.CharacterFormat format = new Wangdera.Controls.CharacterFormat();
			format.ForeColor = Color.Black;
			format.UnderlineFormat = new Wangdera.Controls.UnderlineFormat(Wangdera.Controls.UnderlineStyle.None, Wangdera.Controls.UnderlineColor.Black);
			instructions.Add(new Wangdera.Controls.FormattingInstruction(0, this.richTextEditor.Text.Length, format));

			foreach (SyntaxStyle syntaxStyle in this._syntaxStyles)
			{
				foreach (System.Text.RegularExpressions.Match match in syntaxStyle.Regex.Matches(contents))
				{
					Wangdera.Controls.CharacterFormat characterFormat = new Wangdera.Controls.CharacterFormat();
					characterFormat.BackColor = syntaxStyle.BackColor;
					characterFormat.ForeColor = syntaxStyle.ForeColor;
					instructions.Add(new Wangdera.Controls.FormattingInstruction(match.Index, match.Length, characterFormat));
				}
			}

			this.richTextEditor.BatchFormat(instructions);
		}

		public void FileOpen(string filename)
		{
		    this.Filename = filename;
			System.IO.FileStream stream = new System.IO.FileStream(this.Filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
			System.IO.StreamReader reader = new System.IO.StreamReader(stream, System.Text.Encoding.Default);
			this.EditorText = reader.ReadToEnd();
			reader.Close();
			stream.Close();
			this.IsDirty = false;
			this.richTextEditor.ClearUndo();
		}

		public void FileSave()
		{
			if (!this.IsDirty)
			{
				return;
			}

			if (this.Filename.Length == 0)
			{
				System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
				System.Windows.Forms.DialogResult result = saveFileDialog.ShowDialog();
				if (result != System.Windows.Forms.DialogResult.OK)
				{
					saveFileDialog.Dispose();
					return;
				}
				this.Filename = saveFileDialog.FileName;
				saveFileDialog.Dispose();

				if (this.Filename.Length == 0)
				{
					return;
				}
			}

			System.IO.FileStream stream = new System.IO.FileStream(this.Filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
			System.IO.StreamWriter writer = new System.IO.StreamWriter(stream, System.Text.Encoding.Default);
			writer.Write(this.EditorText);
			writer.Flush();
			stream.Close();
			this.IsDirty = false;
		}

		public void FileSaveAs()
		{
			System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			System.Windows.Forms.DialogResult result = saveFileDialog.ShowDialog();
			if (result != System.Windows.Forms.DialogResult.OK)
			{
				saveFileDialog.Dispose();
				return;
			}
			this.Filename = saveFileDialog.FileName;
			saveFileDialog.Dispose();

			if (this.Filename.Length == 0)
			{
				return;
			}

			this.FileSave();
		}

		public void EditUndo()
		{
			this.richTextEditor.Undo();
		}

		public void EditRedo()
		{
			this.richTextEditor.Redo();
		}

		public void EditCut()
		{
			this.richTextEditor.Cut();
		}

		public void EditCopy()
		{
			this.richTextEditor.Copy();
		}

		public void EditPaste()
		{
			this.richTextEditor.Paste();
		}

		public void EditDelete()
		{
			this.richTextEditor.SelectedText = String.Empty;
		}

		public void EditSelectAll()
		{
			this.richTextEditor.SelectAll();
		}

		public void EditFind()
		{
			bamboo.Controls.Search.FindDialog dialog = new bamboo.Controls.Search.FindDialog("Editor.Find");
			dialog.Text = "Find in " + new System.IO.FileInfo(this.Filename).Name;
			string text = this.richTextEditor.SelectedText;
			if (text.Length > 0)
			{
				dialog.FindText = text;
			}
			dialog.Show();
		}

		public void EditorFind(string findText)
		{
			int selectionStart = this.richTextEditor.Find(findText, this.richTextEditor.SelectionStart + this.richTextEditor.SelectionLength, System.Windows.Forms.RichTextBoxFinds.None);

			if (selectionStart == -1)
			{
				selectionStart = this.richTextEditor.Find(findText, 0, System.Windows.Forms.RichTextBoxFinds.None);
			}

			if (selectionStart == -1)
			{
				System.Windows.Forms.MessageBox.Show("Unable to find \"" + findText + "\"", "Find");
				return;
			}

			this.richTextEditor.SelectionStart = selectionStart;
			this.richTextEditor.Focus();
		}

		public void EditReplace()
		{
			bamboo.Controls.Search.ReplaceDialog dialog = new bamboo.Controls.Search.ReplaceDialog("Editor.Find", "Editor.Replace", "Editor.ReplaceAll");
			dialog.Text = "Replace in " + new System.IO.FileInfo(this.Filename).Name;
			string text = this.richTextEditor.SelectedText;
			if (text.Length > 0)
			{
				dialog.FindText = text;
			}
			dialog.Show();
		}

		public void EditorReplace(string findText, string replaceText)
		{
			int selectionStart = this.richTextEditor.Find(findText, this.richTextEditor.SelectionStart + this.richTextEditor.SelectionLength, System.Windows.Forms.RichTextBoxFinds.None);

			if (selectionStart == -1)
			{
				selectionStart = this.richTextEditor.Find(findText, 0, System.Windows.Forms.RichTextBoxFinds.None);
			}

			if (selectionStart == -1)
			{
				System.Windows.Forms.MessageBox.Show("Unable to find \"" + findText + "\"", "Replace");
				return;
			}

			this.richTextEditor.SelectionStart = selectionStart;
			this.richTextEditor.SelectionLength = findText.Length;
			this.richTextEditor.SelectedText = replaceText;
			this.richTextEditor.Focus();
		}

		public void EditorReplaceAll(string findText, string replaceText)
		{
			int initialPosition = this.richTextEditor.SelectionStart + this.richTextEditor.SelectionLength;

			int selectionStart = this.richTextEditor.Find(findText, 0, System.Windows.Forms.RichTextBoxFinds.None);

			while (selectionStart != -1)
			{
				this.richTextEditor.SelectionStart = selectionStart;
				this.richTextEditor.SelectionLength = findText.Length;
				this.richTextEditor.SelectedText = replaceText;

				selectionStart = this.richTextEditor.Find(findText, this.richTextEditor.SelectionStart + this.richTextEditor.SelectionLength, System.Windows.Forms.RichTextBoxFinds.None);
			}

			this.richTextEditor.Select(initialPosition, 0);
			this.richTextEditor.Focus();
		}

		public void EditorGoto(int line, int column)
		{
			int start = 0;

			try
			{
				for (int i = 0; i < (line - 1); i++)
				{
					// + 1 is for the \n
					start += this.richTextEditor.Lines[i].Length + 1;
				}
				start += (column - 1);

				if (start < 0)
				{
					return;
				}
			}
			catch (System.Exception exception)
			{
				System.Exception IGNORE = exception;
			}

			this.richTextEditor.Select(start, 0);
			this.richTextEditor.Focus();
		}

		public bool Close()
		{
			if (!this._isDirty) return true;

			System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Save changes to " + this.Filename + "?", this.Filename, System.Windows.Forms.MessageBoxButtons.YesNoCancel);

			switch (dialogResult)
			{
				case System.Windows.Forms.DialogResult.Yes:
					{
						//Save and close.
						this.FileSave();
						break;
					}
				case System.Windows.Forms.DialogResult.No:
					{
						//Close it.
						break;
					}
				case System.Windows.Forms.DialogResult.Cancel:
					{
						//Don't close it.
						return false;
					}
			}

			return true;
		}

		#region Comment/Uncomment

		public void EditorComment(string comment)
		{
			string selectedText = this.richTextEditor.SelectedText;
			int selectionStart = this.richTextEditor.SelectionStart;
			int selectionLength = this.richTextEditor.SelectionLength;

			this.FullLineSelect();

			selectedText = this.richTextEditor.SelectedText;
			selectionStart = this.richTextEditor.SelectionStart;
			selectionLength = this.richTextEditor.SelectionLength;

			string result = this.Insert(selectedText, comment);

			this.richTextEditor.SelectedText = result;

			this.richTextEditor.SelectionStart = selectionStart;
			this.richTextEditor.SelectionLength = result.Length;
		}

		public void EditorUncomment(string comment)
		{
			string selectedText = this.richTextEditor.SelectedText;
			int selectionStart = this.richTextEditor.SelectionStart;
			int selectionLength = this.richTextEditor.SelectionLength;

			this.FullLineSelect();

			selectedText = this.richTextEditor.SelectedText;
			selectionStart = this.richTextEditor.SelectionStart;
			selectionLength = this.richTextEditor.SelectionLength;

			string result = this.Remove(selectedText, comment);

			this.richTextEditor.SelectedText = result;

			this.richTextEditor.SelectionStart = selectionStart;
			this.richTextEditor.SelectionLength = result.Length;
		}

		private void FullLineSelect()
		{
			string selectedText = this.richTextEditor.SelectedText;
			int selectionStart = this.richTextEditor.SelectionStart;
			int selectionLength = this.richTextEditor.SelectionLength;

			int lineNumber = this.richTextEditor.GetLineFromCharIndex(selectionStart);
			int lineStart = this.GetLineStart(lineNumber);

			if (selectedText.Length == 0)
			{
				string line = this.richTextEditor.Lines[lineNumber];

				selectionStart = lineStart;
				selectionLength = line.Length;
			}
			else
			{
				int length = 0;
				string[] lines = selectedText.Split("\n".ToCharArray());

				for (int i = 0; i < lines.Length; i++)
				{
					if (i == 0)
					{
						string line = this.richTextEditor.Lines[lineNumber];

						length += line.Length + "\n".Length;
					}
					else if (i == (lines.Length - 1))
					{
						length += lines[i].Length;
					}
					else
					{
						string line = this.richTextEditor.Lines[lineNumber + i];

						length += line.Length + "\n".Length;
					}
				}

				selectionStart = lineStart;
				selectionLength = length;
			}

			this.richTextEditor.SelectionStart = selectionStart;
			this.richTextEditor.SelectionLength = selectionLength;
		}

		private int GetLineStart(int lineNumber)
		{
			int lineStart = 0;

			for (int i = 0; i < lineNumber; i++)
			{
				lineStart += this.richTextEditor.Lines[i].Length + "\n".Length;
			}

			return lineStart;
		}

		private string Insert(string text, string comment)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

			string[] lines = text.Split("\n".ToCharArray());

			for (int i = 0; i < lines.Length; i++)
			{
				string line = lines[i];

				if (i > 0)
				{
					stringBuilder.Append("\n");
				}

				int index = GetStringIndex(comment, line);

				string commentedText;

				if (index == -1)
				{
					//Don't comment the last line if it is empty.
					if (line.Length == 0 && i == (lines.Length - 1))
					{
						commentedText = String.Empty;
					}
					else
					{
						commentedText = comment + line;
					}
				}
				else if (index == 0)
				{
					commentedText = comment + line;
				}
				else
				{
					commentedText = line.Substring(0, index) + comment + line.Substring(index);
				}

				stringBuilder.Append(commentedText);
			}

			return stringBuilder.ToString();
		}

		private string Remove(string text, string comment)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

			string[] lines = text.Split("\n".ToCharArray());

			for (int i = 0; i < lines.Length; i++)
			{
				string line = lines[i];

				if (i > 0)
				{
					stringBuilder.Append("\n");
				}

				int index = GetStringIndex(comment, line);

				string uncommentedText;

				if (index == -1)
				{
					uncommentedText = line;
				}
				else if (index == 0)
				{
					uncommentedText = line.Substring(0, index) + line.Substring(index + comment.Length);
				}
				else
				{
					uncommentedText = line.Substring(0, index) + line.Substring(index + comment.Length);
				}

				stringBuilder.Append(uncommentedText);
			}

			return stringBuilder.ToString();
		}

		private int GetStringIndex(string comment, string line)
		{
			for (int i = 0; i < (line.Length - (comment.Length - 1)); i++)
			{
				char ch = line[i];

				if (line.Substring(i, comment.Length) == comment)
				{
					return i;
				}
				else if (ch == ' ')
				{
				}
				else if (ch == '\t')
				{
				}
				else
				{
					return -1;
				}
			}
			return -1;
		}

		#endregion

		private void richTextEditor_Enter(object sender, EventArgs e)
		{
			Water.Environment.DefineConstant("File.Save", new Water.Method(this, "FileSave"));
			Water.Environment.DefineConstant("File.SaveAs", new Water.Method(this, "FileSaveAs"));
			Water.Environment.DefineConstant("Edit.Undo", new Water.Method(this, "EditUndo"));
			Water.Environment.DefineConstant("Edit.Redo", new Water.Method(this, "EditRedo"));
			Water.Environment.DefineConstant("Edit.Cut", new Water.Method(this, "EditCut"));
			Water.Environment.DefineConstant("Edit.Copy", new Water.Method(this, "EditCopy"));
			Water.Environment.DefineConstant("Edit.Paste", new Water.Method(this, "EditPaste"));
			Water.Environment.DefineConstant("Edit.Delete", new Water.Method(this, "EditDelete"));
			Water.Environment.DefineConstant("Edit.SelectAll", new Water.Method(this, "EditSelectAll"));
			Water.Environment.DefineConstant("Edit.Find", new Water.Method(this, "EditFind"));
			Water.Environment.DefineConstant("Editor.Find", new Water.Method(this, "EditorFind"));
			Water.Environment.DefineConstant("Edit.Replace", new Water.Method(this, "EditReplace"));
			Water.Environment.DefineConstant("Editor.Replace", new Water.Method(this, "EditorReplace"));
			Water.Environment.DefineConstant("Editor.ReplaceAll", new Water.Method(this, "EditorReplaceAll"));
			Water.Environment.DefineConstant("Editor.Goto", new Water.Method(this, "EditorGoto"));
			Water.Environment.DefineConstant("Editor.Comment", new Water.Method(this, "EditorComment"));
			Water.Environment.DefineConstant("Editor.Uncomment", new Water.Method(this, "EditorUncomment"));
		}

		private void richTextEditor_Leave(object sender, EventArgs e)
		{
			Water.Environment.UndefineConstant("File.Save");
			Water.Environment.UndefineConstant("File.SaveAs");
			Water.Environment.UndefineConstant("Edit.Undo");
			Water.Environment.UndefineConstant("Edit.Redo");
			Water.Environment.UndefineConstant("Edit.Cut");
			Water.Environment.UndefineConstant("Edit.Copy");
			Water.Environment.UndefineConstant("Edit.Paste");
			Water.Environment.UndefineConstant("Edit.Delete");
			Water.Environment.UndefineConstant("Edit.SelectAll");
			Water.Environment.UndefineConstant("Edit.Find");
			Water.Environment.UndefineConstant("Editor.Find");
			Water.Environment.UndefineConstant("Edit.Replace");
			Water.Environment.UndefineConstant("Editor.Replace");
			Water.Environment.UndefineConstant("Editor.ReplaceAll");
			Water.Environment.UndefineConstant("Editor.Goto");
			Water.Environment.UndefineConstant("Editor.Comment");
			Water.Environment.UndefineConstant("Editor.Uncomment");
		}

	}
}
