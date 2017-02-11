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

namespace Bamboo.WebServer
{
	/// <summary>
	/// Summary description for TappedStream.
	/// </summary>
	public class TappedStream : System.IO.Stream
	{
		private System.IO.Stream _stream;
		private System.IO.Stream _readTap;
		private System.IO.Stream _writeTap;

		public TappedStream(System.IO.Stream stream, System.IO.Stream readTap, System.IO.Stream writeTap)
		{
			this._stream = stream;
			this._readTap = readTap;
			this._writeTap = writeTap;
		}

		public override bool CanRead
		{
			get { return this._stream.CanRead; }
		}

		public override bool CanSeek
		{
			get { return false; }
		}

		public override bool CanWrite
		{
			get { return this._stream.CanWrite; }
		}

		public override long Length
		{
			get { throw new Exception("The method or operation is not supported."); }
		}

		public override long Position
		{
			get { throw new Exception("The method or operation is not supported."); }
			set { throw new Exception("The method or operation is not supported."); }
		}

		public override void Close()
		{
			if (this._stream != null)
			{
				this._stream.Close();
				this._stream = null;
			}
		}

		public override void Flush()
		{
			this._stream.Flush();

			if (this._readTap != null)
			{
				this._readTap.Flush();
			}

			if (this._writeTap != null)
			{
				this._writeTap.Flush();
			}
		}

		public override long Seek(long offset, System.IO.SeekOrigin origin)
		{
			throw new Exception("The method or operation is not supported.");
		}

		public override void SetLength(long value)
		{
			throw new Exception("The method or operation is not supported.");
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int bytesRead = this._stream.Read(buffer, offset, count);
			if (this._readTap != null)
			{
				this._readTap.Write(buffer, offset, bytesRead);
			}
			return bytesRead;
		}

		//TODO DELETE
		//public override int ReadByte()
		//{
		//    int n = this._stream.ReadByte();
		//    if (n != -1)
		//    {
		//        if (this._readTap != null)
		//        {
		//            this._readTap.WriteByte((byte)n);
		//        }
		//    }
		//    return n;
		//}

		public override void Write(byte[] buffer, int offset, int count)
		{
			this._stream.Write(buffer, offset, count);
			if (this._writeTap != null)
			{
				this._writeTap.Write(buffer, offset, count);
			}
		}

		//TODO DELETE
		//public override void WriteByte(byte value)
		//{
		//    this._stream.WriteByte(value);
		//    if (this._writeTap != null)
		//    {
		//        this._writeTap.WriteByte(value);
		//    }
		//}

	}
}
