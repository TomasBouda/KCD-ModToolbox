using TomLabs.KCDModToolbox.App.ViewModels.Sandbox;
using TomLabs.WPF.Tools;

namespace TomLabs.KCDModToolbox.App.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public AdminPanelViewModel AdminPanel { get; set; } = new AdminPanelViewModel(@"c:\Program Files (x86)\Steam\steamapps\common\KingdomComeDeliverance");

		public MainWindowViewModel()
		{

		}
	}
}
