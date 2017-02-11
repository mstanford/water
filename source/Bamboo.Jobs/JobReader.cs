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

namespace Bamboo.Jobs
{
	public class JobReader
	{
		private System.IO.StreamReader _reader;

		public JobReader(System.IO.Stream stream)
		{
			this._reader = new System.IO.StreamReader(stream);
		}

		public Job Read()
		{
			string type = this._reader.ReadLine();
			switch (type)
			{
				case "Interval":
					{
						string name = this._reader.ReadLine();
						string command = this._reader.ReadLine();
						string args = this._reader.ReadLine();
						System.TimeSpan interval = System.TimeSpan.Parse(this._reader.ReadLine());
						Job job = new Job(name, command, args, interval);
						return job;
					}
				case "Daily":
					{
						string name = this._reader.ReadLine();
						string command = this._reader.ReadLine();
						string args = this._reader.ReadLine();
						System.DateTime time = System.DateTime.Parse(this._reader.ReadLine());
						Job job = new Job(name, command, args, time);
						return job;
					}
				case "Weekly":
					{
						string name = this._reader.ReadLine();
						string command = this._reader.ReadLine();
						string args = this._reader.ReadLine();
						System.DateTime time = System.DateTime.Parse(this._reader.ReadLine());
						string days = this._reader.ReadLine();
						Job job = new Job(name, command, args, time, days);
						return job;
					}
				default:
					{
						throw new System.Exception("Invalid JobType: " + type);
					}
			}
		}

	}
}
