using System;

namespace KP.CsvLib
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class CsvTypeAttribute : Attribute
    {
        public int Index { get; set; }
        public string Value { get; set; }
    }
}
