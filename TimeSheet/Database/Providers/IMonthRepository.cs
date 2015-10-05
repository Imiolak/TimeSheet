using System.Collections.Generic;
using TimeSheet.Model;

namespace TimeSheet.Database.Providers
{
    public interface IMonthRepository
    {
        IEnumerable<Month> GetAll();

        Month GetByName(string monthName);

        void Save(IEnumerable<Month> months);
    }
}
