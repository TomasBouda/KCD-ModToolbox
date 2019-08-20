using System;

namespace TomLabs.KCDModToolbox.Core.Extensions
{
	public static class StringExtensions
	{
		public static Guid ToGuid(this string str)
		{
			return Guid.TryParse(str, out var res) ? res : Guid.Empty;
		}

		public static bool ToBool(this string str)
		{
			return bool.TryParse(str, out var res) ? res : false;
		}
	}
}
