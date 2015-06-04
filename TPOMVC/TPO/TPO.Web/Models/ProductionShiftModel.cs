using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Services.Production;

namespace TPO.Web.Models
{
    public class ProductionShiftModel : BaseViewModel
    {
        public int TypeID { get; set; }
        public string Code { get; set; }

        private string _startTimeStr;
        private string _endTimeStr;

        public string TypeCode { get; set; }

        private static ProductionShiftTypeModel _defaultProductionShiftType = null;

        public ProductionShiftTypeModel DefaultProductionShiftType
        {
            get
            {
                if (_defaultProductionShiftType == null)
                    _defaultProductionShiftType = GetDefaultProductionShiftType();

                return _defaultProductionShiftType;
            }
        }

        private ProductionShiftTypeModel GetDefaultProductionShiftType()
        {
            ProductionShiftTypeModel productionShiftType = new ProductionShiftTypeModel();
            using (ProductionShiftTypeService svc = new ProductionShiftTypeService())
            {
                var dto = svc.GetAll().FirstOrDefault();
                productionShiftType = Mapper.Map<ProductionShiftTypeDto, ProductionShiftTypeModel>(dto);
            }
            return productionShiftType;
        }

        private List<ProductionShiftTypeModel> _productionShiftTypes = null;

        
        public string StartTimeStr { get; set; }

        public string EndTimeStr { get; set;  }
    }
}