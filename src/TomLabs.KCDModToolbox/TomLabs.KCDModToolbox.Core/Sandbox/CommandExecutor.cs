using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TomLabs.Shadowgem.Extensions.Common;

namespace TomLabs.KCDModToolbox.Core.Sandbox
{
	public class CommandExecutor
	{
		private const int CMD_TIMEOUT = 10000;

		private static CommandExecutor _instance;
		public static CommandExecutor Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new CommandExecutor();
				}

				return _instance;
			}
		}

		private string KCDLogPath { get; set; }

		private string BootloaderCommadnLogPath { get; set; }

		public LogWatcher LogWatcher { get; private set; }

		private StringBuilder WatchedLogEntries { get; set; } = new StringBuilder();

		public void Initialize(string kcdDirectory)
		{
			ScriptInjector.SetBridgeFilePath($@"{kcdDirectory}\mods\KCD_Bootloader\invoke_command.txt");

			KCDLogPath = $@"{kcdDirectory}\kcd.log";
			BootloaderCommadnLogPath = $@"{kcdDirectory}\mods\KCD_Bootloader\command_log.txt";

			LogWatcher = LogWatcher.CreateDefault(KCDLogPath);
			LogWatcher.Start();
		}

		private async Task<Match> ExecuteCommandTextAsync(string commandText, string resultRegex)
		{
			return await Task.Run(() =>
			{
				LogWatcher.TextChanged += LogWatcher_LogEntryAwaiter;

				ScriptInjector.Instance.InjectCommand(commandText);

				int spentMS = 0;
				while (!WatchedLogEntries.ToString().Match(resultRegex))
				{
					Thread.Sleep(100);

					spentMS += 100;
					if (spentMS >= CMD_TIMEOUT)
					{
						return null;
					}
				}

				LogWatcher.TextChanged -= LogWatcher_LogEntryAwaiter;

				var match = Regex.Match(WatchedLogEntries.ToString(), resultRegex);
				WatchedLogEntries.Clear();

				return match;
			});
		}

		private void LogWatcher_LogEntryAwaiter(object sender, LogWatcherEventArgs e)
		{
			WatchedLogEntries.Append(e.Content);
		}

		public void ExecuteCommandText(string commandText)
		{
			ScriptInjector.Instance.InjectCommand(commandText);
		}

		public async Task<bool> ExecuteCommandTextAsync(string commandText)
		{
			return await Task.Run(() =>
			{
				// Flush command log
				File.WriteAllText(BootloaderCommadnLogPath, string.Empty);

				ExecuteCommandText(commandText);

				// Wait for command result inside command_log
				int spentMS = 0;
				var fileInfo = new FileInfo(BootloaderCommadnLogPath);
				while (fileInfo.Length == 0)
				{
					Thread.Sleep(100);
					fileInfo.Refresh();

					spentMS += 100;
					if (spentMS >= CMD_TIMEOUT)
					{
						return false;
					}
				}

				bool success = File.ReadAllText(BootloaderCommadnLogPath).Contains("[SUCCESS]");

				// Flush command log
				File.WriteAllText(BootloaderCommadnLogPath, string.Empty);

				return success;
			});
		}

		public async Task<Match> ExecuteCommandWithResult(Command command)
		{
			return await ExecuteCommandTextAsync(command.CommandText, command.ResultPattern);
		}

		public async Task<bool> ExecuteCommandAsync(Command command)
		{
			return await ExecuteCommandTextAsync(command.CommandText);
		}

		public void ExecuteCommand(Command command)
		{
			ExecuteCommandText(command.CommandText);
		}
	}
}
