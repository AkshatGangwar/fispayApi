using FISPAYProject.Business.Contracts;
using FISPAYProject.CoreApi;
using FISPAYProject.Data;
using FISPAYProject.Data.Context;
using FISPAYProject.Model;
using FISPAYProject.Services.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FISPAYProject.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVendorAccountRegistration _iVendorAccountRegistration;
        private readonly FISPAYContext _context = null;

        public AuthenticationController(IConfiguration configuration, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IVendorAccountRegistration iVendorAccountRegistration, FISPAYContext context)
        {
            this._configuration = configuration;
            this._signInManager = signInManager;
            this._userManager = userManager;
            _iVendorAccountRegistration = iVendorAccountRegistration;
            _context = context;
        }

        #region Login Validation  
        /// <summary>  
        /// Login Authenticaton using JWT Token Authentication  
        /// </summary>  
        /// <param name="data"></param>  
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userManager.FindByNameAsync(model.Mobile);
            if (currentUser != null)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Mobile, model.Password, true, false);
                if (result.Succeeded)
                {
                    AuthenticationResult authResult = new AuthenticationResult
                    {
                        Name = currentUser.Name,
                        UserId = new Guid(currentUser.Id),
                        EmailId = currentUser.Email,
                        UserType = currentUser.UserType,
                        //IsWalletActivate = _context.PersonWalletRegistration.Any(t => t.UserId == Guid.Parse(currentUser.Id)) == true ? _context.PersonWalletRegistration.FirstOrDefault(t => t.UserId == Guid.Parse(currentUser.Id)).IsWalletActivate : false
                    };

                    if (_context.PersonWalletRegistration.Any(t => t.UserId == Guid.Parse(currentUser.Id)))
                    {
                        var walletRec = _context.PersonWalletRegistration.FirstOrDefault(t => t.UserId == Guid.Parse(currentUser.Id));
                        authResult.IsWalletActivate = walletRec.IsWalletActivate;
                        authResult.WalletBal = _context.PassbookSummary.Where(t => t.UserId == Guid.Parse(currentUser.Id)).OrderByDescending(t => t.TxnDate).Select(p => p.RemainingBal).FirstOrDefault();
                    }
                    else
                        authResult.IsWalletActivate = false;

                    authResult.IsVendorAccount = _context.VendorAccountRegistration.Any(t => t.VendorId == Guid.Parse(currentUser.Id) && t.IsActive == true) ? true : false;

                    var tokenString = GenerateJSONWebToken(currentUser);
                    authResult.Token = tokenString;

                    var response = new Response<AuthenticationResult>();
                    response.Data = authResult;

                    return Ok(new ApiResult<Response<AuthenticationResult>>(new ApiResultCode(ApiResultType.Success, 200, Constants.MessageKeys.User_Login), response));
                }
                else
                {
                    return BadRequest(new ApiResult<Response<AuthenticationResult>>(new ApiResultCode(ApiResultType.Error, 201, "Invalid credential"), null));
                }
            }
            else
            {
                // User not exists
                return BadRequest(new ApiResult<Response<AuthenticationResult>>(new ApiResultCode(ApiResultType.Error, 201, Constants.MessageKeys.UserNotFound), null));
            }

        }
        #endregion

        [HttpPost]
        [AllowAnonymous]
        [Route("RegisterPerson")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RegisterPerson([FromBody] RegisterModel registerModel)
        {
            Response<bool> response = new Response<bool>();
            response.Data = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                Name = registerModel.Name,
                Email = registerModel.Email,
                UserName = registerModel.Mobile,
                UserType = registerModel.UserType,
                IsActive = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! something went wrong"), response));
            }

            response.Data = true;
            return Ok(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Success, 200, "Person successfully created"), response));
        }

        [HttpGet]
        [Route("GetOTP/{mobileNo}")]
        public async Task<IActionResult> RegistrationOTP(string mobileNo)
        {
            if (!string.IsNullOrEmpty(mobileNo))
            {
                OTPResponse OTPResponse = new OTPResponse
                {
                    OTP = SendOTP(mobileNo)
                };

                var apiResponse = new Response<OTPResponse>();
                apiResponse.Data = OTPResponse;

                return Ok(new ApiResult<Response<OTPResponse>>(new ApiResultCode(ApiResultType.Success, 200, "OTP successfully send"), apiResponse));
            }
            else
            {
                return BadRequest(new ApiResult<Response<OTPResponse>>(new ApiResultCode(ApiResultType.Error, 201, "Mobile no required"), null));
            }
        }

        [HttpGet]
        [Route("UpdatePasswordOTP/{mobileNo}")]
        public async Task<IActionResult> UpdatePasswordOTP(string mobileNo)
        {
            var currentUser = await _userManager.FindByNameAsync(mobileNo);
            if (currentUser != null)
            {

                OTPResponse OTPResponse = new OTPResponse
                {
                    UserId = Guid.Parse(currentUser.Id),
                    OTP = SendOTP(currentUser.UserName)
                };

                var apiResponse = new Response<OTPResponse>();
                apiResponse.Data = OTPResponse;

                return Ok(new ApiResult<Response<OTPResponse>>(new ApiResultCode(ApiResultType.Success, 200, "mobile verified successfully"), apiResponse));
            }
            else
            {
                return BadRequest(new ApiResult<Response<OTPResponse>>(new ApiResultCode(ApiResultType.Error, 201, "User not found"), null));
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetRequestModel forgetRequestModel)
        {
            Response<bool> response = new Response<bool>();
            response.Data = false;

            var currentUser = await _userManager.FindByIdAsync(forgetRequestModel.UserId);

            if (currentUser != null)
            {
                var removePassword = await _userManager.RemovePasswordAsync(currentUser);
                if (removePassword.Succeeded)
                {
                    var updatePassword = await _userManager.AddPasswordAsync(currentUser, forgetRequestModel.Password);
                    if (updatePassword.Succeeded)
                    {
                        response.Data = true;
                        return Ok(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Success, 200, "Password successfully change"), response));
                    }
                    else
                        return BadRequest(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Error, 201, "Internal error"), response));
                }
                else
                {
                    // Oops something went wrong
                    return BadRequest(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! something went wrong"), response));
                }
            }
            else
            {
                // user not found
                return BadRequest(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Error, 201, "User not found"), response));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("RegisterVendor")]
        public async Task<IActionResult> RegisterVendor([FromBody] VendorModel vendorModel)
        {
            Response<bool> response = new Response<bool>();
            response.Data = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                Name = vendorModel.Name,
                UserName = vendorModel.Mobile,
                Email = vendorModel.Email,
                UserType = vendorModel.UserType,
                IsActive = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, vendorModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! something went wrong"), response));
            }

            var registerUser = await _userManager.FindByNameAsync(vendorModel.Mobile);
            if (registerUser != null)
            {
                vendorModel.VendorId = Guid.Parse(registerUser.Id);
                var storeResult = _iVendorAccountRegistration.AddUpdateVendorStore(vendorModel);
                if (storeResult.HasSuccess)
                {
                    response.Data = true;
                    return Ok(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Success, 200, "Vendor successfully created"), response));
                }
                else
                    return BadRequest(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! something went wrong"), response));

            }
            else
                return BadRequest(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! something went wrong"), response));
        }

        [HttpPut]
        [Route("UpdatePersonProfile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdatePersonProfile([FromForm] UpdatePersonModel model)
        {
            Response<bool> response = new Response<bool>();
            response.Data = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userManager.FindByIdAsync(model.Id);

            currentUser.Name = !string.IsNullOrEmpty(model.Name) ? model.Name : currentUser.Name;
            currentUser.Email = !string.IsNullOrEmpty(model.Email) ? model.Email : currentUser.Email;

            IdentityResult result = await _userManager.UpdateAsync(currentUser);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! something went wrong"), response));
            }

            response.Data = true;
            return Ok(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Success, 200, "Person successfully updated"), response));
        }

        [HttpPut]
        [Route("UpdateVendorProfile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateVendorProfile([FromForm] UpdateVendorModel model)
        {
            Response<bool> response = new Response<bool>();
            response.Data = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userManager.FindByIdAsync(model.UserId);

            currentUser.Name = !string.IsNullOrEmpty(model.Name) ? model.Name : currentUser.Name;
            currentUser.Email = !string.IsNullOrEmpty(model.Email) ? model.Email : currentUser.Email;

            IdentityResult result = await _userManager.UpdateAsync(currentUser);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! something went wrong"), response));
            }
            else
            {
                var storeResult = _iVendorAccountRegistration.updateVendorProfile(model);
                response.Data = true;
            }
            
            return Ok(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Success, 200, "Vendor successfully updated"), response));
        }

        [HttpGet]
        [Route("GetPersonProfile/{userId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetPersonProfile(string userId)
        {
            var currentUser = await _userManager.FindByIdAsync(userId);

            if(currentUser!=null)
            {
                UpdatePersonModel personModel = new UpdatePersonModel
                {
                    Id=currentUser.Id,
                    Name = currentUser.Name,
                    Email = currentUser.Email
                };

                var response = new Response<UpdatePersonModel>();
                response.Data = personModel;

                return Ok(new ApiResult<Response<UpdatePersonModel>>(new ApiResultCode(ApiResultType.Success, 200, Constants.MessageKeys.User_Login), response));
            }
            else
            {
                return BadRequest(new ApiResult<Response<UpdatePersonModel>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! person not found"), null));
            }
        }

        [HttpGet]
        [Route("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var allUser = await _userManager.Users.ToListAsync();
            if (allUser != null)
            {
                var response = new Response<List<ApplicationUser>>();
                response.Data = allUser;

                return Ok(new ApiResult<Response<List<ApplicationUser>>>(new ApiResultCode(ApiResultType.Success, 200, "All user fetch"), response));

            }
            else
            {
                return BadRequest(new ApiResult<Response<List<ApplicationUser>>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! person not found"), null));
            }
        }

        [HttpGet]
        [Route("GetUserAssociateWIthStore/{userID}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserAssociateWIthStore(string userID)
        {
            var userInfo = await _userManager.FindByIdAsync(userID);
            if (userInfo != null)
            {
                UserModel model = new UserModel();
                model.UserName = userInfo.Name;
                model.UserType = Convert.ToInt32(userInfo.UserType) == 1 ? "User" : "Vendor";
                model.StoreName = _context.VendorStore.Any(t => t.VendorId == Guid.Parse(userID)) ? _context.VendorStore.FirstOrDefault(t => t.VendorId == Guid.Parse(userID)).Name : null;

                var response = new Response<UserModel>();
                response.Data = model;

                return Ok(new ApiResult<Response<UserModel>>(new ApiResultCode(ApiResultType.Success, 200, "User with store"), response));
            } 
            else
                return BadRequest(new ApiResult<Response<UserModel>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! record not found"), null));
        }

        [HttpGet]
        [Route("GetVendorUserDetailsByMobile/{mobileNo}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetVendorUserDetailsByMobile(string mobileNo)
        {
            var currentUser = await _userManager.FindByNameAsync(mobileNo);
            if (currentUser != null)
            {
                UserVendorModel model = new UserVendorModel();
                model.UserId = Guid.Parse(currentUser.Id);
                model.UserName = currentUser.Name;
                model.UserType = currentUser.UserType;
                model.StoreName = currentUser.UserType == "2" ? _context.VendorStore.FirstOrDefault(t => t.VendorId == Guid.Parse(currentUser.Id) && t.IsActive == true).Name : null;

                var response = new Response<UserVendorModel>();
                response.Data = model;

                return Ok(new ApiResult<Response<UserVendorModel>>(new ApiResultCode(ApiResultType.Success, 200, "Record successfully fetch"), response));
            }
            else
            {
                return BadRequest(new ApiResult<Response<UserVendorModel>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! record not found"), null));
            }
        }

        //[HttpGet]
        //[Route("GetVendorProfile/{userId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<IActionResult> GetVendorProfile(string userId)
        //{
        //    var currentUser = await _userManager.FindByIdAsync(userId);

        //    if (currentUser != null)
        //    {
        //        UpdateVendorModel vendorModel = new UpdateVendorModel
        //        {
        //            UserId = currentUser.Id,
        //            Name = currentUser.Name,
        //            Email = currentUser.Email,
        //            StoreName = currentUser.StoreName,
        //            Address = currentUser.Address,
        //            Landmark = currentUser.Landmark,
        //            City = currentUser.City,
        //            State = currentUser.State,
        //            Country = currentUser.Country,
        //            Zipcode = currentUser.Zipcode
        //        };

        //        var response = new Response<UpdateVendorModel>();
        //        response.Data = vendorModel;

        //        return Ok(new ApiResult<Response<UpdateVendorModel>>(new ApiResultCode(ApiResultType.Success, 200, Constants.MessageKeys.User_Login), response));
        //    }
        //    else
        //    {
        //        return BadRequest(new ApiResult<Response<UpdateVendorModel>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! vendor not found"), null));
        //    }
        //}





        private string GenerateJSONWebToken(ApplicationUser userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId",userInfo.Id),
                new Claim("Email",userInfo.Email),
                new Claim("Mobile",userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email,userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [NonAction]
        public int SendOTP(string mobileNo)
        {
            Random random = new Random();
            int otp = random.Next(1000, 9999);
            string text = otp.ToString();
            string toParty = "91" + mobileNo;
            string apiUrlForSMS = "https://www.thetexting.com/rest/sms/json/message/send?api_key=0fub24ktdddb83o&api_secret=sjz3skb0xitxmkj&to=" + toParty + "&text=" + text;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrlForSMS).Result;

            var msg = response.Content.ReadAsStringAsync().Result;

            return otp;
        }
    }
}
