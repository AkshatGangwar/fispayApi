using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FISPAYProject.CoreApi
{
    [DataContract(Namespace = "")]
    public class ApiResultCode
    {
        private int _statusCode = 0;
        private string _messageText = String.Empty;

        public ApiResultCode()
        {
            this.ResultType = ApiResultType.Error;
        }

        public ApiResultCode(ApiResultType resultType, int statusCode = 0, string messageText = "")
        {
            this.ResultType = resultType;
            this.StatusCode = statusCode;
            if (!string.IsNullOrEmpty(messageText))
            {
                this.MessageText = messageText;
            }
        }

        [DataMember]
        public ApiResultType ResultType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int StatusCode
        {
            get
            {
                return _statusCode;
            }
            set
            {
                _statusCode = value;
            }
        }

        [DataMember(EmitDefaultValue =false)]
        public string MessageText
        {
            get
            {
                return _messageText;
            }
            set
            {
                _messageText = value;
            }
        }
    }
}
