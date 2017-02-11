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

namespace bamboo.Controls.ProjectExplorer.TreeNodes
{
	/// <summary>
	/// Summary description for TreeNode.
	/// </summary>
	public class TreeNode : System.Windows.Forms.TreeNode
	{
		private int _image = -1;
		private int _expandedImage = -1;

		public TreeNode()
		{
		}

		public int Image
		{
			get { return this._image; }
			set
			{
				this._image = value;

				this.ImageIndex = this._image;
				this.SelectedImageIndex = this._image;
			}
		}

		public int ExpandedImage
		{
			get { return this._expandedImage; }
			set { this._expandedImage = value; }
		}

		public virtual void DoubleClick()
		{
		}

		public void Expanded()
		{
			if (this._expandedImage != -1)
			{
				this.ImageIndex = this._expandedImage;
				this.SelectedImageIndex = this._expandedImage;
			}
		}

		public void Collapsed()
		{
			if (this._expandedImage != -1)
			{
				this.ImageIndex = this._image;
				this.SelectedImageIndex = this._image;
			}
		}

	}
}
