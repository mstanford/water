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
	/// Summary description for Solution.
	/// </summary>
	public class Solution
	{
		private System.String _name = String.Empty;
		private ConfigCollection _configs = new ConfigCollection();
		private ProjectCollection _projects = new ProjectCollection();

		public Solution()
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

		public ConfigCollection Configs
		{
			get { return this._configs; }
			set
			{
				this._configs = value;
				this.OnConfigsChanged();
			}
		}

		public ProjectCollection Projects
		{
			get { return this._projects; }
			set
			{
				this._projects = value;
				this.OnProjectsChanged();
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

		public event System.EventHandler ConfigsChanged;

		protected void OnConfigsChanged()
		{
			if(ConfigsChanged != null)
			{
				ConfigsChanged(this, System.EventArgs.Empty);
			}
		}

		public event System.EventHandler ProjectsChanged;

		protected void OnProjectsChanged()
		{
			if(ProjectsChanged != null)
			{
				ProjectsChanged(this, System.EventArgs.Empty);
			}
		}

	}
}
