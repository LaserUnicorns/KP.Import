using System;
using KP.Import.App.Models;
using KP.Import.Models;

namespace KP.Import.App.Helpers
{
    public static class WaterTypeHelper
    {
        private const string HOT = "гор€ч";
        private const string COLD = "холодн";

        public static WaterType GetWaterType(MeterRecord record)
        {
            if (record.MeterName.IndexOf(HOT, StringComparison.CurrentCultureIgnoreCase) > -1)
                return WaterType.Hot;
            if (record.MeterName.IndexOf(COLD, StringComparison.CurrentCultureIgnoreCase) > -1)
                return WaterType.Cold;

            throw new ArgumentException("", nameof(record.MeterName));
        }

        public static WaterType GetWaterType(string s)
        {
            if (s.IndexOf(HOT, StringComparison.OrdinalIgnoreCase) > -1)
                return WaterType.Hot;
            if (s.IndexOf(COLD, StringComparison.OrdinalIgnoreCase) > -1)
                return WaterType.Cold;

            throw new ArgumentException("", nameof(s));
        }
    }
}