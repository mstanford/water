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
	public class NetworkStream : System.IO.Stream
	{
		private System.Net.Sockets.Socket _socket;
		private byte[] _buffer;

		public NetworkStream(System.Net.Sockets.Socket socket)
		{
			this._socket = socket;
			this._buffer = new byte[8192];
		}

        public override bool CanRead
        {
			get { return true; }
		}

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
			get { return true; }
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

        public override void Flush()
        {
        }

        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {
            throw new Exception("The method or operation is not supported.");
        }

        public override void SetLength(long value)
        {
            throw new Exception("The method or operation is not supported.");
        }

		public override int Read(byte[] buffer, int offset, int size)
        {
			//TODO
			////
			//// parameter validation
			////
			//if (buffer == null)
			//{
			//    throw new ArgumentNullException("buffer");
			//}
			//if (offset < 0 || offset > buffer.Length)
			//{
			//    throw new ArgumentOutOfRangeException("offset");
			//}
			//if (size < 0 || size > buffer.Length - offset)
			//{
			//    throw new ArgumentOutOfRangeException("size");
			//}


			if(this._buffer.Length < buffer.Length)
			{
				size = this._buffer.Length;
			}
			int bytesRead = this._socket.Receive(this._buffer, 0, size, 0);
			System.Buffer.BlockCopy(this._buffer, 0, buffer, offset, bytesRead);
			return bytesRead;
		}

		public override void Write(byte[] buffer, int offset, int size)
        {
			//TODO
			////
			//// parameter validation
			////
			//if (buffer == null)
			//{
			//    throw new ArgumentNullException("buffer");
			//}
			//if (offset < 0 || offset > buffer.Length)
			//{
			//    throw new ArgumentOutOfRangeException("offset");
			//}
			//if (size < 0 || size > buffer.Length - offset)
			//{
			//    throw new ArgumentOutOfRangeException("size");
			//}


			int bytesSent = 0;
			while (bytesSent < size)
			{
				int bytesToSend = System.Math.Min(this._buffer.Length, size - bytesSent);

				System.Buffer.BlockCopy(buffer, offset + bytesSent, this._buffer, 0, bytesToSend);

				bytesSent += this._socket.Send(this._buffer, 0, bytesToSend, System.Net.Sockets.SocketFlags.None);
			}
		}

		public override void Close()
		{
			if (this._buffer != null)
			{
				this._buffer = null;
			}

			this._socket = null;
			this._buffer = null;
		}

	}
}
