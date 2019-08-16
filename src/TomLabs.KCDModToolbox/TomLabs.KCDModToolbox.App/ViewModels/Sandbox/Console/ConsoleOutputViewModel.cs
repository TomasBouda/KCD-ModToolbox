using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using TomLabs.KCDModToolbox.Core;
using TomLabs.KCDModToolbox.Core.Extensions.Collections;
using TomLabs.Shadowgem.Extensions.String;
using TomLabs.WPF.Tools;

namespace TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Console
{
	public class ConsoleOutputViewModel : BaseViewModel
	{
		private const int MAX_CONSOLE_ENTRIES = 100;
		private readonly string flushLogPath = $"{AppDomain.CurrentDomain.BaseDirectory}consoleLog.txt";

		private string KCDLogPath { get; set; }

		public ObservableCollection<ConsoleEntry> Entries { get; set; } = new ObservableCollection<ConsoleEntry>();

		public LinkedList<ConsoleEntry> CommandsCache { get; set; } = new LinkedList<ConsoleEntry>();

		public LinkedListNode<ConsoleEntry> CurrentCommand { get; set; }

		private LogWatcher LogWatcher { get; set; }

		private int CommandIndex { get; set; }

		public ConsoleOutputViewModel()
		{
			KCDLogPath = @"C:\Program Files (x86)\Steam\steamapps\common\KingdomComeDeliverance\kcd.log"; // TODO
			LogWatcher = new LogWatcher(KCDLogPath);
			LogWatcher.NotifyFilter = (NotifyFilters.LastWrite | NotifyFilters.Size);
			LogWatcher.TextChanged += LogWatcher_TextChanged;
			LogWatcher.EnableRaisingEvents = true;
		}

		private void LogWatcher_TextChanged(object sender, LogWatcherEventArgs e)
		{
			App.RunOnUIThread(() =>
			{
				foreach (var cmd in e.Content.Split(Environment.NewLine))
				{
					AddLogEntry(cmd);
				}
				CommandIndex = Entries.Count;
			});
		}

		private void Add(string commandText, bool isUserInput = false)
		{
			if (commandText.IsFilled())
			{
				var entry = new ConsoleEntry(commandText.Trim(), isUserInput);
				if (isUserInput)
				{
					CommandsCache.AddLast(entry);
					CurrentCommand = CommandsCache.Last;
				}

				Entries.Add(entry);

				// Flush console to disc if it exceeds its limit
				if (Entries.Count >= MAX_CONSOLE_ENTRIES)
				{
					FlushConsoleToDisc(MAX_CONSOLE_ENTRIES / 2);
				}
			}
		}

		private void AddLogEntry(string commandText)
		{
			Add(commandText, false);
		}


		public void AddAndInject(string commandText)
		{
			// Do not inject if command is special - is just for application
			if (!HandleSpecialCommand(commandText))
			{
				ScriptInjector.Instance.InjectCommand(commandText);
			}

			Add(commandText, true);
		}

		private void FlushConsoleToDisc(int entriesCount)
		{
			var entriesToRemove = Entries.Take(entriesCount);
			File.AppendAllText(flushLogPath, string.Join(Environment.NewLine, entriesToRemove.Select(e => e.CommandText)));
			foreach (var entry in entriesToRemove)
			{
				Entries.Remove(entry);
			}
		}

		public ConsoleEntry NextCommand()
		{
			ConsoleEntry cmd;
			if (CurrentCommand != null)
			{
				cmd = CurrentCommand?.Value;
				CurrentCommand = CurrentCommand.NextOrFirst();
			}
			else
			{
				CurrentCommand = CommandsCache.First;
				cmd = CurrentCommand?.Value;
			}

			return cmd;
		}

		public ConsoleEntry PreviousCommand()
		{
			ConsoleEntry cmd;
			if (CurrentCommand != null)
			{
				cmd = CurrentCommand?.Value;
				CurrentCommand = CurrentCommand.PreviousOrLast();
			}
			else
			{
				CurrentCommand = CommandsCache.Last;
				cmd = CurrentCommand?.Value;
			}

			return cmd;
		}

		public void Clear()
		{
			FlushConsoleToDisc(Entries.Count);
			Entries.Clear();
			CommandsCache.Clear();
			CurrentCommand = null;
		}

		public bool HandleSpecialCommand(string commandText)
		{
			if (commandText == "cls")
			{
				Clear();
				return true;
			}

			return false;
		}
	}
}
