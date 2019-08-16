using System.Windows.Controls;
using TomLabs.KCDModToolbox.App.ViewModels.Sandbox;

namespace TomLabs.KCDModToolbox.App.Views.Sandbox
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
	}
}
