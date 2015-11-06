using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TimeSheet.Utils.Extensions;

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

        public double MinutesWorked
        {
            get
            {
                return Days.Sum(d => d.MinutesWorked);
            }
        }

        public double GetMinutesWorkedWeekly(Day anyDayOfTheWeek)
        {
            var dayOfWeek = anyDayOfTheWeek.Date.DayOfWeek.ToInt();
            var lowerLimit = -dayOfWeek;
            var upperLimit = 4 - dayOfWeek;

            return Days
                .Where(day =>
                    (day.Date - anyDayOfTheWeek.Date).Days >= lowerLimit &&
                    (day.Date - anyDayOfTheWeek.Date).Days <= upperLimit)
                .Sum(day => day.MinutesWorked);
        }
    }
}
