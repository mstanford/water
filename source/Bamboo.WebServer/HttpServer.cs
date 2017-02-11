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
	/// Summary description for HttpServer.
	/// </summary>
	public class HttpServer : System.IDisposable
	{
		private int _port;
		private bool _isStarted = false;
		private HttpPreProcessor _preProcessor;
		private System.Net.Sockets.Socket _socket = null;
		private System.Threading.Thread _thread = null;

		public HttpServer()
		{
		}

		public HttpServer(HttpPreProcessor preProcessor)
		{
			this._preProcessor = preProcessor;
		}

		public HttpServer(HttpProcessor processor) : this(new HttpPreProcessor(processor))
		{
		}

		public HttpPreProcessor PreProcessor
		{
			get { return this._preProcessor; }
			set { this._preProcessor = value; }
		}

		public virtual void Start()
		{
			this.Start(80, 64);
		}

		public void Start(int port, int thread_limit)
		{
			this._port = port;

			if(!this._isStarted)
			{
				this._isStarted = true;

				this._socket = new System.Net.Sockets.Socket(System.Net.IPAddress.Any.AddressFamily, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
				this._socket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, port));
				this._socket.Listen((int)System.Net.Sockets.SocketOptionName.MaxConnections);

				this._thread = new System.Threading.Thread(new System.Threading.ThreadStart(Listen));
				this._thread.Start();
			}
		}

		public void Stop()
		{
			if(this._isStarted)
			{
				this._isStarted = false;

				//Release Listening Socket
				System.Net.Sockets.Socket socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
				System.Net.IPEndPoint ipEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Loopback, this._port);
				socket.Connect(ipEndPoint);
				socket.Close();

				this._port = -1;

				this._thread.Join();
				this._thread = null;
			}
		}

		private void Listen()
		{
			while(this._isStarted)
			{
				try
				{
					System.Net.Sockets.Socket socket = this._socket.Accept();
					if (this._isStarted)
					{
						Process(socket);
					}
				}
				catch (System.Exception exception)
				{
					this.Error(exception);
				}
			}

			this._socket.Close();
			this._socket = null;
		}

		protected virtual void Process(System.Net.Sockets.Socket socket)
		{
			// This is unlimited threaded.
			HttpServerWorker worker = CreateWorker(socket, this._preProcessor);
			System.Threading.ThreadStart threadStart = new System.Threading.ThreadStart(worker.Execute);
			System.Threading.Thread thread = new System.Threading.Thread(threadStart);
			thread.Start();

			//TODO
		}

		protected virtual HttpServerWorker CreateWorker(System.Net.Sockets.Socket socket, HttpPreProcessor preProcessor)
		{
			return new HttpServerWorker(socket, preProcessor);
		}

		protected virtual void Error(System.Exception exception)
		{
			System.Console.WriteLine(exception.Message);
			System.Console.WriteLine();
			System.Console.WriteLine(exception.StackTrace);
			System.Console.WriteLine();
		}

		#region IDisposable Members

		private bool isDisposed = false;

		public void Dispose()
		{
			if (!isDisposed)
			{
				this.Stop();
			}
			isDisposed = true;
		}

		#endregion

	}
}
