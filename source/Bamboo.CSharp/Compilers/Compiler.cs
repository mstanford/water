// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006 Swampware, Inc.
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

namespace Bamboo.CSharp.Compilers
{
	/// <summary>
	/// Summary description for Compiler.
	/// </summary>
	public abstract class Compiler
	{

		public Compiler()
		{
		}

		public abstract string GetFrameworkVersion();
		public abstract string GetFrameworkPath();

		public abstract void Compile(CompilerParameters parameters);

		public event CompilerDiagnosticStatusEventHandler CompilerDiagnosticStatus;

		protected void OnCompilerDaignosticStatus(object sender, CompilerDiagnosticStatusEventArgs e)
		{
			if(CompilerDiagnosticStatus != null)
			{
				CompilerDiagnosticStatus(sender, e);
			}
		}

		public event CompilerOutputEventHandler CompilerOutput;

		protected void OnCompilerOutput(object sender, CompilerOutputEventArgs e)
		{
			if(CompilerOutput != null)
			{
				CompilerOutput(sender, e);
			}
		}

		public event CompilerErrorEventHandler CompilerError;

		protected void OnCompilerError(object sender, CompilerErrorEventArgs e)
		{
			if(CompilerError != null)
			{
				CompilerError(sender, e);
			}
		}

		public event System.EventHandler CompileSucceeded;

		protected void OnCompileSucceeded(object sender, System.EventArgs e)
		{
			if(CompileSucceeded != null)
			{
				CompileSucceeded(sender, e);
			}
		}

		public event System.EventHandler CompileFailed;

		protected void OnCompileFailed(object sender, System.EventArgs e)
		{
			if(CompileFailed != null)
			{
				CompileFailed(sender, e);
			}
		}

	}
}
