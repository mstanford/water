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

namespace Bamboo.VisualStudio.VisualStudio2008.Solution
{
	/// <summary>
	/// Summary description for SolutionReader.
	/// </summary>
	public class SolutionReader
	{

		public static Models.Solution Read(string filename)
		{
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
			string name = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
			System.IO.TextReader reader = System.IO.File.OpenText(filename);
			Models.Solution solution = Read(name, reader);
			reader.Close();
			return solution;
		}

		public static Models.Solution Read(string name, System.IO.TextReader reader)
		{
			Models.Solution solution = new Models.Solution();

			solution.Name = name;

			reader.ReadLine();
			while(reader.Peek() != -1)
			{
				string line = reader.ReadLine();

				if(line.StartsWith("Project"))
				{
					Models.Project project = LoadProject(line, solution);
					solution.Projects.Add(project);

					line = reader.ReadLine();
					if (line.Equals("	ProjectSection(ProjectDependencies) = postProject"))
					{
						line = reader.ReadLine();
						while (!line.Equals("	EndProjectSection"))
						{
							string dependency = line.Split("=".ToCharArray())[0].Trim();
							project.Dependencies.Add(new System.Guid(dependency));
							line = reader.ReadLine();
						}
					}
				}
				else if (line.StartsWith("	GlobalSection(SolutionConfigurationPlatforms)"))
				{
					LoadConfigurations(reader, solution);
				}
			}

			return solution;
		}

		private static Models.Project LoadProject(string line, Models.Solution solution)
		{
			string[] sublines = line.Substring( line.IndexOf("=") + 1 ).Split(",".ToCharArray());
			string name = sublines[0].Trim().Substring(1, sublines[0].Trim().Length - 2);
			string path = sublines[1].Trim().Substring(1, sublines[1].Trim().Length - 2);
			System.Guid guid = new System.Guid( sublines[2].Trim().Substring(1, sublines[2].Trim().Length - 2) );

			Models.Project project = new Models.Project();
			project.Name = name;
			project.Path = path;
			project.Guid = guid;
			return project;
		}

		private static void LoadConfigurations(System.IO.TextReader reader, Models.Solution solution)
		{
			while(reader.Peek() != -1)
			{
				string line = reader.ReadLine();

				if(line.StartsWith("	EndGlobalSection"))
				{
					return;
				}

				Models.Config config = new Models.Config();
				config.Name = line.Split("=".ToCharArray())[0].Trim();
				solution.Configs.Add(config);
			}
		}

		private SolutionReader()
		{
		}

	}
}
