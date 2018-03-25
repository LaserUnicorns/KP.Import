using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace KP.Import.InitApp.DataObjects
{
    public class Readings
    {
        public long AccountNumber { get; set; }
        public decimal ColdWater { get; set; }
        public decimal HotWater { get; set; }
    }

    public class ReadingsMap : CsvClassMap<Readings>
    {
        public ReadingsMap()
        {
            Map(x => x.AccountNumber).Index(0);
            Map(x => x.ColdWater).Index(1);
            Map(x => x.HotWater).Index(2);
        }
    }
}
