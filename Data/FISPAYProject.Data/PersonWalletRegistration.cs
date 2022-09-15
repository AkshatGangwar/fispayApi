using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FISPAYProject.Data
{
    public class PersonWalletRegistration : BaseEntity
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string PanNumber { get; set; }
        public bool IsWalletActivate { get; set; }
    }
}
