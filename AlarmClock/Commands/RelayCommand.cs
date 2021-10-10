using System;
using System.Windows.Input;

namespace AlarmClock.Commands
{
    class RelayCommand : ICommand
    {
        private readonly Action<object> ExecuteAction;
        private readonly Predicate<object> CanExecuteAction;

        public RelayCommand(Action<object> execute)
            : this(execute, _ => true)
        {
        }
        public RelayCommand(Action<object> action, Predicate<object> canExecute)
        {
            ExecuteAction = action;
            CanExecuteAction = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteAction(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteAction(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
