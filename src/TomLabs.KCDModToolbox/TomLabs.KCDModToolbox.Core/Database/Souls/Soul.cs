namespace TomLabs.KCDModToolbox.Core.Database.Souls
{
	public class Soul : GuidEntity
	{
		public string Name { get; set; }

		public string UIName { get; set; }

		public string LocalizedName { get; set; }

		public Soul(string id, string name) : base(id)
		{
			Name = name;
		}
	}
}
