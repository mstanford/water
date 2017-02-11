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
	/// Summary description for VisualStudioDetector.
	/// </summary>
	public class VisualStudioDetector
	{
		private const string VISUAL_STUDIO_REGISTRY_KEY = @"SOFTWARE\Microsoft\VisualStudio";

		/// <summary>
		/// To get the installed versions of visual studio check each key 
		/// for the AssemblyFolders subkey.
		/// </summary>
		/// <returns></returns>
		public static string[] GetInstalledVersions()
		{
			//WARN this will only work on Win32.

			System.Collections.ArrayList arrayList = new System.Collections.ArrayList();



			// Get a non-writable key from the registry
			Microsoft.Win32.RegistryKey visualStudioKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(VISUAL_STUDIO_REGISTRY_KEY, false);
			foreach(string versionKeyName in visualStudioKey.GetSubKeyNames())
			{
				Microsoft.Win32.RegistryKey versionKey = visualStudioKey.OpenSubKey(versionKeyName);
				Microsoft.Win32.RegistryKey assemblyFoldersKey = versionKey.OpenSubKey("AssemblyFolders");

				if(assemblyFoldersKey != null)
				{
					arrayList.Add(versionKeyName);
				}
			}



			// Convert to string[] and return.
			string[] results = new string[arrayList.Count];
			for(int i = 0; i < arrayList.Count; i++)
			{
				results[i] = arrayList[i].ToString();
			}
			return results;
		}

		public static string[] GetAssemblyFolders(string version)
		{
			//WARN this will only work on Win32.

			System.Collections.ArrayList arrayList = new System.Collections.ArrayList();



			// Get a non-writable key from the registry
			Microsoft.Win32.RegistryKey visualStudioKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(VISUAL_STUDIO_REGISTRY_KEY, false);
			Microsoft.Win32.RegistryKey versionKey = visualStudioKey.OpenSubKey(version);
			Microsoft.Win32.RegistryKey assemblyFoldersKey = versionKey.OpenSubKey("AssemblyFolders");
			foreach(string assemblyFolderLocationKeyName in assemblyFoldersKey.GetSubKeyNames())
			{
				Microsoft.Win32.RegistryKey assemblyFolderLocationKey = assemblyFoldersKey.OpenSubKey(assemblyFolderLocationKeyName);
				string path = assemblyFolderLocationKey.GetValue("").ToString();
				arrayList.Add(path);
			}



			// Convert to string[] and return.
			string[] results = new string[arrayList.Count];
			for(int i = 0; i < arrayList.Count; i++)
			{
				results[i] = arrayList[i].ToString();
			}
			return results;
		}

		private VisualStudioDetector()
		{
		}

	}
}
