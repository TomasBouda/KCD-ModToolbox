using System;

namespace TomLabs.KCDModToolbox.Core.Database.Buffs
{
	public class Buff
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public BuffClass Class { get; set; }

		public Buff(string id, string name)
		{
			Id = Guid.Parse(id);
			Name = name;
		}

		public Buff(Guid id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}
