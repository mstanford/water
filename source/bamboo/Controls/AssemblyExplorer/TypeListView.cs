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
	/// Summary description for TypeListView.
	/// </summary>
	public class TypeListView : System.Windows.Forms.ListView
	{
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.ComponentModel.IContainer components;
		private System.Type _type;
		private System.Collections.SortedList _fields = new System.Collections.SortedList( new System.Collections.CaseInsensitiveComparer() );
		private System.Collections.SortedList _properties = new System.Collections.SortedList( new System.Collections.CaseInsensitiveComparer() );
		private System.Collections.SortedList _methods = new System.Collections.SortedList( new System.Collections.CaseInsensitiveComparer() );
		private System.Collections.SortedList _events = new System.Collections.SortedList( new System.Collections.CaseInsensitiveComparer() );

		public TypeListView(System.Type type)
		{
			this._type = type;

			InitializeComponent();

			this.LoadType(this._type);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TypeListView));
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
			// 
			// imageList
			// 
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Magenta;
			// 
			// columnHeaderName
			// 
			this.columnHeaderName.Text = "Name";
			this.columnHeaderName.Width = -2;
			// 
			// TypeListView
			// 
			this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																			  this.columnHeaderName});
			this.FullRowSelect = true;
			this.MultiSelect = false;
			this.SmallImageList = this.imageList;
			this.View = System.Windows.Forms.View.SmallIcon;

		}

		private void LoadType(System.Type type)
		{
			this.Items.Clear();


			foreach(System.Reflection.FieldInfo fieldInfo in type.GetFields() )
			{
				if(fieldInfo.IsPublic && !fieldInfo.IsSpecialName)
				{
					this._fields.Add(fieldInfo.Name, new FieldListViewItem(fieldInfo));
				}
			}

			foreach(System.Reflection.PropertyInfo propertyInfo in type.GetProperties() )
			{
				this._properties.Add(propertyInfo.Name, new PropertyListViewItem(propertyInfo));
			}

			foreach(System.Reflection.MethodInfo methodInfo in type.GetMethods() )
			{
				if(methodInfo.IsPublic && !methodInfo.IsSpecialName)
				{
					System.Reflection.ParameterInfo[] parameterInfos = methodInfo.GetParameters();
					System.Text.StringBuilder parameterStringBuilder = new System.Text.StringBuilder();
					bool first = true;
					foreach(System.Reflection.ParameterInfo parameterInfo in parameterInfos)
					{
						if(first)
						{
							parameterStringBuilder.Append(parameterInfo.ParameterType.Name + " " + parameterInfo.Name);
							first = false;
						}
						else
						{
							parameterStringBuilder.Append(", " + parameterInfo.ParameterType.Name + " " + parameterInfo.Name);
						}
					}
					string text = methodInfo.Name + "(" + parameterStringBuilder.ToString() + ")";

					if(this._methods[text] == null)
					{
						this._methods.Add(text, new MethodListViewItem(text, methodInfo));
					}
				}

			}

			foreach(System.Reflection.EventInfo eventInfo in type.GetEvents() )
			{
				if(!eventInfo.IsSpecialName)
				{
					this._events.Add(eventInfo.Name, new EventListViewItem(eventInfo));
				}
			}

			this.ShowItems();
		}

		private void ShowItems()
		{
			foreach(FieldListViewItem fieldListViewItem in this._fields.Values)
			{
				this.Items.Add(fieldListViewItem);
			}

			foreach(PropertyListViewItem propertyListViewItem in this._properties.Values)
			{
				this.Items.Add(propertyListViewItem);
			}

			foreach(MethodListViewItem methodListViewItem in this._methods.Values)
			{
				this.Items.Add(methodListViewItem);
			}

			foreach(EventListViewItem eventListViewItem in this._events.Values)
			{
				this.Items.Add(eventListViewItem);
			}
		}

	}
}
