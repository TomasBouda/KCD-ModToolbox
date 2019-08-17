using TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Console;
using TomLabs.WPF.Tools;

namespace TomLabs.KCDModToolbox.App.ViewModels.Sandbox
{
	public class AdminPanelViewModel : BaseViewModel
	{
		public ConsoleViewModel Console { get; set; }

		public AdminPanelViewModel(string kcdDirectory)
		{
			Console = new ConsoleViewModel(kcdDirectory);
		}
	}
}
