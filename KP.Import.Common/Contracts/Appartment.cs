using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KP.Import.Common.Contracts
{
    [DataContract(Namespace = "")]
    public class Appartment
    {
        [DataMember]
        public long AccountNumber { get; set; }

        [DataMember]
        public int AppartmentNumber { get; set; }

        [DataMember]
        public string Owner { get; set; }

        [DataMember]
        public List<Reading> Readings { get; set; }
    }
}