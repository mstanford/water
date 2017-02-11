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
	public class Scheduler
	{
		private string _path;
		private Bamboo.Threading.ThreadPool _threadPool;
		private Bamboo.Sql2.Database _database;
		private System.Threading.Thread _thread;
		private System.Reflection.MethodInfo _method;
		private Dictionary<string, Job> _jobs = new Dictionary<string, Job>();
		private object _sync = new object();

		public Scheduler(string path, Bamboo.Threading.ThreadPool threadPool, Bamboo.Sql2.Database database)
		{
			this._path = path;
			this._threadPool = threadPool;
			this._database = database;

			this._method = typeof(Scheduler).GetMethod("ExecuteJob");

			this._thread = new System.Threading.Thread(Run);
			this._thread.Start();
		}

		private void Run()
		{
			if (System.IO.Directory.Exists(this._path))
			{
				foreach (string file in System.IO.Directory.GetFiles(this._path))
				{
					System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
					if (!fileInfo.Name.EndsWith(".tmp"))
					{
						System.IO.Stream stream = System.IO.File.OpenRead(file);
						JobReader jobReader = new JobReader(stream);
						Job job = jobReader.Read();
						this._jobs.Add(job.Name, job);
						stream.Close();
					}
				}
			}


			Bamboo.DataStructures.Table jobTimes = this._database.ReadTable("Statibase.JobSummary");
			Dictionary<string, long> jobTimesLookup = new Dictionary<string, long>();
			foreach (Bamboo.DataStructures.Tuple row in jobTimes.Rows)
			{
				jobTimesLookup.Add((string)row[0], (long)row[1]);
			}


			while (true)
			{
				List<Job> jobs = new List<Job>();
				lock (this._sync)
				{
					foreach (Job job in this._jobs.Values)
					{
						jobs.Add(job);
					}
				}

				System.DateTime now2 = System.DateTime.Now;
				long now = now2.Ticks;

				foreach (Job job in jobs)
				{
					//TODO do this in memory.
					long lastRun = 0;
					if (jobTimesLookup.ContainsKey(job.Name))
					{
						lastRun = jobTimesLookup[job.Name];
					}


					switch (job.Type)
					{
						case JobType.Interval:
							{
								long next = lastRun + job.Interval.Ticks;
								if (next < now)
								{
									this._threadPool.Enqueue(new Bamboo.Threading.ThreadTask(this._method, this, job));
									jobTimesLookup[job.Name] = now;
								}
								break;
							}
						case JobType.Daily:
							{
								System.DateTime lastRunDate = new System.DateTime(lastRun);
								if (!lastRunDate.Date.Equals(now2.Date) && job.Time.TimeOfDay < now2.TimeOfDay)
								{
									this._threadPool.Enqueue(new Bamboo.Threading.ThreadTask(this._method, this, job));
									jobTimesLookup[job.Name] = now;
								}
								break;
							}
						case JobType.Weekly:
							{
								if (job.Days.Contains(GetDay(System.DateTime.Now.DayOfWeek)))
								{
									System.DateTime lastRunDate = new System.DateTime(lastRun);
									if (!lastRunDate.Date.Equals(now2.Date) && job.Time.TimeOfDay < now2.TimeOfDay)
									{
										this._threadPool.Enqueue(new Bamboo.Threading.ThreadTask(this._method, this, job));
										jobTimesLookup[job.Name] = now;
									}
								}
								break;
							}
					}
				}

				System.Threading.Thread.Sleep(500);
			}
		}

		public void Execute(string name)
		{
			this._threadPool.Enqueue(new Bamboo.Threading.ThreadTask(this._method, this, this._jobs[name]));
			//TODO jobTimesLookup[job.Name] = now;
		}

		public void ExecuteJob(Job job)
		{
			long start = System.DateTime.Now.Ticks;

			string command = job.Command;
			string workingDirectory = null;
			string utilityPath = this._path + ".." + System.IO.Path.DirectorySeparatorChar + "Utilities" + System.IO.Path.DirectorySeparatorChar + job.Command + System.IO.Path.DirectorySeparatorChar + job.Command + ".exe";
			if (System.IO.File.Exists(utilityPath))
			{
				command = utilityPath;
				workingDirectory = this._path + ".." + System.IO.Path.DirectorySeparatorChar + "Utilities" + System.IO.Path.DirectorySeparatorChar + job.Command + System.IO.Path.DirectorySeparatorChar; ;
			}

			System.IO.StringWriter outputWriter = new System.IO.StringWriter();
			System.IO.StringWriter errorWriter = new System.IO.StringWriter();
			Process.Execute(command, job.Args, workingDirectory, outputWriter, errorWriter);

			//TODO log this.
		}

		private static string GetDay(System.DayOfWeek dayOfWeek)
		{
			switch (dayOfWeek)
			{
				case DayOfWeek.Monday:
					{
						return "M";
					}
				case DayOfWeek.Tuesday:
					{
						return "T";
					}
				case DayOfWeek.Wednesday:
					{
						return "W";
					}
				case DayOfWeek.Thursday:
					{
						return "H";
					}
				case DayOfWeek.Friday:
					{
						return "F";
					}
				case DayOfWeek.Saturday:
					{
						return "S";
					}
				case DayOfWeek.Sunday:
					{
						return "U";
					}
				default:
					{
						throw new System.Exception();
					}
			}
		}

		public List<string> ListJobs()
		{
			lock (this._sync)
			{
				List<string> jobs = new List<string>();
				foreach (string job in this._jobs.Keys)
				{
					jobs.Add(job);
				}
				return jobs;
			}
		}

		public Job ReadJob(string name)
		{
			lock (this._sync)
			{
				if (System.IO.File.Exists(this._path + System.IO.Path.DirectorySeparatorChar + System.Web.HttpUtility.UrlEncode(name)))
				{
					System.IO.Stream stream = System.IO.File.OpenRead(this._path + System.IO.Path.DirectorySeparatorChar + System.Web.HttpUtility.UrlEncode(name));
					JobReader jobReader = new JobReader(stream);
					Job job = jobReader.Read();
					stream.Close();
					return job;
				}
				else
				{
					throw new System.Exception("Cannot read job.  Job does not exist: " + name);
				}
			}
		}

		public void WriteJob(string name, Job value)
		{
			lock (this._sync)
			{
				string path = this._path + System.IO.Path.DirectorySeparatorChar + System.Web.HttpUtility.UrlEncode(name);
				System.IO.Stream stream = System.IO.File.Create(path + ".tmp");
				JobWriter jobWriter = new JobWriter(stream);
				jobWriter.Write(value);
				stream.Close();
				this._jobs[name] = value;
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}
				System.IO.File.Move(path + ".tmp", path);
			}
		}

		public void DeleteJob(string name)
		{
			lock (this._sync)
			{
				if (this._jobs.ContainsKey(name))
				{
					this._jobs.Remove(name);
				}
				if (System.IO.File.Exists(this._path + System.IO.Path.DirectorySeparatorChar + System.Web.HttpUtility.UrlEncode(name)))
				{
					System.IO.File.Delete(this._path + System.IO.Path.DirectorySeparatorChar + System.Web.HttpUtility.UrlEncode(name));
				}
			}
		}

	}
}
