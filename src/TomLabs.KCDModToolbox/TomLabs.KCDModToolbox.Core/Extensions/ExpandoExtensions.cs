using System.Collections.Generic;
using System.Dynamic;

namespace TomLabs.KCDModToolbox.Core.Extensions
{
	public static class ExpandoExtensions
	{
		public static IDictionary<string, object> AsDict(this ExpandoObject obj)
		{
			return obj as IDictionary<string, object>;
		}

		public static object AsDict(this ExpandoObject obj, string key)
		{
			return obj.AsDict()[key];
		}
	}
}
