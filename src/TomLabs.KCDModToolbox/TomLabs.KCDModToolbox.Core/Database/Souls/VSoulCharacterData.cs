using System;

namespace TomLabs.KCDModToolbox.Core.Database.Souls
{
	public class VSoulCharacterData : GuidEntity
	{
		public string UIName { get; set; }

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
