using System;
using System.IO;
using TomLabs.Shadowgem.Extensions.String;

namespace TomLabs.KCDModToolbox.Core
{
	public class LogWatcherEventArgs : EventArgs
	{
		public string Content { get; set; }
		public LogWatcherEventArgs(string content)
		{
			Content = content;
		}
	}

	public class LogWatcher : FileSystemWatcher
	{
		public string FileName { get; protected set; }

		private FileStream Stream { get; set; }
		private StreamReader Reader { get; set; }

		public delegate void LogWatcherEventHandler(object sender, LogWatcherEventArgs e);

		public event LogWatcherEventHandler TextChanged;

		public LogWatcher(string fileName)
		{
			this.Changed += OnChanged;

			FileName = fileName;
			Path = System.IO.Path.GetDirectoryName(FileName);
			Filter = System.IO.Path.GetFileName(FileName);
		}

		public static LogWatcher CreateDefault(string fileName)
		{
			return new LogWatcher(fileName)
			{
				NotifyFilter = (NotifyFilters.LastWrite | NotifyFilters.Size)
			};
		}

		public void Start()
		{
			Stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			Reader = new StreamReader(Stream);
			Stream.Position = Stream.Length;
			EnableRaisingEvents = true;
		}

		public void Stop()
		{
			EnableRaisingEvents = false;
			Reader.Close();
			Stream.Close();
		}

		public void Reset()
		{
			Stop();
			Start();
		}

		public void OnChanged(object o, FileSystemEventArgs e)
		{
			string content = Reader.ReadToEnd();
			if (content.IsFilled())
			{
				TextChanged?.Invoke(this, new LogWatcherEventArgs(content));
			}
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			Stream.Dispose();
			Reader.Dispose();
		}
	}
}
