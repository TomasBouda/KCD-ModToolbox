using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TomLabs.KCDModToolbox.App.Converters
{
	/// <summary>
	/// Converts boolean value into Visibility
	/// You can specify CoverterParameter="!" to invert the value. Works exactly same like !booleanValue
	/// </summary>
	[ValueConversion(typeof(bool), typeof(Visibility))]
	public sealed class BoolToVisibilityConverter : IValueConverter
	{
		public Visibility TrueValue { get; set; }
		public Visibility FalseValue { get; set; }

		public BoolToVisibilityConverter()
		{
			// set defaults
			TrueValue = Visibility.Visible;
			FalseValue = Visibility.Collapsed;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
			{
				return null;
			}

			bool boolValue = parameter != null && (string)parameter == "!" ? !(bool)value : (bool)value;
			return boolValue ? TrueValue : FalseValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool boolValue = parameter != null && (string)parameter == "!" ? !(bool)value : (bool)value;

			if (Equals(boolValue, TrueValue))
			{
				return true;
			}

			if (Equals(boolValue, FalseValue))
			{
				return false;
			}

			return null;
		}
	}
}
