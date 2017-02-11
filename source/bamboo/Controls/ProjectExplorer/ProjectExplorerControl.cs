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

namespace bamboo.Controls.ProjectExplorer
{
	/// <summary>
	/// Summary description for ProjectExplorerControl.
	/// </summary>
	public class ProjectExplorerControl : UserControl
	{
		private ImageList imageList;
		private ToolStrip toolStrip;
		private ToolStripButton toolStripButtonRefresh;
		private ToolStripButton toolStripButtonShowAllFiles;
		private ToolStripButton toolStripButtonProperties;
		private ToolStripButton toolStripButtonBuild;
		private TreeView treeView;
		private IContainer components;

		public ProjectExplorerControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();


			this.Text = "Project Explorer";
			this.Tag = "View.ProjectExplorer";

			System.Windows.Forms.Application.Idle += new EventHandler(Application_Idle);
		}

		private void ProjectExplorerControl_Load(object sender, EventArgs e)
		{
			LoadSettings();
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectExplorerControl));
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonShowAllFiles = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonProperties = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonBuild = new System.Windows.Forms.ToolStripButton();
			this.treeView = new System.Windows.Forms.TreeView();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Magenta;
			this.imageList.Images.SetKeyName(0, "");
			this.imageList.Images.SetKeyName(1, "");
			this.imageList.Images.SetKeyName(2, "");
			this.imageList.Images.SetKeyName(3, "");
			this.imageList.Images.SetKeyName(4, "");
			this.imageList.Images.SetKeyName(5, "");
			this.imageList.Images.SetKeyName(6, "");
			this.imageList.Images.SetKeyName(7, "");
			this.imageList.Images.SetKeyName(8, "");
			this.imageList.Images.SetKeyName(9, "");
			this.imageList.Images.SetKeyName(10, "");
			this.imageList.Images.SetKeyName(11, "");
			this.imageList.Images.SetKeyName(12, "");
			this.imageList.Images.SetKeyName(13, "");
			this.imageList.Images.SetKeyName(14, "");
			this.imageList.Images.SetKeyName(15, "");
			this.imageList.Images.SetKeyName(16, "");
			this.imageList.Images.SetKeyName(17, "");
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRefresh,
            this.toolStripButtonShowAllFiles,
            this.toolStripButtonProperties,
            this.toolStripButtonBuild});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(392, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip";
			// 
			// toolStripButtonRefresh
			// 
			this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
			this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
			this.toolStripButtonRefresh.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonRefresh.Tag = "Project.Refresh";
			this.toolStripButtonRefresh.Text = "Project.Refresh";
			// 
			// toolStripButtonShowAllFiles
			// 
			this.toolStripButtonShowAllFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonShowAllFiles.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShowAllFiles.Image")));
			this.toolStripButtonShowAllFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonShowAllFiles.Name = "toolStripButtonShowAllFiles";
			this.toolStripButtonShowAllFiles.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonShowAllFiles.Tag = "Project.ShowAllFiles";
			this.toolStripButtonShowAllFiles.Text = "Project.ShowAllFiles";
			// 
			// toolStripButtonProperties
			// 
			this.toolStripButtonProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonProperties.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProperties.Image")));
			this.toolStripButtonProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonProperties.Name = "toolStripButtonProperties";
			this.toolStripButtonProperties.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonProperties.Tag = "View.Properties";
			this.toolStripButtonProperties.Text = "View.Properties";
			// 
			// toolStripButtonBuild
			// 
			this.toolStripButtonBuild.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonBuild.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBuild.Image")));
			this.toolStripButtonBuild.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonBuild.Name = "toolStripButtonBuild";
			this.toolStripButtonBuild.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonBuild.Tag = "Project.Build";
			this.toolStripButtonBuild.Text = "Project.Build";
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.ImageIndex = 0;
			this.treeView.ImageList = this.imageList;
			this.treeView.Location = new System.Drawing.Point(0, 25);
			this.treeView.Name = "treeView";
			this.treeView.SelectedImageIndex = 0;
			this.treeView.Size = new System.Drawing.Size(392, 439);
			this.treeView.TabIndex = 1;
			this.treeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCollapse);
			this.treeView.DoubleClick += new System.EventHandler(this.treeView_DoubleClick);
			this.treeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyDown);
			this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterExpand);
			// 
			// ProjectExplorerControl
			// 
			this.Controls.Add(this.treeView);
			this.Controls.Add(this.toolStrip);
			this.Name = "ProjectExplorerControl";
			this.Size = new System.Drawing.Size(392, 464);
			this.Load += new System.EventHandler(this.ProjectExplorerControl_Load);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private TreeNodes.TreeNode _treeNode;

		public string ProjectName
		{
			get
			{
				if (this._treeNode == null)
				{
					return null;
				}
				return this._treeNode.Text;
			}
		}

		public void OpenProject(string path)
		{
			try
			{
				TreeNodes.TreeNode treeNode = CreateTreeNode(path);

				this._treeNode = treeNode;

				this.treeView.Nodes.Add(this._treeNode);
				this._treeNode.Expand();
				this.treeView.SelectedNode = this._treeNode;

				SaveSettings();
			}
			catch (System.Exception exception)
			{
				int n = 0;
			}
		}

		private bamboo.Controls.ProjectExplorer.TreeNodes.ProjectTreeNode CreateCSharpProjectTreeNode(string path)
		{
			if (Bamboo.VisualStudio.CSharpProjectDetector.Is2005(path))
			{
				Bamboo.VisualStudio.VisualStudio2005.CSharpProject.Models.Project project = Bamboo.VisualStudio.VisualStudio2005.CSharpProject.ProjectReader.Read(path);
				bamboo.Controls.ProjectExplorer.TreeNodes.ProjectTreeNode projectTreeNode = new bamboo.Controls.ProjectExplorer.TreeNodes.ProjectTreeNode(project.Name, path, project, Images.CSharpProject);
				foreach (Bamboo.VisualStudio.VisualStudio2005.CSharpProject.Models.Reference reference in project.References)
				{
					bamboo.Controls.ProjectExplorer.TreeNodes.FileReferenceTreeNode fileReferenceTreeNode = new bamboo.Controls.ProjectExplorer.TreeNodes.FileReferenceTreeNode(reference.Include, reference.HintPath);
					projectTreeNode.References.Add(fileReferenceTreeNode);
				}
				foreach (Bamboo.VisualStudio.VisualStudio2005.CSharpProject.Models.ProjectReference projectReference in project.ProjectReferences)
				{
					bamboo.Controls.ProjectExplorer.TreeNodes.ProjectReferenceTreeNode projectReferenceTreeNode = new bamboo.Controls.ProjectExplorer.TreeNodes.ProjectReferenceTreeNode(projectReference.Name);
					projectTreeNode.References.Add(projectReferenceTreeNode);
				}
				SortingTree fileTree = new SortingTree();
				foreach (Bamboo.VisualStudio.VisualStudio2005.CSharpProject.Models.Item item in project.Items)
				{
					string[] keys = item.Include.Split("\\".ToCharArray());
					fileTree.Add(keys, item);
				}
				string rootPath = path.Substring(0, path.LastIndexOf("\\"));
				foreach (SortingTree node in fileTree.Nodes)
				{
					if (node.IsLeaf())
					{
						Bamboo.VisualStudio.VisualStudio2005.CSharpProject.Models.Item item = (Bamboo.VisualStudio.VisualStudio2005.CSharpProject.Models.Item)node.Value;
						projectTreeNode.Add(new bamboo.Controls.ProjectExplorer.TreeNodes.FileTreeNode(node.Name, rootPath + "\\" + item.Include, GetImage(node.Name)));
					}
					else
					{
						projectTreeNode.Add(CreateCSharpProjectFolderTreeNode(node, rootPath));
					}
				}
				return projectTreeNode;
			}
//            else if (Bamboo.VisualStudio.CSharpProjectDetector.Is2003(path))
//            {
//                Bamboo.VisualStudio.VisualStudio2003.CSharpProject.Models.Project project = Bamboo.VisualStudio.VisualStudio2003.CSharpProject.ProjectReader.Read(path);
//                bamboo.Controls.ProjectExplorer.TreeNodes.ProjectTreeNode projectTreeNode = new bamboo.Controls.ProjectExplorer.TreeNodes.ProjectTreeNode(project.Name, path, project, Images.CSharpProject);
//                foreach (Bamboo.VisualStudio.VisualStudio2003.CSharpProject.Models.Reference reference in project.References)
//                {
//                    int n = 0;
////					bamboo.Controls.ProjectExplorer.TreeNodes.ProjectReferenceTreeNode projectReferenceTreeNode = new bamboo.Controls.ProjectExplorer.TreeNodes.ProjectReferenceTreeNode();
//                }
//                Bamboo.Trees.Tree fileTree = new Bamboo.Trees.Tree();
//                foreach (Bamboo.VisualStudio.VisualStudio2003.CSharpProject.Models.Item item in project.Items)
//                {
////					bamboo.Controls.ProjectExplorer.TreeNodes.FileTreeNode fileTreeNode = new bamboo.Controls.ProjectExplorer.TreeNodes.FileTreeNode();
//                    int n = 0;

//                    string[] keys = item.RelPath.Split("\\".ToCharArray());
//                    fileTree.Add(keys, item);
//                }
//                return projectTreeNode;
//            }
			else
			{
				throw new System.Exception("Invalid project version.");
			}
		}

		private bamboo.Controls.ProjectExplorer.TreeNodes.FolderTreeNode CreateCSharpProjectFolderTreeNode(SortingTree tree, string rootPath)
		{
			bamboo.Controls.ProjectExplorer.TreeNodes.FolderTreeNode folderTreeNode = new bamboo.Controls.ProjectExplorer.TreeNodes.FolderTreeNode(tree.Name);
			foreach (SortingTree node in tree.Nodes)
			{
				if (node.IsLeaf())
				{
					Bamboo.VisualStudio.VisualStudio2005.CSharpProject.Models.Item item = (Bamboo.VisualStudio.VisualStudio2005.CSharpProject.Models.Item)node.Value;
					folderTreeNode.Add(new bamboo.Controls.ProjectExplorer.TreeNodes.FileTreeNode(node.Name, rootPath + "\\" + item.Include, GetImage(node.Name)));
				}
				else
				{
					folderTreeNode.Add(CreateCSharpProjectFolderTreeNode(node, rootPath));
				}
			}
			return folderTreeNode;
		}

		private bamboo.Controls.ProjectExplorer.TreeNodes.TreeNode CreateTreeNode(string path)
		{
			string extension = path.Substring(path.LastIndexOf("."));
			switch (extension)
			{
				case ".csproj":
					{
						return CreateCSharpProjectTreeNode(path);
					}
				case ".sln":
					{
						return CreateSolutionTreeNode(path);
					}
				default:
					{
						throw new System.Exception("Invalid project: " + path);
					}
			}
		}

		private int GetImage(string name)
		{
			string extension = name.Substring(name.LastIndexOf("."));
			switch(extension.ToLower())
			{
				case ".cs":
					{
						return Images.CSharpFile;
					}
				case ".xml":
					{
						return Images.XmlFile;
					}
				default:
					{
						return Images.TextFile;
					}
			}
		}

		private bamboo.Controls.ProjectExplorer.TreeNodes.SolutionTreeNode CreateSolutionTreeNode(string path)
		{
			string rootPath = path.Substring(0, path.LastIndexOf("\\"));
			if (Bamboo.VisualStudio.SolutionDetector.Is2003(path))
			{
				Bamboo.VisualStudio.VisualStudio2003.Solution.Models.Solution solution = Bamboo.VisualStudio.VisualStudio2003.Solution.SolutionReader.Read(path);
				bamboo.Controls.ProjectExplorer.TreeNodes.SolutionTreeNode solutionTreeNode = new bamboo.Controls.ProjectExplorer.TreeNodes.SolutionTreeNode(solution.Name, path, solution);
				foreach (Bamboo.VisualStudio.VisualStudio2003.Solution.Models.Project project in solution.Projects)
				{
					solutionTreeNode.Add(CreateCSharpProjectTreeNode(rootPath + "\\" + project.Path));
				}
				return solutionTreeNode;
			}
			else if (Bamboo.VisualStudio.SolutionDetector.Is2005(path))
			{
				Bamboo.VisualStudio.VisualStudio2005.Solution.Models.Solution solution = Bamboo.VisualStudio.VisualStudio2005.Solution.SolutionReader.Read(path);
				bamboo.Controls.ProjectExplorer.TreeNodes.SolutionTreeNode solutionTreeNode = new bamboo.Controls.ProjectExplorer.TreeNodes.SolutionTreeNode(solution.Name, path, solution);
				foreach (Bamboo.VisualStudio.VisualStudio2005.Solution.Models.Project project in solution.Projects)
				{
					solutionTreeNode.Add(CreateCSharpProjectTreeNode(rootPath + "\\" + project.Path));
				}
				return solutionTreeNode;
			}
			else
			{
				throw new System.Exception("Invalid solution version.");
			}
		}

		public void CloseProject()
		{
			this.treeView.Nodes.Clear();

			this._treeNode = null;

			SaveSettings();
		}

		public bool Close()
		{
			return true;
		}

		private void treeView_DoubleClick(object sender, EventArgs e)
		{
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

			TreeNodes.TreeNode treeNode = (TreeNodes.TreeNode)this.treeView.SelectedNode;
			if (treeNode != null)
			{
				treeNode.DoubleClick();
			}

			this.Cursor = System.Windows.Forms.Cursors.Default;
		}

		private void treeView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Enter)
			{
				this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

				TreeNodes.TreeNode treeNode = (TreeNodes.TreeNode)this.treeView.SelectedNode;
				if (treeNode != null)
				{
					treeNode.DoubleClick();
				}

				this.Cursor = System.Windows.Forms.Cursors.Default;
			}
		}

		//TODO
//        public void ProjectFind()
//        {
//            if (expressions.Count == 0)
//            {
//                Bamboo.Search.FindDialog dialog = new Bamboo.Search.FindDialog("Project.Find", "Project.Replace");
//                dialog.Text = "Find in " + this._treeNode.Text;

//                Bamboo.Editors.IEditor editor = (Bamboo.Editors.IEditor)Bamboo.Application.GetInstance().GetService(typeof(Bamboo.Editors.IEditor));
//                if (editor != null)
//                {
//                    string text = editor.SelectedText;
//                    if (text.Length > 0)
//                    {
//                        dialog.FindText = text;
//                    }
//                }

//                dialog.Show();
//            }
//            else
//            {
//                Find((string)expressions[0]);

//                Bamboo.Search.FindDialog findDialog = (Bamboo.Search.FindDialog)Bamboo.Application.GetInstance().GetService(typeof(Bamboo.Search.FindDialog));
//                if (findDialog != null)
//                {
//                    findDialog.Close();
//                }
//            }
//        }

//        private void Find(string findText)
//        {
//            Command("View.SearchResults");

//            Command("SearchResults.Clear");

//            Bamboo.Search.SearchFileCollection searchFiles = this.GetSearchableFiles();

//            Bamboo.Search.BasicSearchService searchService = new Bamboo.Search.BasicSearchService();
//            Bamboo.Search.SearchResultCollection searchResults = searchService.Search(findText, searchFiles);

//            foreach (Bamboo.Search.SearchResult searchResult in searchResults)
//            {
//                Command("SearchResults.AddResult", searchResult.Filename, searchResult.Line, searchResult.Column, searchResult.Text);
//            }

//            Command("View.SearchResults");
//        }

//        private Bamboo.Search.SearchFileCollection GetSearchableFiles()
//        {
//            Bamboo.Search.SearchFileCollection searchFiles = new Bamboo.Search.SearchFileCollection();

//            foreach (Bamboo.ProjectExplorers.TreeNodes.ProjectTreeNode projectTreeNode in this._treeNode.Projects)
//            {
//                GetSearchableFiles(projectTreeNode, searchFiles);
//            }

//            return searchFiles;
//        }

//        private void GetSearchableFiles(Bamboo.ProjectExplorers.TreeNodes.ProjectTreeNode projectTreeNode, Bamboo.Search.SearchFileCollection searchFiles)
//        {
//            foreach (Bamboo.ProjectExplorers.TreeNodes.FolderTreeNode folderTreeNode in projectTreeNode.Folders)
//            {
//                GetSearchableFiles(folderTreeNode, searchFiles);
//            }
//            foreach (Bamboo.ProjectExplorers.TreeNodes.FileTreeNode fileTreeNode in projectTreeNode.Files)
//            {
//                if (fileTreeNode.IsIncluded)
//                {
//                    searchFiles.Add(new Bamboo.Search.SearchFile(fileTreeNode.Path));
//                }
//            }
//        }

//        private void GetSearchableFiles(Bamboo.ProjectExplorers.TreeNodes.FolderTreeNode folder, Bamboo.Search.SearchFileCollection searchFiles)
//        {
//            foreach (Bamboo.ProjectExplorers.TreeNodes.FolderTreeNode folderTreeNode in folder.Folders)
//            {
//                GetSearchableFiles(folderTreeNode, searchFiles);
//            }
//            foreach (Bamboo.ProjectExplorers.TreeNodes.FileTreeNode fileTreeNode in folder.Files)
//            {
//                if (fileTreeNode.IsIncluded)
//                {
//                    searchFiles.Add(new Bamboo.Search.SearchFile(fileTreeNode.Path));
//                }
//            }
//        }

		//TODO
//        public void ProjectBuild()
//        {
//            Command("View.OutputWindow");
//            Command("OutputWindow.Clear");

//            Command("View.TaskList");
//            Command("TaskList.Clear");

//            Swampware.BambooShoot.VisualStudio.VisualStudio2003.Projects.CSharpProject.CSharpProjectBuilder projectBuilder = new Swampware.BambooShoot.VisualStudio.VisualStudio2003.Projects.CSharpProject.CSharpProjectBuilder();

//            Command("OutputWindow.WriteLine", "");
//            Command("OutputWindow.WriteLine", "--- Begin Build: " + this._treeNode.Project.Name + " ---");
//            Command("OutputWindow.WriteLine", "");

//            //TODO don't hard code this:  "DEBUG"
//            projectBuilder.Build(this._treeNode.Filename, "DEBUG", new Swampware.BambooShoot.VisualStudio.ProjectBuildTracker());

//            foreach (Swampware.BambooShoot.VisualStudio.VisualStudio2003.Projects.CSharpProject.Output output in projectBuilder.Output)
//            {
//                Command("OutputWindow.WriteLine", output.Text);
//            }

//            bool succeeded = true;
//            foreach (Swampware.BambooShoot.VisualStudio.VisualStudio2003.Projects.CSharpProject.Error error in projectBuilder.Errors)
//            {
//                if (error.IsWarning)
//                {
//                    Command("TaskList.AddWarning", error.Description, error.File, error.Line, error.Column);
//                }
//                else
//                {
//                    succeeded = false;
//                    Command("TaskList.AddError", error.Description, error.File, error.Line, error.Column);
//                }
//            }

//            if (succeeded)
//            {
//                Command("OutputWindow.WriteLine", "");
//                Command("OutputWindow.WriteLine", "--- Build Succeeded: " + this._treeNode.Project.Name + " ---");
//                Command("OutputWindow.WriteLine", "");
//                Command("OutputWindow.WriteLine", "");
//                Command("OutputWindow.WriteLine", "");

//                Command("View.OutputWindow");
//            }
//            else
//            {
//                Command("OutputWindow.WriteLine", "");
//                Command("OutputWindow.WriteLine", "--- Build Failed: " + this._treeNode.Project.Name + " ---");
//                Command("OutputWindow.WriteLine", "");
//                Command("OutputWindow.WriteLine", "");
//                Command("OutputWindow.WriteLine", "");

//                Command("View.TaskList");
//            }
//        }

		private void Application_Idle(object sender, EventArgs e)
		{
			foreach (System.Windows.Forms.ToolStripItem toolButton in this.toolStrip.Items)
			{
				if (toolButton.Tag != null)
				{
					string command = (string)toolButton.Tag;
					toolButton.Enabled = Water.Environment.IsConstant(command);
				}
			}
		}

		private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

			TreeNodes.TreeNode treeNode = (TreeNodes.TreeNode)e.Node;
			if (treeNode != null)
			{
				treeNode.Expanded();
			}

			this.Cursor = System.Windows.Forms.Cursors.Default;
		}

		private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

			TreeNodes.TreeNode treeNode = (TreeNodes.TreeNode)e.Node;
			if (treeNode != null)
			{
				treeNode.Collapsed();
			}

			this.Cursor = System.Windows.Forms.Cursors.Default;
		}

		private void LoadSettings()
		{
			Water.Dictionary Settings = (Water.Dictionary)Water.Environment.Identify("Settings");

			if (Settings.IsDefined("Project.Path"))
			{
				string path = (string)Settings["Project.Path"];
				Water.Evaluator.Apply("Project.Open", path);
			}
		}

		private void SaveSettings()
		{
			Water.Dictionary Settings = (Water.Dictionary)Water.Environment.Identify("Settings");

			if (this._treeNode != null)
			{
				if (this._treeNode is TreeNodes.SolutionTreeNode)
				{
					TreeNodes.SolutionTreeNode solutionTreeNode = (TreeNodes.SolutionTreeNode)this._treeNode;
					Settings["Project.Path"] = solutionTreeNode.Path;
				}
				else if (this._treeNode is TreeNodes.ProjectTreeNode)
				{
					TreeNodes.ProjectTreeNode projectTreeNode = (TreeNodes.ProjectTreeNode)this._treeNode;
					Settings["Project.Path"] = projectTreeNode.Path;
				}
				else
				{
					throw new System.Exception("Invalid project node.");
				}
			}
		}

	}
}
