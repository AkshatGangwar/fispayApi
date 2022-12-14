using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FISPAYProject.Model
{
    public record VendorAccountRegistrationModel : BaseModel
    {
        [JsonIgnore]
        public Guid? Id { get; set; }
        public Guid? VendorId { get; init; }
        public string? AccountNumber { get; set; }
        public string? BankName { get; set; }
        public string? IFSCCode { get; set; }
        public string? AccountHolderName { get; set; }
        public string? BranchName { get; set; }
        public int? AccountType { get; set; }
    }
}
