using System.Dynamic;
using System.Windows.Input;
using TomLabs.WPF.Tools;
using TomLabs.WPF.Tools.Commands;

namespace TomLabs.KCDModToolbox.App.ViewModels.Database
{
	public class RowViewModel : BaseViewModel
	{
		public TableViewModel BaseTable { get; set; }

		public bool IsEdited { get; set; }
		public ExpandoObject RawData { get; set; }

		public ICommand SaveCmd { get; set; }
		public ICommand LoadRelatedDataCmd { get; set; }

		public RowViewModel(ExpandoObject rawData, TableViewModel table)
		{
			RawData = rawData;
			BaseTable = table;

			LoadRelatedDataCmd = new RelayCommand(LoadRelatedData);
		}

		public void LoadRelatedData()
		{
			BaseTable.LoadRelatedTables();
		}

		private void Save()
		{

		}
	}
}
