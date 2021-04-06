using System;
using System.Windows.Input;

namespace FirebirdTest.Model
{
    public class VMcommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> _action;
        public static bool ExecuteFlag { get; set; }

        public VMcommand(Action<object> action)
        {
            _action = action;

        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
