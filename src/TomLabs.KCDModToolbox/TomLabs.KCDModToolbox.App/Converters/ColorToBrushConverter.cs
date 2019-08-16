using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TomLabs.KCDModToolbox.App.Converters
{
	[ValueConversion(typeof(Color), typeof(Brush))]
	public sealed class ColorToBrushConverter : IValueConverter
	{
		public ColorToBrushConverter()
		{
		}

		public object Convert(object value, Type targetType,
			object parameter, CultureInfo culture)
		{
			if (!(value is Color))
			{
				return null;
			}

			return new SolidColorBrush((Color)value);
		}

		public object ConvertBack(object value, Type targetType,
			object parameter, CultureInfo culture)
		{
			var brush = value as Brush;

			return ((SolidColorBrush)brush).Color;
		}
	}
}
