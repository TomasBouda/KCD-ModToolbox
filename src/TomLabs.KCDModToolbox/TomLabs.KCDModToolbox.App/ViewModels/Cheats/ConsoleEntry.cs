using System;
using TomLabs.WPF.Tools;

namespace TomLabs.KCDModToolbox.App.ViewModels.Cheats
{
	public class ConsoleEntry : BaseViewModel
	{
		public DateTime Created { get; set; }
		public string CommandText { get; set; }

		public ConsoleEntry(string commandText)
		{
			Created = DateTime.Now;
			CommandText = commandText;
		}
	}
}
