using System;

namespace TomLabs.KCDModToolbox.Core.Database.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	internal class ColumnNameAttribute : Attribute
	{
		public string Name { get; set; }

		public ColumnNameAttribute(string name)
		{
			Name = name;
		}
	}
}
