using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISPAYProject.Model
{
    public class PassbookModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Int64 TxnId { get; set; }
        public DateTime TxnDate { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public char Mode { get; set; }
    }
}
