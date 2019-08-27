using System;
using System.Dynamic;
using TomLabs.KCDModToolbox.Core.Extensions;

namespace TomLabs.KCDModToolbox.Core.Database.Entities.Items
{
	public class Item : GuidEntity, IEntity
	{
		public const string TABLE_NAME = "item";

		public string Name { get; set; }

		public string LocalizedName { get; set; }

		public static Func<ExpandoObject, Item> Selector =>
			r => new Item(r.AsDict("item_id").ToGuid(), r.AsDict("item_name").ToString());

		public Item(Guid id, string name) : base(id)
		{
			Name = name;
		}
	}
}
