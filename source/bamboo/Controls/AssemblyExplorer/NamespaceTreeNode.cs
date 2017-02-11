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

namespace bamboo.Controls.AssemblyExplorer
{
	/// <summary>
	/// Summary description for NamespaceTreeNode.
	/// </summary>
	public class NamespaceTreeNode : bamboo.Controls.AssemblyExplorer.TreeNode
	{
		private const string SEPERATOR = @".";
		private string _name;
		private System.Collections.SortedList _namespaces = new System.Collections.SortedList( new System.Collections.CaseInsensitiveComparer() );
		private System.Collections.SortedList _types = new System.Collections.SortedList( new System.Collections.CaseInsensitiveComparer() );

		public NamespaceTreeNode(string name)
		{
			this._name = name;

			this.Text = this._name;
			this.Tag = this._name;

			this.Nodes.Clear();

			this.ImageIndex = Images.Namespace;
			this.SelectedImageIndex = Images.Namespace;
		}

		public void ShowNodes()
		{
			foreach(NamespaceTreeNode namespaceTreeNode in this._namespaces.Values)
			{
				this.Nodes.Add(namespaceTreeNode);
				namespaceTreeNode.ShowNodes();
			}

			foreach(TypeTreeNode typeTreeNode in this._types.Values)
			{
				this.Nodes.Add(typeTreeNode);
			}
		}

		public void AddType(System.Type type, string name)
		{
			int index = name.IndexOf(SEPERATOR);
			if(index == -1)
			{
				TypeTreeNode typeTreeNode = new TypeTreeNode(type);
				this._types.Add(typeTreeNode.Text, typeTreeNode);
			}
			else
			{
				string ns_left = name.Substring(0, index);
				string ns_right = name.Substring(index + 1);

				NamespaceTreeNode namespaceTreeNode = (NamespaceTreeNode)this._namespaces[ns_left];
				if(namespaceTreeNode == null)
				{
					namespaceTreeNode = new NamespaceTreeNode(ns_left);
					this._namespaces.Add(namespaceTreeNode.Text, namespaceTreeNode);
				}
				namespaceTreeNode.AddType(type, ns_right);
			}
		}

	}
}
