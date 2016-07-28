using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PanoramioTest.Infrastructure
{
    public abstract class RelayCommandBase : ICommand
    {
        private readonly Predicate<object> _canExecute;

        public RelayCommandBase(Predicate<object> canExecute)
        {
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public abstract void Execute(object parameter);
    }
}
