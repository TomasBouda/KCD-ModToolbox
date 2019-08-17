using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TomLabs.KCDModToolbox.Core.Sandbox
{
	public class Command
	{
		public string CommandText { get; set; }

		public string ResultPattern { get; set; }

		public bool ShouldReturnValue { get; }

		public Dictionary<string, object> LastResults { get; private set; } = new Dictionary<string, object>();

		public Command(string commandText, string resultPattern = null)
		{
			CommandText = commandText;
			ResultPattern = resultPattern;
			ShouldReturnValue = resultPattern != null;
		}

		public async Task<Dictionary<string, object>> ExecuteWithResultAsync()
		{
			LastResults.Clear();

			var match = await CommandExecutor.Instance.ExecuteCommandWithResult(this);
			if (match != null)
			{
				foreach (var groupName in new Regex(ResultPattern).GetGroupNames()) // TODO optimize
				{
					LastResults.Add(groupName, match.Groups[groupName].Value);
				}
			}

			return LastResults;
		}

		public void Execute()
		{
			CommandExecutor.Instance.ExecuteCommand(this);
		}

		public async Task<bool> ExecuteAsync()
		{
			return await CommandExecutor.Instance.ExecuteCommandAsync(this);
		}
	}
}
