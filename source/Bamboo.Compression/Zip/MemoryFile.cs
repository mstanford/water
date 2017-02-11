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
	/// Summary description for MemoryFile.
	/// </summary>
	public class MemoryFile
	{
		private string _name;
		private System.DateTime _dateTime;
		private byte[] _bytes;

		private MemoryFile()
		{
		}

		public MemoryFile(string name, byte[] bytes)
		{
			this._name = name;
			this._dateTime = System.DateTime.Now;
			this._bytes = bytes;
		}

		public string Name
		{
			get { return this._name; }
		}

		public System.DateTime DateTime
		{
			get { return this._dateTime; }
		}

		public byte[] Bytes
		{
			get { return this._bytes; }
		}

	}
}
