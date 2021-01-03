using System;
using System.Dynamic;
using TomLabs.KCDModToolbox.Core.Database.Attributes;
using TomLabs.KCDModToolbox.Core.Extensions;

namespace TomLabs.KCDModToolbox.Core.Database.Entities.Buffs
{
	public class Buff : GuidEntity
	{
		public const string TABLE_NAME = "buff";

		[Key]
		[ColumnName("buff_id")]
		public override Guid Id { get; set; }

		[ColumnName("buff_name")]
		public string Name { get; set; }

		[ColumnName("buff_ui_name")]
		public string UIName { get; set; }

		public string LocalizedName { get; set; }

		[ColumnName("buff_desc")]
		public string DescriptionKey { get; set; }

		public string Description { get; set; }

		[ColumnName("buff_class_id")]
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

		public override void Save()
		{
			DataLoader.Instance.Database.GetTable(TABLE_NAME).SaveEntity(this);

			Class.Save();
		}
	}
}
