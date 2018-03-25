using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCodeMap = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<int, string>>;

namespace KP.SubsExport
{
    class ServiceCodeMapFactory : IServiceCodeMapFactory
    {
        private readonly ServiceCodeMap _serviceCodeMap =
            new ServiceCodeMap
            {
                [""] = new Dictionary<int, string>
                {
                    [36] = "11",
                    [06] = "12",
                    [23] = "13",
                    [35] = "14",
                    [02] = "10",
                    [13] = "06",
                    [01] = "01",
                    [09] = "22",
                    [12] = "05",
                    [112] = "01",
                    [22] = "01",
                },
                ["101"] = new Dictionary<int, string>
                {
                    [36] = "19",
                    [06] = "18",
                    [23] = "20",
                    [35] = "20",
                },
            };

        public ServiceCodeMap GetServiceCodeMap()
        {
            return this._serviceCodeMap;
        }

        public string GetServiceCode(int serviceCode, string odn)
        {
            return this._serviceCodeMap[odn][serviceCode];
        }

        public bool ServiceNeeded(int serviceCode)
        {
            return _serviceCodeMap.Any(x => x.Value.ContainsKey(serviceCode));
        }
    }
}
