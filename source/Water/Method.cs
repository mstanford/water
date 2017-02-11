// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006-2008 Swampware, Inc.
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
	/// Summary description for Method.
	/// </summary>
	public class Method : Water.Operator
	{
		private object _instance;
		private System.Type _type;
		private string _methodName;

		public Method(object instance, string methodName)
		{
			this._instance = instance;
			this._type = instance.GetType();
			this._methodName = methodName;
		}

		public Method(System.Type type, string methodName)
		{
			if(type == null)
			{
				throw new Water.Error("Type for '" + methodName + "' is null.");
			}

			this._instance = null;
			this._type = type;
			this._methodName = methodName;
		}

		public object Instance
		{
			get { return this._instance; }
		}

		public System.Type Type
		{
			get { return this._type; }
		}

		public string MethodName
		{
			get { return this._methodName; }
		}

		public override object Evaluate(Water.List expressions)
		{
			System.Reflection.MethodInfo[] methods = GetMethods(this._type, this._methodName);

			object[] values = GetValues(expressions);

			System.Reflection.MethodInfo method = GetMethod(methods, values);
			if(methods.Length == 0)
			{
				throw new Water.Error(this._methodName + " does not exist.");
			}
			if(method == null)
			{
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

				stringBuilder.Append("Parameter mismatch on method: " + this._methodName);
				stringBuilder.Append(System.Environment.NewLine);

				bool first = true;
				foreach(object value in expressions)
				{
					if(first)
					{
						first = false;
					}
					else
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(value.GetType().FullName);
					stringBuilder.Append(" ");
					stringBuilder.Append(value.ToString());
				}
				stringBuilder.Append(System.Environment.NewLine);

				foreach(System.Reflection.MethodInfo method2 in methods)
				{
					first = true;
					foreach(System.Reflection.ParameterInfo parameterInfo in method2.GetParameters())
					{
						if(first)
						{
							first = false;
						}
						else
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(parameterInfo.ParameterType.FullName);
						stringBuilder.Append(" ");
						stringBuilder.Append(parameterInfo.Name);
					}
					stringBuilder.Append(System.Environment.NewLine);
				}

				throw new Water.Error(stringBuilder.ToString());
			}
			if(method.IsStatic)
			{
				return method.Invoke(null, values);
			}
			else
			{
				return method.Invoke(this._instance, values);
			}
		}

		private System.Reflection.MethodInfo[] GetMethods(System.Type type, string methodName)
		{
			System.Collections.ArrayList items = new System.Collections.ArrayList();
			foreach (System.Reflection.MethodInfo methodInfo in type.GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static))
			{
				if(methodInfo.Name.ToLower().Equals(methodName.ToLower()))
				{
					items.Add(methodInfo);
				}
			}
			System.Reflection.MethodInfo[] methodInfos = new System.Reflection.MethodInfo[items.Count];
			int i = 0;
			foreach(System.Reflection.MethodInfo methodInfo in items)
			{
				methodInfos[i] = methodInfo;
				i++;
			}
			return methodInfos;
		}

		private System.Reflection.MethodInfo GetMethod(System.Reflection.MethodInfo[] methodInfos, object[] values)
		{
			System.Reflection.MethodInfo ret = null;
			foreach(System.Reflection.MethodInfo methodInfo in methodInfos)
			{
				if(IsMatch(methodInfo.GetParameters(), values))
				{
					if(ret != null)
					{
						return ret;
					}
					ret = methodInfo;
				}
			}
			return ret;
		}

		private bool IsMatch(System.Reflection.ParameterInfo[] parameterInfos, object[] values)
		{
			if(parameterInfos.Length != values.Length)
			{
				return false;
			}

			int i = 0;
			foreach(System.Reflection.ParameterInfo parameterInfo in parameterInfos)
			{
				object value = values[i];
				if(value != null && !parameterInfo.ParameterType.IsAssignableFrom(value.GetType()))
				{
					return false;
				}
				i++;
			}
			return true;
		}

		private object[] GetValues(Water.List expressions)
		{
			object[] values = new object[expressions.Count];
			int i = 0;
			foreach(object expression in expressions)
			{
				values[i] = Water.Evaluator.Evaluate(expression);
				i++;
			}
			return values;
		}

		public override string ToString()
		{
			return "METHOD " + this.MethodName;
		}

	}
}
