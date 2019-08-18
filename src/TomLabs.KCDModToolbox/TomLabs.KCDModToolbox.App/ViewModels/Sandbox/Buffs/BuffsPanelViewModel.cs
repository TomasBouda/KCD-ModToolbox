using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TomLabs.KCDModToolbox.Core.Database;
using TomLabs.KCDModToolbox.Core.Sandbox;
using TomLabs.WPF.Tools;
using TomLabs.WPF.Tools.Commands;

namespace TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Buffs
{
	public class BuffsPanelViewModel : BaseViewModel
	{
		public bool Immortal { get; set; }
		public bool Invisible { get; set; }

		public ICommand ToggleBuffImmortalCmd { get; set; }
		public ICommand ToggleBuffInvisibleCmd { get; set; }
		public ICommand HealCmd { get; set; }

		public ICommand LoadAllBuffsCmd { get; set; }

		public ObservableCollection<BuffViewModel> AllBuffs { get; set; }

		public BuffsPanelViewModel()
		{
			ToggleBuffImmortalCmd = new RelayCommand(ToggleBuffImmortal);
			ToggleBuffInvisibleCmd = new RelayCommand(ToggleBuffInvisible);
			HealCmd = new RelayCommand(async () => await CommandBuilder.AddBuffHeal().ExecuteAsync());
			LoadAllBuffsCmd = new RelayCommand(async () => await LoadAllBuffsAsync());
		}

		private async void ToggleBuffImmortal()
		{
			if (!Immortal)
			{
				await CommandBuilder.AddBuffImmortal().ExecuteAsync();
			}
			else
			{
				await CommandBuilder.RemoveBuffImmortal().ExecuteAsync();
			}

			Immortal = !Immortal;
		}

		private async void ToggleBuffInvisible()
		{
			if (!Invisible)
			{
				await CommandBuilder.AddBuffInvisible().ExecuteAsync();
			}
			else
			{
				await CommandBuilder.RemoveBuffInvisible().ExecuteAsync();
			}

			Invisible = !Invisible;
		}

		private async Task LoadAllBuffsAsync()
		{
			var buffs = await DataLoader.Instance.GetBuffsAsync();
			AllBuffs = new ObservableCollection<BuffViewModel>(buffs.OrderBy(b => b.Name).Select(b => new BuffViewModel(b)));
		}
	}
}
