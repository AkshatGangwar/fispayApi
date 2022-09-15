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
    public class VendorAccountRegistrationRepository : IVendorAccountRegistration
    {
        private readonly FISPAYContext _context = null;
        public VendorAccountRegistrationRepository(FISPAYContext context)
        {
            _context = context;
        }

        public ApiResult<bool> AddUpdateVendorAccountAccount(VendorAccountRegistrationModel model)
        {
            ApiResult<bool> objResult;

            try
            {
                VendorAccountRegistration? vendorAccountRegistration = null;
                vendorAccountRegistration = model.Id == null ?
                    new VendorAccountRegistration { Id = Guid.NewGuid(), VendorId = model.VendorId.Value, CreatedBy = Guid.NewGuid(), CreatedDate = DateTime.Now }
                    : _context.VendorAccountRegistration.FirstOrDefault(t => t.Id == model.Id);

                #region "Common Data"
                vendorAccountRegistration.AccountNumber = model.AccountNumber;
                vendorAccountRegistration.BankName = model.BankName;
                vendorAccountRegistration.IFSCCode = model.IFSCCode;
                vendorAccountRegistration.AccountHolderName = model.AccountHolderName;
                vendorAccountRegistration.BranchName = model.BranchName;
                vendorAccountRegistration.AccountType = model.AccountType.Value;
                #endregion

                if (model.Id == null)
                {
                    // Add Record
                    vendorAccountRegistration.IsActive = true;
                    _context.VendorAccountRegistration.Add(vendorAccountRegistration);

                }
                else
                {
                    // update Record
                    vendorAccountRegistration.ModifiedBy = model.ModifiedBy;
                    vendorAccountRegistration.ModifiedDate = DateTime.Now;
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

        public ApiResult<bool> AddUpdateVendorStore(VendorModel model)
        {
            ApiResult<bool> objResult;
            try
            {
                VendorStore? vendorStore = null;
                vendorStore = model.Id == null ?
                    new VendorStore { Id = Guid.NewGuid(), VendorId = model.VendorId.Value, CreatedBy = Guid.NewGuid(), CreatedDate = DateTime.Now }
                    : _context.VendorStore.FirstOrDefault(t => t.Id == model.Id);

                #region "Common Data"
                vendorStore.VendorId = model.VendorId.Value;
                vendorStore.Name = model.StoreName;
                vendorStore.Description = model.Description;
                vendorStore.Address = model.Address;
                vendorStore.Landmark = model.Landmark;
                vendorStore.City = model.City;
                vendorStore.District = model.District;
                vendorStore.State = model.State;
                vendorStore.Country = model.Country;
                vendorStore.Zipcode = model.Zipcode;
                vendorStore.Latitude = model.Latitude;
                vendorStore.Longitude = model.Longitude;
                #endregion

                if (model.Id == null)
                {
                    // Add Record
                    vendorStore.IsActive = true;
                    _context.VendorStore.Add(vendorStore);
                }
                else
                {
                    // update Record
                    //vendorStore.ModifiedBy = model.ModifiedBy;
                    vendorStore.ModifiedDate = DateTime.Now;
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

        public ApiResult<bool> updateVendorProfile(UpdateVendorModel model)
        {
            ApiResult<bool> objResult;
            try
            {
                VendorStore? vendorStore = null;
                vendorStore = _context.VendorStore.FirstOrDefault(t => t.VendorId == Guid.Parse(model.UserId));

                #region "Common Data"
                vendorStore.Name = model.StoreName;
                vendorStore.Description = model.Description;
                vendorStore.Address = model.Address;
                vendorStore.Landmark = model.Landmark;
                vendorStore.City = model.City;
                vendorStore.District = model.District;
                vendorStore.State = model.State;
                vendorStore.Country = model.Country;
                vendorStore.Zipcode = model.Zipcode;
                vendorStore.Latitude = model.Latitude;
                vendorStore.Longitude = model.Longitude;
                #endregion

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

        public ApiResult<List<VendorStoreModel>> GetVendorStoreByVendorId(Guid vendorId)
        {
            ApiResult<List<VendorStoreModel>> objResult;

            try
            {
                var record = (from vndrstr in _context.VendorStore
                              where vndrstr.VendorId == vendorId
                              select new VendorStoreModel
                              {
                                  StoreId = vndrstr.Id,
                                  StoreName = vndrstr.Name,
                                  Description = vndrstr.Description,
                                  Address = vndrstr.Address,
                                  Landmark = vndrstr.Landmark,
                                  City = vndrstr.City,
                                  District = vndrstr.District,
                                  State = vndrstr.State,
                                  Country = vndrstr.Country,
                                  Zipcode = vndrstr.Zipcode,
                                  Latitude = vndrstr.Latitude,
                                  Longitude = vndrstr.Longitude
                              }).ToList();

                if (record != null)
                    objResult = new ApiResult<List<VendorStoreModel>>(new ApiResultCode(ApiResultType.Success), record);
                else
                    objResult = new ApiResult<List<VendorStoreModel>>(new ApiResultCode(ApiResultType.Error, 201, "Record not found"));
            }
            catch (Exception)
            {
                objResult = new ApiResult<List<VendorStoreModel>>(new ApiResultCode(ApiResultType.Error, 201, "Internal error"));
            }

            return objResult;
        }

        public ApiResult<List<VendorStoreModel>> GetAllVendorStoreByCityName(string cityName)
        {
            ApiResult<List<VendorStoreModel>> objResult;

            try
            {
                var record = (from vndrstr in _context.VendorStore
                              where vndrstr.City.ToLower() == cityName.ToLower()
                              select new VendorStoreModel
                              {
                                  VendorId = vndrstr.VendorId,
                                  StoreId = vndrstr.Id,
                                  StoreName = vndrstr.Name,
                                  Description = vndrstr.Description,
                                  Address = vndrstr.Address,
                                  Landmark = vndrstr.Landmark,
                                  City = vndrstr.City,
                                  District = vndrstr.District,
                                  State = vndrstr.State,
                                  Country = vndrstr.Country,
                                  Zipcode = vndrstr.Zipcode,
                                  Latitude = vndrstr.Latitude,
                                  Longitude = vndrstr.Longitude
                              }).ToList();

                if (record != null)
                    objResult = new ApiResult<List<VendorStoreModel>>(new ApiResultCode(ApiResultType.Success), record);
                else
                    objResult = new ApiResult<List<VendorStoreModel>>(new ApiResultCode(ApiResultType.Error, 201, "Record not found"));
            }
            catch (Exception)
            {
                objResult = new ApiResult<List<VendorStoreModel>>(new ApiResultCode(ApiResultType.Error, 201, "Internal error"));
            }

            return objResult;
        }
    }
}
