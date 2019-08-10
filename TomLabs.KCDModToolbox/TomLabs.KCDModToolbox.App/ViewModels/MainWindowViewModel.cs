using TomLabs.WPF.Tools;

namespace TomLabs.KCDModToolbox.App.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public DatabaseViewModel Database { get; set; }

		public MainWindowViewModel()
		{
			Database = new DatabaseViewModel();
		}
	}
}
