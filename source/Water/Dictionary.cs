// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006-2007 Swampware, Inc.
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

namespace Water
{
	/// <summary>
	/// Summary description for Dictionary.
	/// </summary>
	public sealed class Dictionary : System.ComponentModel.ICustomTypeDescriptor, 
		System.Collections.IEnumerable
	{
		private System.Collections.SortedList _properties = new System.Collections.SortedList();
		private System.Collections.ArrayList _propertyDescriptors = new System.Collections.ArrayList();

		public Dictionary()
		{
		}

        public int Count
        {
            get { return this._properties.Count; }
        }

        public System.Collections.IEnumerator GetEnumerator()
		{
			return this._properties.Keys.GetEnumerator();
		}

		public object this[string name]
		{
			get { return this._properties[name]; }
			set
			{
				if (this._properties.ContainsKey(name))
				{
					this.Remove(name);
				}
				this.Add(name, value);
			}
		}

		public void Add(string name, object value)
		{
			if (this._properties.ContainsKey(name))
			{
				throw new Water.Error("Cannot override property: " + name);
			}
			this._properties.Add(name, value);

			if(value == null)
			{
				this._propertyDescriptors.Add(new DictionaryProperty(name, typeof(object)));
			}
			else
			{
				this._propertyDescriptors.Add(new DictionaryProperty(name, value.GetType()));
			}
		}

		public void Remove(string name)
		{
			if(!this._properties.ContainsKey(name))
			{
				throw new Water.Error("Cannot remove property: " + name);
			}
			this._properties.Remove(name);
			this._propertyDescriptors.Remove(GetPropertyDescriptor(name));
		}

		public void Clear()
		{
			this._properties.Clear();
			this._propertyDescriptors.Clear();
		}

		public bool IsDefined(string name)
		{
			return this._properties.ContainsKey(name);
		}

		public override bool Equals(object obj)
		{
			return Water.Instructions.AreEqual(this, obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#region ICustomTypeDescriptor Members

		private DictionaryProperty GetPropertyDescriptor(string name)
		{
			foreach(DictionaryProperty dictionaryProperty in this._propertyDescriptors)
			{
				if(dictionaryProperty.Name.Equals(name))
				{
					return dictionaryProperty;
				}
			}
			throw new Water.Error("Property not found: " + name);
		}

		public System.ComponentModel.AttributeCollection GetAttributes()
		{
			return System.ComponentModel.TypeDescriptor.GetAttributes(this, true);
		}

		public string GetClassName()
		{
			return System.ComponentModel.TypeDescriptor.GetClassName(this, true);
		}

		public string GetComponentName()
		{
			return System.ComponentModel.TypeDescriptor.GetComponentName(this, true);
		}

		public System.ComponentModel.TypeConverter GetConverter()
		{
			return System.ComponentModel.TypeDescriptor.GetConverter(this, true);
		}

		public System.ComponentModel.EventDescriptor GetDefaultEvent()
		{
			return System.ComponentModel.TypeDescriptor.GetDefaultEvent(this, true);
		}

		public System.ComponentModel.PropertyDescriptor GetDefaultProperty()
		{
			return System.ComponentModel.TypeDescriptor.GetDefaultProperty(this, true);
		}

		public object GetEditor(Type editorBaseType)
		{
			return System.ComponentModel.TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		public System.ComponentModel.EventDescriptorCollection GetEvents()
		{
			return System.ComponentModel.TypeDescriptor.GetEvents(this, true);
		}

		public System.ComponentModel.EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return System.ComponentModel.TypeDescriptor.GetEvents(this, attributes, true);
		}

		public object GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
		{
			return this;
		}

		public System.ComponentModel.PropertyDescriptorCollection GetProperties()
		{
			System.ComponentModel.PropertyDescriptor[] properties = new System.ComponentModel.PropertyDescriptor[this._propertyDescriptors.Count];
			int i = 0;
			foreach(DictionaryProperty dictionaryProperty in this._propertyDescriptors)
			{
				properties[i] = dictionaryProperty;
				i++;
			}
			return new System.ComponentModel.PropertyDescriptorCollection(properties);
		}

		public System.ComponentModel.PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return GetProperties();
		}

		#endregion

		/// <summary>
		/// Summary description for DictionaryProperty.
		/// </summary>
		public class DictionaryProperty : System.ComponentModel.PropertyDescriptor
		{
			private string _name;
			private System.Type _type;

			public DictionaryProperty(string name, System.Type type) : base(name, null)
			{
				this._name = name;
				this._type = type;
			}

			public override bool CanResetValue(object component)
			{
				return false;
			}

			public override Type ComponentType
			{
				get { return null; }
			}

			public override object GetValue(object component)
			{
				Dictionary dict = (Dictionary)component;
				return dict._properties[this._name];
			}

			public override bool IsReadOnly
			{
				get { return false; }
			}

			public override Type PropertyType
			{
				get { return this._type; }
			}

			public override void ResetValue(object component)
			{
			}

			public override void SetValue(object component, object value)
			{
				Dictionary dict = (Dictionary)component;
				dict._properties[this._name] = value;
			}

			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

		}

	}
}
