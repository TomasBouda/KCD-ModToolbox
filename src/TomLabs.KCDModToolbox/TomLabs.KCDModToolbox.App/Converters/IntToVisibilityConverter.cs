using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TomLabs.KCDModToolbox.App.Converters
{
	/// <summary>
	/// Converts int value into Visibility
	/// You must specify CoverterParameter="" to invert the value. For example >=20 or ==10
	/// </summary>
	[ValueConversion(typeof(int), typeof(Visibility))]
	public sealed class IntToVisibilityConverter : IntToBoolConverter
	{
		public Visibility TrueValue { get; set; }
		public Visibility FalseValue { get; set; }

		public IntToVisibilityConverter()
		{
			// set defaults
			TrueValue = Visibility.Visible;
			FalseValue = Visibility.Collapsed;
		}

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var res = (bool)base.Convert(value, targetType, parameter, culture);
			return res ? TrueValue : FalseValue;
		}
	}
}