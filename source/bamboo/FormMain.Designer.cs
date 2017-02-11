namespace bamboo
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.fileExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.closeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.addNewItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addExistingItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.commandWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.outputWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.projectExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertyBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.searchResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.taskListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonFileNew = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonFileOpen = new System.Windows.Forms.ToolStripButton();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.sandDockManager = new TD.SandDock.SandDockManager();
			this.leftDockContainer = new TD.SandDock.DockContainer();
			this.rightDockContainer = new TD.SandDock.DockContainer();
			this.bottomDockContainer = new TD.SandDock.DockContainer();
			this.documentContainer = new TD.SandDock.DocumentContainer();
			this.menuStrip.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.projectToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(1016, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNewToolStripMenuItem,
            this.fileOpenToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveAllToolStripMenuItem,
            this.toolStripMenuItem5,
            this.closeToolStripMenuItem,
            this.toolStripMenuItem3,
            this.fileExitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// fileNewToolStripMenuItem
			// 
			this.fileNewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fileNewToolStripMenuItem.Image")));
			this.fileNewToolStripMenuItem.Name = "fileNewToolStripMenuItem";
			this.fileNewToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.fileNewToolStripMenuItem.Tag = "File.New";
			this.fileNewToolStripMenuItem.Text = "New";
			this.fileNewToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// fileOpenToolStripMenuItem
			// 
			this.fileOpenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fileOpenToolStripMenuItem.Image")));
			this.fileOpenToolStripMenuItem.Name = "fileOpenToolStripMenuItem";
			this.fileOpenToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.fileOpenToolStripMenuItem.Tag = "File.Open";
			this.fileOpenToolStripMenuItem.Text = "Open";
			this.fileOpenToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(110, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.saveToolStripMenuItem.Tag = "File.Save";
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.saveAsToolStripMenuItem.Tag = "File.SaveAs";
			this.saveAsToolStripMenuItem.Text = "Save As";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// saveAllToolStripMenuItem
			// 
			this.saveAllToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAllToolStripMenuItem.Image")));
			this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
			this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.saveAllToolStripMenuItem.Tag = "File.SaveAll";
			this.saveAllToolStripMenuItem.Text = "Save All";
			this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(110, 6);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.closeToolStripMenuItem.Tag = "File.Close";
			this.closeToolStripMenuItem.Text = "Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(110, 6);
			// 
			// fileExitToolStripMenuItem
			// 
			this.fileExitToolStripMenuItem.Name = "fileExitToolStripMenuItem";
			this.fileExitToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.fileExitToolStripMenuItem.Tag = "File.Exit";
			this.fileExitToolStripMenuItem.Text = "Exit";
			this.fileExitToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// projectToolStripMenuItem
			// 
			this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripMenuItem2,
            this.closeToolStripMenuItem1,
            this.toolStripMenuItem4,
            this.addNewItemToolStripMenuItem,
            this.addExistingItemToolStripMenuItem});
			this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
			this.projectToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
			this.projectToolStripMenuItem.Text = "Project";
			this.projectToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.newToolStripMenuItem.Tag = "Project.New";
			this.newToolStripMenuItem.Text = "New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.openToolStripMenuItem.Tag = "Project.Open";
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(155, 6);
			// 
			// closeToolStripMenuItem1
			// 
			this.closeToolStripMenuItem1.Name = "closeToolStripMenuItem1";
			this.closeToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
			this.closeToolStripMenuItem1.Tag = "Project.Close";
			this.closeToolStripMenuItem1.Text = "Close";
			this.closeToolStripMenuItem1.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(155, 6);
			// 
			// addNewItemToolStripMenuItem
			// 
			this.addNewItemToolStripMenuItem.Name = "addNewItemToolStripMenuItem";
			this.addNewItemToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.addNewItemToolStripMenuItem.Tag = "Project.AddNewItem";
			this.addNewItemToolStripMenuItem.Text = "Add New Item";
			this.addNewItemToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// addExistingItemToolStripMenuItem
			// 
			this.addExistingItemToolStripMenuItem.Name = "addExistingItemToolStripMenuItem";
			this.addExistingItemToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.addExistingItemToolStripMenuItem.Tag = "Project.AddExistingItem";
			this.addExistingItemToolStripMenuItem.Text = "Add Existing Item";
			this.addExistingItemToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator2,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.findToolStripMenuItem,
            this.replaceToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripMenuItem.Image")));
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.undoToolStripMenuItem.Tag = "Edit.Undo";
			this.undoToolStripMenuItem.Text = "Undo";
			this.undoToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// redoToolStripMenuItem
			// 
			this.redoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripMenuItem.Image")));
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.redoToolStripMenuItem.Tag = "Edit.Redo";
			this.redoToolStripMenuItem.Text = "Redo";
			this.redoToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(109, 6);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.cutToolStripMenuItem.Tag = "Edit.Cut";
			this.cutToolStripMenuItem.Text = "Cut";
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.copyToolStripMenuItem.Tag = "Edit.Copy";
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.pasteToolStripMenuItem.Tag = "Edit.Paste";
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(109, 6);
			// 
			// findToolStripMenuItem
			// 
			this.findToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("findToolStripMenuItem.Image")));
			this.findToolStripMenuItem.Name = "findToolStripMenuItem";
			this.findToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.findToolStripMenuItem.Tag = "Edit.Find";
			this.findToolStripMenuItem.Text = "Find";
			this.findToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// replaceToolStripMenuItem
			// 
			this.replaceToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("replaceToolStripMenuItem.Image")));
			this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
			this.replaceToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.replaceToolStripMenuItem.Tag = "Edit.Replace";
			this.replaceToolStripMenuItem.Text = "Replace";
			this.replaceToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commandWindowToolStripMenuItem,
            this.outputWindowToolStripMenuItem,
            this.projectExplorerToolStripMenuItem,
            this.propertyBrowserToolStripMenuItem,
            this.searchResultsToolStripMenuItem,
            this.taskListToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// commandWindowToolStripMenuItem
			// 
			this.commandWindowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("commandWindowToolStripMenuItem.Image")));
			this.commandWindowToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.commandWindowToolStripMenuItem.Name = "commandWindowToolStripMenuItem";
			this.commandWindowToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.commandWindowToolStripMenuItem.Tag = "View.CommandWindow";
			this.commandWindowToolStripMenuItem.Text = "Command Window";
			this.commandWindowToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// outputWindowToolStripMenuItem
			// 
			this.outputWindowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("outputWindowToolStripMenuItem.Image")));
			this.outputWindowToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.outputWindowToolStripMenuItem.Name = "outputWindowToolStripMenuItem";
			this.outputWindowToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.outputWindowToolStripMenuItem.Tag = "View.OutputWindow";
			this.outputWindowToolStripMenuItem.Text = "Output Window";
			this.outputWindowToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// projectExplorerToolStripMenuItem
			// 
			this.projectExplorerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("projectExplorerToolStripMenuItem.Image")));
			this.projectExplorerToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.projectExplorerToolStripMenuItem.Name = "projectExplorerToolStripMenuItem";
			this.projectExplorerToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.projectExplorerToolStripMenuItem.Tag = "View.ProjectExplorer";
			this.projectExplorerToolStripMenuItem.Text = "Project Explorer";
			this.projectExplorerToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// propertyBrowserToolStripMenuItem
			// 
			this.propertyBrowserToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("propertyBrowserToolStripMenuItem.Image")));
			this.propertyBrowserToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.propertyBrowserToolStripMenuItem.Name = "propertyBrowserToolStripMenuItem";
			this.propertyBrowserToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.propertyBrowserToolStripMenuItem.Tag = "View.PropertyBrowser";
			this.propertyBrowserToolStripMenuItem.Text = "Property Browser";
			this.propertyBrowserToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// searchResultsToolStripMenuItem
			// 
			this.searchResultsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("searchResultsToolStripMenuItem.Image")));
			this.searchResultsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.searchResultsToolStripMenuItem.Name = "searchResultsToolStripMenuItem";
			this.searchResultsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.searchResultsToolStripMenuItem.Tag = "View.SearchResults";
			this.searchResultsToolStripMenuItem.Text = "Search Results";
			this.searchResultsToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// taskListToolStripMenuItem
			// 
			this.taskListToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("taskListToolStripMenuItem.Image")));
			this.taskListToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.taskListToolStripMenuItem.Name = "taskListToolStripMenuItem";
			this.taskListToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.taskListToolStripMenuItem.Tag = "View.TaskList";
			this.taskListToolStripMenuItem.Text = "Task List";
			this.taskListToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_Click);
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonFileNew,
            this.toolStripButtonFileOpen});
			this.toolStrip.Location = new System.Drawing.Point(0, 24);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(1016, 25);
			this.toolStrip.TabIndex = 1;
			this.toolStrip.Text = "toolStrip";
			this.toolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip_ItemClicked);
			// 
			// toolStripButtonFileNew
			// 
			this.toolStripButtonFileNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonFileNew.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFileNew.Image")));
			this.toolStripButtonFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonFileNew.Name = "toolStripButtonFileNew";
			this.toolStripButtonFileNew.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonFileNew.Tag = "File.New";
			this.toolStripButtonFileNew.Text = "File.New";
			// 
			// toolStripButtonFileOpen
			// 
			this.toolStripButtonFileOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFileOpen.Image")));
			this.toolStripButtonFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonFileOpen.Name = "toolStripButtonFileOpen";
			this.toolStripButtonFileOpen.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonFileOpen.Tag = "File.Open";
			this.toolStripButtonFileOpen.Text = "File.Open";
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 712);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(1016, 22);
			this.statusStrip.TabIndex = 2;
			this.statusStrip.Text = "statusStrip";
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// sandDockManager
			// 
			this.sandDockManager.OwnerForm = this;
			// 
			// leftDockContainer
			// 
			this.leftDockContainer.AllowDrop = false;
			this.leftDockContainer.Dock = System.Windows.Forms.DockStyle.Left;
			this.leftDockContainer.Guid = new System.Guid("ed4bfbfe-223d-479f-91b4-11b6acf8a290");
			this.leftDockContainer.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.leftDockContainer.Location = new System.Drawing.Point(0, 49);
			this.leftDockContainer.Manager = this.sandDockManager;
			this.leftDockContainer.Name = "leftDockContainer";
			this.leftDockContainer.Size = new System.Drawing.Size(0, 663);
			this.leftDockContainer.TabIndex = 0;
			// 
			// rightDockContainer
			// 
			this.rightDockContainer.AllowDrop = false;
			this.rightDockContainer.Dock = System.Windows.Forms.DockStyle.Right;
			this.rightDockContainer.Guid = new System.Guid("6628daaa-4400-43f9-a474-f49b487c9094");
			this.rightDockContainer.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.rightDockContainer.Location = new System.Drawing.Point(1016, 49);
			this.rightDockContainer.Manager = this.sandDockManager;
			this.rightDockContainer.Name = "rightDockContainer";
			this.rightDockContainer.Size = new System.Drawing.Size(0, 663);
			this.rightDockContainer.TabIndex = 1;
			// 
			// bottomDockContainer
			// 
			this.bottomDockContainer.AllowDrop = false;
			this.bottomDockContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomDockContainer.Guid = new System.Guid("163cd4c5-cd03-4c78-bc4f-f26dd5f77fee");
			this.bottomDockContainer.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.bottomDockContainer.Location = new System.Drawing.Point(0, 712);
			this.bottomDockContainer.Manager = this.sandDockManager;
			this.bottomDockContainer.Name = "bottomDockContainer";
			this.bottomDockContainer.Size = new System.Drawing.Size(1016, 0);
			this.bottomDockContainer.TabIndex = 2;
			// 
			// documentContainer
			// 
			this.documentContainer.AllowDrop = false;
			this.documentContainer.Guid = new System.Guid("30ef8b75-c67b-4d1c-9ae8-aeddf52b1c7b");
			this.documentContainer.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
			this.documentContainer.Location = new System.Drawing.Point(0, 49);
			this.documentContainer.Manager = this.sandDockManager;
			this.documentContainer.Name = "documentContainer";
			this.documentContainer.Size = new System.Drawing.Size(1016, 663);
			this.documentContainer.TabIndex = 3;
			// 
			// FormMain
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1016, 734);
			this.Controls.Add(this.documentContainer);
			this.Controls.Add(this.bottomDockContainer);
			this.Controls.Add(this.leftDockContainer);
			this.Controls.Add(this.rightDockContainer);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.menuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip;
			this.Name = "FormMain";
			this.Text = "Bamboo";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.StatusStrip statusStrip;
		private TD.SandDock.SandDockManager sandDockManager;
		private TD.SandDock.DocumentContainer documentContainer;
		private TD.SandDock.DockContainer leftDockContainer;
		private TD.SandDock.DockContainer rightDockContainer;
		private TD.SandDock.DockContainer bottomDockContainer;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.ToolStripMenuItem fileNewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileOpenToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem fileExitToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButtonFileNew;
		private System.Windows.Forms.ToolStripButton toolStripButtonFileOpen;
		private System.Windows.Forms.ToolStripMenuItem commandWindowToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem outputWindowToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem projectExplorerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem propertyBrowserToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem searchResultsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem taskListToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem addNewItemToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addExistingItemToolStripMenuItem;
	}
}

