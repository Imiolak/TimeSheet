using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet.Model
{
    public class Month
    {
        public string MonthName { get; set; }
        public virtual IEnumerable<Day> Days { get; set; }

        public double GetMinutesWorkedCount()
        {
            return Days.Sum(d => d.GetDailyMinutesWorked());
        }
    }
}
