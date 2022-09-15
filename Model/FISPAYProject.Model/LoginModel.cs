using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISPAYProject.Model
{
    public class LoginModel
    {
        public string Mobile { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticationResult
    {
        public string Name { get; set; }
        //public string MiddleName { get; set; }
        //public string LastName { get; set; }
        public Guid UserId { get; set; }
        public string EmailId { get; set; }
        public string Token { get; set; }
        public string UserType { get; set; }
        public bool IsWalletActivate { get; set; }
        public bool IsVendorAccount { get; set; }
        public decimal WalletBal { get; set; }
    }

    public class OTPResponse
    {
        public Guid UserId { get; set; }
        public int OTP { get; set; }
    }
}
