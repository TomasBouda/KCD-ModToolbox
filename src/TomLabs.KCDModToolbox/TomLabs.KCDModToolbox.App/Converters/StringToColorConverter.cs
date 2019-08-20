using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TomLabs.KCDModToolbox.App.Converters
{
	[ValueConversion(typeof(string), typeof(Color))]
	public sealed class StringToColorConverter : IValueConverter
	{
		public StringToColorConverter()
		{
		}

		public object Convert(object value, Type targetType,
			object parameter, CultureInfo culture)
		{
			if (!(value is string))
			{
				return null;
			}

			return (Color)ColorConverter.ConvertFromString((string)value);
		}

		public object ConvertBack(object value, Type targetType,
			object parameter, CultureInfo culture)
		{
			var color = (Color)value;

			return color.ToString();
		}
	}
}
