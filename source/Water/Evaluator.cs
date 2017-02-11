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
	/// Summary description for Evaluator.
	/// </summary>
	public class Evaluator
	{

		/// <summary>
		/// Evaluates any type of object.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static object Evaluate(object o)
		{
			if (o is Water.List)
			{
				Water.List list = (Water.List)o;
				return Apply(list.First(), list.NotFirst());
			}
			else if(o is Water.Identifier)
			{
				return EvaluateIdentifier((Water.Identifier)o);
			}
			else if(o is string)
			{
				return EvaluateString((string)o);
			}
			else if(o is Water.Quote)
			{
				Water.Quote quote = (Water.Quote)o;
				return quote.Evaluate(new Water.List());
			}
			else
			{
				return o;
			}
		}

		public static object Apply(string command, params string[] args)
		{
			Water.List cmd = new Water.List();
			cmd.Add(new Water.Identifier(command));
			foreach (string arg in args)
			{
				cmd.Add(arg);
			}
			return Water.Evaluator.Evaluate(cmd);
		}

		public static object Apply(object command, Water.List args)
		{
			if (command is Water.List)
			{
				Water.List list = (Water.List)command;
				object command2 = Apply(list.First(), list.NotFirst());

				//TODO I don't like this.
				if(command2 == null && args.Count == 0)
				{
					return null;
				}

				return Apply(command2, args);
			}
			else if (command is Water.Identifier)
			{
				Water.Identifier identifier = (Water.Identifier)command;
				object command2 = Evaluate(identifier);
				if(command2 == null)
				{
					throw new Water.Error(identifier.Value + " is not defined.");
				}
				return Apply(command2, args);
			}
			else
			{
				Water.Operator op = (Water.Operator)command;
				return op.Evaluate(args);
			}
		}

		#region Identifier

		public static object EvaluateIdentifier(Water.Identifier identifier)
		{
			string name = identifier.Value;
			object value = Water.Environment.Identify(name);
			if(value != null)
			{
				//Exact match
				return value;
			}

			//Break it down.
			System.Text.StringBuilder tracker = new System.Text.StringBuilder();
			System.IO.TextReader reader = new System.IO.StringReader(name);
			string segment = GetSegment(reader);
			tracker.Append(segment);
			value = Water.Environment.Identify(segment);
			segment = "";
			while(reader.Peek() != -1)
			{
				if(value == null)
				{
                    throw new Water.Error(identifier.Value + " is not defined.");
				}

				char ch = (char)reader.Read();
				if(ch == '[')
				{
					value = ApplyIndexer(GetIndexer(reader), value);
					segment = "";
				}
				else if(ch == '.')
				{
					segment += GetSegment(reader);
					if(IsProperty(value, segment))
					{
						tracker.Append(".");
						tracker.Append(segment);
						value = ApplyProperty(segment, value);
						segment = "";
					}
					else if(IsMethod(value, segment))
					{
						tracker.Append(".");
						tracker.Append(segment);
						if(reader.Peek() != -1)
						{
							throw new Water.Error(identifier.Value + " is not defined.");
						}
						return new Water.Method(value, segment);
					}
					else
					{
						segment += ".";
					}
				}
			}
			if(segment.Length > 0)
			{
				throw new Water.Error(identifier.Value + " is not defined.");
			}
			return value;
		}

		private static string GetSegment(System.IO.TextReader reader)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			while(reader.Peek() != -1)
			{
				char ch = (char)reader.Peek();
				if(ch == '[')
				{
					break;
				}
				else if(ch == '.')
				{
					break;
				}
				else
				{
					stringBuilder.Append(ch);
					reader.Read();
				}
			}
			return stringBuilder.ToString();
		}

		private static bool IsProperty(object value, string name)
		{
			if(value is System.Collections.IDictionary)
			{
				System.Collections.IDictionary dictionary = (System.Collections.IDictionary)value;
				return dictionary.Contains(name);
			}

			foreach(System.ComponentModel.PropertyDescriptor propertyDescriptor in System.ComponentModel.TypeDescriptor.GetProperties(value))
			{
				if(propertyDescriptor.Name.ToLower().Equals(name.ToLower()))
				{
					return true;
				}
			}
            foreach (System.Reflection.PropertyInfo propertyInfo in value.GetType().GetProperties())
            {
                if (propertyInfo.Name.ToLower().Equals(name.ToLower()))
                {
                    return true;
                }
            }

			System.Reflection.FieldInfo fieldInfo = value.GetType().GetField(name);
			if(fieldInfo != null)
			{
				return true;
			}

			return false;
		}

		private static bool IsMethod(object value, string name)
		{
			System.Type type = value.GetType();
			foreach (System.Reflection.MethodInfo methodInfo in type.GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static))
			{
				if(methodInfo.Name.ToLower().Equals(name.ToLower()))
				{
					return true;
				}
			}
			return false;
		}

		private static object ApplyProperty(string name, object value)
		{
			if(value is System.Collections.IDictionary)
			{
				System.Collections.IDictionary dictionary = (System.Collections.IDictionary)value;
				return dictionary[name];
			}
			else
			{
				System.Reflection.FieldInfo fieldInfo = value.GetType().GetField(name);
				if(fieldInfo != null)
				{
					return fieldInfo.GetValue(value);
				}
				else
				{
                    foreach (System.ComponentModel.PropertyDescriptor propertyDescriptor in System.ComponentModel.TypeDescriptor.GetProperties(value))
                    {
                        if (propertyDescriptor.Name.ToLower().Equals(name.ToLower()))
                        {
                            return propertyDescriptor.GetValue(value);
                        }
                    }
                    foreach (System.Reflection.PropertyInfo propertyInfo in value.GetType().GetProperties())
                    {
                        if (propertyInfo.Name.ToLower().Equals(name.ToLower()))
                        {
                            return propertyInfo.GetValue(value, new object[] { });
                        }
                    }
                    throw new Water.Error("Unable to find property: " + name);
				}
			}
		}

		private static object ApplyIndexer(string name, object value)
		{
			object index = Evaluate(Water.Parser.Parse(name));

			if(value is Water.Dictionary)
			{
				Water.Dictionary dictionary = (Water.Dictionary)value;

				string key = index.ToString();
				value = dictionary[key];

				//TODO DELETE
//				if(name.StartsWith("\"") && name.EndsWith("\""))
//				{
//					string key = EvaluateString(name.Substring(1, name.Length - 2));
//					value = dictionary[key];
//				}
//				else
//				{
//					string key = (string)Water.Environment.Identify(name);
//					value = dictionary[key];
//				}
			}
			else if (value.GetType().IsArray)
			{
				// Array Indexer
				System.Reflection.MethodInfo methodInfo = value.GetType().GetMethod("GetValue", new System.Type[] { index.GetType() } );
				value = methodInfo.Invoke(value, new object[] { index } );
			}
			else
			{
				System.Reflection.PropertyInfo propertyInfo = value.GetType().GetProperty("Item", new System.Type[] { index.GetType() } );
				if(propertyInfo == null)
				{
					throw new Water.Error("Unable to apply indexer.");
				}

				if(Water.Instructions.IsInt(index))
				{
					value = propertyInfo.GetValue(value, new object[] { index } );
				}
				else if(name.StartsWith("\"") && name.EndsWith("\""))
				{
					string key = EvaluateString(name.Substring(1, name.Length - 2));
					value = propertyInfo.GetValue(value, new object[] { key } );
				}
				else
				{
					string key = (string)Water.Environment.Identify(name);
					value = propertyInfo.GetValue(value, new object[] { key } );
				}
			}
			return value;
		}

		private static string GetIndexer(System.IO.TextReader reader)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			char ch;
			while((ch = (char)reader.Read()) != ']')
			{
				stringBuilder.Append(ch);
			}
			return stringBuilder.ToString();
		}

		#endregion

		#region String

		public static string EvaluateString(string s)
		{
			char[] ach = s.ToCharArray();

			System.Text.StringBuilder tagStringBuilder = new System.Text.StringBuilder();
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

			bool inTag = false;
			bool isEscaped = false;
			for(int i = 0; i < ach.Length; i++)
			{
				if(isEscaped)
				{
					if(ach[i] == '}')
					{
						isEscaped = false;
					}
					tagStringBuilder.Append(ach[i]);
				}
				else if(inTag)
				{
					if(ach[i] == '{')
					{
						isEscaped = true;
						tagStringBuilder.Append(ach[i]);
					}
					else if(ach[i] == '}')
					{
						inTag = false;

						//Replace tag
						string name = tagStringBuilder.ToString();

						object val = Evaluate(Water.Parser.Parse(name));

						if(val == null)
						{
							throw new Water.Error("Tag evaluated to null: " + name);
						}
						string value = val.ToString();
						stringBuilder.Append(value);
						tagStringBuilder = new System.Text.StringBuilder();
					}
					else
					{
						tagStringBuilder.Append(ach[i]);
					}
				}
				else
				{
					if(ach[i] == '$' && (i + 1) < ach.Length && ach[i + 1] == '{')
					{
						inTag = true;
						i++;
					}
					else
					{
						stringBuilder.Append(ach[i]);
					}
				}
			}
			return stringBuilder.ToString();
		}

		#endregion

		#region Boolean

		public static bool EvaluateBoolean(object expression)
		{
			object value = Evaluate(expression);
			if(value == null)
			{
				throw new Water.Error("Value is null.");
			}
			else if(!(value is bool))
			{
				throw new Water.Error("Value is not a boolean.");
			}
			return (bool)value;
		}

		#endregion

		private Evaluator()
		{
		}

	}
}
