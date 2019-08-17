using System.Windows.Controls;
using TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Console;

namespace TomLabs.KCDModToolbox.App.Views.Sandbox.Console
{
	/// <summary>
	/// Interaction logic for ConsoleView.xaml
	/// </summary>
	public partial class ConsoleView : UserControl
	{
		private ConsoleViewModel VM { get; set; }
		public ConsoleView()
		{
			InitializeComponent();

			VM = DataContext as ConsoleViewModel;
		}

		private void UserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
		{
			VM = DataContext as ConsoleViewModel;
		}

		private void txtConsoleInput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			Scroller.ScrollToBottom();
		}

		private void IcEntities_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
		{

		}
	}
}
