using System;
using System.Windows.Input;

namespace Logical_cxem.Commands
{
    public class Command : ICommand
    {
        private readonly Action _action;
        private readonly bool _canExecute;

        public Command(Action action, bool canExecute = true)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}