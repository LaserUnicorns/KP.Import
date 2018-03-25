using System;
using KP.Import.Models.Base;

namespace KP.Import.Models
{
    public class AccountRecord : ExportRecord
    {
        public DateTime DebtDate { get; set; }
        
        public decimal DebtAmount { get; set; }
        
        public string Address { get; set; }
    }
}