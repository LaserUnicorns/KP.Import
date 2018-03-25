using System;
using KP.Import.Models.Base;

namespace KP.Import.Models
{
    public class MeterReadingRecord : ImportRecord
    {
        public int MeterCode { get; set; }
        
        public DateTime ReadingDate { get; set; }
        
        public decimal Reading { get; set; }
    }
}