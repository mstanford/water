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

namespace Bamboo.CSharp
{
	/// <summary>
	/// Summary description for FrameworkDetector.
	/// </summary>
	public class FrameworkDetector
	{
		private const string FRAMEWORK_REGISTRY_KEY = @"SOFTWARE\Microsoft\.NETFramework";

		public static string GetFrameworkPath(string version)
		{
			string PATTERN= @"v" + version.Replace(".", @"\.") + @"\.\d+";
			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(PATTERN);



			string installRoot = GetInstallRoot();
			System.IO.DirectoryInfo installRootDirectoryInfo = new System.IO.DirectoryInfo(installRoot);
			foreach(System.IO.DirectoryInfo directoryInfo in installRootDirectoryInfo.GetDirectories())
			{
				if(regex.IsMatch(directoryInfo.FullName))
				{
					string path = directoryInfo.FullName;
					if(!path.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
					{
						path += System.IO.Path.DirectorySeparatorChar;
					}
					return path;
				}
			}



			throw new System.Exception(".NET " + version + " not installed.");
		}

		public static string GetInstallRoot()
		{
			//WARN this will only work on Win32.

			// Get a non-writable key from the registry
			Microsoft.Win32.RegistryKey netFramework = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(FRAMEWORK_REGISTRY_KEY, false);

			// Retrieve the install root path for the framework
			string installRoot = netFramework.GetValue("InstallRoot").ToString();

			return installRoot;
		}

		public static bool IsInstalled(string version)
		{
			foreach (string installedVersion in GetInstalledVersions())
			{
				if (installedVersion.StartsWith("v" + version + "."))
				{
					return true;
				}
			}
			return false;
		}

		public static string[] GetInstalledVersions()
		{
			System.Collections.ArrayList arrayList = new System.Collections.ArrayList();



			const string PATTERN = @"v\d+\.\d+\.\d+";
			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(PATTERN);



			string installRoot = GetInstallRoot();
			System.IO.DirectoryInfo installRootDirectoryInfo = new System.IO.DirectoryInfo(installRoot);
			foreach(System.IO.DirectoryInfo directoryInfo in installRootDirectoryInfo.GetDirectories())
			{
				if(regex.IsMatch(directoryInfo.Name))
				{
					arrayList.Add(directoryInfo.Name);
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

		public static string[] GetAssemblyFolders()
		{
			//WARN this will only work on Win32.

			System.Collections.ArrayList arrayList = new System.Collections.ArrayList();



			// Get a non-writable key from the registry
			Microsoft.Win32.RegistryKey frameworkKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(FRAMEWORK_REGISTRY_KEY, false);
			Microsoft.Win32.RegistryKey assemblyFoldersKey = frameworkKey.OpenSubKey("AssemblyFolders");
			if(assemblyFoldersKey == null)
			{
				return new string[]{};
			}
			foreach(string assemblyFolderLocationKeyName in assemblyFoldersKey.GetSubKeyNames())
			{
				Microsoft.Win32.RegistryKey assemblyFolderLocationKey = assemblyFoldersKey.OpenSubKey(assemblyFolderLocationKeyName);
				if (assemblyFolderLocationKey.GetValue("") != null)
				{
					string path = assemblyFolderLocationKey.GetValue("").ToString();
					arrayList.Add(path);
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

		public static string GetSDKInstallRoot(string version)
		{
			//WARN this will only work on Win32.

			// Get a non-writable key from the registry
			Microsoft.Win32.RegistryKey netFramework = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(FRAMEWORK_REGISTRY_KEY, false);

			// Retrieve the install root path for the framework
			string installRoot = netFramework.GetValue("sdkInstallRoot" + version).ToString();

			return installRoot;
		}

		private FrameworkDetector()
		{
		}

	}
}
