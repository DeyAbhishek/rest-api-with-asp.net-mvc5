
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Services.Application;
using TPO.Services.RawMaterials;

namespace TPO.Web.Models
{
    public class RawMaterial: BaseViewModel
    {
        #region Variables
        #endregion

        #region Properties
        public string Code { get; set; }
        public string Description { get; set; }
        public string FSBPID { get; set; }

        public double PricePerUoM { get; set; }
        public double Density { get; set; }
        public double Stock { get; set; }

        public int UoMId { get; set; }
        public int RawMaterialVendorId { get; set; }
        public DateTime LastModified { get; set; }

        public string VendorName { get; set;  }

        private RawMaterialVendorModel _defaultVendor = null;

        public int DefaultVendorId
        {
            get
            {
                if (_defaultVendor == null)
                    _defaultVendor = GetDefaultVendor();

                return _defaultVendor.Id;
            }
        }

        private RawMaterialVendorModel GetDefaultVendor()
        {
            using (RawMaterialVendorService service = new RawMaterialVendorService())
            {
                var dto = service.GetByPlantId(PlantId).First();
                return Mapper.Map<RawMaterialVendorDto, RawMaterialVendorModel>(dto);
            }
        }

        public string DefaultVendorName
        {
            get
            {
                if (_defaultVendor == null)
                    _defaultVendor = GetDefaultVendor();

                return _defaultVendor.Vendor;
            }
        }
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        private static List<UnitOfMeasureDefaultViewModel> _uomDefaults = null;

        public List<UnitOfMeasureDefaultViewModel> UoMDefaults
        {
            get
            {
                if (_uomDefaults == null)
                    _uomDefaults = GetUoMDefaults();

                return _uomDefaults;
            }
        }

        private List<UnitOfMeasureDefaultViewModel> GetUoMDefaults()
        {
            List<UnitOfMeasureDefaultViewModel> uomDefaults = new List<UnitOfMeasureDefaultViewModel>();

            using (UnitOfMeasureDefaultService service = new UnitOfMeasureDefaultService())
            {
                var dtos = service.GetAllByPlantId(PlantId);
                uomDefaults.AddRange(Mapper.Map<List<UnitOfMeasureDefaultDto>, List<UnitOfMeasureDefaultViewModel>>(dtos));
            }

            return uomDefaults;
        }

        private string _uoM = string.Empty;
        public string UoM
        {
            get
            {
                if (string.IsNullOrEmpty(_uoM))
                    _uoM = GetUoMCode(UoMId);
                return _uoM;
            }
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private string GetUoMCode(int uoMID)
        {
            using (UnitOfMeasureService service = new UnitOfMeasureService())
            {
                return service.Get(uoMID).Code;
            }
        }
        #endregion

        #region Events
        #endregion

        #region Event Handlers
        #endregion
    }
}