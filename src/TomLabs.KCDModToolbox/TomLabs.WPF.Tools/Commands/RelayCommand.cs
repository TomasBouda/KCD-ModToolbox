using System;
using System.Windows.Input;

namespace TomLabs.WPF.Tools.Commands
{
	public class RelayCommand : RelayCommandBase, ICommand
	{
		private Action _mAction;

		/// <summary>
		/// Default constructor
		/// </summary>
		public RelayCommand(Action action) : base(null)
		{
			_mAction = action;
		}

		public RelayCommand(Action action, Func<bool> canExecFunc) : base(canExecFunc)
		{
			_mAction = action;
		}

		/// <summary>
		/// Executes the commands Action
		/// </summary>
		/// <param name="parameter"></param>
		public void Execute(object parameter)
		{
			_mAction();
		}
	}
}