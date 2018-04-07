using System.Diagnostics;

namespace KP.SubsExport
{
    [DebuggerDisplay("{ServiceCode} - {Amount}")]
    public class ReceiptItemFull
    {
        public string Snils { get; set; }
        public string Organization { get; set; }
        public string AccountNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Building { get; set; }
        public string Flat { get; set; }
        public int RegisteredCount { get; set; }
        public int TempCount { get; set; }
        public string ServiceCode { get; set; }
        public decimal Amount { get; set; }
    }
}