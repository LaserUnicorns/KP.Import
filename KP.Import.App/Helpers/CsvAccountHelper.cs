using System;
using System.Collections.Generic;
using System.Linq;
using KP.Import.App.Models;
using KP.Import.Common.Contracts;
using KP.Import.Models;

namespace KP.Import.App.Helpers
{
    public static class CsvAccountHelper
    {
        public static CsvAccount ToCsvAccount(this IGrouping<long, ExchangeRecord> grouping)
        {
            return new CsvAccount
            {
                AccountNumber = grouping.Key,
                HotWaterMeters = grouping.Where(x => WaterTypeHelper.GetWaterType(x.Note) == WaterType.Hot).ToList(),
                ColdWaterMeters = grouping.Where(x => WaterTypeHelper.GetWaterType(x.Note) == WaterType.Cold).ToList()
            };
        }

        public static List<ExchangeRecord> GetMeters(this CsvAccount account, ReadingKind kind)
        {
            switch (kind)
            {
                case ReadingKind.Cold:
                    return account.ColdWaterMeters;
                case ReadingKind.Hot:
                    return account.HotWaterMeters;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }
    }
}