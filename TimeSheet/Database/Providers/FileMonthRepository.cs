using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TimeSheet.Model;
using TimeSheet.Utils;

namespace TimeSheet.Database.Providers
{
    public class FileMonthRepository : IMonthRepository
    {
        private const string FileName = "timeSheet.bak";

        public IEnumerable<Month> GetAll()
        {
            var months = FileSerializer.Deserialize<IEnumerable<Month>>(FileName);

            return months ?? new ObservableCollection<Month>();
        }

        public Month GetByName(string monthName)
        {
            var months = FileSerializer.Deserialize<IEnumerable<Month>>(FileName);

            return months == null ? null : months.First(m => m.MonthName == monthName);
        }

        public void Save(IEnumerable<Month> months)
        {
            FileSerializer.Serialize(months, FileName);
        }
    }
}
