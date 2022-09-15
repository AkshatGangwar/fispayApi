using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FISPAYProject.Model
{
    public record BaseModel
    {
        [JsonIgnore]
        public bool? IsActive { get; set; }
        [JsonIgnore]
        public Guid? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }
        [JsonIgnore]
        public Guid? ModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }
    }
}
