using System;

namespace KP.CsvLib
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class CsvFieldAttribute : Attribute
    {
        public int Index { get; set; }
        public string Format { get; set; }
    }
}