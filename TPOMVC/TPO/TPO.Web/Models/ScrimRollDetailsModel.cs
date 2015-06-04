using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Services.Application;
using TPO.Services.Scrim;

namespace TPO.Web.Models
{
    public class ScrimRollDetailsModel : BaseViewModel
    {

        private int _scrimRollId = 0;
        private ScrimRollModel _scrimRoll = null;
        private ScrimTypeModel _scrimType = null;
        private double? _area = null;
        private double? _receivedArea = null;
        private double? _areaUsed = null;
        private UnitOfMeasureModel _areaUnitOfMeasureModel = null;

        private ScrimRollModel ScrimRoll
        {
            get
            {
                if (_scrimRoll == null)
                {
                    using (ScrimRollService service = new ScrimRollService())
                    {
                        var dto = service.Get(_scrimRollId);
                        _scrimRoll = Mapper.Map<ScrimRollDto, ScrimRollModel>(dto);
                    }
                }
                return _scrimRoll;
            }
        }

        public ScrimRollDetailsModel(int scrimRollId)
        {
            _scrimRollId = scrimRollId;
        }

        public ScrimTypeModel ScrimType
        {
            get
            {
                if (_scrimType == null)
                    _scrimType = GetScrimType(ScrimRoll.TypeID);

                return _scrimType;
            }
        }

        //private Dictionary<int, ScrimTypeModel> _scrimTypes = new Dictionary<int, ScrimTypeModel>(); 
        private ScrimTypeModel GetScrimType(int typeId)
        {
            //if (!_scrimTypes.ContainsKey(typeId))
            {
                ScrimTypeModel type = null;
                using (ScrimTypeService service = new ScrimTypeService())
                {
                    var dto = service.Get(typeId);
                    type = Mapper.Map<ScrimTypeDto, ScrimTypeModel>(dto);
                    //_scrimTypes.Add(typeId, type);
                }
                return type;
            }
            //return _scrimTypes[typeId];
        }

        public double Area
        {
            get
            {
                if (!_area.HasValue)
                    _area = GetArea(ScrimRoll.Length, (decimal)ScrimType.Width);
                return Math.Round((double)_area.Value, 0);
            }
        }

        public double ReceivedArea
        {
            get
            {
                if (!_receivedArea.HasValue)
                    _receivedArea = GetArea(ScrimRoll.ReceivedLength, (decimal)ScrimType.Width);
                return Math.Round((double)_receivedArea.Value, 0);
            }
        }

        public double AreaUsed
        {
            get
            {
                if (!_areaUsed.HasValue)
                    _areaUsed = GetArea(ScrimRoll.LengthUsed, (decimal)ScrimType.Width);
                //return _areaUsed.Value;
               // AreaUsed = _areaUsed.Value;
                return Math.Round((double)_areaUsed, 0);
            }
        }

        private double GetArea(decimal length, decimal width)
        {
            string areaUnitCode = ScrimType.AreaUoM.Substring(2);
            int areaUoMID = GetUnitOfMeasure(areaUnitCode).Id;
            decimal convertedLength = Convert(length, ScrimRoll.LengthUoMID, areaUoMID);
            decimal convertedWidth = Convert(width, ScrimType.WidthUoMID, areaUoMID);
            using (ScrimTypeService service = new ScrimTypeService())
            {
                return (double)(convertedLength * convertedWidth);
            }
        }

        private decimal Convert(decimal fromMeasurement, int fromUoMID, int toUoMID)
        {
            using (UoMConversionService service = new UoMConversionService())
            {
                return service.ConvertUoM(fromUoMID, fromMeasurement, toUoMID);
            }
        }

        public UnitOfMeasureModel AreaUnitOfMeasure
        {
            get
            {
                if (_areaUnitOfMeasureModel == null)
                    _areaUnitOfMeasureModel = GetUnitOfMeasure(ScrimType.AreaUoMID);

                return _areaUnitOfMeasureModel;
            }
        }

        public string AreaUomText
        {
            get { return ScrimType.AreaUoM; }
        }

        //private Dictionary<int, UnitOfMeasureModel> _uomsById = new Dictionary<int, UnitOfMeasureModel>(); 
        //private Dictionary<string, UnitOfMeasureModel> _uomsByCode = new Dictionary<string, UnitOfMeasureModel>(); 
        private UnitOfMeasureModel GetUnitOfMeasure(int uomId)
        {
            //if (!_uomsById.ContainsKey(uomId))
            {
                UnitOfMeasureModel uom = null;
                using (UnitOfMeasureService service = new UnitOfMeasureService())
                {
                    var dto = service.Get(uomId);
                    uom = Mapper.Map<UnitOfMeasureDto, UnitOfMeasureModel>(dto);
                    //_uomsById.Add(uomId, uom);
                    //_uomsByCode.Add(uom.Code, uom);
                }
                return uom;
            }
            //return _uomsById[uomId];
        }

        private UnitOfMeasureModel GetUnitOfMeasure(string code)
        {
            //if (!_uomsByCode.ContainsKey(code))
            {
                UnitOfMeasureModel uom = null;
                using (UnitOfMeasureService service = new UnitOfMeasureService())
                {
                    var dto = service.GetByCode(code);
                    uom = Mapper.Map<UnitOfMeasureDto, UnitOfMeasureModel>(dto);
                    //_uomsById.Add(uom.ID, uom);
                    //_uomsByCode.Add(uom.Code, uom);
                }
                return uom;
            }
            //return _uomsByCode[code];
        }
    }
}