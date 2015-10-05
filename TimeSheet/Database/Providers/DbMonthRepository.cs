using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TimeSheet.Database.DAL;
using TimeSheet.Model;
using TimeSheet.Utils.Mappers;

namespace TimeSheet.Database.Providers
{
    public class DbMonthRepository : IMonthRepository
    {
        public IEnumerable<Month> GetAll()
        {
            //using (var context = new TimeSheetContext())
            //{
            //    return context.Months.Select(EntityToModel);
            //}
            return new List<Month>
            {
                new Month("October 2015")
                {
                    Days = new ObservableCollection<Day>
                    {
                        new Day(DateTime.Today)
                        {
                            WorkTimePeriods = new ObservableCollection<TimePeriod>
                            {
                                new TimePeriod { StartTime = DateTime.Now - TimeSpan.FromHours(5), EndTime = DateTime.Now },
                                new TimePeriod { StartTime = DateTime.Now - TimeSpan.FromHours(5), EndTime = DateTime.Now }
                            },
                            BreakTimePeriods = new ObservableCollection<TimePeriod>()
                        }
                    }
                }
            };
        }

        public Month GetByName(string monthName)
        {
            using (var context = new TimeSheetContext())
            {
                var month = context.Months.FirstOrDefault(m => m.MonthName == monthName);
                if (month == null)
                {
                    throw new KeyNotFoundException("No such month exists.");
                }
                return EntityToModel.Map(month);
            }
        }

        public void Save(IEnumerable<Month> months)
        {
            throw new NotImplementedException();
        }
    }
}
