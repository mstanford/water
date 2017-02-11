// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005 Swampware, Inc.
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

namespace Bamboo.VisualStudio.VisualStudio2003.Solution
{
	/// <summary>
	/// Summary description for SolutionWriter.
	/// </summary>
	public class SolutionWriter
	{

		/// <summary>
		/// Writes a solution to a file.
		/// </summary>
		/// <param name="solution"></param>
		/// <param name="filename"></param>
		public static void Write(Models.Solution solution, System.IO.TextWriter writer)
		{
			writer.WriteLine("Microsoft Visual Studio Solution File, Format Version 8.00");
			foreach(Models.Project project in solution.Projects)
			{
				writer.WriteLine("Project(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\") = \"" + project.Name + "\", \"" + project.Path + "\", \"{" + project.Guid.ToString().ToUpper() + "}\"");
				writer.WriteLine("	ProjectSection(ProjectDependencies) = postProject");
				writer.WriteLine("	EndProjectSection");
				writer.WriteLine("EndProject");
			}
			writer.WriteLine("Global");
			writer.WriteLine("	GlobalSection(SolutionConfiguration) = preSolution");
			writer.WriteLine("		Debug = Debug");
			writer.WriteLine("		Release = Release");
			writer.WriteLine("	EndGlobalSection");
			writer.WriteLine("	GlobalSection(ProjectConfiguration) = postSolution");
			foreach(Models.Project project in solution.Projects)
			{
				writer.WriteLine("		{" + project.Guid.ToString().ToUpper() + "}.Debug.ActiveCfg = Debug|.NET");
				writer.WriteLine("		{" + project.Guid.ToString().ToUpper() + "}.Debug.Build.0 = Debug|.NET");
				writer.WriteLine("		{" + project.Guid.ToString().ToUpper() + "}.Release.ActiveCfg = Release|.NET");
				writer.WriteLine("		{" + project.Guid.ToString().ToUpper() + "}.Release.Build.0 = Release|.NET");
			}
			writer.WriteLine("	EndGlobalSection");
			writer.WriteLine("	GlobalSection(ExtensibilityGlobals) = postSolution");
			writer.WriteLine("	EndGlobalSection");
			writer.WriteLine("	GlobalSection(ExtensibilityAddIns) = postSolution");
			writer.WriteLine("	EndGlobalSection");
			writer.WriteLine("EndGlobal");

			writer.Flush();
		}

		private SolutionWriter()
		{
		}

	}
}
