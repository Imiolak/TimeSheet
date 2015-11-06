using System;

namespace TimeSheet.Utils.Extensions
{
    public static class DayOfWeekExtensions
    {
        public static int ToInt(this DayOfWeek dayOfWeek)
        {
            return (int) (dayOfWeek + 6)%7;
        }
    }
}
