using System;

namespace TomLabs.KCDModToolbox.Core.Database.Entities.Items
{
	public class Inventory2Item : Entity
	{
		public Guid InventoryId { get; set; }

		public Guid ItemId { get; set; }

		public int Amount { get; set; }

		public Inventory2Item(Guid inventoryId, Guid itemId)
		{
			InventoryId = inventoryId;
			ItemId = itemId;
		}
	}
}
