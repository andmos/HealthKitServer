using System;
using System.Windows.Input;
namespace HealthKitServer
{
	public class DelegateCommand : ICommand
	{
		private readonly Func<bool> m_canExecute;
		private readonly Action<object> m_execute;
		public DelegateCommand(Action<object> execute, Func<bool> canExecute)
		{
			m_canExecute = canExecute;
			m_execute = execute;
		}
		public event EventHandler CanExecuteChanged;
		public bool CanExecute(object parameter)
		{
			return m_canExecute();
		}
		public void Execute(object parameter)
		{
			m_execute(parameter);
		}

		public void RaiseCanExecuteChanged()
		{
			if (CanExecuteChanged != null)
			{
				CanExecuteChanged(this, new EventArgs());
			}
		}
	}
}