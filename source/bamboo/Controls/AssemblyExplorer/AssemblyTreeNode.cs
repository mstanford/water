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
	/// Summary description for AssemblyTreeNode.
	/// </summary>
	public class AssemblyTreeNode : bamboo.Controls.AssemblyExplorer.TreeNode
	{
		private const string SEPERATOR = @".";
		private System.Reflection.Assembly _assembly;
		private System.Collections.SortedList _namespaces = new System.Collections.SortedList( new System.Collections.CaseInsensitiveComparer() );
		private System.Collections.SortedList _types = new System.Collections.SortedList( new System.Collections.CaseInsensitiveComparer() );

		public AssemblyTreeNode(System.Reflection.Assembly assembly)
		{
			this._assembly = assembly;

			this.Text = this._assembly.GetName().Name;
			this.Tag = this._assembly.GetName();

			this.ImageIndex = Images.Assembly;
			this.SelectedImageIndex = Images.Assembly;

			this.Nodes.Clear();

			System.Type[] types = this._assembly.GetTypes();
			foreach(System.Type type in types)
			{
				if(type.IsPublic)
				{
					this.AddType(type);
				}
			}

			this.ShowNodes();
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

		private void AddType(System.Type type)
		{
			int index = type.FullName.IndexOf(SEPERATOR);
			if(index == -1)
			{
				TypeTreeNode typeTreeNode = new TypeTreeNode(type);
				this._types.Add(typeTreeNode.Text, typeTreeNode);
			}
			else
			{
				string ns_left = type.FullName.Substring(0, index);
				string ns_right = type.FullName.Substring(index + 1);

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
