using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.SubsExport
{
    class DateService : IDateService
    {
        public DateTime GetWorkingDate()
        {
            var today = DateTime.Today;
            if (today.Day < 14)
            {
                return new DateTime(today.Year, today.Month, 1).AddDays(-1);
            }
            else
            {
                return new DateTime(today.Year, today.Month, 1).AddMonths(1).AddDays(-1);
            }
        }
    }
}
