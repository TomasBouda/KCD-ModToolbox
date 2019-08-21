using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Locations;

namespace TomLabs.KCDModToolbox.App.Views.Sandbox.Locations
{
	/// <summary>
	/// Interaction logic for LocationsPanelView.xaml
	/// </summary>
	public partial class LocationsPanelView : UserControl
	{
		private LocationsPanelViewModel VM { get; set; }

		public LocationsPanelView()
		{
			InitializeComponent();
		}

		private void UserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
		{
			VM = DataContext as LocationsPanelViewModel;
		}

		private void Image_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
			{
				var image = sender as Image;
				var mousePos = Mouse.GetPosition(image);

				var x = mousePos.X * 5;
				var y = (image.ActualWidth - mousePos.Y) * 5;

				Debug.WriteLine($"X:{x} Y:{y}");

				VM.TeleportPlayerAsync(x, y);
			}
		}
	}
}
