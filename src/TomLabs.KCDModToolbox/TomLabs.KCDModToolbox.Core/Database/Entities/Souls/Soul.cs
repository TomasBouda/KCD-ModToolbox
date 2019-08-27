using System;
using System.Dynamic;
using System.Linq;
using TomLabs.KCDModToolbox.Core.Database.Entities.Items;
using TomLabs.KCDModToolbox.Core.Extensions;
using TomLabs.Shadowgem.Extensions.String;

namespace TomLabs.KCDModToolbox.Core.Database.Entities.Souls
{
	public class Soul : GuidEntity, IWithRelations, IEntity
	{
		public const string TABLE_NAME = "soul";

		public string Name { get; set; }

		public string UIName { get; set; }

		public string LocalizedName { get; set; }

		public int CombatLevel { get; set; }

		public int Courage { get; set; }

		public int Vision { get; set; }

		public int Faction { get; set; }

		public int Strength { get; set; }

		public int Agility { get; set; }

		public int Vitality { get; set; }

		public int Speech { get; set; }

		public int Hearing { get; set; }

		public int Charisma { get; set; }

		public double Scale { get; set; }

		public Guid InitialClothingPresetId { get; set; }

		public Guid InitialWeaponPresetId { get; set; }
		public Weapon2WeaponPreset InitialWeaponPreset { get; set; }

		public Guid InventoryId { get; set; }

		public Guid HeadId { get; set; }

		public Guid HairId { get; set; }

		public Guid BrainId { get; set; }

		public Guid BodyId { get; set; }

		public int VIPClassId { get; set; }

		public static Func<ExpandoObject, Soul> Selector =>
			r => new Soul(r.AsDict("soul_id").ToString(), r.AsDict("soul_name").ToString())
			{
				CombatLevel = r.AsDict("combat_level").ToString().ToInt(),
				Courage = r.AsDict("courage").ToString().ToInt(),
				Faction = r.AsDict("faction").ToString().ToInt(),
				Strength = r.AsDict("str").ToString().ToInt(),
				Agility = r.AsDict("agi").ToString().ToInt(),
				Vitality = r.AsDict("vit").ToString().ToInt(),
				Speech = r.AsDict("spc").ToString().ToInt(),
				Hearing = r.AsDict("hearing").ToString().ToInt(),
				Vision = r.AsDict("vision").ToString().ToInt(),
				Charisma = r.AsDict("charisma").ToString().ToInt(),
				VIPClassId = r.AsDict("soul_vip_class_id").ToString().ToInt(),
				InitialClothingPresetId = r.AsDict("initial_clothing_preset_id").ToGuid(),
				InitialWeaponPresetId = r.AsDict("initial_weapon_preset_id").ToGuid(),
				InventoryId = r.AsDict("inventory_id").ToGuid(),
				HairId = r.AsDict("character_hair_id").ToGuid(),
				BrainId = r.AsDict("brain_id").ToGuid(),
				HeadId = r.AsDict("character_head_id").ToGuid(),
				BodyId = r.AsDict("character_body_id").ToGuid(),
			};

		public Soul() { }

		public Soul(string id, string name) : base(id)
		{
			Name = name;
		}

		public void LoadRelations()
		{
			InitialWeaponPreset = DataLoader.Instance.GetWeapon2WeaponPresets().FirstOrDefault(p => p.WeaponPresetId == InitialWeaponPresetId);
		}
	}
}
