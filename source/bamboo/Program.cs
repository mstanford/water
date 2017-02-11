// ------------------------------------------------------------------------------
// 
// Copyright (c) 2008 Swampware, Inc.
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
using System.Collections.Generic;
using System.Windows.Forms;

namespace bamboo
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);



			System.AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
			System.AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			System.Reflection.Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
			string location = assembly.Location;
			int index = location.LastIndexOf(System.IO.Path.DirectorySeparatorChar);
			string startup_path = location.Substring(0, index + 1);
			string script_path = startup_path + @"scripts" + System.IO.Path.DirectorySeparatorChar;

			Water.Environment.DefineConstant("_StartupPath", startup_path);
			Water.AssemblyCache.AddAssemblyFolder(startup_path);
			Water.AssemblyCache.Add("Water", typeof(Water.Operator).Assembly);
			Water.AssemblyCache.Add("bamboo", typeof(bamboo.Program).Assembly);

			Water.Environment.DefineConstant("load-block", new Water.Operators.LoadBlockOperator());
			Water.Environment.DefineConstant("load-method", new Water.Operators.LoadMethodOperator());
			Water.Environment.DefineConstant("load-operator", new Water.Operators.LoadOperatorOperator());

			Water.Environment.DefineConstant("include", new Water.Method(typeof(Water.Instructions), "Include"));
			Water.Environment.DefineConstant("print", new Water.Method(typeof(Water.Instructions), "Print"));

			Water.Environment.DefineConstant("exec", new Water.Method(typeof(Water.Instructions), "Exec"));
			Water.Environment.DefineConstant("spawn", new Water.Method(typeof(Water.Instructions), "Spawn"));

			//Push a stack frame
			Water.Environment.Push();

			LoadSettings(startup_path + "bamboo.settings");

			FormMain formMain = new FormMain();
			Water.Environment.DefineConstant("Workspace", formMain);

			try
			{
				Water.Instructions.Include(script_path + "startup.water");
				Water.Instructions.Include(script_path + "bamboo.water");
			}
			catch (System.Exception exception)
			{
				System.IO.StringWriter writer = new System.IO.StringWriter();
				Water.Printer.PrintError(exception, writer);
				System.Windows.Forms.MessageBox.Show(writer.ToString());
			}

			foreach (string arg in args)
			{
				if (Water.Environment.IsConstant("File.Open"))
				{
					Water.Evaluator.Apply("File.Open", arg);
				}
			}
			Application.Run(formMain);

			SaveSettings(startup_path + "bamboo.settings");
		}

		private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			string name = args.Name;
			int index = name.IndexOf(",");
			if (index != -1)
			{
				name = name.Substring(0, index);
			}
			return Water.AssemblyCache.LoadAssembly(name);
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
		{
			System.Exception exception = (System.Exception)args.ExceptionObject;
			System.Windows.Forms.MessageBox.Show(exception.Message + "\n\n" + exception.StackTrace);
		}

		private static void LoadSettings(string path)
		{
			if (!System.IO.File.Exists(path))
			{
				Water.Environment.DefineConstant("Settings", new Water.Dictionary());
			}
			else
			{
				Water.TextReader reader = new Water.TextReader(new System.IO.StreamReader(path));
				Water.Dictionary Settings = (Water.Dictionary)Water.Parser.Parse(reader);
				Water.Environment.DefineConstant("Settings", Settings);
				reader.Close();
			}
		}

		private static void SaveSettings(string path)
		{
			Water.Dictionary Settings = (Water.Dictionary)Water.Environment.Identify("Settings");
			System.IO.StreamWriter writer = new System.IO.StreamWriter(path);
			Water.Generator.Generate(Settings, writer);
			writer.Flush();
			writer.Close();
		}

	}
}