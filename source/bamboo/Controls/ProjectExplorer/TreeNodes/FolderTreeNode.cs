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

namespace bamboo.Controls.ProjectExplorer.TreeNodes
{
	/// <summary>
	/// Summary description for FolderTreeNode.
	/// </summary>
	public class FolderTreeNode : TreeNode
	{
		private System.Collections.SortedList _sortedFolderNodes = new System.Collections.SortedList(new System.Collections.CaseInsensitiveComparer());
		private System.Collections.SortedList _sortedFileNodes = new System.Collections.SortedList(new System.Collections.CaseInsensitiveComparer());

		public FolderTreeNode(string text)
		{
            this.Text = text;
			this.Image = Images.ClosedFolder;
			this.ExpandedImage = Images.OpenFolder;
		}

		public System.Collections.ICollection Folders
		{
			get { return this._sortedFolderNodes.Values; }
		}

		public System.Collections.ICollection Files
		{
			get { return this._sortedFileNodes.Values; }
		}

		public void Add(FolderTreeNode treeNode)
		{
			this._sortedFolderNodes.Add(treeNode.Text, treeNode);
			int index = this._sortedFolderNodes.IndexOfKey(treeNode.Text);
			this.Nodes.Insert(index, treeNode);
		}

		public void Add(FileTreeNode treeNode)
		{
			this._sortedFileNodes.Add(treeNode.Text, treeNode);
			int index = this._sortedFolderNodes.Count + this._sortedFileNodes.IndexOfKey(treeNode.Text);
			this.Nodes.Insert(index, treeNode);
		}

	}
}
