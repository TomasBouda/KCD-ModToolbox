using System;

namespace TomLabs.KCDModToolbox.Core.Extensions
{
	public static class StringExtensions
	{
		public static Guid ToGuid(this string str)
		{
			return Guid.TryParse(str, out var res) ? res : Guid.Empty;
		}
	}
}
