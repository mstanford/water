// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005-2007 Swampware, Inc.
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
	/// Summary description for SolutionBuilder.
	/// </summary>
	public class SolutionBuilder
	{
		private ProjectBuildTracker _projectBuildTracker = new ProjectBuildTracker();
		private Bamboo.VisualStudio.VisualStudio2003.CSharpProject.ProjectBuilder _csharpProjectBuilder;
		private bool _succeeded = true;

		public SolutionBuilder(Bamboo.VisualStudio.VisualStudio2003.CSharpProject.ProjectBuilder csharpProjectBuilder)
		{
			this._csharpProjectBuilder = csharpProjectBuilder;
		}

		public bool Succeeded
		{
			get { return this._succeeded; }
		}

		public void Build(string filename, string configuration)
		{
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
			string name = fileInfo.Name.Substring( 0, fileInfo.Name.ToLower().LastIndexOf(".sln") );
			System.IO.TextReader reader = fileInfo.OpenText();
			Models.Solution solution = SolutionReader.Read(filename, reader);
			reader.Close();

			string projectFolder = LastLeft(filename, System.IO.Path.DirectorySeparatorChar.ToString());

			this.Build(solution, configuration, projectFolder, String.Empty);
		}

		public void Build(string filename, string configuration, string output)
		{
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
			string name = fileInfo.Name.Substring( 0, fileInfo.Name.ToLower().LastIndexOf(".sln") );
			System.IO.TextReader reader = fileInfo.OpenText();
			Models.Solution solution = SolutionReader.Read(filename, reader);
			reader.Close();

			string projectFolder = LastLeft(filename, System.IO.Path.DirectorySeparatorChar.ToString());

			if(!output.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
			{
				output += System.IO.Path.DirectorySeparatorChar;
			}

			this.Build(solution, configuration, projectFolder, output);
		}

		private void Build(Bamboo.VisualStudio.VisualStudio2003.Solution.Models.Solution solution, string configuration, string projectFolder, string output)
		{
			this._projectBuildTracker.Clear();

			System.Collections.Hashtable projectPaths = new System.Collections.Hashtable();

			Bamboo.VisualStudio.ProjectDependencies.ProjectCollection projectsToResolve = new Bamboo.VisualStudio.ProjectDependencies.ProjectCollection();
			foreach(Bamboo.VisualStudio.VisualStudio2003.Solution.Models.Project project in solution.Projects)
			{
				if(project.Path.ToLower().EndsWith(".csproj"))
				{
					string filename = projectFolder + System.IO.Path.DirectorySeparatorChar + project.Path;

					System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
					string name = fileInfo.Name.Substring( 0, fileInfo.Name.ToLower().LastIndexOf(".csproj") );
					System.IO.TextReader reader = fileInfo.OpenText();
					Bamboo.VisualStudio.VisualStudio2003.CSharpProject.Models.Project csproj2003 = Bamboo.VisualStudio.VisualStudio2003.CSharpProject.ProjectReader.Read(name, reader);
					reader.Close();


					projectPaths.Add(csproj2003.ProjectGuid, filename);


					Bamboo.VisualStudio.ProjectDependencies.Project projectToResolve = new Bamboo.VisualStudio.ProjectDependencies.Project();
					projectToResolve.Name = csproj2003.Name;
					projectToResolve.Guid = csproj2003.ProjectGuid;
					projectToResolve.Tag = csproj2003;
					foreach(Bamboo.VisualStudio.VisualStudio2003.CSharpProject.Models.Reference projectReference in csproj2003.References)
					{
						if(projectReference.Project.Length > 0)
						{
							Bamboo.VisualStudio.ProjectDependencies.ProjectDependency projectDependency = new Bamboo.VisualStudio.ProjectDependencies.ProjectDependency();
							projectDependency.Name = projectReference.Name;
							projectDependency.ProjectGuid = new System.Guid(projectReference.Project);
							projectToResolve.Dependencies.Add(projectDependency);
						}
					}
					projectsToResolve.Add(projectToResolve);
				}
				else
				{
					throw new System.Exception("Unsupported project type.");
				}
			}

			Bamboo.VisualStudio.ProjectDependencies.ProjectCollection resolvedProjects = Bamboo.VisualStudio.ProjectDependencies.ProjectDependencyResolver.Resolve(projectsToResolve);


			foreach(Bamboo.VisualStudio.ProjectDependencies.Project project in resolvedProjects)
			{
				if(project.Tag is Bamboo.VisualStudio.VisualStudio2003.CSharpProject.Models.Project)
				{
					Bamboo.VisualStudio.VisualStudio2003.CSharpProject.Models.Project csproj2003 = (Bamboo.VisualStudio.VisualStudio2003.CSharpProject.Models.Project)project.Tag;

					string projectPath = projectPaths[csproj2003.ProjectGuid].ToString();

					if(output.Length == 0)
					{
						this._csharpProjectBuilder.Build(projectPath, configuration, this._projectBuildTracker);
					}
					else
					{
						this._csharpProjectBuilder.Build(projectPath, configuration, output, this._projectBuildTracker);
					}

					if(this._csharpProjectBuilder.Succeeded)
					{
						this._projectBuildTracker.Add(csproj2003.ProjectGuid, this._csharpProjectBuilder.Assembly);
					}
					else
					{
						this._succeeded = false;
						return;
					}
				}
			}
		}

		private static string LastLeft(string input, string marker)
		{
			int index = input.LastIndexOf(marker);
			if(index == -1)
			{
				return input;
			}
			return input.Substring(0, index);
		}

	}
}
