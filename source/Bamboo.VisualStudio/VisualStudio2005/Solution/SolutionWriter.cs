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

namespace Bamboo.VisualStudio.VisualStudio2005.Solution
{
	/// <summary>
	/// Summary description for SolutionWriter.
	/// </summary>
	public class SolutionWriter
	{

		public static void Write(Models.Solution solution, System.IO.TextWriter writer)
		{
			if(!writer.Encoding.Equals(System.Text.Encoding.UTF8))
			{
				throw new System.Exception("Solution writer requires UTF8 encoding.");
			}
			System.Text.UTF8Encoding encoding = (System.Text.UTF8Encoding)writer.Encoding;
			byte[] bom = encoding.GetPreamble();
			if(bom.Length != 3 || bom[0] != 239 || bom[1] != 187 || bom[2] != 191)
			{
				throw new System.Exception("Solution writer requires UTF8 encoding with a Byte Order Mark.");
			}

			writer.WriteLine("");
			writer.WriteLine("Microsoft Visual Studio Solution File, Format Version 9.00");
			writer.WriteLine("# Visual Studio 2005");
			foreach(Models.Project project in solution.Projects)
			{
				writer.WriteLine("Project(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\") = \"" + project.Name + "\", \"" + project.Path + "\", \"{" + project.Guid.ToString().ToUpper() + "}\"");
				writer.WriteLine("EndProject");
			}
			writer.WriteLine("Global");
			writer.WriteLine("	GlobalSection(SolutionConfigurationPlatforms) = preSolution");
			writer.WriteLine("		Debug|Any CPU = Debug|Any CPU");
			writer.WriteLine("		Release|Any CPU = Release|Any CPU");
			writer.WriteLine("	EndGlobalSection");
			writer.WriteLine("	GlobalSection(ProjectConfigurationPlatforms) = postSolution");
			foreach(Models.Project project in solution.Projects)
			{
				writer.WriteLine("		{" + project.Guid.ToString().ToUpper() + "}.Debug|Any CPU.ActiveCfg = Debug|Any CPU");
				writer.WriteLine("		{" + project.Guid.ToString().ToUpper() + "}.Debug|Any CPU.Build.0 = Debug|Any CPU");
				writer.WriteLine("		{" + project.Guid.ToString().ToUpper() + "}.Release|Any CPU.ActiveCfg = Release|Any CPU");
				writer.WriteLine("		{" + project.Guid.ToString().ToUpper() + "}.Release|Any CPU.Build.0 = Release|Any CPU");
			}
			writer.WriteLine("	EndGlobalSection");
			writer.WriteLine("	GlobalSection(SolutionProperties) = preSolution");
			writer.WriteLine("		HideSolutionNode = FALSE");
			writer.WriteLine("	EndGlobalSection");
			writer.WriteLine("EndGlobal");

			writer.Flush();
		}

		public SolutionWriter()
		{
		}

	}
}
