using System;
using System.Windows.Input;

namespace DiningRoomMenu
{
    public class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly Action action;
        private readonly Predicate<object> canExecute;

        public DelegateCommand(Action action)
        {
            this.action = action;
            this.canExecute = obj => true;
        }

        public DelegateCommand(Action action, Predicate<object> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
