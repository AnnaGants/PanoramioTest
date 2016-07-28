using PanoramioTest.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PanoramioTest.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected static IDataService _dataService;

        public ViewModelBase(IDataService dataService)
        {
            _dataService = dataService;   
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
