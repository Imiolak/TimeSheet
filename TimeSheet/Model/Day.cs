using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeSheet.Model
{
    public class Day
    {
        public DateTime Date { get; set; }
        public virtual IEnumerable<TimePeriod> TimePeriods { get; set; }

        public double GetDailyMinutesWorked()
        {
            return TimePeriods.Sum(p => p.GetMinutesWorkedCount());
        }
    }
}
