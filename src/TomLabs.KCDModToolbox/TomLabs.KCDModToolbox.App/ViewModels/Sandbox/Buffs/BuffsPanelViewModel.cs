using System.Windows.Input;
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

		public BuffsPanelViewModel()
		{
			ToggleBuffImmortalCmd = new RelayCommand(ToggleBuffImmortal);
			ToggleBuffInvisibleCmd = new RelayCommand(ToggleBuffInvisible);
			HealCmd = new RelayCommand(async () => await CommandBuilder.AddBuffHeal().ExecuteAsync());
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
	}
}
