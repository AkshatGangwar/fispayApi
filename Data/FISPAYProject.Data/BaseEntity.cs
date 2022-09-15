using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISPAYProject.Data
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
