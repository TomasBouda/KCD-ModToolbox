using System;
using TomLabs.KCDModToolbox.Core;
using TomLabs.WPF.Tools;

namespace TomLabs.KCDModToolbox.App.ViewModels
{
	public class DatabaseViewModel : BaseViewModel
	{
		private readonly string DbFilePath = @"c:\Program Files (x86)\Steam\steamapps\common\KingdomComeDeliverance\Data\Tables.pak";
		private readonly string WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "dbCache";

		private DatabaseDescriptor db;

		public TableViewModel SelectedTable { get; set; }

		public DatabaseViewModel()
		{
			var dl = new DataLoader(DbFilePath, WorkingDirectory);
			db = dl.GetDatabase();
		}

		public void ShowTable(string tableName, bool loadRelations)
		{
			SelectedTable = new TableViewModel(db.GetTable(tableName, loadRelations));
		}
	}
}
