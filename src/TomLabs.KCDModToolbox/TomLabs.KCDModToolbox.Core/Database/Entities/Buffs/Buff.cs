using System;
using System.Dynamic;
using TomLabs.KCDModToolbox.Core.Extensions;

namespace TomLabs.KCDModToolbox.Core.Database.Entities.Buffs
{
	public class Buff : GuidEntity, IEntity
	{
		public const string TABLE_NAME = "buff";

		public string Name { get; set; }

		public string UIName { get; set; }

		public string LocalizedName { get; set; }

		public string DescriptionKey { get; set; }

		public string Description { get; set; }

		public int ClassId { get; set; }

		public BuffClass Class { get; set; }

		public static Func<ExpandoObject, Buff> Selector =>
			r => new Buff(r.AsDict("buff_id").ToString(), r.AsDict("buff_name").ToString(), r.AsDict("buff_class_id").ToString())
			{
				UIName = r.AsDict("buff_ui_name").ToString(),
				DescriptionKey = r.AsDict("buff_desc").ToString(),
			};

		public Buff() { }

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
