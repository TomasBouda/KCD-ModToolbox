using System.Collections.Generic;

namespace TomLabs.KCDModToolbox.Core.Database.Localizations
{
	public class LocalizationsLoader
	{
		private DatabaseDescriptor Database { get; set; }

		public LocalizationsLoader(DatabaseDescriptor database)
		{
			Database = database;
		}

		public IDictionary<string, string> GetSoulLocalizations()
		{
			return Database.GetLocalizationTable("text_ui_soul")?.Rows;
		}

		public IDictionary<string, string> GetItemLocalizations()
		{
			return Database.GetLocalizationTable("text_ui_items")?.Rows;
		}
	}
}
