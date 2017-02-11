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
using System.Net;
using System.Net.Sockets;

namespace Bamboo.UDP
{
	public class UdpSender
	{
		private IPEndPoint _ipEndPoint;
		private Socket _socket;

		public UdpSender()
		{
		}

		public void Open(string address, int port)
		{
			// TTL
			// 0 - LAN
			// N - N router hops
			Open(address, port, 0);
		}

		public void Open(string address, int port, int TTL)
		{
			this._ipEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
			this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			this._socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, TTL);
		}

		public void Send(byte[] bytes, int TTL)
		{
			this._socket.SendTo(bytes, 0, bytes.Length, SocketFlags.None, this._ipEndPoint);
		}

		public void Close()
		{
			this._socket.Close();
		}

	}
}
