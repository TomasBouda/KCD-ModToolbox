using System.Windows;
using System.Windows.Controls;

namespace TomLabs.KCDModToolbox.App.Controls
{
	public class ConsoleTextBox : TextBox
	{
		public static readonly DependencyProperty CaretPositionProperty =
			DependencyProperty.Register("CaretPosition", typeof(int), typeof(ConsoleTextBox),
				new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnCaretPositionChanged));

		public int CaretPosition
		{
			get { return (int)GetValue(CaretPositionProperty); }
			set { SetValue(CaretPositionProperty, value); }
		}

		public ConsoleTextBox()
		{
			SelectionChanged += (s, e) => CaretPosition = CaretIndex;
		}

		private static void OnCaretPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			(d as ConsoleTextBox).CaretIndex = (int)e.NewValue;
		}
	}
}
