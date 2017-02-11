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
	/// Summary description for Instructions.
	/// </summary>
	public class Instructions
	{

		#region Boolean

		public static bool And(bool a, bool b)
		{
			if (!a)
			{
				return false;
			}
			return (a && b);
		}

		public static bool Or(bool a, bool b)
		{
			if (a)
			{
				return true;
			}
			return (a || b);
		}

		public static bool Not(bool a)
		{
			return !a;
		}

		public static bool IsNull(object a)
		{
			return (a == null);
		}

		public static new bool Equals(object a, object b)
		{
			if (a == null)
			{
				return false;
			}
			if (b == null)
			{
				return false;
			}
			return a.Equals(b);
		}

		#endregion

		#region Equals

		public static bool AreEqual(object a, object b)
		{
			if (a is System.Collections.IDictionary)
			{
				if (!(b is System.Collections.IDictionary))
				{
					return false;
				}
				return AreEqual((System.Collections.IDictionary)a, (System.Collections.IDictionary)b);
			}
			else if (a is System.Collections.IEnumerable)
			{
				if (!(b is System.Collections.IEnumerable))
				{
					return false;
				}
				return AreEqual((System.Collections.IEnumerable)a, (System.Collections.IEnumerable)b);
			}
			else
			{
				return a.Equals(b);
			}
		}

		public static bool AreEqual(System.Collections.IDictionary a, System.Collections.IDictionary b)
		{
			if (a.Count != b.Count)
			{
				return false;
			}

			foreach (object key in a.Keys)
			{
				if (!b.Contains(key))
				{
					return false;
				}
				if (!AreEqual(a[key], b[key]))
				{
					return false;
				}
			}

			return true;
		}

		public static bool AreEqual(System.Collections.IEnumerable a, System.Collections.IEnumerable b)
		{
			System.Collections.ArrayList aa = new System.Collections.ArrayList();
			foreach (object item in a)
			{
				aa.Add(item);
			}

			System.Collections.ArrayList ab = new System.Collections.ArrayList();
			foreach (object item in b)
			{
				ab.Add(item);
			}

			if (aa.Count != ab.Count)
			{
				return false;
			}

			for (int i = 0; i < aa.Count; i++)
			{
				if (!AreEqual(aa[i], ab[i]))
				{
					return false;
				}
			}

			return true;
		}

		#endregion

		#region Error

		public static bool Error(string message)
		{
			throw new Water.Error(message);
		}

		#endregion

		#region Reflection

		public static bool IsDictionary(object o)
		{
			return ((o is System.Collections.IDictionary) || (o is Water.Dictionary));
		}

		public static bool IsList(object o)
		{
			return ((o is System.Collections.IEnumerable) && !(o is string));
		}

		public static bool IsString(object value)
		{
			return (value is string);
		}

		public static bool IsDecimal(object value)
		{
			return (value is decimal);
		}

		public static bool IsDouble(object value)
		{
			return (value is double);
		}

		public static bool IsInt(object value)
		{
			return (value is int);
		}

		#endregion

		#region Math

		#region Add

		public static int Add(int a, int b)
		{
			return a + b;
		}

		public static long Add(long a, long b)
		{
			return a + b;
		}

		public static decimal Add(decimal a, decimal b)
		{
			return a + b;
		}

		public static float Add(float a, float b)
		{
			return a + b;
		}

		public static double Add(double a, double b)
		{
			return a + b;
		}

		public static double Add(object a, object b)
		{
			return System.Convert.ToDouble(a) + System.Convert.ToDouble(b);
		}

		#endregion

		#region Subtract

		public static int Subtract(int a, int b)
		{
			return a - b;
		}

		public static long Subtract(long a, long b)
		{
			return a - b;
		}

		public static decimal Subtract(decimal a, decimal b)
		{
			return a - b;
		}

		public static float Subtract(float a, float b)
		{
			return a - b;
		}

		public static double Subtract(double a, double b)
		{
			return a - b;
		}

		public static double Subtract(object a, object b)
		{
			return System.Convert.ToDouble(a) - System.Convert.ToDouble(b);
		}

		#endregion

		#region Multiply

		public static int Multiply(int a, int b)
		{
			return a * b;
		}

		public static long Multiply(long a, long b)
		{
			return a * b;
		}

		public static decimal Multiply(decimal a, decimal b)
		{
			return a * b;
		}

		public static float Multiply(float a, float b)
		{
			return a * b;
		}

		public static double Multiply(double a, double b)
		{
			return a * b;
		}

		public static double Multiply(object a, object b)
		{
			return System.Convert.ToDouble(a) * System.Convert.ToDouble(b);
		}

		#endregion

		#region Divide

		public static int Divide(int a, int b)
		{
			return a / b;
		}

		public static long Divide(long a, long b)
		{
			return a / b;
		}

		public static int Remainder(int a, int b)
		{
			return a % b;
		}

		public static long Remainder(long a, long b)
		{
			return a % b;
		}

		public static decimal Divide(decimal a, decimal b)
		{
			return a / b;
		}

		public static float Divide(float a, float b)
		{
			return a / b;
		}

		public static double Divide(double a, double b)
		{
			return a / b;
		}

		public static double Divide(object a, object b)
		{
			return System.Convert.ToDouble(a) / System.Convert.ToDouble(b);
		}

		#endregion

		#region Square/SquareRoot

		public static long Square(int a)
		{
			return a * a;
		}

		public static long Square(long a)
		{
			return a * a;
		}

		public static decimal Square(decimal a)
		{
			return a * a;
		}

		public static double Square(float a)
		{
			return a * a;
		}

		public static double Square(double a)
		{
			return a * a;
		}

		public static double SquareRoot(int a)
		{
			return System.Math.Sqrt(a);
		}

		public static double SquareRoot(long a)
		{
			return System.Math.Sqrt(a);
		}

		public static double SquareRoot(decimal a)
		{
			return System.Math.Sqrt(System.Convert.ToDouble(a));
		}

		public static double SquareRoot(float a)
		{
			return System.Math.Sqrt(a);
		}

		public static double SquareRoot(double a)
		{
			return System.Math.Sqrt(a);
		}

		#endregion

		#region GreaterThan

		public static bool GreaterThan(int a, int b)
		{
			return (a > b);
		}

		public static bool GreaterThan(long a, long b)
		{
			return (a > b);
		}

		public static bool GreaterThan(decimal a, decimal b)
		{
			return (a > b);
		}

		public static bool GreaterThan(float a, float b)
		{
			return (a > b);
		}

		public static bool GreaterThan(double a, double b)
		{
			return (a > b);
		}

		#endregion

		#region LessThan

		public static bool LessThan(int a, int b)
		{
			return (a < b);
		}

		public static bool LessThan(long a, long b)
		{
			return (a < b);
		}

		public static bool LessThan(decimal a, decimal b)
		{
			return (a < b);
		}

		public static bool LessThan(float a, float b)
		{
			return (a < b);
		}

		public static bool LessThan(double a, double b)
		{
			return (a < b);
		}

		#endregion

		#region GreaterThanOrEquals

		public static bool GreaterThanOrEquals(int a, int b)
		{
			return (a >= b);
		}

		public static bool GreaterThanOrEquals(long a, long b)
		{
			return (a >= b);
		}

		public static bool GreaterThanOrEquals(decimal a, decimal b)
		{
			return (a >= b);
		}

		public static bool GreaterThanOrEquals(float a, float b)
		{
			return (a >= b);
		}

		public static bool GreaterThanOrEquals(double a, double b)
		{
			return (a >= b);
		}

		#endregion

		#region LessThanOrEquals

		public static bool LessThanOrEquals(int a, int b)
		{
			return (a <= b);
		}

		public static bool LessThanOrEquals(long a, long b)
		{
			return (a <= b);
		}

		public static bool LessThanOrEquals(decimal a, decimal b)
		{
			return (a <= b);
		}

		public static bool LessThanOrEquals(float a, float b)
		{
			return (a <= b);
		}

		public static bool LessThanOrEquals(double a, double b)
		{
			return (a <= b);
		}

		#endregion

		#endregion

		#region Diagnostics

		public static void Trace()
		{
			Water.Interpreter.Trace = true;
		}

		public static void NoTrace()
		{
			Water.Interpreter.Trace = false;
		}

		#endregion

		#region Water-Specific Instructions

		public static void Break()
		{
			Water.Environment.Break = true;
		}

		public static void Return()
		{
			Water.Environment.Return = true;
			Water.Environment.ReturnValue = null;
		}

		public static void Return(object value)
		{
			Water.Environment.Return = true;
			Water.Environment.ReturnValue = value;
		}

		public static void Exit()
		{
			Water.Environment.DefineConstant("_Exit", "_Exit");
		}

		public static void Include(string filename)
		{
			string path = Water.FileReader.ResolvePath(filename);

			Water.Dictionary includes = Water.Environment.Includes;
			if(includes[path] == null)
			{
				includes.Add(path, path);



				Water.FileReader reader = null;
				try
				{
					reader = new Water.FileReader(path);

					Water.Interpreter.Interpret(new Water.Iterator(reader));
				}
				finally
				{
					if (reader != null)
					{
						reader.Close();
					}
				}
			}
		}

		public static void Restart()
		{
			Water.Environment.DefineConstant("_Restart", "_Restart");
		}

		#endregion

		#region Processes

		public static string Exec(string filename)
		{
			return Exec(filename, "");
		}

		public static string Exec(string filename, string args)
		{
			System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();
			processStartInfo.FileName = filename;
			if(args.Length > 0)
			{
				// The user is responsible for wrapping args in double-quotes if necessary.
				processStartInfo.Arguments = args;
			}
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.WorkingDirectory = System.Environment.CurrentDirectory;

			System.Diagnostics.Process process = System.Diagnostics.Process.Start(processStartInfo);
			System.IO.StringWriter stringWriter = new System.IO.StringWriter();
			while(!process.HasExited)
			{
				stringWriter.Write(process.StandardOutput.ReadToEnd());
				process.WaitForExit(50);
			}
			stringWriter.Write(process.StandardOutput.ReadToEnd());
			process.Close();
			process.Dispose();

			return stringWriter.ToString();
		}

		public static void Spawn(string filename)
		{
			Spawn(filename, "");
		}

		public static void Spawn(string filename, string args)
		{
			System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();
			processStartInfo.FileName = filename;
			if(args.Length > 0)
			{
				// The user is responsible for wrapping args in double-quotes if necessary.
				processStartInfo.Arguments = args;
			}
			processStartInfo.WorkingDirectory = System.Environment.CurrentDirectory;

			System.Diagnostics.Process process = System.Diagnostics.Process.Start(processStartInfo);
			if(process != null)
			{
				process.Close();
				process.Dispose();
			}
		}

		#endregion

		private Instructions()
		{
		}

	}
}
