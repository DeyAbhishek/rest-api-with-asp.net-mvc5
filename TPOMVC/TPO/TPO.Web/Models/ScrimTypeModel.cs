using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Services.Application;
using TPO.Services.Scrim;

namespace TPO.Web.Models
{
    public class ScrimTypeModel : BaseViewModel
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int WidthUoMID { get; set; }

        public int WeightUoMID { get; set; }
        public int LengthUoMID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public bool IsLiner { get; set; }
        public System.DateTime LastModified { get; set; }
        public int AreaUoMID { get; set; }

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
                var dtos = service.GetAllByPlantId(PlantID);
                uomDefaults.AddRange(Mapper.Map<List<UnitOfMeasureDefaultDto>, List<UnitOfMeasureDefaultViewModel>>(dtos));
            }

            return uomDefaults;
        }

        private double? _area = null;
        public double Area
        {
            get
            {
                if (!_area.HasValue)
                    _area = GetArea();
                return _area.Value;
            }
        }

        private double GetArea()
        {
            string areaUnitCode = AreaUoM.Substring(2);
            int areaUoMID = (new UnitOfMeasureService().GetByCode(areaUnitCode)).ID;
            decimal length = Convert(Length, LengthUoMID, areaUoMID);
            decimal width = Convert(Width, WidthUoMID, areaUoMID);
            using (ScrimTypeService service = new ScrimTypeService())
            {
                return (double)(length * width);
            }
        }

        private decimal Convert(double fromMeasurement, int fromUoMID, int toUoMID)
        {
            using (UoMConversionService service = new UoMConversionService())
            {
                return service.ConvertUoM(fromUoMID, (decimal)fromMeasurement, toUoMID);
            }
        }


        private string _widthUoM = string.Empty;
        public string WidthUoM
        {
            get
            {
                if (string.IsNullOrEmpty(_widthUoM))
                    _widthUoM = GetUoMCode(WidthUoMID);
                return _widthUoM;
            }
        }

        private string _lengthUoM = string.Empty;
        public string LengthUoM
        {
            get
            {
                if (string.IsNullOrEmpty(_lengthUoM))
                    _lengthUoM = GetUoMCode(LengthUoMID);
                return _lengthUoM;
            }
        }

        private string _weightUoM = string.Empty;
        public string WeightUoM
        {
            get
            {
                if (string.IsNullOrEmpty(_weightUoM))
                    _weightUoM = GetUoMCode(WeightUoMID);
                return _weightUoM;
            }
        }

        private string _areaUoM = string.Empty;
        public string AreaUoM
        {
            get
            {
                if (string.IsNullOrEmpty(_areaUoM))
                    _areaUoM = GetUoMCode(AreaUoMID);
                return _areaUoM;
            }
        }

        private string GetUoMCode(int uoMID)
        {
            using (UnitOfMeasureService service = new UnitOfMeasureService())
            {
                return service.Get(uoMID).Code;
            }
        }

    }
}