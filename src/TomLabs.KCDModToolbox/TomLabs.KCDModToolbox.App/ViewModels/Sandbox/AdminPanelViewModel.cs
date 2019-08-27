using TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Buffs;
using TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Console;
using TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Items;
using TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Locations;
using TomLabs.KCDModToolbox.App.ViewModels.Sandbox.NPC;
using TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Time;
using TomLabs.KCDModToolbox.Core.Sandbox;
using TomLabs.WPF.Tools;

namespace TomLabs.KCDModToolbox.App.ViewModels.Sandbox
{
	public class AdminPanelViewModel : BaseViewModel
	{
		public ConsoleViewModel Console { get; set; }

		public TimePanelViewModel TimePanel { get; set; } = new TimePanelViewModel();

		public BuffsPanelViewModel BuffsPanel { get; set; } = new BuffsPanelViewModel();

		public NPCPanelViewModel NPCPanel { get; set; } = new NPCPanelViewModel();

		public ItemsPanelViewModel ItemsPanel { get; set; } = new ItemsPanelViewModel();

		public LocationsPanelViewModel LocationsPanel { get; set; } = new LocationsPanelViewModel();

		public AdminPanelViewModel(string kcdDirectory)
		{
			CommandExecutor.Instance.Initialize(kcdDirectory);
			Console = new ConsoleViewModel(kcdDirectory);
		}

	}
}
