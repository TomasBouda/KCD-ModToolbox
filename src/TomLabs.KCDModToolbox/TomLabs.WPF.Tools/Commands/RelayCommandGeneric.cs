using System;
using System.Windows.Input;

namespace TomLabs.WPF.Tools.Commands
{
	public class RelayCommand<T> : RelayCommandBase, ICommand
	{
		private Action<T> mAction;

		/// <summary>
		/// Default constructor
		/// </summary>
		public RelayCommand(Action<T> action) : base(null)
		{
			mAction = action;
		}

		public RelayCommand(Action<T> action, Func<bool> canExecFunc) : base(canExecFunc)
		{
			mAction = action;
		}

		/// <summary>
		/// Executes the commands Action
		/// </summary>
		/// <param name="parameter"></param>
		public void Execute(object parameter)
		{
			mAction((T)parameter);
		}
	}
}