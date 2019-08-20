using System;

namespace TomLabs.KCDModToolbox.Core.Extensions
{
	public static class ObjectExtensions
	{
		public static Guid ToGuid(this object o)
		{
			return o.ToString().ToGuid();
		}
	}
}
