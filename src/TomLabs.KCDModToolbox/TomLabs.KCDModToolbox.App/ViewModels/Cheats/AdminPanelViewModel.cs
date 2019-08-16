using System.Collections.ObjectModel;
using System.Windows.Input;
using TomLabs.KCDModToolbox.Core;
using TomLabs.WPF.Tools;
using TomLabs.WPF.Tools.Commands;

namespace TomLabs.KCDModToolbox.App.ViewModels.Cheats
{
	public class AdminPanelViewModel : BaseViewModel
	{
		public string ConsoleInput { get; set; } = string.Empty;
		public ObservableCollection<ConsoleEntry> ConsoleOutput { get; set; } = new ObservableCollection<ConsoleEntry>();

		public ICommand RunCommandCmd { get; set; }

		public AdminPanelViewModel()
		{
			RunCommandCmd = new RelayCommand(RunCommand);
		}

		public void RunCommand()
		{
			// TODO send to bootloader/injector
			ScriptInjector.Instance.InjectCommand(ConsoleInput);
			ConsoleOutput.Add(new ConsoleEntry(ConsoleInput));
			ConsoleInput = string.Empty;
		}
	}
}
