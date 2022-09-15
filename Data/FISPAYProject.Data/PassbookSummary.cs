using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISPAYProject.Data
{
    [Table("PassbookSummary")]
    public class PassbookSummary 
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Int64 TxnId { get; set; }
        public DateTime TxnDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public char Mode { get; set; }
        public string? Comment { get; set; }
        public decimal RemainingBal { get; set; }
    }
}
