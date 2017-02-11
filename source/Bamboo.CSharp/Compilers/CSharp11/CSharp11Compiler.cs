// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005-2008 Swampware, Inc.
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

namespace Bamboo.CSharp.Compilers.CSharp11
{
	/// <summary>
	/// Summary description for CSharp11Compiler.
	/// </summary>
	public class CSharp11Compiler : Bamboo.CSharp.Compilers.Compiler
	{

		public CSharp11Compiler()
		{
		}

		public override string GetFrameworkVersion()
		{
			return "1.1";
		}

		public override string GetFrameworkPath()
		{
			return FrameworkDetector.GetFrameworkPath(this.GetFrameworkVersion());
		}

		public override void Compile(CompilerParameters parameters)
		{
			string CSC_EXE = this.GetFrameworkPath() + @"csc.exe";

			if(CSC_EXE == null)
			{
				this.OnCompilerOutput(this, new Bamboo.CSharp.Compilers.CompilerOutputEventArgs("The .NET 1.1 Framework is not installed."));
				this.OnCompileFailed(this, System.EventArgs.Empty);
				return;
			}
			
			this.OnCompilerDaignosticStatus(this, new CompilerDiagnosticStatusEventArgs("CSC_EXE:" + CSC_EXE));

			System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();
			processStartInfo.FileName = CSC_EXE;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.CreateNoWindow = true;
			processStartInfo.WorkingDirectory = System.Environment.CurrentDirectory;



			string arguments = String.Empty;

			arguments += " /warn:4";

			if(parameters.ApplicationIcon.Length > 0)
			{
				arguments += " /win32icon:\"" + parameters.ApplicationIcon + "\"";
			}

			arguments += " /out:\"" + parameters.Assembly + "\"";

			arguments += " /target:" + parameters.Target;

			arguments += " /D:NET11";
			if (parameters.Define.Length > 0)
			{
				arguments += " /D:" + parameters.Define;
			}

			if(parameters.Doc.Length > 0)
			{
				arguments += " /doc:\"" + parameters.Doc + "\"";
			}

			if(parameters.Lib.Length > 0)
			{
				string argument = "/lib:\"" + parameters.Lib + "\"";
				this.OnCompilerDaignosticStatus(this, new CompilerDiagnosticStatusEventArgs("SET_LIB:" + argument));
				arguments += " " + argument;
			}

			foreach(string reference in parameters.References)
			{
				string argument = "/R:\"" + reference + "\"";
				this.OnCompilerDaignosticStatus(this, new CompilerDiagnosticStatusEventArgs("ADD_REFERENCE:" + argument));
				arguments += " " + argument;
			}

			foreach(string resource in parameters.Resources)
			{
				string argument = "/resource:" + resource;
				this.OnCompilerDaignosticStatus(this, new CompilerDiagnosticStatusEventArgs("ADD_RESOURCE:" + argument));
				arguments += " " + argument;
			}

			if(parameters.Debug)
			{
				arguments += " /debug+";
				arguments += " /optimize-";
			}
			else
			{
				arguments += " /debug-";
				arguments += " /optimize+";
			}



			foreach(string source in parameters.Sources)
			{
				string argument = source;
				this.OnCompilerDaignosticStatus(this, new CompilerDiagnosticStatusEventArgs("ADD_SOURCE:" + argument));
				arguments += " " + argument;
			}


			processStartInfo.Arguments = arguments;
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



			bool hasErrors = false;

			string filename_pattern = @"([^(]+)";
			string goto_pattern = filename_pattern + @"(\(([0-9]+),([0-9]+)\))?: ";
			string error_pattern = @"(fatal error|error|warning) (CS[0-9]{4}): (.*)";
			string pattern = @"^(" + goto_pattern + @")?" + error_pattern + "$";

			System.Text.RegularExpressions.Regex errorRegex = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.Compiled);

			System.IO.StringReader stringReader = new System.IO.StringReader(stringWriter.ToString());
			while(-1 != stringReader.Peek())
			{
				string output = stringReader.ReadLine();

				System.Text.RegularExpressions.MatchCollection matches = errorRegex.Matches(output);
				if(matches.Count != 0)
				{
					System.Text.RegularExpressions.Match match = matches[0];
					string error_file = match.Groups[2].Value;
					int error_line = (match.Groups[4].Value == "") ? 0 : Int32.Parse(match.Groups[4].Value);
					int error_column = (match.Groups[5].Value == "") ? 0 : Int32.Parse(match.Groups[5].Value);
					string error_code = match.Groups[7].Value;
					string error_description = match.Groups[8].Value;

					if( match.Groups[6].Value == "error" )
					{
						Bamboo.CSharp.Compilers.Error error = new Bamboo.CSharp.Compilers.Error(false, error_file, error_line, error_column, error_code, error_description, output);
						parameters.Errors.Add(error);

						this.OnCompilerError(this, new Bamboo.CSharp.Compilers.CompilerErrorEventArgs(error));
						hasErrors = true;
					}
					else if( match.Groups[6].Value == "warning" )
					{
						Bamboo.CSharp.Compilers.Error warning = new Bamboo.CSharp.Compilers.Error(true, error_file, error_line, error_column, error_code, error_description, output);
						parameters.Errors.Add(warning);

						this.OnCompilerError(this, new Bamboo.CSharp.Compilers.CompilerErrorEventArgs(warning));
					}
				}



				parameters.Output.Add(output);

				this.OnCompilerOutput(this, new Bamboo.CSharp.Compilers.CompilerOutputEventArgs(output));
			}



			if(hasErrors)
			{
				this.OnCompileFailed(this, System.EventArgs.Empty);
			}
			else
			{
				this.OnCompileSucceeded(this, System.EventArgs.Empty);
			}

		}

	}
}
