using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.SubsExport
{
    public class SnilsMapFactory : ISnilsMapFactory
    {
        private readonly Dictionary<string, string> _snilsMap = 
            new Dictionary<string, string>
            {
                ["461702100"] = "133-041-479-12"
            }; 

        public Dictionary<string, string> GetSnilsMap()
        {
            return this._snilsMap;
        }

        public string GetSnilsByAccountNumber(string account)
        {
            return this._snilsMap[account];
        }
    }
}
