using System.Globalization;
using CsvHelper.Configuration;
using KP.Import.Models;

namespace KP.Import.App.Util
{
    public class ExchangeRecordMap : CsvClassMap<ExchangeRecord>
    {
        public ExchangeRecordMap()
        {
            Map(x => x.Type).Index(0).TypeConverterOption("D");
            Map(x => x.AccountNumber).Index(1);
            Map(x => x.Code).Index(2);
            Map(x => x.Date).Index(3).TypeConverterOption("dd.MM.yyyy");
            Map(x => x.Value).Index(4).TypeConverterOption(CultureInfo.InvariantCulture);
            Map(x => x.Note).Index(5);
        }
    }
}
