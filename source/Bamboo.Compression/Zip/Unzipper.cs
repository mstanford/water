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
	/// Summary description for Unzipper.
	/// </summary>
	public class Unzipper
	{

		public static void Unzip(string input, string output)
		{
			string path = output;
			if(!path.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
			{
				path += System.IO.Path.DirectorySeparatorChar;
			}

			using(System.IO.FileStream fileStream = System.IO.File.OpenRead(input))
			{
				NZlib.Zip.ZipInputStream zipInputStream = new NZlib.Zip.ZipInputStream(fileStream);

				NZlib.Zip.ZipEntry zipEntry;
				int size = 2048;
				byte[] data = new byte[2048];
				while( (zipEntry = zipInputStream.GetNextEntry()) != null) 
				{
					System.IO.FileInfo fileinfo = new System.IO.FileInfo(path + zipEntry.Name);
					if( !fileinfo.Directory.Exists )
					{
						fileinfo.Directory.Create();
					}

					System.IO.FileStream outputFileStream = System.IO.File.Create(path + zipEntry.Name);

					while (true) 
					{
						size = zipInputStream.Read(data, 0, data.Length);
						if (size > 0) 
						{
							outputFileStream.Write(data, 0, size);
						} 
						else 
						{
							break;
						}
					}

					outputFileStream.Flush();
					outputFileStream.Close();
				}

				zipInputStream.Close();

				fileStream.Close();
			}
		}

		private Unzipper()
		{
		}

	}
}
