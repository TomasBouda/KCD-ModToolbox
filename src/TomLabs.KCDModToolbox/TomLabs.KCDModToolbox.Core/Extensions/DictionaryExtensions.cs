using System.Collections.Generic;

namespace TomLabs.KCDModToolbox.Core.Extensions
{
	public static class DictionaryExtensions
	{
		public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
		{
			return dict.TryGetValue(key, out var val) ? val : defaultValue;
		}
	}
}
