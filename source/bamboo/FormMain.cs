// ------------------------------------------------------------------------------
// 
// Copyright (c) 2008 Swampware, Inc.
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace bamboo
{
	public partial class FormMain : Form
	{

		public FormMain()
		{
			InitializeComponent();

			System.Windows.Forms.Application.Idle += new EventHandler(Application_Idle);
		}

		private void FormMain_Load(object sender, System.EventArgs e)
		{
			this.LoadSettings();
		}

		private void FormMain_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
			{
				e.Effect = System.Windows.Forms.DragDropEffects.Copy;
			}
			else
			{
				e.Effect = System.Windows.Forms.DragDropEffects.None;
			}
		}

		private void FormMain_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

			Array array = (Array)e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop);
			if (array != null)
			{
				foreach (object item in array)
				{
					string filename = item.ToString();
					if (Water.Environment.IsConstant("File.Open"))
					{
						Water.Evaluator.Apply("File.Open", filename);
					}
				}
			}

			this.Cursor = System.Windows.Forms.Cursors.Default;
		}

		private void FormMain_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			string key = Keys.KeyToString(e);

			switch (key)
			{
				case "Ctrl+N":
					{
						if (Water.Environment.IsConstant("File.New"))
						{
							Water.Evaluator.Apply("File.New");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+O":
					{
						if (Water.Environment.IsConstant("File.Open"))
						{
							Water.Evaluator.Apply("File.Open");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+Shift+N":
					{
						if (Water.Environment.IsConstant("Project.New"))
						{
							Water.Evaluator.Apply("Project.New");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+Shift+O":
					{
						if (Water.Environment.IsConstant("Project.Open"))
						{
							Water.Evaluator.Apply("Project.Open");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+S":
					{
						if (Water.Environment.IsConstant("File.Save"))
						{
							Water.Evaluator.Apply("File.Save");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+Shift+S":
					{
						if (Water.Environment.IsConstant("File.SaveAll"))
						{
							Water.Evaluator.Apply("File.SaveAll");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+Z":
					{
						if (Water.Environment.IsConstant("Edit.Undo"))
						{
							Water.Evaluator.Apply("Edit.Undo");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+Y":
					{
						if (Water.Environment.IsConstant("Edit.Redo"))
						{
							Water.Evaluator.Apply("Edit.Redo");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+X":
					{
						if (Water.Environment.IsConstant("Edit.Cut"))
						{
							Water.Evaluator.Apply("Edit.Cut");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+C":
					{
						if (Water.Environment.IsConstant("Edit.Copy"))
						{
							Water.Evaluator.Apply("Edit.Copy");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+V":
					{
						if (Water.Environment.IsConstant("Edit.Paste"))
						{
							Water.Evaluator.Apply("Edit.Paste");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+A":
					{
						if (Water.Environment.IsConstant("Edit.SelectAll"))
						{
							Water.Evaluator.Apply("Edit.SelectAll");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+G":
					{
						if (Water.Environment.IsConstant("Edit.Goto"))
						{
							Water.Evaluator.Apply("Edit.Goto");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+F":
					{
						if (Water.Environment.IsConstant("Edit.Find"))
						{
							Water.Evaluator.Apply("Edit.Find");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+H":
					{
						if (Water.Environment.IsConstant("Edit.Replace"))
						{
							Water.Evaluator.Apply("Edit.Replace");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+Shift+F":
					{
						if (Water.Environment.IsConstant("Project.Find"))
						{
							Water.Evaluator.Apply("Project.Find");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+Shift+H":
					{
						if (Water.Environment.IsConstant("Project.Replace"))
						{
							Water.Evaluator.Apply("Project.Replace");
							e.Handled = true;
						}
						break;
					}
				case "F2":
					{
						if (Water.Environment.IsConstant("Project.Rename"))
						{
							Water.Evaluator.Apply("Project.Rename");
							e.Handled = true;
						}
						break;
					}
				case "F3":
					{
						if (Water.Environment.IsConstant("Edit.FindNext"))
						{
							Water.Evaluator.Apply("Edit.FindNext");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+Alt+O":
					{
						if (Water.Environment.IsConstant("View.OutputWindow"))
						{
							Water.Evaluator.Apply("View.OutputWindow");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+Shift+B":
					{
						if (Water.Environment.IsConstant("Project.Build"))
						{
							Water.Evaluator.Apply("Project.Build");
							e.Handled = true;
						}
						break;
					}
				case "F5":
					{
						if (Water.Environment.IsConstant("Project.Run"))
						{
							Water.Evaluator.Apply("Project.Run");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+W":
					{
						if (Water.Environment.IsConstant("Window.CloseAllDocuments"))
						{
							Water.Evaluator.Apply("Window.CloseAllDocuments");
							e.Handled = true;
						}
						break;
					}
				case "F1":
					{
						if (Water.Environment.IsConstant("Help.F1Help"))
						{
							Water.Evaluator.Apply("Help.F1Help");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+F4":
					{
						if (Water.Environment.IsConstant("File.Close"))
						{
							Water.Evaluator.Apply("File.Close");
							e.Handled = true;
						}
						break;
					}
				case "F4":
					{
						if (Water.Environment.IsConstant("View.Properties"))
						{
							Water.Evaluator.Apply("View.Properties");
							e.Handled = true;
						}
						break;
					}
				case "Ctrl+Shift+A":
					{
						if (Water.Environment.IsConstant("Project.AddNewFile"))
						{
							Water.Evaluator.Apply("Project.AddNewFile");
							e.Handled = true;
						}
						break;
					}
			}
		}

		public TD.SandDock.DockControl ActiveDocument
		{
			get { return this.documentContainer.ActiveDocument; }
		}

		public TD.SandDock.DockControl FindWindow(string key)
		{
			foreach (TD.SandDock.DockControl dockControl in this.sandDockManager.GetDockControls())
			{
				System.Windows.Forms.Control control = (System.Windows.Forms.Control)dockControl.Controls[0];
				string controlKey = (string)control.Tag;
				if (controlKey != null)
				{
					if (controlKey.Equals(key))
					{
						return dockControl;
					}
				}
			}
			return null;
		}

		private TD.SandDock.DockControl CreateDockControl(System.Windows.Forms.Control control, System.Drawing.Image image)
		{
//			control.TextChanged += new EventHandler(control_TextChanged);
			TD.SandDock.DockControl dockControl = new TD.SandDock.DockControl(control, control.Text);
			if (image != null)
			{
				dockControl.TabImage = image;
			}
			dockControl.Closing += new CancelEventHandler(dockControl_Closing);
			return dockControl;
		}

		//private void control_TextChanged(object sender, EventArgs e)
		//{
		//}

		private void dockControl_Closing(object sender, CancelEventArgs e)
		{
			TD.SandDock.DockControl dockControl = (TD.SandDock.DockControl)sender;
			System.Windows.Forms.Control control = dockControl.Controls[0];
			if(control is bamboo.Controls.Editor.EditorControl)
			{
				bamboo.Controls.Editor.EditorControl editor = (bamboo.Controls.Editor.EditorControl)control;
				if(!editor.Close())
				{
					e.Cancel = true;
					return;
				}
			}
			else if(control is bamboo.Controls.ProjectExplorer.ProjectExplorerControl)
			{
				bamboo.Controls.ProjectExplorer.ProjectExplorerControl projectExplorer = (bamboo.Controls.ProjectExplorer.ProjectExplorerControl)control;
				if(!projectExplorer.Close())
				{
					e.Cancel = true;
					return;
				}
			}

			dockControl.Closing -= new CancelEventHandler(dockControl_Closing);
//			control.TextChanged -= new EventHandler(control_TextChanged);
		}

		public void AddDocument(System.Windows.Forms.Control control)
		{
			TD.SandDock.DockControl dockControl = CreateDockControl(control, null);

			this.documentContainer.AddDocument(dockControl);
			this.documentContainer.ActiveDocument = dockControl;
		}

		public void AddLeft(System.Windows.Forms.Control control, System.Drawing.Image image)
		{
			TD.SandDock.DockControl dockControl = CreateDockControl(control, image);

			TD.SandDock.ControlLayoutSystem controlLayoutSystem;
			if (this.leftDockContainer.LayoutSystem.LayoutSystems.Count == 0)
			{
				controlLayoutSystem = new TD.SandDock.ControlLayoutSystem(250, 582);
				this.leftDockContainer.LayoutSystem.LayoutSystems.Add(controlLayoutSystem);
			}
			else
			{
				controlLayoutSystem = (TD.SandDock.ControlLayoutSystem)this.leftDockContainer.LayoutSystem.LayoutSystems[0];
			}
			controlLayoutSystem.Controls.Add(dockControl);
			dockControl.Activate();
		}

		public void AddBottom(System.Windows.Forms.Control control, System.Drawing.Image image)
		{
			TD.SandDock.DockControl dockControl = CreateDockControl(control, image);

			TD.SandDock.ControlLayoutSystem controlLayoutSystem;
			if (this.bottomDockContainer.LayoutSystem.LayoutSystems.Count == 0)
			{
				controlLayoutSystem = new TD.SandDock.ControlLayoutSystem(250, 582);
				this.bottomDockContainer.LayoutSystem.LayoutSystems.Add(controlLayoutSystem);
			}
			else
			{
				controlLayoutSystem = (TD.SandDock.ControlLayoutSystem)this.bottomDockContainer.LayoutSystem.LayoutSystems[0];
			}
			controlLayoutSystem.Controls.Add(dockControl);
			dockControl.Activate();
		}

		public void AddRight(System.Windows.Forms.Control control, System.Drawing.Image image)
		{
			TD.SandDock.DockControl dockControl = CreateDockControl(control, image);

			TD.SandDock.ControlLayoutSystem controlLayoutSystem;
			if (this.rightDockContainer.LayoutSystem.LayoutSystems.Count == 0)
			{
				controlLayoutSystem = new TD.SandDock.ControlLayoutSystem(250, 582);
				this.rightDockContainer.LayoutSystem.LayoutSystems.Add(controlLayoutSystem);
			}
			else
			{
				controlLayoutSystem = (TD.SandDock.ControlLayoutSystem)this.rightDockContainer.LayoutSystem.LayoutSystems[0];
			}
			controlLayoutSystem.Controls.Add(dockControl);
			dockControl.Activate();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);



			this.SaveSettings();
		}

		private void menuStrip_Click(object sender, EventArgs e)
		{
			if (((System.Windows.Forms.ToolStripMenuItem)sender).Tag != null)
			{
				Water.Evaluator.Apply((string)((System.Windows.Forms.ToolStripMenuItem)sender).Tag);
			}
		}

		private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem.Tag != null)
			{
				Water.Evaluator.Apply((string)e.ClickedItem.Tag);
			}
		}

		private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem.Tag != null)
			{
				Water.Evaluator.Apply((string)e.ClickedItem.Tag);
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			foreach (System.Windows.Forms.ToolStripMenuItem menu in this.menuStrip.Items)
			{
				foreach (System.Windows.Forms.ToolStripItem menuItem in menu.DropDownItems)
				{
					if (menuItem.Tag != null)
					{
						string command = (string)menuItem.Tag;
						menuItem.Enabled = Water.Environment.IsConstant(command);
					}
				}
			}
			foreach (System.Windows.Forms.ToolStripItem toolButton in this.toolStrip.Items)
			{
				if (toolButton.Tag != null)
				{
					string command = (string)toolButton.Tag;
					toolButton.Enabled = Water.Environment.IsConstant(command);
				}
			}



			string title = "";

			TD.SandDock.DockControl dockControl;
			bamboo.Controls.ProjectExplorer.ProjectExplorerControl projectExplorer = null;
			bamboo.Controls.Editor.EditorControl editorControl = null;

			dockControl = FindWindow("View.ProjectExplorer");
			if (dockControl != null)
			{
				projectExplorer = (bamboo.Controls.ProjectExplorer.ProjectExplorerControl)dockControl.Controls[0];
				if (projectExplorer.ProjectName != null && projectExplorer.ProjectName != "")
				{
					title += projectExplorer.ProjectName;
				}
			}

			dockControl = this.ActiveDocument;
			if (dockControl != null && dockControl.Controls[0] is bamboo.Controls.Editor.EditorControl)
			{
				editorControl = (bamboo.Controls.Editor.EditorControl)dockControl.Controls[0];
				if (title.Length > 0)
				{
					title += ", ";
				}
				title += editorControl.Text;
			}

			string text = "Bamboo";
			if (title.Length > 0)
			{
				text += " - " + title;
			}

			if (this.Text != text)
			{
				this.Text = text;
			}
		}

		private void LoadSettings()
		{
			Water.Dictionary Settings = (Water.Dictionary)Water.Environment.Identify("Settings");

			if (Settings.IsDefined("WindowState"))
			{
				this.WindowState = (System.Windows.Forms.FormWindowState)System.Enum.Parse(typeof(System.Windows.Forms.FormWindowState), (string)Settings["WindowState"]);
			}
			if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
			{
				if (Settings.IsDefined("Left"))
				{
					this.Left = (int)Settings["Left"];
				}
				if (Settings.IsDefined("Top"))
				{
					this.Top = (int)Settings["Top"];
				}
				if (Settings.IsDefined("Width"))
				{
					this.Width = (int)Settings["Width"];
				}
				if (Settings.IsDefined("Height"))
				{
					this.Height = (int)Settings["Height"];
				}
			}


			if (Settings.IsDefined("Commands"))
			{
				Water.List commands = (Water.List)Settings["Commands"];
				foreach (Water.List command in commands)
				{
					Water.Evaluator.Evaluate(command);
				}
			}
		}

		private void SaveSettings()
		{
			if (Water.Environment.IsConstant("Window.CloseAllDocuments"))
			{
				Water.Evaluator.Apply("Window.CloseAllDocuments");
			}



			Water.Dictionary Settings = (Water.Dictionary)Water.Environment.Identify("Settings");

			Settings["WindowState"] = this.WindowState.ToString();
			if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
			{
				Settings["Left"] = this.Left;
				Settings["Top"] = this.Top;
				Settings["Width"] = this.Width;
				Settings["Height"] = this.Height;
			}



			Water.List commands = new Water.List();
			Settings["Commands"] = commands;

			bool showStartPage = true;
			foreach (TD.SandDock.DockControl dockControl in this.documentContainer.Controls)
			{
				if (dockControl.Controls[0] is bamboo.Controls.Editor.EditorControl)
				{
					bamboo.Controls.Editor.EditorControl editorControl = (bamboo.Controls.Editor.EditorControl)dockControl.Controls[0];

					Water.List command = new Water.List();
					command.Add(new Water.Identifier("File.Open"));
					command.Add(editorControl.Filename);
					commands.Add(command);
					showStartPage = false;
				}
				else if (dockControl.Controls[0] is bamboo.Controls.AssemblyExplorer.AssemblyExplorerControl)
				{
					bamboo.Controls.AssemblyExplorer.AssemblyExplorerControl assemblyExplorerControl = (bamboo.Controls.AssemblyExplorer.AssemblyExplorerControl)dockControl.Controls[0];

					Water.List command = new Water.List();
					command.Add(new Water.Identifier("File.Open"));
					command.Add(assemblyExplorerControl.Filename);
					commands.Add(command);
					showStartPage = false;
				}
				else
				{
					string tag = (string)dockControl.Controls[0].Tag;
					if (tag != null)
					{
						Water.List command = new Water.List();
						command.Add(new Water.Identifier(tag));
						command.Add("Document");
						commands.Add(command);
						showStartPage = false;
					}
				}
			}
			if(showStartPage)
			{
				Water.List command = new Water.List();
				command.Add(new Water.Identifier("View.StartPage"));
				commands.Add(command);
			}

			if (this.leftDockContainer.LayoutSystem.LayoutSystems.Count != 0)
			{
				TD.SandDock.ControlLayoutSystem controlLayoutSystem = (TD.SandDock.ControlLayoutSystem)this.leftDockContainer.LayoutSystem.LayoutSystems[0];
				foreach (TD.SandDock.DockControl dockControl in controlLayoutSystem.Controls)
				{
					string tag = (string)dockControl.Controls[0].Tag;
					if (tag != null)
					{
						Water.List command = new Water.List();
						command.Add(new Water.Identifier(tag));
						command.Add("Left");
						commands.Add(command);
					}
				}
			}

			if (this.bottomDockContainer.LayoutSystem.LayoutSystems.Count != 0)
			{
				TD.SandDock.ControlLayoutSystem controlLayoutSystem = (TD.SandDock.ControlLayoutSystem)this.bottomDockContainer.LayoutSystem.LayoutSystems[0];
				foreach (TD.SandDock.DockControl dockControl in controlLayoutSystem.Controls)
				{
					string tag = (string)dockControl.Controls[0].Tag;
					if (tag != null)
					{
						Water.List command = new Water.List();
						command.Add(new Water.Identifier(tag));
						command.Add("Bottom");
						commands.Add(command);
					}
				}
			}

			if (this.rightDockContainer.LayoutSystem.LayoutSystems.Count != 0)
			{
				TD.SandDock.ControlLayoutSystem controlLayoutSystem = (TD.SandDock.ControlLayoutSystem)this.rightDockContainer.LayoutSystem.LayoutSystems[0];
				foreach (TD.SandDock.DockControl dockControl in controlLayoutSystem.Controls)
				{
					string tag = (string)dockControl.Controls[0].Tag;
					if (tag != null)
					{
						Water.List command = new Water.List();
						command.Add(new Water.Identifier(tag));
						command.Add("Right");
						commands.Add(command);
					}
				}
			}
		}

	}
}