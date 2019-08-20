using System;

namespace TomLabs.KCDModToolbox.Core.Database.Souls
{
	public class Soul : GuidEntity
	{
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
		public Guid InventoryId { get; set; }
		public Guid HeadId { get; set; }
		public Guid HairId { get; set; }
		public Guid BrainId { get; set; }

		public Guid BodyId { get; set; }

		public int VIPClassId { get; set; }

		public Soul(string id, string name) : base(id)
		{
			Name = name;
		}
	}
}
