// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005-2006 Swampware, Inc.
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

namespace Bamboo.CSharp.Compilers.CSharpLatest
{
	/// <summary>
	/// Summary description for CSharpLatestCompiler.
	/// </summary>
	public class CSharpLatestCompiler : Bamboo.CSharp.Compilers.Compiler
	{
		private Bamboo.CSharp.Compilers.Compiler _compiler;

		public CSharpLatestCompiler()
		{
			if (Bamboo.CSharp.FrameworkDetector.IsInstalled("2.0"))
			{
				this._compiler = new Bamboo.CSharp.Compilers.CSharp20.CSharp20Compiler();
			}
			else if (Bamboo.CSharp.FrameworkDetector.IsInstalled("1.1"))
			{
				this._compiler = new Bamboo.CSharp.Compilers.CSharp11.CSharp11Compiler();
			}
			else
			{
				throw new System.Exception("No supported .NET version installed.");
			}
		}

		public override string GetFrameworkVersion()
		{
			return this._compiler.GetFrameworkVersion();
		}

		public override string GetFrameworkPath()
		{
			return this._compiler.GetFrameworkPath();
		}

		public override void Compile(CompilerParameters parameters)
		{
			this._compiler.Compile(parameters);
		}

	}
}
