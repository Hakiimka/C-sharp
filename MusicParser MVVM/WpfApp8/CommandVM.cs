using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp8
{
    class CommandVM : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Action<object> Click;
        public CommandVM(Action<Object> click)
        {
            Click = click;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Click(parameter);
        }
    }
}
