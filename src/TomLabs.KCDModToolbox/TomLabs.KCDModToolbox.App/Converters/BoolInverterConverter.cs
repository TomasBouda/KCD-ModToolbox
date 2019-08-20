using System;
using System.Globalization;
using System.Windows.Data;

namespace TomLabs.KCDModToolbox.App.Converters
{
	/// <summary>
	/// Inverts boolean value. Works like !value
	/// </summary>
	[ValueConversion(typeof(bool), typeof(bool))]
	public sealed class BoolInverterConverter : IValueConverter
	{
		public BoolInverterConverter()
		{
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
			{
				return null;
			}

			return !((bool)value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (Equals(value, true))
			{
				return false;
			}

			if (Equals(value, false))
			{
				return true;
			}

			return null;
		}
	}
}