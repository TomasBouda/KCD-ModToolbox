using System;
using System.Collections.Generic;

namespace TomLabs.KCDModToolbox.Core.Database
{
	public class Column
	{
		public bool IsKey { get; set; }

		public string Name { get; set; }

		public string TypeName { get; set; }

		public List<TableDescriptor> ReferencesTo { get; protected set; } = new List<TableDescriptor>();

		public Type Type { get; protected set; }

		[Obsolete("Only for serialization!")]
		public Column()
		{

		}

		public Column(string name, string typeName)
		{
			Name = name;
			TypeName = typeName;
		}

		public Column(string name, Type type)
		{
			Name = name;
			Type = type;
		}
	}
}
