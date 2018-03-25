namespace KP.SubsExport
{
    class DbReceiptItem
    {
        public string AccountNumber { get; set; }
        public string Flat { get; set; }
        public int RegisteredCount { get; set; }
        public int ServiceCode { get; set; }
        public string ODN { get; set; }
        public decimal Amount { get; set; }
    }
}