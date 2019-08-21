using System.Threading.Tasks;
using TomLabs.KCDModToolbox.Core.Sandbox;
using TomLabs.WPF.Tools;

namespace TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Locations
{
	public class LocationsPanelViewModel : BaseViewModel
	{
		public int Z { get; set; } = 50;

		public async Task TeleportPlayerAsync(double x, double y)
		{
			await CommandBuilder.TeleportPlayer((int)x, (int)y, Z).ExecuteAsync();
		}
	}
}
