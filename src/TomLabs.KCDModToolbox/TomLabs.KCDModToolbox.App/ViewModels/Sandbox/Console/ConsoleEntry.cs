using System;
using System.Windows.Media;
using TomLabs.WPF.Tools;

namespace TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Console
{
	public class ConsoleEntry : BaseViewModel
	{
		public bool IsUserInput { get; set; }
		public DateTime Created { get; set; }
		public string CommandText { get; set; }

		public Color Foreground { get; set; }

		public ConsoleEntry(string commandText, bool isUserInput = false)
		{
			Created = DateTime.Now;
			CommandText = commandText;
			IsUserInput = isUserInput;
			Foreground = GetColorByCommandText(CommandText, IsUserInput);
		}

		private Color GetColorByCommandText(string commandText, bool isUserInput)
		{
			if (commandText.Contains("[VERBOSE]"))
			{
				return Colors.Blue;
			}
			else if (commandText.Contains("[DEBUG]"))
			{
				return Colors.LightGreen;
			}
			else if (commandText.Contains("[INFO]"))
			{
				return Colors.Cyan;
			}
			else if (commandText.Contains("[WARNING]"))
			{
				return Colors.Orange;
			}
			else if (commandText.Contains("[ERROR]"))
			{
				return Colors.Red;
			}
			else if (isUserInput)
			{
				return Colors.Yellow;
			}
			else
			{
				return Colors.White;
			}
		}
	}
}
