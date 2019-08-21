using System;

namespace TomLabs.KCDModToolbox.Core.Database.Items
{
	public class Weapon2WeaponPreset
	{
		public Guid ItemId { get; set; }

		public Guid WeaponPresetId { get; set; }

		public Item Item { get; set; }

		public Weapon2WeaponPreset(Guid itemId, Guid weaponPresetId)
		{
			WeaponPresetId = weaponPresetId;
			ItemId = itemId;
		}
	}
}
