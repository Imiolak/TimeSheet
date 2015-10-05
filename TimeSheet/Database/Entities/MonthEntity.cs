using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace TimeSheet.Database.Entities
{
    public class MonthEntity
    {
        public int Id { get; set; }

        public string MonthName { get; set; }

        [Column("Days")]
        public new DbSet<DayEntity> Days { get; set; }
    }
}
