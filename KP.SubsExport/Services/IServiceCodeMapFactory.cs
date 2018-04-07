using System.Collections.Generic;

namespace KP.SubsExport
{
    public enum ImasServiceCode
    {
        HotWater = 36,
        ColdWater = 06,
        ColdWaterSewerage = 23,
        HotWaterSewerage = 35,
        Heating = 02,
        Electricity = 13,
        Repair = 01,
        Gas = 09,
        Garbage = 12,
        Domophone = 112,
        BankFee = 22,
    }

    public interface IServiceCodeMapFactory
    {
        string GetServiceCode(ImasServiceCode serviceCode, string odn);
        Dictionary<string, Dictionary<ImasServiceCode, string>> GetServiceCodeMap();
        bool ServiceNeeded(ImasServiceCode serviceCode);
    }
}