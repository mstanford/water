// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006-2007 Swampware, Inc.
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

namespace Bamboo.VisualStudio.VisualStudio2003.CSharpProject
{
	/// <summary>
	/// Summary description for ProjectReferenceRules.
	/// </summary>
	public class ProjectReferenceRules
	{

		public static bool IsFrameworkReference(string hintPath, string path, Bamboo.CSharp.Compilers.Compiler compiler)
		{
			string referencePath = GetReferencePath(path, hintPath, compiler);
			if(referencePath == null)
			{
				return false;
			}

			if(referencePath.Length == 0)
			{
				return false;
			}

			string installRoot = Bamboo.CSharp.FrameworkDetector.GetInstallRoot();

			if(referencePath.ToLower().StartsWith(installRoot.ToLower()))
			{
				return true;
			}

			return false;
		}

		public static bool IsAssemblyFolderReference(string hintPath, string path, Bamboo.CSharp.Compilers.Compiler compiler)
		{
			string referencePath = GetReferencePath(path, hintPath, compiler);
			if(referencePath == null)
			{
				return false;
			}

			if(referencePath.Length == 0)
			{
				return false;
			}

			string[] assemblyFolders = Bamboo.CSharp.FrameworkDetector.GetAssemblyFolders();

			foreach(string assemblyFolder in assemblyFolders)
			{
				if(referencePath.ToLower().StartsWith(assemblyFolder.ToLower()))
				{
					return true;
				}
			}

			return false;
		}

		public static string GetFrameworkReferencePath(string projectPath, string hintPath, Bamboo.CSharp.Compilers.Compiler compiler)
		{
			string frameworkPath = compiler.GetFrameworkPath();

			int index = hintPath.LastIndexOf("\\");
			string name = hintPath.Substring(index);

			return frameworkPath + name;
		}

		//TODO resolve this in project reader.
		public static string GetReferencePath(string projectPath, string hintPath, Bamboo.CSharp.Compilers.Compiler compiler)
		{
			string referencePath = System.IO.Path.Combine(projectPath, hintPath);
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(referencePath);

			string name = fileInfo.Name;

			if(fileInfo.Exists)
			{
				return fileInfo.FullName;
			}

			string tempHintPath = hintPath;

			int index = tempHintPath.IndexOf(@".." + System.IO.Path.DirectorySeparatorChar);
			while(index != -1)
			{
				tempHintPath = tempHintPath.Substring(index + 3);

				referencePath = System.IO.Path.Combine(projectPath, tempHintPath);
				fileInfo = new System.IO.FileInfo(referencePath);
				if(fileInfo.Exists)
				{
					return fileInfo.FullName;
				}

				index = tempHintPath.IndexOf("..");
			}

			tempHintPath = hintPath;

			for(int i = 0; i < 10; i++)
			{
				tempHintPath = @".." + System.IO.Path.DirectorySeparatorChar + tempHintPath;

				referencePath = System.IO.Path.Combine(projectPath, tempHintPath);
				fileInfo = new System.IO.FileInfo(referencePath);
				if(fileInfo.Exists)
				{
					return fileInfo.FullName;
				}
			}


			//
			// Check in the framework.
			//
			string frameworkPath = compiler.GetFrameworkPath();
			if(System.IO.File.Exists(frameworkPath + name))
			{
				return frameworkPath + name;
			}


			//
			// Check in the assembly folders.
			//
			string[] assemblyFolders = Bamboo.CSharp.FrameworkDetector.GetAssemblyFolders();
			foreach(string assemblyFolder in assemblyFolders)
			{
				string sep = "";

				if(!assemblyFolder.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
				{
					sep = System.IO.Path.DirectorySeparatorChar.ToString();
				}

				if(System.IO.File.Exists(assemblyFolder + sep + name))
				{
					return assemblyFolder + sep + name;
				}
			}

			return null;
		}

		private ProjectReferenceRules()
		{
		}

	}
}
