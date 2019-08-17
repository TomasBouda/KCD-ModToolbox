using TomLabs.KCDModToolbox.Core.Database;
using TomLabs.WPF.Tools;

namespace TomLabs.KCDModToolbox.App.ViewModels.Database
{
	public class DatabaseViewModel : BaseViewModel
	{

		private DatabaseDescriptor db;

		public TableViewModel SelectedTable { get; set; }

		public DatabaseViewModel()
		{
			//var dl = new DataLoader(DbFilePath, WorkingDirectory);
			//db = dl.Database;
		}

		public void ShowTable(string tableName, bool loadRelations)
		{
			SelectedTable = new TableViewModel(db.GetTable(tableName, loadRelations));
		}
	}
}
