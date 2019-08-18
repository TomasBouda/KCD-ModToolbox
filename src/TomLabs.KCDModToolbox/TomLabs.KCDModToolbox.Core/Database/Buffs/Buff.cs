using System;

namespace TomLabs.KCDModToolbox.Core.Database.Buffs
{
	public class Buff
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int ClassId { get; set; }
		public BuffClass Class { get; set; }

		public Buff(string id, string name, string classId)
		{
			Id = Guid.Parse(id);
			Name = name;
			ClassId = int.Parse(classId);
		}

		public Buff(Guid id, string name, int classId)
		{
			Id = id;
			Name = name;
			ClassId = classId;
		}
	}
}
