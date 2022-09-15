using System.Runtime.Serialization;

namespace FISPAYProject.CoreApi
{
    [DataContract(Namespace = "")]
    public enum ApiResultType
    {
        [EnumMember(Value = "Success")]
        Success,

        [EnumMember(Value = "Error")]
        Error
    }
}
