using System.Windows.Controls;
using TomLabs.KCDModToolbox.App.ViewModels.Cheats;

namespace TomLabs.KCDModToolbox.App.Views
{
	/// <summary>
	/// Interaction logic for AdminPanelView.xaml
	/// </summary>
	public partial class AdminPanelView : UserControl
	{
		private AdminPanelViewModel VM { get; set; }

		public AdminPanelView()
		{
			InitializeComponent();

			VM = new AdminPanelViewModel();
			DataContext = VM;
		}

		private void txtConsoleInput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			Scroller.ScrollToBottom();
		}

		private void ItemsControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			txtConsoleInput.Focus();
		}
	}
}
