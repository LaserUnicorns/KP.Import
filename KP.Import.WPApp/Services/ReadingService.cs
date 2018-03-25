using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KP.Import.Common.Contracts;

namespace KP.Import.WPApp.Services
{
    public interface IReadingService
    {
        Task<HttpResponseMessage> SaveReadings(long accountNumber,
            int month,
            int year,
            decimal? coldWater,
            decimal? hotWater);
    }

    public class ReadingService : ServiceBase, IReadingService
    {
        protected override string Controller => "Reading";
        
        public Task<HttpResponseMessage> SaveReadings(long accountNumber, int month, int year, decimal? coldWater, decimal? hotWater)
        {
            var readings = new List<Reading>();
            if (coldWater.HasValue)
            {
                readings.Add(new Reading
                {
                    AccountNumber = accountNumber,
                    Month = month,
                    Kind = ReadingKind.Cold,
                    Year = year,
                    Value = coldWater.Value
                });
            }

            if (hotWater.HasValue)
            {
                readings.Add(new Reading
                {
                    AccountNumber = accountNumber,
                    Month = month,
                    Kind = ReadingKind.Hot,
                    Year = year,
                    Value = hotWater.Value
                });
            }
            return Post("SaveReadings", readings);
        }
    }
}
