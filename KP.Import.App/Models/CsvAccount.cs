using System.Collections.Generic;
using KP.Import.Models;

namespace KP.Import.App.Models
{
    public class CsvAccount
    {
        public long AccountNumber { get; set; }
        public List<ExchangeRecord> HotWaterMeters { get; set; }
        public List<ExchangeRecord> ColdWaterMeters { get; set; }
    }
}