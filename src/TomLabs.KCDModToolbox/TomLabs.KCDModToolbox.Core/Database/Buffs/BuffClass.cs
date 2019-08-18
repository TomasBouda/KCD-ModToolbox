namespace TomLabs.KCDModToolbox.Core.Database.Buffs
{
	public class BuffClass
	{
		public int Id { get; set; }
		public string Name { get; set; }

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
