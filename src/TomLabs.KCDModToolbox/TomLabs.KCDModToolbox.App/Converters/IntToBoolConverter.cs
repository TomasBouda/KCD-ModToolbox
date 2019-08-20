using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using TomLabs.Shadowgem.Extensions.String;

namespace TomLabs.KCDModToolbox.App.Converters
{
	/// <summary>
	/// Converts int value into Visibility
	/// You must specify CoverterParameter="" to invert the value. For example >=20 or ==10
	/// </summary>
	[ValueConversion(typeof(int), typeof(Visibility))]
	public class IntToBoolConverter : IValueConverter
	{
		public IntToBoolConverter()
		{
		}

		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is int) || parameter == null || !(parameter is string))
			{
				return false;
			}

			var intValue = (int)value;
			var groups = Regex.Match((string)parameter, @"(?<mark>[=><]+)(?<digit>\d+)").Groups;
			int paramValue = groups["digit"].Value.ToInt();
			string mark = groups["mark"].Value;
			bool greater = mark.Contains(">");
			bool lower = mark.Contains("<");
			bool equals = mark.Contains("=");

			return equals
					? intValue == paramValue
					: greater
						? intValue > paramValue
						: lower
							? intValue < paramValue
							: false;
		}

		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}