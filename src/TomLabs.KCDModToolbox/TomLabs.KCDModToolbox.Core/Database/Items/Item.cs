using System;

namespace TomLabs.KCDModToolbox.Core.Database.Items
{
	public class Item : GuidEntity
	{
		public string Name { get; set; }

		public string LocalizedName { get; set; }

		public Item(Guid id, string name) : base(id)
		{
			Name = name;
		}
	}
}
