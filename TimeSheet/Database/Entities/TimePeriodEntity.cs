using System;

namespace TimeSheet.Database.Entities
{
    public class TimePeriodEntity
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
