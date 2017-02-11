// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005-2006 Swampware, Inc.
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

namespace Bamboo.Compression.Zip
{
	/// <summary>
	/// Summary description for Zipper.
	/// </summary>
	public class Zipper
	{
		private static int ZIP_LEVEL = 9;

		public static void Zip(string output, string sourceFolder)
		{
			FileCollection files = new FileCollection();

			if(!sourceFolder.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
			{
				sourceFolder += System.IO.Path.DirectorySeparatorChar;
			}

			GetFiles(sourceFolder, files, "");

			Zip(output, files);
		}

		private static void GetFiles(string folder, FileCollection files, string fileprefix)
		{
			foreach(string directory in System.IO.Directory.GetDirectories(folder))
			{
				GetFiles(directory, files, fileprefix + new System.IO.DirectoryInfo(directory).Name + System.IO.Path.DirectorySeparatorChar);
			}

			foreach(string file in System.IO.Directory.GetFiles(folder))
			{
				files.Add(new File(fileprefix + new System.IO.FileInfo(file).Name, file));
			}
		}

		public static void Zip(string output, FileCollection files)
		{
			MemoryFileCollection memoryFiles = new MemoryFileCollection();

			Zip(output, files, memoryFiles);
		}

		public static void Zip(string output, FileCollection files, MemoryFileCollection memoryFiles)
		{
			System.IO.DirectoryInfo directoryInfo = new System.IO.FileInfo(output).Directory;
			if(!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}

			using(System.IO.FileStream fileStream = System.IO.File.Create(output))
			{
				NZlib.Zip.ZipOutputStream zipOutputStream = new NZlib.Zip.ZipOutputStream(fileStream);
				zipOutputStream.SetLevel(ZIP_LEVEL);


				//Files
				foreach(File file in files)
				{
					AddFile(zipOutputStream, file);
				}

				//MemoryFiles
				foreach(MemoryFile memoryFile in memoryFiles)
				{
					Add(zipOutputStream, memoryFile.Name, memoryFile.DateTime, memoryFile.Bytes.Length, memoryFile.Bytes);
				}


				zipOutputStream.Finish();
				zipOutputStream.Close();

				fileStream.Close();
			}
		}

		private static void AddFile(NZlib.Zip.ZipOutputStream zipOutputStream, File file)
		{
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(file.Path);

			System.IO.FileStream fileStream = System.IO.File.OpenRead(fileInfo.FullName);

			System.DateTime dateTime = fileInfo.LastWriteTime;
			long filesize = fileInfo.Length;

			byte[] buffer = new byte[fileStream.Length];
			fileStream.Read(buffer, 0, buffer.Length);
			fileStream.Close();

			Add(zipOutputStream, file.Name, dateTime, filesize, buffer);
		}

		private static void Add(NZlib.Zip.ZipOutputStream zipOutputStream, string name, System.DateTime dateTime, long size, byte[] buffer)
		{
			NZlib.Zip.ZipEntry zipEntry = new NZlib.Zip.ZipEntry(name);
			zipEntry.DateTime = dateTime;
			if( ZIP_LEVEL == 0 )
			{
				zipEntry.Size = size;
				// calculate crc32 of current file
				NZlib.Checksums.Crc32 crc = new NZlib.Checksums.Crc32();
				crc.Update(buffer);
				zipEntry.Crc  = crc.Value;
			}
			zipOutputStream.PutNextEntry(zipEntry);
			zipOutputStream.Write(buffer, 0, buffer.Length);
		}

		private Zipper()
		{
		}

	}
}
