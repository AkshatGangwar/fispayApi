using FISPAYProject.Business.Contracts;
using FISPAYProject.CoreApi;
using FISPAYProject.Data;
using FISPAYProject.Data.Context;
using FISPAYProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISPAYProject.Business
{
    public class WalletRepository : IWallet
    {
        private readonly FISPAYContext _context = null;
        public WalletRepository(FISPAYContext context)
        {
            _context = context;
        }

        public ApiResult<bool> AddUpdateWallet(WalletModel model)
        {
            ApiResult<bool> objResult;

            try
            {
                PersonWalletRegistration? personWalletRegistration = null;
                personWalletRegistration = !_context.PersonWalletRegistration.Any(t => t.UserId == model.UserId)
                    ? new PersonWalletRegistration { UserId = model.UserId.Value, IsActive = true, CreatedBy = model.UserId.Value, CreatedDate = DateTime.Now }
                    : _context.PersonWalletRegistration.FirstOrDefault(t => t.UserId == model.UserId);

                #region "Common Data"
                personWalletRegistration.PanNumber = model.PanNumber;
                #endregion

                if (personWalletRegistration.Id == Guid.Empty)
                {
                    // Add Record
                    personWalletRegistration.Id = Guid.NewGuid();
                    _context.PersonWalletRegistration.Add(personWalletRegistration);
                }
                else
                {
                    personWalletRegistration.ModifiedBy = model.UserId;
                    personWalletRegistration.ModifiedDate = DateTime.Now;
                }


                int result = _context.SaveChanges();

                if (result > 0)
                    objResult = new ApiResult<bool>(new ApiResultCode(ApiResultType.Success), true);
                else
                    objResult = new ApiResult<bool>(new ApiResultCode(ApiResultType.Error, 201, "Something went wrong"));
            }
            catch (Exception)
            {
                objResult = new ApiResult<bool>(new ApiResultCode(ApiResultType.Error, 201, "Internal error"));
            }

            return objResult;
        }

        public ApiResult<bool> WalletActivate(bool isWalletActivate, Guid currentUserId)
        {
            ApiResult<bool> objResult;

            try
            {
                var isWalletExists = _context.PersonWalletRegistration.FirstOrDefault(t => t.UserId == currentUserId);
                if (isWalletExists != null)
                {
                    isWalletExists.IsWalletActivate = true;

                    int result = _context.SaveChanges();
                    if (result > 0)
                    {
                        int getSeqNo = _context.PassbookSummary.Max(p => (int?)p.TxnId) ?? 0;

                        PassbookSummary passbookModel = new PassbookSummary
                        {

                            Id = Guid.NewGuid(),
                            UserId = currentUserId,
                            TxnId = getSeqNo > 0 ? getSeqNo + 1 : 1,
                            TxnDate = DateTime.Now,
                            Description = "Wallet successfully register",
                            Amount = 0,
                            Mode = 'C',
                        };

                        _context.PassbookSummary.Add(passbookModel);
                        _context.SaveChanges();

                        objResult = new ApiResult<bool>(new ApiResultCode(ApiResultType.Success), true);
                    }

                    else
                        objResult = new ApiResult<bool>(new ApiResultCode(ApiResultType.Error, 201, "Something went wrong"));
                }
                else
                {
                    objResult = new ApiResult<bool>(new ApiResultCode(ApiResultType.Error, 201, "Wallet doesn't exists"));
                }
            }
            catch (Exception)
            {
                objResult = new ApiResult<bool>(new ApiResultCode(ApiResultType.Error, 201, "Internal error"));
            }

            return objResult;
        }

        public ApiResult<WalletResponse> AddWalletAmountByUser(AddWalletAmount model)
        {
            ApiResult<WalletResponse> objResult;

            try
            {
                PassbookSummary passbookSummary = new PassbookSummary { Id = Guid.NewGuid(), UserId = model.UserId.Value };

                #region "Common Data"
                int getSqnNo = _context.PassbookSummary.Max(t => (int?)t.TxnId) ?? 0;

                passbookSummary.TxnId = getSqnNo > 0 ? getSqnNo + 1 : 1;
                passbookSummary.TxnDate = DateTime.Now;
                passbookSummary.Amount = model.Amount;
                passbookSummary.Mode = 'C';
                passbookSummary.Description = "Amount successfully credit";
                passbookSummary.Comment = model.Comment;
                passbookSummary.RemainingBal = _context.PassbookSummary.Any(t => t.UserId == model.UserId.Value) ? _context.PassbookSummary.Where(t => t.UserId == model.UserId.Value).OrderByDescending(p => p.TxnDate).FirstOrDefault().RemainingBal + model.Amount : model.Amount;
                #endregion

                _context.PassbookSummary.Add(passbookSummary);

                int result = _context.SaveChanges();
                if (result > 0)
                {
                    decimal totalBal = _context.PassbookSummary.Where(t => t.UserId == model.UserId).OrderByDescending(t=>t.TxnDate).FirstOrDefault().RemainingBal;
                    WalletResponse walletResponse = new WalletResponse
                    {
                        UserId = model.UserId.Value,
                        TotalBalance = totalBal
                    };

                     objResult = new ApiResult<WalletResponse>(new ApiResultCode(ApiResultType.Success), walletResponse);
                }
                else
                {
                    objResult = new ApiResult<WalletResponse>(new ApiResultCode(ApiResultType.Error, 201, "Something went wrong"));
                }

            }
            catch (Exception)
            {
                objResult = new ApiResult<WalletResponse>(new ApiResultCode(ApiResultType.Error, 201, "Internal error"));
            }

            return objResult;
        }

        public ApiResult<List<WalletHistoryModel>> GetUserWalletHistory(Guid userId)
        {
            ApiResult<List<WalletHistoryModel>> objResult;

            try
            {
                var records = (from wlthstry in _context.PassbookSummary
                               where wlthstry.UserId == userId
                               select new WalletHistoryModel
                               {
                                   TxnID = "TXN" + wlthstry.Id,
                                   TxnDate = wlthstry.TxnDate,
                                   Description = wlthstry.Description,
                                   Amount = wlthstry.Amount,
                                   TxnMode = wlthstry.Mode,
                                   Comment = wlthstry.Comment
                               }).OrderByDescending(t => t.TxnDate).Take(15).ToList();

                if (records != null)
                {
                    objResult = new ApiResult<List<WalletHistoryModel>>(new ApiResultCode(ApiResultType.Success), records);
                }
                else
                    objResult = new ApiResult<List<WalletHistoryModel>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! record not found"));
            }
            catch (Exception)
            {
                objResult = new ApiResult<List<WalletHistoryModel>>(new ApiResultCode(ApiResultType.Error, 201, "Internal error"));
            }

            return objResult;

        }


        public ApiResult<PdfWalletModel> GetWalletHistoryPDF(PdfWalletModel model)
        {
            ApiResult<PdfWalletModel> objResult;
            try
            {

                var record = (from psmmry in _context.PassbookSummary
                              where (psmmry.UserId == model.UserId && (psmmry.TxnDate.Date >= model.From && psmmry.TxnDate.Date <= model.To))
                              select new WalletHistoryListModel
                              {
                                  TxnDate = psmmry.TxnDate.ToShortDateString(),
                                  Description = psmmry.Description,
                                  TxnMode = psmmry.Mode == 'C' ? "Cr." : "Dr.",
                                  Amount = psmmry.Amount,
                                  RemainingBal = psmmry.RemainingBal
                              }).ToList();

                model.WalletList = record;

                objResult = new ApiResult<PdfWalletModel>(new ApiResultCode(ApiResultType.Success), model);

            }
            catch (Exception)
            {
                objResult = new ApiResult<PdfWalletModel>(new ApiResultCode(ApiResultType.Error, 201, "Internal error"));
            }
            return objResult;
        }

        public ApiResult<bool> AddUpdatePayment(PaymentModel model, Guid currentUserId)
        {
            ApiResult<bool> objResult;
            try
            {
                #region "Credit Party"
                int getSeqNo = _context.PassbookSummary.Max(p => (int?)p.TxnId) ?? 0;

                PassbookSummary creditkModel = new PassbookSummary
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse(model.UserId),
                    TxnId = getSeqNo > 0 ? getSeqNo + 1 : 1,
                    TxnDate = DateTime.Now,
                    Description = "Amount credit",
                    Amount = model.Amount,
                    Mode = 'C',
                    RemainingBal = _context.PassbookSummary.Any(t => t.UserId == Guid.Parse(model.UserId)) ? _context.PassbookSummary.Where(t => t.UserId == Guid.Parse(model.UserId)).OrderByDescending(p => p.TxnDate).FirstOrDefault().RemainingBal + model.Amount : model.Amount,
                };
                _context.PassbookSummary.Add(creditkModel);
                #endregion

                #region "Debit Party"
                PassbookSummary debitModel = new PassbookSummary
                {
                    Id = Guid.NewGuid(),
                    UserId = currentUserId,
                    TxnId = creditkModel.TxnId + 1,
                    TxnDate = DateTime.Now,
                    Description = "Amount debit",
                    Amount = model.Amount,
                    Mode = 'D',
                    RemainingBal = _context.PassbookSummary.Any(t => t.UserId == currentUserId) ? _context.PassbookSummary.Where(t => t.UserId == currentUserId).OrderByDescending(p => p.TxnDate).FirstOrDefault().RemainingBal - model.Amount : model.Amount,
                };
                _context.PassbookSummary.Add(debitModel);
                #endregion

                int result = _context.SaveChanges();
                if (result > 0)
                    objResult = new ApiResult<bool>(new ApiResultCode(ApiResultType.Success), true);
                else
                    objResult = new ApiResult<bool>(new ApiResultCode(ApiResultType.Error), false);
            }
            catch (Exception)
            {
                objResult = new ApiResult<bool>(new ApiResultCode(ApiResultType.Error, 201, "Internal error"));
            }
            return objResult;
        }
    }
}
