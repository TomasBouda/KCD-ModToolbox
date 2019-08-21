using System;
using TomLabs.KCDModToolbox.App.ViewModels.Sandbox;
using TomLabs.KCDModToolbox.Core.Database;
using TomLabs.WPF.Tools;

namespace TomLabs.KCDModToolbox.App.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		private string KCDDirectory { get; set; }
		private string DbWorkingDirectory { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "dbCache";

		public AdminPanelViewModel AdminPanel { get; set; }

		public MainWindowViewModel()
		{
			KCDDirectory = @"c:\Program Files (x86)\Steam\steamapps\common\KingdomComeDeliverance";

			DataLoader.Instance.SetPaths(KCDDirectory, DbWorkingDirectory, ELocalizations.Czech);
			AdminPanel = new AdminPanelViewModel(KCDDirectory);
		}
	}
}
