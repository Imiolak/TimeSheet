using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using TimeSheet.Model;

namespace TimeSheet.Database.Entities
{
    public class DayEntity : Day
    {
        public int Id { get; set; }
        [Column("TimePeriods")]
        public new DbSet<TimePeriodEntity> TimePeriods { get; set; } 
    }
}
