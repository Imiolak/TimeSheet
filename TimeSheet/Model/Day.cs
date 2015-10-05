using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TimeSheet.Model
{
    [Serializable]
    public class Day
    {
        public Day(DateTime date)
        {
            Date = date;
            WorkTimePeriods = new ObservableCollection<TimePeriod>();
            BreakTimePeriods = new ObservableCollection<TimePeriod>();
        }

        public DateTime Date { get; set; }
        public ICollection<TimePeriod> WorkTimePeriods { get; set; }
        public ICollection<TimePeriod> BreakTimePeriods { get; set; } 

        public double MinutesWorked
        {
            get { return WorkTimePeriods.Sum(p => p.MinutesSpan) - BreakTimePeriods.Sum(p => p.MinutesSpan); }
        }

        public string ShortDate
        {
            get { return Date.ToShortDateString(); }
        }
    }
}
