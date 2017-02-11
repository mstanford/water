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
	/// Summary description for Generator.
	/// </summary>
	public class Generator
	{

		public static void Generate(object value, System.IO.TextWriter writer)
		{
			Generate(value, new Water.TextWriter(writer));
		}

		public static void Generate(object value, Water.TextWriter writer)
		{
			#region Generate

			if(value == null)
			{
				writer.WriteLine("null");
			}
			else if(value is bool)
			{
				bool b = (bool)value;

				writer.WriteLine(b.ToString());
			}
			else if(value is byte)
			{
				byte n = (byte)value;

				writer.WriteLine(n.ToString());
			}
            else if (value is char)
            {
                char ch = (char)value;

                writer.WriteLine(ch.ToString());
            }
            else if (value is short)
			{
				short n = (short)value;

				writer.WriteLine(n.ToString());
			}
			else if(value is int)
			{
				int n = (int)value;

				writer.WriteLine(n.ToString());
			}
			else if(value is long)
			{
				long n = (long)value;

				writer.WriteLine(n.ToString());
			}
			else if(value is decimal)
			{
				decimal d = (decimal)value;

				writer.WriteLine(d.ToString());
			}
			else if(value is float)
			{
				float f = (float)value;

				writer.WriteLine(f.ToString());
			}
			else if(value is double)
			{
				double d = (double)value;

				writer.WriteLine(d.ToString());
			}
			else if (value is System.DateTime)
			{
				System.DateTime dt = (System.DateTime)value;

				writer.WriteLine(dt.ToString());
			}
			else if (value is Water.Identifier)
			{
				Water.Identifier identifier = (Water.Identifier)value;

				writer.WriteLine(identifier.Value);
			}
			else if(value is Water.Quote)
			{
				Water.Quote quote = (Water.Quote)value;

				writer.Write("\'");
				Generate(quote.Expression, writer);
			}
			else if(value is Water.Comma)
			{
				Water.Comma comma = (Water.Comma)value;

				writer.Write(",");
				Generate(comma.Expression, writer);
			}
			else if(value is byte[])
			{
				byte[] bytes = (byte[])value;

				writer.Write("[");
				writer.Write(System.Convert.ToBase64String(bytes));
				writer.WriteLine("]");
			}
			else if(value is string)
			{
				string s = (string)value;

				writer.WriteLine("\"" + Escape(s) + "\"");
			}
			else if (value is System.Enum)
			{
				System.Enum enum_ = (System.Enum)value;

				writer.WriteLine(enum_.ToString());
			}
			else if ((value is System.Collections.IDictionary))
			{
				System.Collections.IDictionary dictionary = (System.Collections.IDictionary)value;

				writer.WriteLine("{");
				writer.Indent();

				bool first = true;
				foreach (object key in dictionary.Keys)
				{
					try
					{
						object dictionaryValue = dictionary[key];
						if (writer.NewlineString.Length == 0)
						{
							if (first)
							{
								first = false;
							}
							else
							{
								writer.Write(" ");
							}
						}

						writer.Write(key.ToString() + ": ");
						Generate(dictionaryValue, writer);
					}
					catch (System.Exception exception)
					{
						string thisIsAHackToIgnoreCompilerWarning = exception.Message;
					}
				}

				writer.Unindent();
				writer.WriteLine("}");
			}
            else if ((value is Water.Dictionary))
            {
                //Water.Dictionary dictionary = (Water.Dictionary)value;

                //DUP
                writer.WriteLine("{");
                writer.Indent();

                bool first = true;

                System.Type type = value.GetType();
                foreach (System.Reflection.FieldInfo fieldInfo in type.GetFields())
                {
                    if (!fieldInfo.IsStatic)
                    {
                        try
                        {
                            object fieldValue = fieldInfo.GetValue(value);
                            if (writer.NewlineString.Length == 0)
                            {
                                if (first)
                                {
                                    first = false;
                                }
                                else
                                {
                                    writer.Write(" ");
                                }
                            }

                            writer.Write(fieldInfo.Name + ": ");
                            Generate(fieldValue, writer);
                        }
                        catch (System.Exception exception)
                        {
                            string thisIsAHackToIgnoreCompilerWarning = exception.Message;
                        }
                    }
                }

                System.ComponentModel.PropertyDescriptorCollection propertyDescriptors = System.ComponentModel.TypeDescriptor.GetProperties(value);
                foreach (System.ComponentModel.PropertyDescriptor propertyDescriptor in propertyDescriptors)
                {
                    try
                    {
                        object propertyValue = propertyDescriptor.GetValue(value);
                        if (writer.NewlineString.Length == 0)
                        {
                            if (first)
                            {
                                first = false;
                            }
                            else
                            {
                                writer.Write(" ");
                            }
                        }

                        writer.Write(propertyDescriptor.Name + ": ");
                        Generate(propertyValue, writer);
                    }
                    catch (System.Exception exception)
                    {
                        string thisIsAHackToIgnoreCompilerWarning = exception.Message;
                    }
                }

                writer.Unindent();
                writer.WriteLine("}");
            }
            else if (Water.Instructions.IsList(value))
			{
				System.Collections.IEnumerable list = (System.Collections.IEnumerable)value;

				writer.WriteLine("(");
				writer.Indent();

				bool first = true;
				foreach(object item in list)
				{
					if(writer.NewlineString.Length == 0)
					{
						if(first)
						{
							first = false;
						}
						else
						{
							writer.Write(" ");
						}
					}

					Generate(item, writer);
				}

				writer.Unindent();
				writer.WriteLine(")");
			}
			else
			{
                //DUP
                writer.WriteLine("{");
				writer.Indent();

				bool first = true;

				System.Type type = value.GetType();
				foreach(System.Reflection.FieldInfo fieldInfo in type.GetFields())
				{
					if (!fieldInfo.IsStatic)
					{
						try
						{
							object fieldValue = fieldInfo.GetValue(value);
							if (writer.NewlineString.Length == 0)
							{
								if (first)
								{
									first = false;
								}
								else
								{
									writer.Write(" ");
								}
							}

							writer.Write(fieldInfo.Name + ": ");
							Generate(fieldValue, writer);
						}
						catch (System.Exception exception)
						{
							string thisIsAHackToIgnoreCompilerWarning = exception.Message;
						}
					}
				}

				System.ComponentModel.PropertyDescriptorCollection propertyDescriptors = System.ComponentModel.TypeDescriptor.GetProperties(value);
				foreach(System.ComponentModel.PropertyDescriptor propertyDescriptor in propertyDescriptors)
				{
					try
					{
						object propertyValue = propertyDescriptor.GetValue(value);
						if(writer.NewlineString.Length == 0)
						{
							if(first)
							{
								first = false;
							}
							else
							{
								writer.Write(" ");
							}
						}

						writer.Write(propertyDescriptor.Name + ": ");
						Generate(propertyValue, writer);
					}
					catch(System.Exception exception)
					{
						string thisIsAHackToIgnoreCompilerWarning = exception.Message;
					}
				}

				writer.Unindent();
				writer.WriteLine("}");
			}

			writer.Flush();

			#endregion
		}

		public static string Generate(object value)
		{
			System.IO.StringWriter writer = new System.IO.StringWriter();
			Generate(value, writer);
			writer.Flush();
			string s = writer.ToString();
			writer.Close();
			return s;
		}

		public static string Generate(object value, string indentationString, string newlineString)
		{
			System.IO.StringWriter writer = new System.IO.StringWriter();
			Water.TextWriter textWriter = new Water.TextWriter(writer, indentationString, newlineString);
			Generate(value, textWriter);
			writer.Flush();
			string s = writer.ToString();
			textWriter.Close();
			return s;
		}

		private static string Escape(string input)
		{
			#region Escape

			System.IO.StringReader reader = new System.IO.StringReader(input);
			System.IO.StringWriter writer = new System.IO.StringWriter();

			int n;
			while ((n = reader.Read()) != -1)
			{
				char ch = (char)n;
				switch (ch)
				{
					case '\\' :
					case '\'' :
					case '\"' :
					case '\0' :
					case '\a' :
					case '\b' :
					case '\f' :
					case '\n' :
					case '\r' :
					case '\t' :
					case '\v' :
					{
						writer.Write('\\');
						writer.Write(ch);
						break;
					}
					default :
					{
						writer.Write(ch);
						break;
					}
				}
			}
			
			return writer.ToString();

			//System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(input);

			////backslash
			//stringBuilder.Replace("\\", "\\\\");

			////single quote
			//stringBuilder.Replace("\'", "\\\'");

			////double quote
			//stringBuilder.Replace("\"", "\\\"");

			////null
			//stringBuilder.Replace("\0", "\\0");

			////alert
			//stringBuilder.Replace("\a", "\\a");

			////backspace
			//stringBuilder.Replace("\b", "\\b");

			////form feed
			//stringBuilder.Replace("\f", "\\f");

			////newline
			//stringBuilder.Replace("\n", "\\n");

			////carriage return
			//stringBuilder.Replace("\r", "\\r");

			////tab
			//stringBuilder.Replace("\t", "\\t");

			////vertical tab
			//stringBuilder.Replace("\v", "\\v");

			//return stringBuilder.ToString();

			#endregion
		}

		private Generator()
		{
		}

	}
}
