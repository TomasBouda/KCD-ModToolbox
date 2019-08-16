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

		private void ItemsControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			txtConsoleInput.Focus();
		}

		private void TxtConsoleInput_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == System.Windows.Input.Key.Down)
			{
				txtConsoleInput.CaretIndex = txtConsoleInput.Text.Length;
			}
			else if (e.Key == System.Windows.Input.Key.Up)
			{
				txtConsoleInput.CaretIndex = txtConsoleInput.Text.Length;
			}
		}
	}
}
