using System.Windows.Input;
using TomLabs.KCDModToolbox.Core.Database.Buffs;
using TomLabs.KCDModToolbox.Core.Sandbox;
using TomLabs.WPF.Tools;
using TomLabs.WPF.Tools.Commands;

namespace TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Buffs
{
	public class BuffViewModel : BaseViewModel
	{
		public Buff Details { get; set; }

		public ICommand AddCmd { get; set; }
		public ICommand RemoveCmd { get; set; }

		public BuffViewModel(Buff buff)
		{
			Details = buff;

			AddCmd = new RelayCommand(async () => await CommandBuilder.AddBuff(Details.Id.ToString()).ExecuteAsync());
			RemoveCmd = new RelayCommand(async () => await CommandBuilder.RemoveBuff(Details.Id.ToString()).ExecuteAsync());
		}
	}
}
