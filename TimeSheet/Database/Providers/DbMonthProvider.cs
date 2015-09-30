using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using TimeSheet.Database.DAL;
using TimeSheet.Database.Entities;
using TimeSheet.Model;

namespace TimeSheet.Database.Providers
{
    public static class DbMonthProvider
    {
        public static IEnumerable<Month> GetAll()
        {
            //using (var context = new TimeSheetContext())
            //{
            //    return context.Months.Select(EntityToModel);
            //}
            return new List<Month>
            {
                new Month
                {
                    MonthName = "October 2015",
                    Days = new List<Day>
                    {
                        new Day
                        {
                            Date = DateTime.Today,
                            TimePeriods = new List<TimePeriod>
                            {
                                new TimePeriod { StartTime = DateTime.Now - TimeSpan.FromHours(5), EndTime = DateTime.Now },
                                new TimePeriod { StartTime = DateTime.Now - TimeSpan.FromHours(5), EndTime = DateTime.Now }
                            }
                        }
                    }
                }
            };
        }

        public static Month GetByName(string monthName)
        {
            using (var context = new TimeSheetContext())
            {
                var month = context.Months.FirstOrDefault(m => m.MonthName == monthName);
                if (month == null)
                {
                    throw new KeyNotFoundException("No such month exists.");
                }
                return month;
            }
        }

        private static Month EntityToModel(MonthEntity entity)
        {
            return new Month
            {
                MonthName = entity.MonthName,
                Days = entity.Days
            };
        }
    }
}
