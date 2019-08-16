using System;
using System.Windows;

namespace TomLabs.KCDModToolbox.App
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static void RunOnUIThread(Action action)
		{
			Current.Dispatcher.Invoke(action);
		}
	}
}
