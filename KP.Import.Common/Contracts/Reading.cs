using System.Runtime.Serialization;

namespace KP.Import.Common.Contracts
{
    [DataContract(Namespace = "")]
    public class Reading
    {
        [DataMember]
        public int ReadingID { get; set; }

        [DataMember]
        public long AccountNumber { get; set; }

        [DataMember]
        public decimal Value { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int Month { get; set; }

        [DataMember]
        public ReadingKind Kind { get; set; }
    }
}