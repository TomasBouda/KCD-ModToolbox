using TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Console;
using TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Time;
using TomLabs.KCDModToolbox.Core.Sandbox;
using TomLabs.WPF.Tools;

namespace TomLabs.KCDModToolbox.App.ViewModels.Sandbox
{
	public class AdminPanelViewModel : BaseViewModel
	{
		public ConsoleViewModel Console { get; set; }

		public TimePanelViewModel TimePanel { get; set; } = new TimePanelViewModel();

		public AdminPanelViewModel(string kcdDirectory)
		{
			CommandExecutor.Instance.Initialize(kcdDirectory);
			Console = new ConsoleViewModel(kcdDirectory);
		}

	}
}
