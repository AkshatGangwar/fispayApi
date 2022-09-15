using System.Runtime.Serialization;

namespace FISPAYProject.CoreApi
{
    [DataContract(Name = "ApiResult", Namespace = "")]
    public class ApiResult<T>
    {
        public ApiResult()
        {
            ResultCode = new ApiResultCode();
        }

        public ApiResult(ApiResultCode apiResultCode, T dataObject) : this(apiResultCode)
        {
            this.DataObject = dataObject;
        }

        public ApiResult(ApiResultCode apiResultCode)
        {
            this.ResultCode = apiResultCode;
        }

        [DataMember]
        public ApiResultCode ResultCode { get; private set; }

        [DataMember(EmitDefaultValue =false)]
        public T DataObject { get; private set; }

        public bool HasSuccess
        {
            get
            {
                return (this.ResultCode.ResultType == ApiResultType.Success && this.DataObject != null);
            }
        }
    }
}
