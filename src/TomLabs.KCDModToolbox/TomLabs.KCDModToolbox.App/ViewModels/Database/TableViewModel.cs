using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TomLabs.KCDModToolbox.Core;
using TomLabs.Shadowgem.Extensions.String;
using TomLabs.WPF.Tools;
using TomLabs.WPF.Tools.Commands;

namespace TomLabs.KCDModToolbox.App.ViewModels.Database
{
	public class TableViewModel : BaseViewModel
	{
		public TableDescriptor Details { get; set; }

		public ObservableCollection<DataGridColumn> Columns { get; set; } = new ObservableCollection<DataGridColumn>();

		public ObservableCollection<RowViewModel> Rows { get; set; }

		public ObservableCollection<TableViewModel> RelatedTables { get; set; }

		public RowViewModel SelectedRow { get; set; }

		public ICommand LoadDataCmd { get; set; }

		public TableViewModel(TableDescriptor tableDescriptor)
		{
			Details = tableDescriptor;
			LoadColumns();

			LoadDataCmd = new RelayCommand(LoadData);
		}

		public void LoadData()
		{
			Task.Run(() =>
			{
				Details.LoadTableData();

				Rows = new ObservableCollection<RowViewModel>(Details.Rows.Select(r => new RowViewModel(r, this)));
			});
		}

		private TableViewModel LoadData(string columnName, string value)
		{
			if (value.IsFilled())
			{
				Task.Run(() =>
				{
					Details.LoadTableData(columnName, value.ToString());

					Rows = new ObservableCollection<RowViewModel>(Details.Rows.Select(r => new RowViewModel(r, this)));
				});
			}

			return this;
		}

		private TableViewModel LoadColumns()
		{
			foreach (var col in Details.Columns)
			{
				Columns.Add(new DataGridTextColumn
				{
					Header = col.Name,
					IsReadOnly = col.IsKey,
					Binding = new System.Windows.Data.Binding($"{nameof(RowViewModel.RawData)}.{col.Name}")
				});
			}

			return this;
		}

		public void LoadRelatedTables()
		{
			RelatedTables = new ObservableCollection<TableViewModel>(Details
				.Columns
				.Where(c => c.ReferencesTo != null)
				.SelectMany(c =>
					c.ReferencesTo.Select(t => new TableViewModel(t).LoadData(c.Name, ((IDictionary<string, object>)SelectedRow.RawData)[c.Name].ToString()))
					));
		}
	}
}
