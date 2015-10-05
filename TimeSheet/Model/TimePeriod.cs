using System;

namespace TimeSheet.Model
{
    [Serializable]
    public class TimePeriod
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public double MinutesSpan
        {
            get
            {
                var time = EndTime - StartTime;
                return time.TotalMinutes;
            }
        }

        public string Formatted
        {
            get
            {
                return string.Format("{0}:{1} - {2}:{3}",
                DecimateInt(StartTime.Hour),
                DecimateInt(StartTime.Minute),
                DecimateInt(EndTime.Hour),
                DecimateInt(EndTime.Minute));
            }
        }

        private string DecimateInt(int number)
        {
            return number >= 10 ? number.ToString() : string.Format("0{0}", number);
        }
    }
}
