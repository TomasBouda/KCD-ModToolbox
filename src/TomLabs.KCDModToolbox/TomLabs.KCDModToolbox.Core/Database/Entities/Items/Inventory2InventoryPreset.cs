using System;

namespace TomLabs.KCDModToolbox.Core.Database.Entities.Items
{
	public class Inventory2InventoryPreset : Entity
	{
		public Guid InventoryId { get; set; }

		public Guid InventoryPresetId { get; set; }

		public Inventory2InventoryPreset(Guid inventoryId, Guid inventoryPresetId)
		{
			InventoryId = inventoryId;
			InventoryPresetId = inventoryPresetId;
		}
	}
}
