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

namespace Bamboo.WebServer
{
	/// <summary>
	/// Summary description for HttpServerWorker.
	/// </summary>
	public class HttpServerWorker
	{
		private System.Net.Sockets.Socket _socket;
		private HttpPreProcessor _preProcessor;

		public HttpServerWorker(System.Net.Sockets.Socket socket, HttpPreProcessor preProcessor)
		{
			this._socket = socket;
			this._preProcessor = preProcessor;
		}

		protected System.Net.Sockets.Socket Socket
		{
			get { return this._socket; }
		}

		public virtual void Execute()
		{
			try
			{
				int receiveBufferSize = (int)this._socket.GetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.ReceiveBuffer);
				int sendBufferSize = (int)this._socket.GetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.SendBuffer);

				Bamboo.WebServer.NetworkStream networkStream = new Bamboo.WebServer.NetworkStream(this._socket);
				Bamboo.WebServer.BufferedStream bufferedStream = new Bamboo.WebServer.BufferedStream(networkStream, receiveBufferSize, sendBufferSize);

				this._preProcessor.PreProcess(this._socket, bufferedStream);

				bufferedStream.Close();
			}
			catch (System.Exception exception)
			{
				this.Error(exception);
			}

			try
			{
				this._socket.Close();
			}
			catch (System.Exception exception)
			{
				this.Error(exception);
			}

			this._socket = null;
			this._preProcessor = null;
		}

		protected virtual void Error(System.Exception exception)
		{
			System.Console.WriteLine(exception.Message);
			System.Console.WriteLine();
			System.Console.WriteLine(exception.StackTrace);
			System.Console.WriteLine();
		}

	}
}
