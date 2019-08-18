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
			VM.ConsoleOutput.Entries.CollectionChanged += Entries_CollectionChanged;
		}

		private void Entries_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			Scroller.ScrollToBottom();
		}
	}
}
