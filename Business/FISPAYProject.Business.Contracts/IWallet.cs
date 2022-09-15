using FISPAYProject.CoreApi;
using FISPAYProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISPAYProject.Business.Contracts
{
    public interface IWallet
    {
        ApiResult<bool> AddUpdateWallet(WalletModel model);
        ApiResult<bool> WalletActivate(bool isWalletActivate, Guid currentUserId);
        ApiResult<WalletResponse> AddWalletAmountByUser(AddWalletAmount model);
        ApiResult<List<WalletHistoryModel>> GetUserWalletHistory(Guid userId);
        ApiResult<PdfWalletModel> GetWalletHistoryPDF(PdfWalletModel model);
        ApiResult<bool> AddUpdatePayment(PaymentModel model, Guid currentUserId);
    }
}
