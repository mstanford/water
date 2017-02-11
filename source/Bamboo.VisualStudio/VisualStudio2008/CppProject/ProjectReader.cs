// ------------------------------------------------------------------------------
// 
// Copyright (c) 2008 Swampware, Inc.
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

namespace Bamboo.VisualStudio.VisualStudio2008.CppProject
{
	/// <summary>
	/// Summary description for ProjectReader.
	/// </summary>
	public class ProjectReader
	{

		public static Models.Project Read(string filename)
		{
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
			string name = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
			System.IO.TextReader reader = System.IO.File.OpenText(filename);
			Models.Project project = Read(name, reader);
			reader.Close();
			return project;
		}

		/// <summary>
		/// Reads a project from a file, and returns the project.
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static Models.Project Read(string name, System.IO.TextReader reader)
		{
			Models.Project project = new Models.Project();

			project.Name = name;

			System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
			xmlDocument.Load(reader);

			//TODO

			return project;
		}

		private ProjectReader()
		{
		}

	}
}
