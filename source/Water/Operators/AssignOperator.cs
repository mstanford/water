// ------------------------------------------------------------------------------
// 
// Copyright (c) 2007-2008 Swampware, Inc.
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

namespace Water.Operators
{
	/// <summary>
	/// Summary description for AssignOperator.
	/// </summary>
	public class AssignOperator : Water.Operator
	{

		public AssignOperator()
		{
		}

		public override object Evaluate(Water.List expressions)
		{
			if(expressions.Count != 2)
			{
				throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
			}


			if (!(expressions[0] is Water.Identifier))
			{
				throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
			}
			string target = ((Water.Identifier)expressions[0]).Value;
			if(target.IndexOf(".") != -1)
			{
				object o = Water.Evaluator.Evaluate(new Water.Identifier(target.Substring(0, target.LastIndexOf("."))));
				string property_name = target.Substring(target.LastIndexOf(".") + ".".Length);
				object value = Water.Evaluator.Evaluate(expressions[1]);


				if(o is System.Collections.IDictionary)
				{
					System.Collections.IDictionary dictionary = (System.Collections.IDictionary)o;
					dictionary[property_name] = value;
				}
				else
				{
					System.Reflection.FieldInfo fieldInfo = o.GetType().GetField(property_name);
					if (fieldInfo != null)
					{
						fieldInfo.SetValue(o, value);
						return null;
					}
					else
					{
						foreach (System.ComponentModel.PropertyDescriptor propertyDescriptor in System.ComponentModel.TypeDescriptor.GetProperties(o))
						{
							if (propertyDescriptor.Name.ToLower().Equals(property_name.ToLower()))
							{
								propertyDescriptor.SetValue(o, value);
								return null;
							}
						}
						foreach (System.Reflection.PropertyInfo propertyInfo in o.GetType().GetProperties())
						{
							if (propertyInfo.Name.ToLower().Equals(property_name.ToLower()))
							{
								propertyInfo.SetValue(o, value, new object[] { });
								return null;
							}
						}
						throw new Water.Error("Property does not exist: " + property_name);
					}
				}
			}
			else
			{
				if(expressions.Count != 2)
				{
					throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
				}


				if(!(expressions[0] is Identifier))
				{
					throw new Water.Error("Invalid arguments.\n\t" + expressions.ToString());
				}
				string name = ((Identifier)expressions[0]).Value;

				Water.Environment.RedefineVariable(name, Water.Evaluator.Evaluate(expressions[1]));
			}


			return null;
		}

		public override string ToString()
		{
			return GetType().FullName;
		}

	}
}
