using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using TomLabs.KCDModToolbox.Core.Extensions.Collections;
using TomLabs.Shadowgem.Extensions.String;
using TomLabs.WPF.Tools;
using TomLabs.WPF.Tools.Commands;

namespace TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Console
{
	public class ConsoleInputViewModel : BaseViewModel
	{
		private string _text = string.Empty;
		public string Text
		{
			get => _text;
			set
			{
				if (SuggestionOn && !IsSuggesting)
				{
					StopSuggestion();
				}

				_text = value;
			}
		}

		public int CaretIndex { get; set; }

		private ObservableCollection<string> AllSuggestions { get; set; }

		private LinkedList<string> SuggestionsCommands { get; set; } = new LinkedList<string>();

		private LinkedListNode<string> CurrentSuggestion { get; set; }

		private string SuggestionBaseText { get; set; }
		public bool IsSuggesting { get; private set; }
		private bool SuggestionOn { get; set; }

		public ICommand SuggestNextCmd { get; set; }
		public ICommand SuggestPreviousCmd { get; set; }

		public ConsoleInputViewModel()
		{
			// TODO
			AllSuggestions = new ObservableCollection<string>(File.ReadAllLines(@"C:\Data\GIT\KCD-Cheat\Source\Docs\commands.txt"));

			SuggestNextCmd = new RelayCommand(SuggestNextCommand);
			SuggestPreviousCmd = new RelayCommand(SuggestPreviousCommand);
		}

		private void SuggestNextCommand()
		{
			IsSuggesting = true;
			if (!SuggestionOn)
			{
				SuggestionOn = true;
				SuggestionBaseText = Text;
				SuggestionsCommands = new LinkedList<string>(AllSuggestions.Where(c => c.StartsWith(SuggestionBaseText)));
				CurrentSuggestion = SuggestionsCommands.First;

				if (CurrentSuggestion?.Value.IsFilled() == true)
				{
					Text = CurrentSuggestion.Value;
				}
			}
			else
			{
				CurrentSuggestion = CurrentSuggestion?.NextOrFirst();
				if (CurrentSuggestion?.Value.IsFilled() == true)
				{
					Text = CurrentSuggestion.Value;
				}
			}
			IsSuggesting = false;

			MoveCaretToEnd();
		}

		private void SuggestPreviousCommand()
		{
			IsSuggesting = true;
			CurrentSuggestion = CurrentSuggestion?.PreviousOrLast();
			if (CurrentSuggestion?.Value.IsFilled() == true)
			{
				Text = CurrentSuggestion.Value;
			}
			IsSuggesting = false;

			MoveCaretToEnd();
		}

		private void StopSuggestion()
		{
			SuggestionOn = false;
		}

		public void MoveCaretToEnd()
		{
			CaretIndex = Text.Length;
		}

		public void Clear()
		{
			Text = string.Empty;
		}
	}
}
