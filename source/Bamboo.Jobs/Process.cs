// ------------------------------------------------------------------------------
// 
// Copyright (c) 2009 Swampware, Inc.
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
using System.Text;

namespace Bamboo.Jobs
{
	public class Process
	{

		public static void Execute(string command, string args, System.IO.TextWriter outputWriter, System.IO.TextWriter errorWriter)
		{
			Execute(command, args, outputWriter, errorWriter);
		}

		public static void Execute(string command, string args, string workingDirectory, System.IO.TextWriter outputWriter, System.IO.TextWriter errorWriter)
		{
			System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo(command, args);
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.UseShellExecute = false;
			if (workingDirectory != null)
			{
				processStartInfo.WorkingDirectory = workingDirectory;
			}
			System.Diagnostics.Process process = System.Diagnostics.Process.Start(processStartInfo);
			while (!process.HasExited)
			{
				outputWriter.Write(process.StandardOutput.ReadToEnd());
				errorWriter.Write(process.StandardError.ReadToEnd());
				process.WaitForExit(50);
			}
			outputWriter.Write(process.StandardOutput.ReadToEnd());
			errorWriter.Write(process.StandardError.ReadToEnd());
			process.Close();
			process.Dispose();
			outputWriter.Flush();
			errorWriter.Flush();
		}

	}
}
