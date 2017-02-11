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

namespace Bamboo.VisualStudio
{
	/// <summary>
	/// Summary description for Path.
	/// </summary>
	public class Path
	{
		/// <summary>
		/// Creates a relative path from two paths.
		/// </summary>
		/// <param name="absolutePath"></param>
		/// <param name="rootPath"></param>
		/// <returns></returns>
		public static string Relative(string absolutePath, string rootPath)
		{
			string PATH_ESCAPE = @".." + System.IO.Path.DirectorySeparatorChar;

			string[] absolutePaths = new string[2];
			string[] rootPaths = new string[2];

			bool passedLimit = false;

			int index1 = absolutePath.IndexOf(System.IO.Path.VolumeSeparatorChar);
			int index2 = rootPath.IndexOf(System.IO.Path.VolumeSeparatorChar);
			if(index1 != -1 && index2 != -1)
			{
				string volume1 = absolutePath.Substring(0, index1);
				string volume2 = rootPath.Substring(0, index2);
				if(!volume1.ToLower().Equals(volume2.ToLower()))
				{
					return absolutePath;
				}
			}

			while(Contains(rootPath, System.IO.Path.DirectorySeparatorChar.ToString()))
			{
				absolutePaths[0] = Left(absolutePath, System.IO.Path.DirectorySeparatorChar.ToString());
				absolutePaths[1] = Right(absolutePath, System.IO.Path.DirectorySeparatorChar.ToString());
				rootPaths[0] = Left(rootPath, System.IO.Path.DirectorySeparatorChar.ToString());
				rootPaths[1] = Right(rootPath, System.IO.Path.DirectorySeparatorChar.ToString());

				if(rootPaths[1] == "")
				{
					rootPath = rootPaths[0];
					break;
				}

				if(!absolutePaths[0].ToLower().Equals(rootPaths[0].ToLower()))
				{
					passedLimit = true;
				}

				if(passedLimit)
				{
					absolutePath = PATH_ESCAPE + absolutePath;
					rootPath = rootPaths[1];
				}
				else
				{
					absolutePath = absolutePaths[1];
					rootPath = rootPaths[1];
				}
			}

			absolutePaths[0] = Left(absolutePath, System.IO.Path.DirectorySeparatorChar.ToString());
			absolutePaths[1] = Right(absolutePath, System.IO.Path.DirectorySeparatorChar.ToString());

			if(!absolutePaths[0].ToLower().Equals(rootPath.ToLower()))
			{
				passedLimit = true;
			}

			if(passedLimit)
			{
				absolutePath = PATH_ESCAPE + absolutePath;
			}
			else
			{
				absolutePath = absolutePaths[1];
			}

			return absolutePath;
		}

		/// <summary>
		/// Determines if a path contains a seperator.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="seperator"></param>
		/// <returns></returns>
		public static bool Contains(string s, string seperator)
		{
			return (s.IndexOf(seperator) != -1);
		}

		public static string GetFileName(string path)
		{
			string name = new System.IO.FileInfo(path).Name;
			string extension = new System.IO.FileInfo(path).Extension;

			return name.Substring(0, name.Length - extension.Length);
		}

		private static string Left(string input, string marker)
		{
			int index = input.IndexOf(marker);
			if (index == -1)
			{
				return input;
			}
			return input.Substring(0, index);
		}

		private static string Right(string input, string marker)
		{
			int index = input.IndexOf(marker);
			if (index == -1)
			{
				return input;
			}
			return input.Substring(index + marker.Length);
		}

		private Path()
		{
		}

	}
}
