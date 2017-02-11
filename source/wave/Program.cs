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

namespace wave
{
	/// <summary>
	/// Summary description for Program.
	/// </summary>
	class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			System.Console.WriteLine("wave - Interactive Water Console");
			System.Console.WriteLine("Copyright (c) 2006-2008 Swampware, Inc.");
			System.Console.WriteLine("For more information visit http://www.swampware.com");
			System.Console.WriteLine("");

			System.AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
			System.AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			System.Reflection.Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
			string location = assembly.Location;
			int index = location.LastIndexOf(System.IO.Path.DirectorySeparatorChar);
			string startup_path = location.Substring(0, index + 1);
			string script_path = startup_path + @"scripts" + System.IO.Path.DirectorySeparatorChar;

			while (true)
			{
				Water.Environment.DefineConstant("_StartupPath", startup_path);
				Water.AssemblyCache.AddAssemblyFolder(startup_path);
				Water.AssemblyCache.Add("Water", typeof(Water.Operator).Assembly);

				Water.Environment.DefineConstant("load-block", new Water.Operators.LoadBlockOperator());
				Water.Environment.DefineConstant("load-method", new Water.Operators.LoadMethodOperator());
				Water.Environment.DefineConstant("load-operator", new Water.Operators.LoadOperatorOperator());

				Water.Environment.DefineConstant("include", new Water.Method(typeof(Water.Instructions), "Include"));
				Water.Environment.DefineConstant("print", new Water.Method(typeof(Water.Printer), "Print"));

				Water.Environment.DefineConstant("exit", new Water.Method(typeof(Water.Instructions), "Exit"));
				Water.Environment.DefineConstant("restart", new Water.Method(typeof(Water.Instructions), "Restart"));

				Water.Environment.DefineConstant("exec", new Water.Method(typeof(Water.Instructions), "Exec"));
				Water.Environment.DefineConstant("spawn", new Water.Method(typeof(Water.Instructions), "Spawn"));

				//Push a stack frame
				Water.Environment.Push();

				try
				{
					if (System.IO.File.Exists(script_path + "startup.water"))
					{
						Water.Instructions.Include(script_path + "startup.water");
					}

					foreach (string arg in args)
					{
						// Run file.
						Water.Interpreter.Interpret(new Water.Iterator(new Water.FileReader(arg)));
					}
				}
				catch (System.Exception exception)
				{
					Water.Printer.PrintError(exception);
				}


				while(!Water.Environment.IsConstant("_Exit") &&
					  !Water.Environment.IsConstant("_Restart"))
				{
					try
					{
						// Run console.
						Water.Interpreter.Interpret(new wave.ConsoleIterator());
					}
					catch (System.Exception exception)
					{
						Water.Printer.PrintError(exception);
					}
				}


				if (Water.Environment.IsConstant("_Exit"))
				{
					break;
				}
				else if (Water.Environment.IsConstant("_Restart"))
				{
					System.Console.WriteLine("restarting...");
					System.Console.WriteLine("");
				}
				else
				{
					throw new Water.Error("UNKNOWN RESTART STATE.");
				}


				Water.Environment.Reset();
				Water.AssemblyCache.Clear();
			}
		}

		private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			string name = args.Name;
			int index = name.IndexOf(",");
			if(index != -1)
			{
				name = name.Substring(0, index);
			}
			return Water.AssemblyCache.LoadAssembly(name);
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
		{
			System.Exception exception = (System.Exception)args.ExceptionObject;
			System.Console.Write(exception.Message + "\n\n" + exception.StackTrace);
		}

	}
}
