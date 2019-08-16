using System.Windows.Input;
using TomLabs.WPF.Tools;
using TomLabs.WPF.Tools.Commands;

namespace TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Console
{
	public class ConsoleViewModel : BaseViewModel
	{
		public ConsoleInputViewModel ConsoleInput { get; set; } = new ConsoleInputViewModel();

		public ConsoleOutputViewModel ConsoleOutput { get; set; } = new ConsoleOutputViewModel();

		public ICommand RunCommandCmd { get; set; }
		public ICommand InsertPreviousCommandCmd { get; set; }
		public ICommand InsertNextCommandCmd { get; set; }
		public ICommand ClearInputCmd { get; set; }
		public ICommand ClearConsoleCmd { get; set; }

		public ICommand SuggestCmd { get; set; }

		public ConsoleViewModel()
		{
			RunCommandCmd = new RelayCommand(RunCommand);
			InsertPreviousCommandCmd = new RelayCommand(InsertPreviousCommand);
			InsertNextCommandCmd = new RelayCommand(InsertNextCommand);
			ClearInputCmd = new RelayCommand(ClearInput);
			ClearConsoleCmd = new RelayCommand(ClearConsole);
			SuggestCmd = new RelayCommand(() => ConsoleInput.Intelisense());
		}

		public void RunCommand()
		{
			ConsoleOutput.AddAndInject(ConsoleInput.Text);
			ClearInput();
		}

		public void ClearInput()
		{
			ConsoleInput.Clear();
		}

		public void ClearConsole()
		{
			ConsoleOutput.Clear();
		}

		private void InsertPreviousCommand()
		{
			ConsoleInput.Text = ConsoleOutput.PreviousCommand().CommandText;
		}

		private void InsertNextCommand()
		{
			ConsoleInput.Text = ConsoleOutput.NextCommand().CommandText;
		}
	}
}
