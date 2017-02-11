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

namespace Bamboo.VisualStudio.VisualStudio2003.Solution.Models
{
	/// <summary>
	/// Summary description for Project.
	/// </summary>
	public class Project
	{
		private System.String _name = String.Empty;
		private System.String _path = String.Empty;
		private System.Guid _guid;

		public Project()
		{
		}

		public System.String Name
		{
			get { return this._name; }
			set
			{
				this._name = value;
				this.OnNameChanged();
			}
		}

		public System.String Path
		{
			get { return this._path; }
			set
			{
				this._path = value;
				this.OnPathChanged();
			}
		}

		public System.Guid Guid
		{
			get { return this._guid; }
			set
			{
				this._guid = value;
				this.OnGuidChanged();
			}
		}

		public event System.EventHandler NameChanged;

		protected void OnNameChanged()
		{
			if(NameChanged != null)
			{
				NameChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler PathChanged;

		protected void OnPathChanged()
		{
			if(PathChanged != null)
			{
				PathChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler GuidChanged;

		protected void OnGuidChanged()
		{
			if(GuidChanged != null)
			{
				GuidChanged(this, System.EventArgs.Empty);
			}
		}

	}
}
