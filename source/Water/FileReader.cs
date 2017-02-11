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

namespace Water
{
	/// <summary>
	/// Summary description for FileReader.
	/// </summary>
	public class FileReader : Water.TextReader
	{
		private string _currentDirectory;
		private Water.TextReader _reader;

		public FileReader(string filename)
		{
			// Does the file exist?
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
			if(!fileInfo.Exists)
			{
				throw new Water.Error(fileInfo.FullName + " does not exist.");
			}

			// Push the current directory.
			this._currentDirectory = System.Environment.CurrentDirectory;
			string newCurrentDirectory = fileInfo.DirectoryName;
			if(!newCurrentDirectory.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
			{
				newCurrentDirectory += System.IO.Path.DirectorySeparatorChar;
			}
			System.Environment.CurrentDirectory = newCurrentDirectory;

			//Reader
			System.IO.Stream source_stream = fileInfo.OpenRead();
			System.IO.MemoryStream target_stream = new System.IO.MemoryStream();
			byte[] bytes = new byte[4096];
			int numbytes;
			while( (numbytes = source_stream.Read(bytes, 0, 4096)) > 0 )
			{
				target_stream.Write(bytes, 0, numbytes);
			}
			source_stream.Close();
			target_stream.Position = 0;
			this._reader = new Water.TextReader(new System.IO.StreamReader(target_stream), filename);
		}

		public override string Filename
		{
			get { return this._reader.Filename; }
		}

		public override int LineNumber
		{
			get { return this._reader.LineNumber; }
		}

		public override int ColumnNumber
		{
			get { return this._reader.ColumnNumber; }
		}

		public override int Peek()
		{
			#region Peek

			return this._reader.Peek();

			#endregion
		}

		public override int Read()
		{
			#region Read

			return this._reader.Read();

			#endregion
		}

		public override void Close()
		{
			#region Close

			this._reader.Close();

			//Reset to previous current directory.
			System.Environment.CurrentDirectory = this._currentDirectory;

			#endregion
		}

		public static string ResolvePath(string path)
		{
			string resolvedPath;
			if(System.IO.Path.IsPathRooted(path))
			{
				resolvedPath = path;
			}
			else
			{
				resolvedPath = System.Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + path;
			}
			return resolvedPath;
		}

	}
}
