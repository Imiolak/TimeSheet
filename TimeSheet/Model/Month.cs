using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TimeSheet.Model
{
    [Serializable]
    public class Month
    {
        public Month(string monthName)
        {
            MonthName = monthName;
            Days = new ObservableCollection<Day>();
        }

        public string MonthName { get; set; }
        public ICollection<Day> Days { get; set; }

        public double GetMinutesWorkedCount()
        {
            return Days.Sum(d => d.MinutesWorked);
        }
    }
}
