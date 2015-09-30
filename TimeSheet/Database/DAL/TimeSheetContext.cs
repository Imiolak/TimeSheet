using System.Data.Entity;
using TimeSheet.Database.Entities;

namespace TimeSheet.Database.DAL
{
    public class TimeSheetContext : DbContext
    {
        public DbSet<MonthEntity> Months { get; set; }
        public DbSet<DayEntity> Days { get; set; }
        public DbSet<TimePeriodEntity> TimePeriods { get; set; } 
    }
}
