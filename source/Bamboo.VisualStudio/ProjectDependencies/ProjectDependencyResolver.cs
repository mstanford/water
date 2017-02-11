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

namespace Bamboo.VisualStudio.ProjectDependencies
{
	/// <summary>
	/// Summary description for ProjectDependencyResolver.
	/// </summary>
	public class ProjectDependencyResolver
	{

		public static void Rollcall(ProjectCollection projects)
		{
			foreach(Project project in projects)
			{
				foreach(ProjectDependency projectDependency in project.Dependencies)
				{
					bool isResolvable = false;

					foreach(Project projectToResolve in projects)
					{
						if(projectToResolve.Guid.Equals(projectDependency.ProjectGuid))
						{
							isResolvable = true;
							break;
						}
					}

					if(!isResolvable)
					{
						throw new System.Exception("Unable to resolve dependency \"" + projectDependency.Name + "\"");
					}
				}
			}
		}

		/// <summary>
		/// Topological sorting algorithm.
		/// </summary>
		/// <param name="projects"></param>
		/// <returns></returns>
		public static ProjectCollection Resolve(ProjectCollection projects)
		{
			Rollcall(projects);

			ProjectCollection buildOrder = new ProjectCollection();

			while(projects.Count > 0)
			{
				ProjectCollection resolvedProjects = new ProjectCollection();

				for(int i = 0; i < projects.Count; i++)
				{
					if(projects[i].Dependencies.Count == 0)
					{
						resolvedProjects.Add(projects[i]);
					}
				}

				foreach(Project project in resolvedProjects)
				{
					projects.Remove(project);
					buildOrder.Add(project);

					foreach(Project unresolvedProject in projects)
					{
						ProjectDependencyCollection resolvedDependencies = new ProjectDependencyCollection();

						foreach(ProjectDependency dependency in unresolvedProject.Dependencies)
						{
							if(dependency.ProjectGuid == project.Guid)
							{
								resolvedDependencies.Add(dependency);
							}
						}

						foreach(ProjectDependency resolvedDependency in resolvedDependencies)
						{
							unresolvedProject.Dependencies.Remove(resolvedDependency);
						}

					}
				}
			}

			return buildOrder;
		}

		private ProjectDependencyResolver()
		{
		}

	}
}
