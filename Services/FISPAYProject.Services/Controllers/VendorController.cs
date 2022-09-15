using FISPAYProject.Business.Contracts;
using FISPAYProject.CoreApi;
using FISPAYProject.Model;
using FISPAYProject.Services.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FISPAYProject.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorAccountRegistration _vendor;

        public VendorController(IVendorAccountRegistration vendor)
        {
            _vendor = vendor;
        }

        [HttpPost]
        [Route("VendorAccountRegistration")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult VendorAccountRegistration(VendorAccountRegistrationModel model)
        {
            Response<bool> response = new Response<bool>();
            response.Data = false;

            var currentUser = User.Identity.Name;

            if (ModelState.IsValid)
            {
                var result = _vendor.AddUpdateVendorAccountAccount(model);
                if(result.HasSuccess)
                {
                    response.Data = true;
                    return Ok(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Success, 200, "Account registration successfully done"), response));
                }
                else
                    return BadRequest(new ApiResult<Response<bool>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! something went wrong"), response));
            }
            else
            {
                return BadRequest(new ApiResult<Response<OTPResponse>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! data is not valid"), null));
            }
        }

        [HttpGet]
        [Route("GetVendorStore/{vendorId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetVendorStore(Guid vendorId)
        {
            var result = _vendor.GetVendorStoreByVendorId(vendorId);
            if (result != null)
            {
                var response = new Response<List<VendorStoreModel>>();

                response.Data = result.DataObject;

                return Ok(new ApiResult<Response<List<VendorStoreModel>>>(new ApiResultCode(ApiResultType.Success, 200, "Store successfully fetch"), response));
            }
            else
            {
                return BadRequest(new ApiResult<Response<List<VendorStoreModel>>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! something went wrong"), null));
            }
        }

        [HttpGet]
        [Route("GetStoreByCity/{cityName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetStoreByCity(string cityName)
        {
            var result = _vendor.GetAllVendorStoreByCityName(cityName);
            if (result != null)
            {
                var response = new Response<List<VendorStoreModel>>();

                response.Data = result.DataObject;

                return Ok(new ApiResult<Response<List<VendorStoreModel>>>(new ApiResultCode(ApiResultType.Success, 200, "Store successfully fetch city wise"), response));
            }
            else
            {
                return BadRequest(new ApiResult<Response<List<VendorStoreModel>>>(new ApiResultCode(ApiResultType.Error, 201, "Oops! something went wrong"), null));
            }
        }

    }
}
