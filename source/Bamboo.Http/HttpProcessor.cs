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
	public class HttpProcessor : TcpProcessor
	{
		private Bamboo.DataStructures.BufferPool _bufferPool;

		protected HttpProcessor(Bamboo.DataStructures.BufferPool bufferPool)
			: base(bufferPool)
		{
			this._bufferPool = bufferPool;
		}

		protected override void ProcessTcp(System.IO.Stream stream)
		{
			byte[] buffer = this._bufferPool.Dequeue();

			string method;
			string path;
			int contentLength = 0;
			string contentType = "";
			string host = "";

			try
			{
				// Read request.
				string line = ReadLine(stream);
				int index = line.IndexOf(" ");
				method = line.Substring(0, index);
				line = line.Substring(index + 1);
				index = line.IndexOf(" ");
				path = line.Substring(0, index);

				line = ReadLine(stream);
				while (line.Length > 0)
				{
					index = line.IndexOf(":");
					string key = line.Substring(0, index);
					switch (key)
					{
						case "Content-Type": contentType = line.Substring(index + 1).Trim(); break;
						case "Content-Length": contentLength = Int32.Parse(line.Substring(index + 1).Trim()); break;
						case "Host": host = line.Substring(index + 1).Trim(); break;
					}

					line = ReadLine(stream);
				}
			}
			catch (System.Net.Sockets.SocketException socketException)
			{
				//TODO log error.
				return;
			}

			// Process request.
			HttpRequest request = (contentLength == 0) ? new HttpRequest(method, path, host) : new HttpRequest(method, path, host, contentLength, contentType, ReadContent(contentLength, stream, buffer));
			HttpResponse response = new HttpResponse(this._bufferPool);
			ProcessHttp(request, response);
			response.Writer.Flush();
			response.ContentLength = (int)response.Content.Length;
			if (response.ContentLength > 0)
			{
				response.Content.Position = 0;
			}

			// Write response.
			try
			{
				System.IO.TextWriter writer = new System.IO.StreamWriter(stream);
				switch (response.Status)
				{
					case 200: writer.Write("HTTP/1.1 200 OK\r\n"); break;
					case 302: writer.Write("HTTP/1.1 302 Found\r\n"); break;
					case 404: writer.Write("HTTP/1.1 404 Not Found\r\n"); break;
					case 500: writer.Write("HTTP/1.1 500 Internal Server Error\r\n"); break;
					default: throw new System.Exception("Unhandled status code.");
				}
				if (response.Status == 302)
				{
					writer.Write("Location: " + response.RedirectUrl + "\r\n");
				}
				writer.Write("Connection: close\r\n");
				writer.Write("Content-Length: " + response.ContentLength + "\r\n");
				if (response.ContentLength > 0)
				{
					if (response.ContentType == null)
					{
						writer.Write("Content-Type: " + "text/html" + "\r\n");
					}
					else
					{
						writer.Write("Content-Type: " + response.ContentType + "\r\n");
					}
				}
				writer.Write("\r\n");
				writer.Flush();
				if (response.ContentLength > 0)
				{
					int bytesRemaining = response.ContentLength;
					while (bytesRemaining > 0)
					{
						int bytesRead = response.Content.Read(buffer, 0, Math.Min(buffer.Length, bytesRemaining));
						stream.Write(buffer, 0, bytesRead);
						bytesRemaining -= bytesRead;
					}
				}
			}
			catch (System.Net.Sockets.SocketException socketException)
			{
				//TODO log error.
			}

			// Clean up.
			if (request.Content != null)
			{
				request.Content.Close();
			}
			if(response.Content != null)
			{
				response.Content.Close();
			}

			this._bufferPool.Enqueue(buffer);
		}

		public virtual void ProcessHttp(HttpRequest request, HttpResponse response)
		{
			throw new System.Exception("Override.");
		}

		private string ReadLine(System.IO.Stream stream)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			while (true)
			{
				char ch = (char)stream.ReadByte();
				if (ch == '\r')
				{
					stream.ReadByte();
					break;
				}
				else if (ch == '\n')
				{
					break;
				}
				stringBuilder.Append(ch);
			}
			return stringBuilder.ToString();
		}

		private Bamboo.DataStructures.MemoryStream ReadContent(int contentLength, System.IO.Stream stream, byte[] buffer)
		{
			Bamboo.DataStructures.MemoryStream content = new Bamboo.DataStructures.MemoryStream(this._bufferPool);
			int bytesRemaining = contentLength;
			while (bytesRemaining > 0)
			{
				int bytesRead = stream.Read(buffer, 0, Math.Min(buffer.Length, bytesRemaining));
				content.Write(buffer, 0, bytesRead);
				bytesRemaining -= bytesRead;
			}
			content.Position = 0;
			return content;
		}

		protected Dictionary<string, object> ParseQueryString(string query)
		{
			return ParseVariables(new System.IO.StringReader(query));
		}

		protected Dictionary<string, object> ParseForm(Bamboo.DataStructures.MemoryStream stream)
		{
			if (stream == null || stream.Length == 0)
			{
				return new Dictionary<string, object>(0);
			}
			else
			{
				return ParseVariables(new System.IO.StreamReader(stream));
			}
		}

		private Dictionary<string, object> ParseVariables(System.IO.TextReader reader)
		{
			Dictionary<string, object> variables = new Dictionary<string, object>();
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			string key = null;
			int n;
			if ((n = reader.Peek()) != -1)
			{
				while ((n = reader.Peek()) != -1)
				{
					char ch = (char)reader.Read();
					switch (ch)
					{
						case '=':
							{
								key = UrlDecode(stringBuilder.ToString());
								stringBuilder = new System.Text.StringBuilder();
								break;
							}
						case '&':
							{
								if (key.EndsWith("[]"))
								{
									key = key.Substring(0, key.Length - 2);
									string value = UrlDecode(stringBuilder.ToString());
									if (!variables.ContainsKey(key))
									{
										variables.Add(key, new List<string>());
									}
									((List<string>)variables[key]).Add(value);
								}
								else
								{
									variables.Add(key, UrlDecode(stringBuilder.ToString()));
								}
								stringBuilder = new System.Text.StringBuilder();
								break;
							}
						default:
							{
								stringBuilder.Append(ch);
								break;
							}
					}
				}
				if (key.EndsWith("[]"))
				{
					key = key.Substring(0, key.Length - 2);
					string value = UrlDecode(stringBuilder.ToString());
					if (!variables.ContainsKey(key))
					{
						variables.Add(key, new List<string>());
					}
					((List<string>)variables[key]).Add(value);
				}
				else
				{
					variables.Add(key, UrlDecode(stringBuilder.ToString()));
				}
			}
			return variables;
		}

		public static string UrlEncode(string s)
		{
			return System.Web.HttpUtility.UrlEncode(s);
		}

		public static string UrlDecode(string s)
		{
			return System.Web.HttpUtility.UrlDecode(s);
		}

		public static string HtmlEncode(string s)
		{
			return System.Web.HttpUtility.HtmlEncode(s);
		}

		public static string HtmlDecode(string s)
		{
			return System.Web.HttpUtility.HtmlDecode(s);
		}

	}
}
