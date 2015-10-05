using System.Collections.ObjectModel;
using System.Linq;
using TimeSheet.Database.Entities;
using TimeSheet.Model;

namespace TimeSheet.Utils.Mappers
{
    public static class EntityToModel
    {
        public static Month Map(MonthEntity entity)
        {
            return new Month(entity.MonthName)
            {
                Days = new ObservableCollection<Day>(entity.Days.Select(Map))
            };
        }

        public static Day Map(DayEntity entity)
        {
            return new Day(entity.Date)
            {
                WorkTimePeriods = new ObservableCollection<TimePeriod>(entity.WorkTimePeriods.Select(Map)),
                BreakTimePeriods = new ObservableCollection<TimePeriod>(entity.BreakTimePeriods.Select(Map))
            };
        }

        public static TimePeriod Map(TimePeriodEntity entity)
        {
            return new TimePeriod
            {
                StartTime = entity.StartTime,
                EndTime = entity.EndTime
            };
        }
    }
}
