using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISPAYProject.Model
{
    public class PaymentModel
    {
        //public Guid Id { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string? Comment { get; set; }
    }
}
