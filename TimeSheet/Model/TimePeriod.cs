using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet.Model
{
    public class TimePeriod
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public double GetMinutesWorkedCount()
        {
            var time = EndTime - StartTime;
            return time.TotalMinutes;
        }
    }
}
