using System;

namespace TomLabs.KCDModToolbox.Core.Database.Items
{
	public class Inventory2InventoryPreset
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
