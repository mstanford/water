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
	public class Printer
	{

		public static void Print(object value)
		{
			System.IO.TextWriter output = Water.Environment.Output;
			Print(value, output);
		}

		public static void Print(object value, System.IO.TextWriter output)
		{
			if (value is bool)
			{
				output.WriteLine(value.ToString());
			}
			else if (value is bool[])
			{
				bool[] array = (bool[])value;

				for (int i = 0; i < array.Length; i++)
				{
					if (i > 0)
					{
						output.Write(" ");
					}
					output.Write(array[i].ToString());
				}
				output.WriteLine();
			}
			else if (value is bool[,])
			{
				bool[,] matrix = (bool[,])value;

				for (int y = 0; y < matrix.GetLength(1); y++)
				{
					for (int x = 0; x < matrix.GetLength(0); x++)
					{
						if (x > 0)
						{
							output.Write("\t");
						}
						output.Write(matrix[x, y].ToString());
					}
					output.WriteLine();
				}
			}
			else if (value is byte)
			{
				output.WriteLine(value.ToString());
			}
			else if (value is byte[])
			{
				byte[] array = (byte[])value;

				for (int i = 0; i < array.Length; i++)
				{
					if (i > 0)
					{
						output.Write(" ");
					}
					output.Write(array[i].ToString());
				}
				output.WriteLine();
			}
			else if (value is byte[,])
			{
				byte[,] matrix = (byte[,])value;

				for (int y = 0; y < matrix.GetLength(1); y++)
				{
					for (int x = 0; x < matrix.GetLength(0); x++)
					{
						if (x > 0)
						{
							output.Write("\t");
						}
						output.Write(matrix[x, y].ToString());
					}
					output.WriteLine();
				}
			}
			else if (value is char)
			{
				output.WriteLine(value.ToString());
			}
			else if (value is char[])
			{
				char[] array = (char[])value;

				for (int i = 0; i < array.Length; i++)
				{
					if (i > 0)
					{
						output.Write(" ");
					}
					output.Write(array[i].ToString());
				}
				output.WriteLine();
			}
			else if (value is char[,])
			{
				char[,] matrix = (char[,])value;

				for (int y = 0; y < matrix.GetLength(1); y++)
				{
					for (int x = 0; x < matrix.GetLength(0); x++)
					{
						if (x > 0)
						{
							output.Write("\t");
						}
						output.Write(matrix[x, y].ToString());
					}
					output.WriteLine();
				}
			}
			else if (value is short)
			{
				output.WriteLine(value.ToString());
			}
			else if (value is short[])
			{
				short[] array = (short[])value;

				for (int i = 0; i < array.Length; i++)
				{
					if (i > 0)
					{
						output.Write(" ");
					}
					output.Write(array[i].ToString());
				}
				output.WriteLine();
			}
			else if (value is short[,])
			{
				short[,] matrix = (short[,])value;

				for (int y = 0; y < matrix.GetLength(1); y++)
				{
					for (int x = 0; x < matrix.GetLength(0); x++)
					{
						if (x > 0)
						{
							output.Write("\t");
						}
						output.Write(matrix[x, y].ToString());
					}
					output.WriteLine();
				}
			}
			else if (value is int)
			{
				output.WriteLine(value.ToString());
			}
			else if (value is int[])
			{
				int[] array = (int[])value;

				for (int i = 0; i < array.Length; i++)
				{
					if (i > 0)
					{
						output.Write(" ");
					}
					output.Write(array[i].ToString());
				}
				output.WriteLine();
			}
			else if (value is int[,])
			{
				int[,] matrix = (int[,])value;

				for (int y = 0; y < matrix.GetLength(1); y++)
				{
					for (int x = 0; x < matrix.GetLength(0); x++)
					{
						if (x > 0)
						{
							output.Write("\t");
						}
						output.Write(matrix[x, y].ToString());
					}
					output.WriteLine();
				}
			}
			else if (value is long)
			{
				output.WriteLine(value.ToString());
			}
			else if (value is long[])
			{
				long[] array = (long[])value;

				for (int i = 0; i < array.Length; i++)
				{
					if (i > 0)
					{
						output.Write(" ");
					}
					output.Write(array[i].ToString());
				}
				output.WriteLine();
			}
			else if (value is long[,])
			{
				long[,] matrix = (long[,])value;

				for (int y = 0; y < matrix.GetLength(1); y++)
				{
					for (int x = 0; x < matrix.GetLength(0); x++)
					{
						if (x > 0)
						{
							output.Write("\t");
						}
						output.Write(matrix[x, y].ToString());
					}
					output.WriteLine();
				}
			}
			else if (value is float)
			{
				output.WriteLine(value.ToString());
			}
			else if (value is float[])
			{
				float[] array = (float[])value;

				for (int i = 0; i < array.Length; i++)
				{
					if (i > 0)
					{
						output.Write(" ");
					}
					output.Write(array[i].ToString());
				}
				output.WriteLine();
			}
			else if (value is float[,])
			{
				float[,] matrix = (float[,])value;

				for (int y = 0; y < matrix.GetLength(1); y++)
				{
					for (int x = 0; x < matrix.GetLength(0); x++)
					{
						if (x > 0)
						{
							output.Write("\t");
						}
						output.Write(matrix[x, y].ToString());
					}
					output.WriteLine();
				}
			}
			else if (value is double)
			{
				output.WriteLine(value.ToString());
			}
			else if (value is double[])
			{
				double[] array = (double[])value;

				for (int i = 0; i < array.Length; i++)
				{
					if (i > 0)
					{
						output.Write(" ");
					}
					output.Write(array[i].ToString());
				}
				output.WriteLine();
			}
			else if (value is double[,])
			{
				double[,] matrix = (double[,])value;

				for (int y = 0; y < matrix.GetLength(1); y++)
				{
					for (int x = 0; x < matrix.GetLength(0); x++)
					{
						if (x > 0)
						{
							output.Write("\t");
						}
						output.Write(matrix[x, y].ToString());
					}
					output.WriteLine();
				}
			}
			else if (value is decimal)
			{
				output.WriteLine(value.ToString());
			}
			else if (value is decimal[])
			{
				decimal[] array = (decimal[])value;

				for (int i = 0; i < array.Length; i++)
				{
					if (i > 0)
					{
						output.Write(" ");
					}
					output.Write(array[i].ToString());
				}
				output.WriteLine();
			}
			else if (value is decimal[,])
			{
				decimal[,] matrix = (decimal[,])value;

				for (int y = 0; y < matrix.GetLength(1); y++)
				{
					for (int x = 0; x < matrix.GetLength(0); x++)
					{
						if (x > 0)
						{
							output.Write("\t");
						}
						output.Write(matrix[x, y].ToString());
					}
					output.WriteLine();
				}
			}
			else if (value is string)
			{
				output.WriteLine((string)value);
			}
			else if (value is string[])
			{
				string[] array = (string[])value;

				for (int i = 0; i < array.Length; i++)
				{
					if (i > 0)
					{
						output.Write(" ");
					}
					output.Write(array[i]);
				}
				output.WriteLine();
			}
			else if (value is string[,])
			{
				string[,] matrix = (string[,])value;

				for (int y = 0; y < matrix.GetLength(1); y++)
				{
					for (int x = 0; x < matrix.GetLength(0); x++)
					{
						if (x > 0)
						{
							output.Write("\t");
						}
						if (matrix[x, y] == null)
						{
							output.Write("");
						}
						else
						{
							output.Write(matrix[x, y]);
						}
					}
					output.WriteLine();
				}
			}
			else if (value is System.DateTime)
			{
				output.WriteLine(value.ToString());
			}
			else if (value is System.Guid)
			{
				output.WriteLine(value.ToString());
			}
			else if (value is System.Type)
			{
				System.Type type = (System.Type)value;
				output.WriteLine(type.FullName);
			}
			else if (value is System.IO.Stream)
			{
				System.IO.Stream stream = (System.IO.Stream)value;

				long position = stream.Position;
				stream.Position = 0;

				System.IO.StreamReader reader = new System.IO.StreamReader(stream);
				output.WriteLine(reader.ReadToEnd());

				stream.Position = position;
			}
			else if (value is Water.Operator)
			{
				Water.Operator op = (Water.Operator)value;

				output.WriteLine(op.ToString());
			}
			else if (value is object[])
			{
				object[] array = (object[])value;

				for (int i = 0; i < array.Length; i++)
				{
					if (i > 0)
					{
						output.Write(" ");
					}
					output.Write(array[i].ToString());
				}
				output.WriteLine();
			}
			else if (value is object[,])
			{
				object[,] matrix = (object[,])value;

				for (int y = 0; y < matrix.GetLength(1); y++)
				{
					for (int x = 0; x < matrix.GetLength(0); x++)
					{
						if (x > 0)
						{
							output.Write("\t");
						}
						if (matrix[x, y] == null)
						{
							output.Write("null");
						}
						else
						{
							output.Write(matrix[x, y].ToString());
						}
					}
					output.WriteLine();
				}
			}
			else
			{
				output.WriteLine(Water.Generator.Generate(value, "\t", "\n"));
			}
		}

		public static void PrintError(System.Exception exception)
		{
			PrintError(exception, Water.Environment.Output);
		}

		public static void PrintError(System.Exception exception, System.IO.TextWriter output)
		{
			Water.List statements = new Water.List();
			for (int i = (Water.Environment.Variables.Count - 1); i >= 0; i--)
			{
				Water.Dictionary frame = (Water.Dictionary)Water.Environment.Variables[i];
				if (frame.IsDefined("_Statement"))
				{
					statements.Add(frame["_Statement"]);
				}
			}

			PrintError(exception, statements, output);
		}

		private static void PrintError(System.Exception exception, Water.List statements, System.IO.TextWriter output)
		{
			if (exception is Water.Error)
			{
				PrintMessage(exception, output);
				PrintStatementStack(statements, output);
			}
			else if (exception is System.Reflection.TargetInvocationException)
			{
				PrintError(exception.InnerException, statements, output);
			}
			else
			{
				PrintMessage(exception, output);
				PrintStatementStack(statements, output);
				PrintStackTrace(exception, output);

				if (exception.InnerException != null)
				{
					PrintMessage(exception.InnerException, output);
					PrintStackTrace(exception.InnerException, output);
				}
			}
		}

		private static void PrintMessage(System.Exception exception, System.IO.TextWriter output)
		{
			output.WriteLine();
			output.WriteLine("[ERROR]");
			output.WriteLine();
			output.WriteLine(exception.Message);
			output.WriteLine();
			output.WriteLine();
		}

		private static void PrintStatementStack(Water.List statements, System.IO.TextWriter output)
		{
			output.WriteLine();
			output.WriteLine("[STACK_TRACE]");

			if (statements.Count > 0)
			{
				for (int j = 0; j < statements.Count; j++)
				{
					output.WriteLine();

					Water.Statement statement = (Water.Statement)statements[j];

					if (statement != null && statement.File != null)
					{
						output.Write("[" + new System.IO.FileInfo((string)statement.File).Name + " (" + statement.Line + "," + statement.Column + ")]");
						output.Write("   ");
					}

					if (Water.StatementParser.GetBlock(statement.Expression) != null)
					{
						output.Write(Water.Generator.Generate(statement.Expression.NotLast()));
					}
					else
					{
						output.Write(Water.Generator.Generate(statement.Expression));
					}

					output.WriteLine();
				}
			}

			output.WriteLine();
		}

		private static void PrintStackTrace(System.Exception exception, System.IO.TextWriter output)
		{
			if (exception.StackTrace != null)
			{
				output.WriteLine();
				output.WriteLine("[NATIVE_STACK_TRACE]");
				output.WriteLine();
				output.WriteLine(exception.StackTrace);
				output.WriteLine();
				output.WriteLine();
			}
		}

		private Printer()
		{
		}

	}
}
