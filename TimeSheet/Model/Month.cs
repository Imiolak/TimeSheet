using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using TimeSheet.Utils.Extensions;

namespace TimeSheet.Model
{
    [Serializable]
    public class Month
    {
        private const int DefaultHourlyRate = 24;

        public Month(string monthName)
        {
            MonthName = monthName;
            HourlyWage = DefaultHourlyRate;
            Days = new ObservableCollection<Day>();
        }

        public string MonthName { get; set; }
        public int HourlyWage { get; set; }
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

        public void ExportToFile()
        {
            var file = new StreamWriter(string.Format("{0}.txt", MonthName));

            foreach (var day in Days)
            {
                var hoursWorked = day.MinutesWorked/60;
                file.WriteLine($"{day.ShortDate}\t{hoursWorked}");
            }

            file.Close();
        }
    }
}
