using System;

namespace TomLabs.KCDModToolbox.Core.Database.Entities
{
	public abstract class GuidEntity : Entity
	{
		public Guid Id { get; set; }

		public GuidEntity()
		{

		}

		public GuidEntity(string id)
		{
			Id = Guid.Parse(id);
		}

		public GuidEntity(Guid id)
		{
			Id = id;
		}
	}
}
