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

namespace Bamboo.VisualStudio.VisualStudio2003.CSharpProject.Models
{
	/// <summary>
	/// Summary description for ProjectCollection.
	/// </summary>
	public class ProjectCollection : System.Collections.CollectionBase
	{

		public ProjectCollection()
		{
		}

		public Project this[int index]
		{
			get { return (Project)this.List[index]; }
		}

		public void Add(Project item)
		{
			this.List.Add(item);
		}

		public void Remove(Project item)
		{
			this.List.Remove(item);
		}

		public bool Contains(Project item)
		{
			return this.List.Contains(item);
		}

		public event System.EventHandler Changed;

		protected override void OnClearComplete()
		{
			if(Changed != null)
			{
				Changed(this, System.EventArgs.Empty);
			}
		}

		protected override void OnInsertComplete(int index, object value)
		{
			if(Changed != null)
			{
				Changed(this, System.EventArgs.Empty);
			}
		}

		protected override void OnRemoveComplete(int index, object value)
		{
			if(Changed != null)
			{
				Changed(this, System.EventArgs.Empty);
			}
		}

	}
}
