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

		public int Faction { get; set; }

		public Guid InitialClothingPresetId { get; set; }

		public int VIPClassId { get; set; }

		public Soul(string id, string name) : base(id)
		{
			Name = name;
		}
	}
}
