using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISPAYProject.Data
{
    [Table("VendorStore")]
    public class VendorStore : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid VendorId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Landmark { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Zipcode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
    }
}
