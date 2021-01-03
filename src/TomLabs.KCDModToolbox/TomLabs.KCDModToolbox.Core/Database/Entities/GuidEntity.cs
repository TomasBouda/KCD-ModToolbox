using System;
using TomLabs.KCDModToolbox.Core.Database.Attributes;

namespace TomLabs.KCDModToolbox.Core.Database.Entities
{
	public abstract class GuidEntity : Entity
	{
		[Key]
		public virtual Guid Id { get; set; }

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
