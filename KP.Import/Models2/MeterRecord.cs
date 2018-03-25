using System;
using KP.Import.Models.Base;

namespace KP.Import.Models
{
    public class MeterRecord : ExportRecord
    {
        public int MeterCode { get; set; }
        
        public DateTime LastReadingDate { get; set; }
        
        public decimal LastReading { get; set; }
        
        public string MeterName { get; set; }
    }
}