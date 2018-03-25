using System;

namespace KP.Import.Models
{
    public class ExchangeRecord
    {
        public ExchangeRecordType Type { get; set; }
        public long AccountNumber { get; set; }
        public int? Code { get; set; }
        public DateTime? Date { get; set; }
        public decimal Value { get; set; }
        public string Note { get; set; }
    }
}