using System.Collections.Generic;

namespace KP.SubsExport
{
    interface IServiceCodeMapFactory
    {
        string GetServiceCode(int serviceCode, string odn);
        Dictionary<string, Dictionary<int, string>> GetServiceCodeMap();
        bool ServiceNeeded(int serviceCode);
    }
}