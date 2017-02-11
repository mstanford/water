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
	/// To create a certificate use mono's makecert:
	/// 
	///		makecert -n "CN=localhost" -r -sv ssl.pvk ssl.cer
	///		
	/// </summary>
	public class SecureHttpServer : Bamboo.WebServer.HttpServer
	{
		private Bamboo.WebServer.HttpPreProcessor _preProcessor;
		private byte[] _certificate;
		private byte[] _privateKey;

		public SecureHttpServer(Bamboo.WebServer.HttpPreProcessor preProcessor, byte[] certificate, byte[] privateKey) : base(preProcessor)
		{
			this._preProcessor = preProcessor;
			this._certificate = certificate;
			this._privateKey = privateKey;
		}

		public SecureHttpServer(Bamboo.WebServer.HttpProcessor processor, byte[] certificate, byte[] privateKey) : this(new HttpPreProcessor(processor), certificate, privateKey)
		{
		}

		public byte[] Certificate
		{
			get { return this._certificate; }
		}

		public byte[] PrivateKey
		{
			get { return this._privateKey; }
		}

		public override void Start()
		{
			this.Start(443, 64);
		}

		protected override void Process(System.Net.Sockets.Socket socket)
		{
			// This is unlimited threaded.
			HttpServerWorker worker = CreateWorker(socket, this._preProcessor, this.Certificate, this.PrivateKey);
			System.Threading.ThreadStart threadStart = new System.Threading.ThreadStart(worker.Execute);
			System.Threading.Thread thread = new System.Threading.Thread(threadStart);
			thread.Start();

			//TODO
		}

		protected virtual SecureHttpServerWorker CreateWorker(System.Net.Sockets.Socket socket, HttpPreProcessor preProcessor, byte[] certificate, byte[] privateKey)
		{
			return new SecureHttpServerWorker(socket, preProcessor, certificate, privateKey);
		}

	}
}
