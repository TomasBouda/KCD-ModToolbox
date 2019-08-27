using System;
using System.Dynamic;
using TomLabs.KCDModToolbox.Core.Extensions;

namespace TomLabs.KCDModToolbox.Core.Database.Entities.Buffs
{
	public class BuffClass : Entity, IEntity
	{
		public const string TABLE_NAME = "buff_class";

		public int Id { get; set; }

		public string Name { get; set; }

		public static Func<ExpandoObject, BuffClass> Selector =>
			r => new BuffClass(
					r.AsDict("buff_class_id").ToString(),
					r.AsDict("buff_class_name").ToString()
					);

		public BuffClass() { }

		public BuffClass(string id, string name)
		{
			Id = int.Parse(id);
			Name = name;
		}

		public BuffClass(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}
