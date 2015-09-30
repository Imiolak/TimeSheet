using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using TimeSheet.Model;

namespace TimeSheet.Database.Entities
{
    public class MonthEntity : Month
    {
        public int Id { get; set; }
        [Column("Days")]
        public new DbSet<DayEntity> Days { get; set; }
    }
}
