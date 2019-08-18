using System;

namespace TomLabs.KCDModToolbox.Core.Database.Buffs
{
	public class Buff : GuidEntity
	{
		public string Name { get; set; }
		public string UIName { get; set; }
		public string LocalizedName { get; set; }

		public string DescriptionKey { get; set; }
		public string Description { get; set; }
		public int ClassId { get; set; }
		public BuffClass Class { get; set; }

		public Buff(string id, string name, string classId) : base(id)
		{
			Name = name;
			ClassId = int.Parse(classId);
		}

		public Buff(Guid id, string name, int classId) : base(id)
		{
			Name = name;
			ClassId = classId;
		}
	}
}
