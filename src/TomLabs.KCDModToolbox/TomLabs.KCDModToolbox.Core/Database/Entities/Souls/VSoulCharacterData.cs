using System;
using System.Dynamic;
using TomLabs.KCDModToolbox.Core.Extensions;

namespace TomLabs.KCDModToolbox.Core.Database.Entities.Souls
{
	public class VSoulCharacterData : GuidEntity, IEntity
	{
		public const string TABLE_NAME = "v_soul_character_data";

		public string UIName { get; set; }

		public static Func<ExpandoObject, VSoulCharacterData> Selector =>
			r => new VSoulCharacterData(
					r.AsDict("soul_id").ToString(),
					r.AsDict("name_string_id").ToString()
				);

		public VSoulCharacterData() { }

		public VSoulCharacterData(string id, string uiName) : base(id)
		{
			UIName = uiName;
		}

		public VSoulCharacterData(Guid id, string uiName) : base(id)
		{
			UIName = uiName;
		}
	}
}
