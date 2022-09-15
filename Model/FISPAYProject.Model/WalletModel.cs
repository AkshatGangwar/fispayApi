using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FISPAYProject.Model
{
    public class WalletModel
    {
        [JsonIgnore]
        public Guid? Id { get; set; }
        [JsonIgnore]
        public Guid? UserId { get; set; }
        public string PanNumber { get; set; }
        //public bool IsWalletActivated { get; set; }
        [JsonIgnore]
        public string? Mobile { get; set; }
    }

    public class AddWalletAmount
    {
        public string Comment { get; set; }
        public decimal Amount { get; set; }
        [JsonIgnore]
        public Guid? UserId { get; set; }
    }

    public class WalletResponse
    {
        public decimal TotalBalance { get; set; }
        public Guid UserId { get; set; }
    }

    public class WalletHistoryModel
    {
        public DateTime TxnDate { get; set; }
        public string TxnID { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public char TxnMode { get; set; }
        public string Comment { get; set; }
    }

    public class PdfWalletModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime From { get; set; }
        public string FromDateString { get; set; }
        public DateTime To { get; set; }
        public string ToDateString { get; set; }

        public List<WalletHistoryListModel> WalletList { get; set; }
    }

    public class WalletHistoryListModel
    {
        public string TxnDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string TxnMode { get; set; }
        public decimal RemainingBal { get; set; }
    }


}
