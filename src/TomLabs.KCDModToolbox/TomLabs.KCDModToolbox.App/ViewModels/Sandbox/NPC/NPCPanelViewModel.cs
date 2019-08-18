using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TomLabs.KCDModToolbox.Core.Database;
using TomLabs.WPF.Tools;
using TomLabs.WPF.Tools.Commands;

namespace TomLabs.KCDModToolbox.App.ViewModels.Sandbox.NPC
{
	public class NPCPanelViewModel : BaseViewModel
	{
		public string SearchText { get; set; } = string.Empty;

		private List<NPCViewModel> AllNPCs { get; set; }
		public ObservableCollection<NPCViewModel> FilteredNPCs { get; set; }

		public ICommand SearchCmd { get; set; }

		public NPCPanelViewModel()
		{
			SearchCmd = new RelayCommand(async () => await SearchNPC());
		}

		private async Task LoadAllSouls()
		{
			AllNPCs = (await DataLoader.Instance.GetSoulsAsync()).Select(s => new NPCViewModel(s)).ToList();
		}

		private async Task SearchNPC()
		{
			if (AllNPCs == null)
			{
				await LoadAllSouls();
			}

			FilteredNPCs = new ObservableCollection<NPCViewModel>(
				AllNPCs.Where(n =>
					n.Details.LocalizedName?.Contains(SearchText, System.StringComparison.InvariantCultureIgnoreCase) == true
					||
					n.Details.Name.Contains(SearchText, System.StringComparison.InvariantCultureIgnoreCase)
					)
				);
		}
	}
}
