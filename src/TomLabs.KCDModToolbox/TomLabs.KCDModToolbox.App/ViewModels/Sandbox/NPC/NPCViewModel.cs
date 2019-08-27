using System.Windows.Input;
using TomLabs.KCDModToolbox.Core.Database.Entities.Souls;
using TomLabs.KCDModToolbox.Core.Sandbox;
using TomLabs.WPF.Tools;
using TomLabs.WPF.Tools.Commands;

namespace TomLabs.KCDModToolbox.App.ViewModels.Sandbox.NPC
{
	public class NPCViewModel : BaseViewModel
	{
		public string Name => Details.LocalizedName ?? Details.Name;
		public Soul Details { get; set; }

		public bool IsFavorite { get; set; }

		public ICommand SpawnCmd { get; set; }
		public ICommand ToggleFavoriteCmd { get; set; }

		public NPCViewModel(Soul soul)
		{
			Details = soul;

			SpawnCmd = new RelayCommand(async () => await CommandBuilder.SpawnNpc(Details.Id.ToString()).ExecuteAsync());
			ToggleFavoriteCmd = new RelayCommand(() => IsFavorite = !IsFavorite);
		}
	}
}
