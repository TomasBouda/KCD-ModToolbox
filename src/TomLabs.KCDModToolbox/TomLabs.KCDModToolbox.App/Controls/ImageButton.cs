using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TomLabs.KCDModToolbox.App.Controls
{
	public class ImageButton : Button
	{
		public static readonly DependencyProperty ButtonTextProperty =
			DependencyProperty.Register(nameof(ButtonText), typeof(string), typeof(ImageButton));

		public static readonly DependencyProperty ButtonCommandProperty =
			DependencyProperty.Register(nameof(ButtonCommand), typeof(ICommand), typeof(ImageButton));

		public static readonly DependencyProperty ImageSourceProperty =
			DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(ImageButton));

		public static readonly DependencyProperty CornerRadiusProperty =
			DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(ImageButton));

		public ICommand ButtonCommand
		{
			get => (ICommand)GetValue(ButtonCommandProperty);
			set => SetValue(ButtonCommandProperty, value);
		}

		public string ButtonText
		{
			get => (string)GetValue(ButtonTextProperty);
			set => SetValue(ButtonTextProperty, value);
		}

		public ImageSource ImageSource
		{
			get => (ImageSource)GetValue(ButtonTextProperty);
			set => SetValue(ButtonTextProperty, value);
		}

		public CornerRadius CornerRadius
		{
			get => (CornerRadius)GetValue(ButtonTextProperty);
			set => SetValue(ButtonTextProperty, value);
		}

		static ImageButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
		}
	}
}
