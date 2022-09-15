using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISPAYProject.Model
{
    public class UserVendorModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string? StoreName { get; set; }
    }
}
