using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PanoramioTest.Infrastructure
{
    public class RelayCommand<T> : RelayCommandBase
    {
        private readonly Action<T> _execute;

        public RelayCommand(Action<T> execute)
        : this(execute, null)
        { }

        public RelayCommand(Action<T> execute, Predicate<object> canExecute)
            :base(canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("Action was not set!");

            _execute = execute;
        }

        public override void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
    
    public class RelayCommand : RelayCommandBase
    {
        private readonly Action _execute;

        public RelayCommand(Action execute)
        : this(execute, null)
        { }

        public RelayCommand(Action execute, Predicate<object> canExecute)
            : base(canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("Action was not set!");

            _execute = execute;
        }

        public override void Execute(object parameter)
        {
            _execute();
        }
    }
}
