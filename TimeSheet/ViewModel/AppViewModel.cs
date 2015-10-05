using System.Collections.Generic;
using System.Collections.ObjectModel;
using TimeSheet.Database.Providers;
using TimeSheet.Model;

namespace TimeSheet.ViewModel
{
    public class AppViewModel
    {
        private IMonthRepository _monthrepository;

        public AppViewModel(IMonthRepository repository)
        {
            _monthrepository = repository;
            ReloadData();
        }

        public void ReloadData()
        {
            Months = new ObservableCollection<Month>(_monthrepository.GetAll());
        }

        public ICollection<Month> Months { get; private set; }
        public ICollection<Day> Days { get; set; }
    }
}
