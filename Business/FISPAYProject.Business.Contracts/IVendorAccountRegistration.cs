using FISPAYProject.CoreApi;
using FISPAYProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISPAYProject.Business.Contracts
{
    public interface IVendorAccountRegistration
    {
        ApiResult<bool> AddUpdateVendorAccountAccount(VendorAccountRegistrationModel model);
        ApiResult<bool> AddUpdateVendorStore(VendorModel model);
        ApiResult<List<VendorStoreModel>> GetVendorStoreByVendorId(Guid vendorId);
        ApiResult<List<VendorStoreModel>> GetAllVendorStoreByCityName(string cityName);
        ApiResult<bool> updateVendorProfile(UpdateVendorModel model);
    }
}
