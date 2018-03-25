using CsvHelper.Configuration;
using KP.Import.App.Models;

namespace KP.Import.App.Util
{
    public class PaymentMap : CsvClassMap<Payment>
    {
        public PaymentMap()
        {
            Map(x => x.ApartmentNumber).Index(0);
            Map(x => x.Amount).Index(1);
        }
    }
}