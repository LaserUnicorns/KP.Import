using System;
using KP.Import.Models.Base;

namespace KP.Import.Models
{
    public class PaymentRecord : ImportRecord
    {
        public DateTime PaymentDate { get; set; }
        
        public decimal PaymentAmount { get; set; }
    }
}