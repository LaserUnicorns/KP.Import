using System.Runtime.Serialization;

namespace KP.Import.Common.Contracts
{
    [DataContract(Namespace = "")]
    public enum ReadingKind
    {
        Cold = 1,
        Hot = 2
    }
}