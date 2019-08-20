using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace TomLabs.LookForBrick.App.Converters
{
	public class BoolMultiValueConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var bits = values.Where(v => v is bool).Select(v => (bool)v);
			bool? andOperator = (string)parameter == "AND"
				? true
				: (string)parameter == "OR"
					? false
					: (bool?)null;

			if (andOperator == null)
			{
				throw new ArgumentException("You need to pass 'parameter' to BoolMultiValueConverter");
			}

			bool boolValue = andOperator.Value
				? bits.All(b => b == true)
				: bits.Any(b => b == true);

			return boolValue;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException($"{nameof(BoolMultiValueConverter)} doesn't support ConvertBack method.");
		}
	}
}
