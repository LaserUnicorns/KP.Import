using System.Collections.Generic;

namespace KP.SubsExport
{
    public interface ISnilsMapFactory
    {
        string GetSnilsByAccountNumber(string account);
        Dictionary<string, string> GetSnilsMap();
    }
}