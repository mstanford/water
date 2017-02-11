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
	/// Summary description for HttpPreProcessor.
	/// </summary>
	public class HttpPreProcessor
	{
		private HttpProcessor _processor;

		public HttpPreProcessor(HttpProcessor processor)
		{
			this._processor = processor;
		}

		public virtual void PreProcess(System.Net.Sockets.Socket socket, System.IO.Stream stream)
		{
			#region Process

			//socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.SendTimeout, 15000);
			//socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.ReceiveTimeout, 15000);

			Bamboo.WebServer.HttpRequest request = new Bamboo.WebServer.HttpRequest();
			Bamboo.WebServer.HttpResponse response = new Bamboo.WebServer.HttpResponse();
			byte[] buffer = new byte[8192];

			try
			{
				ReadRequest(socket, stream, request, buffer);

				this._processor.Process(request, response);

				KeepCookies(request, response);
				WriteResponse(response, stream);

				stream.Flush();
			}
			catch (System.Exception exception)
			{
				throw new System.Exception("HTTP ERROR", exception);
			}
			finally
			{
				if (buffer != null)
				{
					buffer = null;
				}
				if (request != null)
				{
					request.Dispose();
					request = null;
				}
				if (response != null)
				{
					response.Dispose();
					response = null;
				}
			}

			//stream.Flush();
			//TODO DELETE stream.Close();

			//TODO set everyting = null

			#endregion
		}

		public static void ReadRequest(System.Net.Sockets.Socket socket, System.IO.Stream stream, Bamboo.WebServer.HttpRequest request, byte[] buffer)
		{
			#region ReadRequest

			Bamboo.WebServer.HttpReader httpReader = new Bamboo.WebServer.HttpReader(stream);

			// Address:Port info.
			string localEndPoint = socket.LocalEndPoint.ToString();
			int index = localEndPoint.LastIndexOf(":");
			request.LocalAddress = localEndPoint.Substring(0, index);
			request.LocalPort = Int32.Parse(localEndPoint.Substring(index + 1));
			string remoteEndPoint = socket.RemoteEndPoint.ToString();
			index = remoteEndPoint.LastIndexOf(":");
			request.RemoteAddress = remoteEndPoint.Substring(0, index);
			request.RemotePort = Int32.Parse(remoteEndPoint.Substring(index + 1));

			// Status line
			string line = httpReader.ReadStartLine();
			if (line == null)
			{
				throw new System.Exception("Could not read request line.");
			}
			string[] sublines = line.Split(" ".ToCharArray());
			request.Method = sublines[0].ToUpper();
			request.PathAndQuery = sublines[1];

			index = request.PathAndQuery.IndexOf("?");
			if (index == -1)
			{
				request.Path = request.PathAndQuery;
				request.Query = "";
			}
			else
			{
				request.Path = request.PathAndQuery.Substring(0, index);
				request.Query = request.PathAndQuery.Substring(index + 1);
			}

			string[] header;
			while ((header = httpReader.ReadHeader()) != null)
			{
				if (header.Length == 1)
				{
					request.Headers.Add(header[0], "");
				}
				else if (header.Length == 2)
				{
					request.Headers.Add(header[0], header[1]);
				}
			}

			// Load cookies
			request.Cookies = new System.Collections.Hashtable(new System.Collections.CaseInsensitiveHashCodeProvider(), new System.Collections.CaseInsensitiveComparer());
			string cookies = (string)request.Headers["Cookie"];
			if (cookies != null)
			{
				foreach (string cookie in cookies.Split(";".ToCharArray()))
				{
					index = cookie.IndexOf("=");
					if (index != -1)
					{
						string name = cookie.Substring(0, index).Trim();
						string value = cookie.Substring(index + 1).Trim();
						request.Cookies[name] = value;
					}
					else
					{
						//throw new System.Exception("ERROR: " + headers["Cookie"].ToString());
					}
				}
			}

			request.QueryString = new System.Collections.Hashtable(0, new System.Collections.CaseInsensitiveHashCodeProvider(), new System.Collections.CaseInsensitiveComparer());
			if (request.Query.Length > 0)
			{
				ParseGetData(request.Query, request.QueryString);
			}

			httpReader.ReadBody(request.Input, buffer);
			request.Input.Position = 0;

			string contentType = (string)request.Headers["Content-Type"];
			if (contentType != null && request.Input.Length > 0)
			{
				if (contentType == "application/x-www-form-urlencoded")
				{
					ParsePostData(request.Input, request.Form);
				}
				else if (contentType.StartsWith("multipart/form-data"))
				{
					string boundary = contentType.Substring(contentType.IndexOf(";") + 1).Trim();
					boundary = "--" + boundary.Substring(boundary.IndexOf("=") + 1);

					ParseMultipartFormData(boundary, request.Input, request.Form, request.Files);
				}
			}

			#endregion
		}

		public static void KeepCookies(HttpRequest request, HttpResponse response)
		{
			#region KeepCookies

			foreach (string name in request.Cookies.Keys)
			{
				if (!response.Cookies.ContainsKey(name))
				{
					response.Cookies.Add(name, new Cookie(name, (string)request.Cookies[name]));
				}
			}

			#endregion
		}

		public static void WriteResponse(HttpResponse response, System.IO.Stream stream)
		{
			#region WriteResponse

			Bamboo.WebServer.HttpWriter httpWriter = new Bamboo.WebServer.HttpWriter(stream);

			httpWriter.WriteStartLine("HTTP/1.1 " + response.Status + " " + StatusCodes.ReasonPhrase(response.Status));
			httpWriter.WriteHeader("Server", "Swampware Bamboo WebServer 1.1");
			httpWriter.WriteHeader("Connection", "close");

			if (response.Headers["Content-Length"] == null)
			{
				response.Headers.Add("Content-Length", response.Output.Length.ToString());
			}
			foreach (string key in response.Headers.Keys)
			{
				httpWriter.WriteHeader(key, (string)response.Headers[key]);
			}

			foreach (Cookie cookie in response.Cookies.Values)
			{
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

				stringBuilder.Append(cookie.Name);
				stringBuilder.Append("=");
				stringBuilder.Append(cookie.Value);
				stringBuilder.Append(";");

				if (cookie.Domain.Length > 0)
				{
					stringBuilder.Append(" ");
					stringBuilder.Append("domain");
					stringBuilder.Append("=");
					stringBuilder.Append(cookie.Domain);
					stringBuilder.Append(";");
				}

				if (cookie.Expires.Length > 0)
				{
					stringBuilder.Append(" ");
					stringBuilder.Append("expires");
					stringBuilder.Append("=");
					stringBuilder.Append(cookie.Expires);
					stringBuilder.Append(";");
				}

				if (cookie.Path.Length > 0)
				{
					stringBuilder.Append(" ");
					stringBuilder.Append("path");
					stringBuilder.Append("=");
					stringBuilder.Append(cookie.Path);
					stringBuilder.Append(";");
				}

				httpWriter.WriteHeader("Set-Cookie", stringBuilder.ToString());
			}

			httpWriter.WriteEndHeader();

			httpWriter.WriteBody(response.Output);

			#endregion
		}

		public static void ParseGetData(string query, System.Collections.Hashtable queryString)
		{
			#region ParseGetData

			//TODO DUP
			foreach (string pair in query.Split("&".ToCharArray()))
			{
				if (pair.Length == 0)
				{
				}
				else
				{
					//TODO check for improper strings.
					string[] nv = pair.Split("=".ToCharArray());
					if (nv[1] != null)
					{
						nv[1] = System.Web.HttpUtility.UrlDecode(nv[1]);
					}
					queryString.Add(System.Web.HttpUtility.UrlDecode(nv[0]), nv[1]);
				}
			}

			#endregion
		}

		public static void ParsePostData(System.IO.Stream input, System.Collections.Hashtable form)
		{
			#region ParsePostData

			// Sometimes this can incorrectly be set by default.
			try
			{
				//TODO DUP
				string postData = new System.IO.StreamReader(input).ReadToEnd();
				string[] afd = postData.Split("&".ToCharArray());
				foreach (string fd in afd)
				{
					string[] afdv = fd.Split("=".ToCharArray());
					form.Add(System.Web.HttpUtility.UrlDecode(afdv[0]), System.Web.HttpUtility.UrlDecode(afdv[1]));
				}
			}
			catch (System.Exception exception)
			{
				string thisIsAHackToIgnoreCompilerWarning = exception.Message;
			}

			#endregion
		}

		public static void ParseMultipartFormData(string boundary, System.IO.Stream input, System.Collections.Hashtable form, System.Collections.Hashtable files)
		{
			#region ParseFileData

			try
			{
				System.IO.StreamReader reader = new System.IO.StreamReader(input);

				string line = reader.ReadLine();
				if (!line.Equals(boundary))
				{
					//Error
					input.Position = 0;
					return;
				}

				while (line != (boundary + "--"))
				{
					string formname = null;
					string formvalue = null;
					string filename = null;
					string contentType = null;
					System.IO.MemoryStream filestream = null;
					System.IO.StreamWriter filestreamwriter = null;

					// Read headers
					string name;
					string value;
					while ((line = reader.ReadLine()) != "")
					{
						int index = line.IndexOf(";");
						if (index == -1)
						{
							//TODO other headers.
							int o = 0;
						}
						else
						{
							string left = line.Substring(0, index);
							line = line.Substring(index + 1).Trim();

							index = left.IndexOf(":");
							string cd = left.Substring(0, index);
							string fd = left.Substring(index + 1).Trim();
							if (cd != "Content-Disposition")
							{
								//Error
								input.Position = 0;
								return;
							}

							while ((index = line.IndexOf(";")) > -1)
							{
								left = line.Substring(0, index);
								line = line.Substring(index + 1).Trim();

								index = left.IndexOf("=");
								name = left.Substring(0, index);
								value = left.Substring(index + 1).Trim();

								switch (name)
								{
									case "name":
										{
											formname = value.Substring(1, value.Length - 2);
											break;
										}
									case "filename":
										{
											filename = value;
											break;
										}
									default:
										{
											//Error
											input.Position = 0;
											return;
										}
								}
							}

							index = line.IndexOf("=");
							name = line.Substring(0, index);
							value = line.Substring(index + 1).Trim();

							switch (name)
							{
								case "name":
									{
										formname = value.Substring(1, value.Length - 2);
										break;
									}
								case "filename":
									{
										filename = value;
										break;
									}
								default:
									{
										//Error
										input.Position = 0;
										return;
									}
							}
						}
					}


					//Read body.
					if (filename != null)
					{
						filestream = new System.IO.MemoryStream();
						filestreamwriter = new System.IO.StreamWriter(filestream);
					}

					line = reader.ReadLine();
					while (!line.StartsWith(boundary))
					{
						if (filename == null)
						{
							if (formvalue != null)
							{
								input.Position = 0;
								return;
							}
							//TODO System.Web.HttpUtility.UrlDecode(
							formvalue = line;
							form.Add(formname, formvalue);
						}
						else
						{
							filestreamwriter.WriteLine(line);
						}
						line = reader.ReadLine();
					}

					if (filestreamwriter != null)
					{
						filestreamwriter.Flush();
						filestream.Position = 0;

						//TODO what about filename and content-type
						files.Add(formname, filestream);
					}
				}

				input.Position = 0;
			}
			catch (System.Exception exception)
			{
				string thisIsAHackToIgnoreCompilerWarning = exception.Message;
			}

			#endregion
		}

	}
}
