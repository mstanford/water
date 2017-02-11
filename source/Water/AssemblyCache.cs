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

namespace Water
{
	/// <summary>
	/// Summary description for AssemblyCache.
	/// </summary>
	public class AssemblyCache
	{
		private static System.Collections.Hashtable _cache = new System.Collections.Hashtable(new System.Collections.CaseInsensitiveHashCodeProvider(), new System.Collections.CaseInsensitiveComparer());
		private static System.Collections.ArrayList _assemblyFolders = new System.Collections.ArrayList();

		public static System.Reflection.Assembly LoadAssembly(string name)
		{
			if(_cache.ContainsKey(name))
			{
				return (System.Reflection.Assembly)_cache[name];
			}

			return null;
		}

		public static void Add(string name, string path)
		{
			if(_cache.ContainsKey(name))
			{
				_cache.Remove(name);
			}

			LoadAssembly(name, path);
		}

		public static void Add(string name, byte[] bytes)
		{
			if (_cache.ContainsKey(name))
			{
				_cache.Remove(name);
			}

			System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(bytes);
			_cache.Add(name, assembly);
		}

		public static void Add(string name, System.Reflection.Assembly assembly)
		{
			if (_cache.ContainsKey(name))
			{
				_cache.Remove(name);
			}

			_cache.Add(name, assembly);
		}

		public static void AddAssemblyFolder(string assemblyFolder)
		{
			_assemblyFolders.Add(assemblyFolder);
		}

		public static void Clear()
		{
			_cache.Clear();
			_assemblyFolders.Clear();
		}

		private static System.Reflection.Assembly LoadAssembly(string name, string path)
		{
			string root = path.Substring(0, path.LastIndexOf("."));
			if (System.IO.File.Exists(root + ".pdb"))
			{
				byte[] rawAssembly = ReadBytes(path);
				byte[] rawSymbolStore = ReadBytes(root + ".pdb");
				System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(rawAssembly, rawSymbolStore);
				_cache.Add(name, assembly);
				return assembly;
			}
			else
			{
				byte[] rawAssembly = ReadBytes(path);
				System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(rawAssembly);
				_cache.Add(name, assembly);
				return assembly;
			}
		}

		private static byte[] ReadBytes(string path)
		{
			System.IO.FileStream stream = System.IO.File.OpenRead(path);
			byte[] bytes = new byte[stream.Length];
			stream.Read(bytes, 0, bytes.Length);
			stream.Close();
			return bytes;
		}

		private AssemblyCache()
		{
		}

	}
}
