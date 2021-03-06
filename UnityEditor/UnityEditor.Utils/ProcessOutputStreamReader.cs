using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace UnityEditor.Utils
{
	internal class ProcessOutputStreamReader
	{
		private readonly Func<bool> hostProcessExited;

		private readonly StreamReader stream;

		internal List<string> lines;

		private Thread thread;

		internal ProcessOutputStreamReader(Process p, StreamReader stream) : this(() => p.HasExited, stream)
		{
		}

		internal ProcessOutputStreamReader(Func<bool> hostProcessExited, StreamReader stream)
		{
			this.hostProcessExited = hostProcessExited;
			this.stream = stream;
			this.lines = new List<string>();
			this.thread = new Thread(new ThreadStart(this.ThreadFunc));
			this.thread.Start();
		}

		private void ThreadFunc()
		{
			if (!this.hostProcessExited())
			{
				try
				{
					while (this.stream.BaseStream != null)
					{
						string text = this.stream.ReadLine();
						if (text == null)
						{
							break;
						}
						object obj = this.lines;
						lock (obj)
						{
							this.lines.Add(text);
						}
					}
				}
				catch (ObjectDisposedException)
				{
					object obj2 = this.lines;
					lock (obj2)
					{
						this.lines.Add("Could not read output because an ObjectDisposedException was thrown.");
					}
				}
			}
		}

		internal string[] GetOutput()
		{
			if (this.hostProcessExited())
			{
				this.thread.Join();
			}
			object obj = this.lines;
			string[] result;
			lock (obj)
			{
				result = this.lines.ToArray();
			}
			return result;
		}
	}
}
