using System;
using System.Dynamic;
using TomLabs.KCDModToolbox.Core.Extensions;

namespace TomLabs.KCDModToolbox.Core.Database.Entities.Items
{
	public class Weapon2WeaponPreset : Entity
	{
		public const string TABLE_NAME = "weapon2weapon_preset";

		public Guid ItemId { get; set; }

		public Guid WeaponPresetId { get; set; }

		public Item Item { get; set; }

		public static Func<ExpandoObject, Weapon2WeaponPreset> Selector =>
			r => new Weapon2WeaponPreset(
					   r.AsDict("item_id").ToGuid(),
					   r.AsDict("weapon_preset_id").ToGuid()
				);

		public Weapon2WeaponPreset(Guid itemId, Guid weaponPresetId)
		{
			WeaponPresetId = weaponPresetId;
			ItemId = itemId;
		}
	}
}
