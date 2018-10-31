using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCodeMap = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<KP.SubsExport.ImasServiceCode, string>>;

namespace KP.SubsExport
{
    public class ServiceCodeMapFactory : IServiceCodeMapFactory
    {
        private readonly ServiceCodeMap _serviceCodeMap =
            new ServiceCodeMap
            {
                [""] = new Dictionary<ImasServiceCode, string>
                {
                    [ImasServiceCode.HotWater] = "11",
                    [ImasServiceCode.ColdWater] = "12",
                    [ImasServiceCode.ColdWaterSewerage] = "13",
                    [ImasServiceCode.HotWaterSewerage] = "14",
                    [ImasServiceCode.Heating] = "10",
                    [ImasServiceCode.Electricity] = "06",
                    [ImasServiceCode.Repair] = "01",
                    [ImasServiceCode.Gas] = "22",
                    [ImasServiceCode.Garbage] = "27",
                },
                ["101"] = new Dictionary<ImasServiceCode, string> // odn
                {
                    [ImasServiceCode.HotWater] = "11",
                    [ImasServiceCode.ColdWater] = "12",
                    [ImasServiceCode.ColdWaterSewerage] = "13",
                    [ImasServiceCode.HotWaterSewerage] = "14",
                },
            };

        public ServiceCodeMap GetServiceCodeMap()
        {
            return this._serviceCodeMap;
        }

        public string GetServiceCode(ImasServiceCode serviceCode, string odn)
        {
            return this._serviceCodeMap[odn][serviceCode];
        }

        public bool ServiceNeeded(ImasServiceCode serviceCode)
        {
            return _serviceCodeMap.Any(x => x.Value.ContainsKey(serviceCode));
        }
    }
}
