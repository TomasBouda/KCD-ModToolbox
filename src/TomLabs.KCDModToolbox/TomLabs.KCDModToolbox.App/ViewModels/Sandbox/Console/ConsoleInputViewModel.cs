using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using TomLabs.WPF.Tools;

namespace TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Console
{
	public class ConsoleInputViewModel : BaseViewModel
	{
		public string Text { get; set; } = string.Empty;

		public ObservableCollection<string> AllSuggestions { get; set; }

		public LinkedList<string> SuggestionsCommands { get; set; } = new LinkedList<string>();


		public ConsoleInputViewModel()
		{
			// TODO
			AllSuggestions = new ObservableCollection<string>(File.ReadAllLines(@"C:\Data\GIT\KCD-Cheat\Source\Docs\commands.txt"));
		}

		public void Intelisense()
		{

		}

		public void Clear()
		{
			Text = string.Empty;
		}
	}
}
