// ------------------------------------------------------------------------------
// 
// Copyright (c) 2009 Swampware, Inc.
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
using System.Collections.Generic;
using System.Text;

namespace Bamboo.Http
{
	public class TcpServer
	{
		private Bamboo.Threading.ThreadPool _threadPool;
		private TcpProcessor _processor;
		private System.Reflection.MethodInfo _method;
		private System.Net.Sockets.Socket _socket;
		private System.Threading.Thread _thread;

		public TcpServer(int port, Bamboo.Threading.ThreadPool threadPool, TcpProcessor processor)
		{
			this._threadPool = threadPool;
			this._processor = processor;
			this._method = processor.GetType().GetMethod("Process");

			this._socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
			this._socket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, port));
			this._socket.Listen((int)System.Net.Sockets.SocketOptionName.MaxConnections);

			this._thread = new System.Threading.Thread(Run);
			this._thread.Start();
		}

		private void Run()
		{
			while (true)
			{
				this._threadPool.Enqueue(new Bamboo.Threading.ThreadTask(this._method, this._processor, this._socket.Accept()));
			}
		}

		public void Close()
		{
			this._socket.Close();
			this._socket = null;
		}

	}
}
