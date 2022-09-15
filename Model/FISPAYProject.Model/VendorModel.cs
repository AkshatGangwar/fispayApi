using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FISPAYProject.Model
{
    public class VendorModel
    {
        [JsonIgnore]
        public Guid? Id { get; set; }
        [JsonIgnore]
        public Guid? VendorId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string StoreName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public string? District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
    }

    public class UpdateVendorModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public string? District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string Description { get; set; }
    }

    public class VendorStoreModel
    {
        public Guid VendorId { get; set; }
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public string? District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
    }
}
