using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.SubsExport
{
    public class ServiceTransformer
    {
        private readonly ISnilsMapFactory _snilsMap;
        private readonly IServiceCodeMapFactory _serviceCodeMap;

        public ServiceTransformer(ISnilsMapFactory snilsMap, IServiceCodeMapFactory serviceCodeMap)
        {
            _snilsMap = snilsMap;
            _serviceCodeMap = serviceCodeMap;
        }

        [Pure]
        public List<ReceiptItemFull> Map(List<DbReceiptItem> items)
        {
            return items
                .Where(x => _serviceCodeMap.ServiceNeeded(x.ServiceCode))
                .Select(x => new ReceiptItemFull
                {
                    Snils = _snilsMap.GetSnilsByAccountNumber(x.AccountNumber),
                    Organization = Properties.Resources.Organization,
                    AccountNumber = x.AccountNumber.ToString(),
                    City = "г Ярославль",
                    Street = "ул Полянки Б.",
                    House = "17",
                    Building = "2",
                    Flat = x.Flat,
                    RegisteredCount = x.RegisteredCount,
                    TempCount = 0,
                    ServiceCode = _serviceCodeMap.GetServiceCode(x.ServiceCode, x.ODN),
                    Amount = x.Amount

                })
                //.GroupBy(x => x.ServiceCode, x => x, (sc, xs) =>
                //{
                //    var xsList = xs as IList<ReceiptItemFull> ?? xs.ToList();
                //    var item = xsList.First();
                //    item.Amount = xsList.Sum(x => x.Amount);
                //    return item;
                //})
                .ToList();
        }
    }
}
