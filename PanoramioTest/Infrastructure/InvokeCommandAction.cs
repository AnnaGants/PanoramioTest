using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace PanoramioTest.Infrastructure
{
    public sealed class InvokeCommandAction : DependencyObject, IAction
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(InvokeCommandAction), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(InvokeCommandAction), new PropertyMetadata(null));

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty PassEventArgsProperty = DependencyProperty.Register("PassEventArgs", typeof(bool), typeof(InvokeCommandAction), new PropertyMetadata(false));

        public bool PassEventArgs
        {
            get { return (bool)GetValue(PassEventArgsProperty); }
            set { SetValue(PassEventArgsProperty, value); }
        }
        
        public object Execute(object sender, object eventArgs)
        {
            object param = CommandParameter == null ? 
                (PassEventArgs ? eventArgs : null) : (PassEventArgs ? new object[] { CommandParameter, eventArgs } : CommandParameter);

            Command.Execute(param);

            return null;
        }
    }
}
