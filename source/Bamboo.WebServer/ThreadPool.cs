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
	/// Summary description for ThreadPool.
	/// </summary>
	public class ThreadPool
	{
		private object _lock = new object();
		private System.Collections.Queue _pool;

		public ThreadPool(int capacity)
		{
			this._pool = new System.Collections.Queue(capacity);
			for(int i = 0; i < capacity; i++)
			{
				System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(Run));
				//TODO thread.IsBackground = true; // Foreground threads prevent a process from terminating.
				//TODO thread.ApartmentState = System.Threading.ApartmentState.MTA;
				thread.Start();
				this._pool.Enqueue(thread);
			}
		}

		public int Count
		{
			get { return this._pool.Count; }
		}

		private void Run()
		{
			while(true)
			{
				ThreadTask task;
				lock(this._lock)
				{
					while(this._pool.Count == 0)
					{
						System.Threading.Monitor.Wait(this._lock);
					}
					task = (ThreadTask)this._pool.Dequeue();
				}

				//TODO record event
				task.Execute();
				//TODO record event
			}
		}

		public void Enqueue(ThreadTask task)
		{
			//TODO record event

			lock(this._lock)
			{
				this._pool.Enqueue(task);
				System.Threading.Monitor.Pulse(this._lock);
			}
		}

	}
}
