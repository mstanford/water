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
	/// Summary description for SolutionTreeNode.
	/// </summary>
	public class SolutionTreeNode : TreeNode
	{
		private string _path;
		private object _solution;
		private System.Collections.SortedList _sortedProjectNodes = new System.Collections.SortedList(new System.Collections.CaseInsensitiveComparer());

		public SolutionTreeNode(string name, string path, object solution)
		{
			this.Text = name;
			this._path = path;
			this.Image = Images.Solution;
			this._solution = solution;
		}

		public string Path
		{
			get { return this._path; }
		}

		public object Solution
		{
			get { return this._solution; }
		}

		public System.Collections.ICollection Projects
		{
			get { return this._sortedProjectNodes.Values; }
		}

		public void Add(ProjectTreeNode treeNode)
		{
			this._sortedProjectNodes.Add(treeNode.Text, treeNode);
			int index = this._sortedProjectNodes.IndexOfKey(treeNode.Text);
			this.Nodes.Insert(index, treeNode);
		}

	}
}
