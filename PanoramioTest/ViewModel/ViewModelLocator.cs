using PanoramioTest.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanoramioTest.ViewModel
{
    public class ViewModelLocator
    {
        public static MainViewModel Main { get; }

        static ViewModelLocator()
        {
            /*При добавлении в приложение большего количества ViewModel'ей здесь следует использовать IOC Container*/
            IDataService dataService = new DataService();
            Main = new MainViewModel(dataService);
        }
    }
}
