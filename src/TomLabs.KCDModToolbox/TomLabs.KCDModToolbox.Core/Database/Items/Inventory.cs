using System;
using System.Collections.Generic;

namespace TomLabs.KCDModToolbox.Core.Database.Items
{
	public class Inventory : GuidEntity
	{
		public List<Item> Items { get; set; }

		public Inventory(Guid id) : base(id)
		{

		}
	}
}
