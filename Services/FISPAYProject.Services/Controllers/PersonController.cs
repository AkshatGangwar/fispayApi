using FISPAYProject.Business.Contracts;
using FISPAYProject.CoreApi;
using FISPAYProject.Data;
using FISPAYProject.Model;
using FISPAYProject.Services.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pdfGenerator;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace FISPAYProject.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IWallet _wallet;
        private readonly UserManager<ApplicationUser> _userManager;

        public PersonController(IWallet wallet, UserManager<ApplicationUser> userManager)
        {
            _wallet = wallet;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("WalletRegistration")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult WalletRegistration([FromBody] WalletModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Guid.Parse(User.FindFirst("UserId").Value);
                model.Mobile = User.FindFirst("Mobile").Value;

                var result = _wallet.AddUpdateWallet(model);

                if(result.HasSuccess)
                {
                    OTPResponse OTPResponse = new OTPResponse
                    {
                        UserId = model.UserId.Value,
                        OTP = SendOTP(model.Mobile)
                    };

                    var apiResponse = new Response<OTPResponse>();
                    apiResponse.Data = OTPResponse;

                    return Ok(new ApiResult<Response<OTPResponse>>(new ApiResultCode(ApiResultType.Success, 200, "Wallet registration successfully done"), apiResponse));
                }
                else
                    return BadRequest(new ApiResult<Response<OTPResponse>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! something went wrong"), null));
            }
            else
            {
                return BadRequest(new ApiResult<Response<OTPResponse>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! data is not valid"), null));
            }
        }

        [HttpPut]
        [Route("WalletActivation")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult WalletActivation([FromForm] bool isActivate = false)
        {
            Response<bool> response = new Response<bool>();
            response.Data = false;

            var UserId = Guid.Parse(User.FindFirst("UserId").Value);

            var result = _wallet.WalletActivate(isActivate, UserId);

            if (result.HasSuccess)
            {
                response.Data = true;
                return Ok(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Success, 200, "Wallet successfully activated"), response));
            }
            else
            {
                return BadRequest(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! something went wrong"), response));
            }
        }

        [HttpPost]
        [Route("AddWalletAmount")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AddWalletAmount(AddWalletAmount model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Guid.Parse(User.FindFirst("UserId").Value);

                var result = _wallet.AddWalletAmountByUser(model);
                if (result.HasSuccess)
                {
                    var apiResponse = new Response<WalletResponse>();
                    apiResponse.Data = result.DataObject;

                    return Ok(new ApiResult<Response<WalletResponse>>(new ApiResultCode(ApiResultType.Success, 200, "Amount successfully added"), apiResponse));
                }
                else
                {
                    return BadRequest(new ApiResult<Response<WalletResponse>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! something went wrong"), null));
                }
            }
            else
                return BadRequest(new ApiResult<Response<WalletResponse>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! data is not valid"), null));
        }

        [HttpGet]
        [Route("GetWalletHistoryByUserId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetWalletHistoryByUserId()
        {
            var currentUserId= Guid.Parse(User.FindFirst("UserId").Value);
            var result = _wallet.GetUserWalletHistory(currentUserId);

            if(result.HasSuccess)
            {
                var apiResponse = new Response<List<WalletHistoryModel>>();
                apiResponse.Data = result.DataObject;

                return Ok(new ApiResult<Response<List<WalletHistoryModel>>>(new ApiResultCode(ApiResultType.Success, 200, "Wallet history fetch"), apiResponse));
            }
            else
                return BadRequest(new ApiResult<Response<List<WalletHistoryModel>>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! something went wrong"), null));
        }

        [HttpGet]
        [Route("GetPassbookHistoryAsync/{fromDate}/{toDate}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<byte[]> GetPassbookHistoryAsync(DateTime fromDate,DateTime toDate)
        {
            var UserId = User.FindFirst("UserId").Value;
            var userRecord = await _userManager.FindByIdAsync(UserId);

            PdfWalletModel model = new PdfWalletModel
            {
                UserId = Guid.Parse(UserId),
                Name = userRecord.Name,
                Mobile = userRecord.UserName,
                Email = userRecord.Email,
                From = fromDate,
                To = toDate,
                FromDateString = fromDate.ToShortDateString(),
                ToDateString = toDate.ToShortDateString()
            };

            var result = _wallet.GetWalletHistoryPDF(model);


            var document = new PdfDocument();
            PdfGenerator.AddPdfPages(document, TemplateGenerator.GetHTMLString(result.DataObject), PageSize.A4);
            Byte[]? res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                res = ms.ToArray();
            }
            return res;
        }

        [HttpPost]
        [Route("UserPayment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UserPayment(PaymentModel model)
        {
            Response<bool> response = new Response<bool>();
            response.Data = false;
            if (ModelState.IsValid)
            {
                var currentUserId = Guid.Parse(User.FindFirst("UserId").Value);

                var result = _wallet.AddUpdatePayment(model, currentUserId);
                if (result.HasSuccess)
                {
                    var apiResponse = new Response<bool>();
                    apiResponse.Data = true;

                    return Ok(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Success, 200, "Amount successfully added"), apiResponse));
                }
                else
                {
                    return BadRequest(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! something went wrong"), null));
                }
            }
            else
                return BadRequest(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! data is not valid"), null));
        }


        [NonAction]
        public int SendOTP(string mobileNo)
        {
            Random random = new Random();
            int otp = random.Next(1000, 9999);
            string text = "Your one time password is " + otp + ".";
            string toParty = "91" + mobileNo;
            string apiUrlForSMS = "https://www.thetexting.com/rest/sms/json/message/send?api_key=0fub24ktdddb83o&api_secret=sjz3skb0xitxmkj&to=" + toParty + "&text=" + text;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrlForSMS).Result;

            var msg = response.Content.ReadAsStringAsync().Result;

            return otp;
        }
    }
}
