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
	/// Summary description for LoggingPreProcessor.
	/// </summary>
	public abstract class LoggingPreProcessor : Bamboo.WebServer.HttpPreProcessor
	{
		private Bamboo.WebServer.HttpProcessor _processor;

		public LoggingPreProcessor(Bamboo.WebServer.HttpProcessor processor) : base(processor)
		{
			this._processor = processor;
		}

		public override void PreProcess(System.Net.Sockets.Socket socket, System.IO.Stream stream)
		{
			// Address:Port info.
			string localAddress = "";
			int localPort = 0;
			string remoteAddress = "";
			int remotePort = 0;

			string start = System.DateTime.UtcNow.ToString();
			byte[] request_bytes = null;
			byte[] response_bytes = null;
			System.Exception error = null;

			System.IO.MemoryStream requestStream = new System.IO.MemoryStream();
			System.IO.MemoryStream responseStream = new System.IO.MemoryStream();
			Bamboo.WebServer.TappedStream tappedStream = new Bamboo.WebServer.TappedStream(stream, requestStream, responseStream);

			Bamboo.WebServer.HttpRequest request = new Bamboo.WebServer.HttpRequest();
			Bamboo.WebServer.HttpResponse response = new Bamboo.WebServer.HttpResponse();
			byte[] buffer = new byte[8192];

			try
			{
				// Address:Port info.
				string localEndPoint = socket.LocalEndPoint.ToString();
				int index = localEndPoint.LastIndexOf(":");
				localAddress = localEndPoint.Substring(0, index);
				localPort = Int32.Parse(localEndPoint.Substring(index + 1));
				string remoteEndPoint = socket.RemoteEndPoint.ToString();
				index = remoteEndPoint.LastIndexOf(":");
				remoteAddress = remoteEndPoint.Substring(0, index);
				remotePort = Int32.Parse(remoteEndPoint.Substring(index + 1));

				socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.SendTimeout, 15000);
				socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.ReceiveTimeout, 15000);

				ReadRequest(socket, tappedStream, request, buffer);

				this._processor.Process(request, response);
				KeepCookies(request, response);
				WriteResponse(response, tappedStream);

				tappedStream.Flush();
				//TODO DELETE tappedStream.Close();
			}
			catch (System.Exception exception)
			{
				error = exception;
			}
			finally
			{
				if (buffer != null)
				{
					buffer = null;
				}
				if (request != null)
				{
					request = null;
				}
				if (response != null)
				{
					response = null;
				}
			}

			requestStream.Flush();
			responseStream.Flush();

			request_bytes = requestStream.ToArray();
			response_bytes = responseStream.ToArray();

			requestStream.Close();
			requestStream = null;

			responseStream.Close();
			responseStream = null;

			string end = System.DateTime.UtcNow.ToString();

			this.Log(localAddress, localPort, remoteAddress, remotePort, start, end, request_bytes, response_bytes, request, response, error);

			if (request != null)
			{
				request = null;
			}
			if (response != null)
			{
				response = null;
			}

			//TODO set everyting = null
		}

		protected abstract void Log(string localAddress, int localPort, string remoteAddress, int remotePort, string start, string end, byte[] request_bytes, byte[] response_bytes, HttpRequest request, HttpResponse response, System.Exception error);

	}
}
