using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace TimeSheet.Database.Entities
{
    public class DayEntity
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Column("WorkTimePeriods")]
        public new DbSet<TimePeriodEntity> WorkTimePeriods { get; set; }

        [Column("BreakTimePeriods")]
        public new DbSet<TimePeriodEntity> BreakTimePeriods { get; set; }
    }
}
