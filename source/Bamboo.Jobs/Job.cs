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
	public class Job
	{
		public JobType Type;
		public string Name;
		public string Command;
		public string Args;
		public System.TimeSpan Interval;
		public System.DateTime Time;
		public string Days = "";

		/// <summary>
		/// At a given interval.
		/// </summary>
		/// <param name="interval"></param>
		public Job(string name, string command, string args, System.TimeSpan interval)
		{
			this.Type = JobType.Interval;
			this.Name = name;
			this.Command = command;
			this.Args = args;
			this.Interval = interval;
		}

		/// <summary>
		/// Every day at a given time.
		/// </summary>
		/// <param name="time"></param>
		public Job(string name, string command, string args, System.DateTime time)
		{
			this.Type = JobType.Daily;
			this.Name = name;
			this.Command = command;
			this.Args = args;
			this.Time = time;
		}

		/// <summary>
		/// Given days at a given time.
		/// </summary>
		/// <param name="days"></param>
		/// <param name="time"></param>
		public Job(string name, string command, string args, System.DateTime time, string days)
		{
			this.Type = JobType.Weekly;
			this.Name = name;
			this.Command = command;
			this.Args = args;
			this.Time = time;
			this.Days = days;
		}

	}
}
