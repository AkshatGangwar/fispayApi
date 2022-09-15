using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISPAYProject.Model
{
    public sealed class Constants
    {
        private Constants() { }

        public struct MessageKeys
        {
            public const string User_Login = "User successfully login";
            public const string SomethingWentWrong = "Oops! something went wrong";
            public const string UserNotFound = "User does not exists";

        }
    }
}
