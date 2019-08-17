using System.ComponentModel;
using System.Windows.Input;
using TomLabs.WPF.Tools.Commands;

namespace TomLabs.WPF.Tools
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// The event that is fired when any child property changes its value
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		public ICommand OpenUrlCmd { get; set; }

		public BaseViewModel()
		{
			OpenUrlCmd = new RelayCommand<string>(OpenUrl);
		}
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void OpenUrl(string url)
		{
			Utils.OpenBrowser(url);
		}
	}
}