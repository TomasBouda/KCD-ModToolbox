using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using TomLabs.KCDModToolbox.Core.Extensions;

namespace TomLabs.KCDModToolbox.App.Converters
{
	/// <summary>
	/// Converter for multiple boolean values
	/// Use ConverterParameter="AND|OR" for specifying converter logic operator
	/// </summary>
	[ValueConversion(typeof(bool), typeof(Visibility))]
	public sealed class BoolToVisibilityMultiConverter : IMultiValueConverter
	{
		public Visibility TrueValue { get; set; }
		public Visibility FalseValue { get; set; }

		public BoolToVisibilityMultiConverter()
		{
			// set defaults
			TrueValue = Visibility.Visible;
			FalseValue = Visibility.Collapsed;
		}

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var bits = values.Select(v => v
				.ToString()
				.ToBool());
			bool? andOperator = (string)parameter == "AND"
				? true
				: (string)parameter == "OR"
					? false
					: (bool?)null;

			if (andOperator == null)
			{
				return TrueValue;
			}

			bool boolValue = andOperator.Value
				? bits.All(b => b == true)
				: bits.Any(b => b == true);

			return boolValue ? TrueValue : FalseValue;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}