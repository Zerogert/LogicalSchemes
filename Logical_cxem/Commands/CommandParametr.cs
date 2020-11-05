using System;
using System.Windows.Input;

namespace Logical_cxem
{
    public class CommandParametr : ICommand
    {
        private readonly Action<object> _action;
        private readonly bool _canExecute;

        public CommandParametr(Action<object> action, bool canExecute = true)
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
            _action(parameter);
        }
    }
}