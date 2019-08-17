using System;

namespace TomLabs.WPF.Tools.Commands
{
	public abstract class RelayCommandBase
	{
		private Func<bool> _canExecFunc;

		#region Public Events

		public event EventHandler CanExecuteChanged = (sender, e) => { };

		#endregion Public Events

		public RelayCommandBase(Func<bool> canExecFunc)
		{
			_canExecFunc = canExecFunc;
		}

		public bool CanExecute(object parameter)
		{
			if (_canExecFunc != null)
			{
				return _canExecFunc();
			}

			return true;
		}

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, new EventArgs());
		}
	}
}