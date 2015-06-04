using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Services.Application;
using TPO.Services.Scrim;


namespace TPO.Web.Models
{
    [DisplayName("Scrim Roll:")]
    public class ScrimRollModel : BaseViewModel
    {
        #region Constructors

        #endregion // Constructors

        #region Variables
        private DateTime? _wovenDate;
        private DateTime _DateReceived;



        #endregion

        #region Properties
        [DisplayName("Roll Number:")]
        [Required(ErrorMessage = "The Roll Number field is required")]
        [StringLength(25, ErrorMessage = "Roll Number must be under 25 characters")]
        public string Code { get; set; }
        [DisplayName("Lot:")]
        [StringLength(10, ErrorMessage = "Lot code must be under 10 characters")]
        public string LotNumber { get; set; }
        [DisplayName("Woven Lot:")]
        public Nullable<int> WovenLotNumber { get; set; }

    
        [DisplayName("Roll Type:")]
        public int TypeID { get; set; }

        [Required]
        public int WeightUoMID { get; set; }
        [Required]
        public int LengthUoMID { get; set; }

        private bool _isLoaded = false;
        public bool IsLoaded { get { return _isLoaded; } set { _isLoaded = value;  } }

        private decimal _length = 0;
        [Range(0, 9999, ErrorMessage = "Value cannot be negative.")]
        public decimal Length {
            get { return _length; }
            set
            {
                _length = value;
                if (IsLoaded)
                    SetWeightFromLength(_length, ref _weight);
            } 
        }

        private void SetWeightFromLength(decimal length, ref decimal weight)
        {
            if (ReceivedLength > 0)
                weight = ReceivedWeight * (length / ReceivedLength);
            else
            {
                weight = 0;
            }
        }

        private decimal _weight = 0;

        [DisplayName("Roll Weight")]
        [Range(0, 9999, ErrorMessage = "Value cannot be negative.")]
        public decimal Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                if (IsLoaded)
                    SetLengthFromWeight(_weight, ref _length);
            }
        }

        private void SetLengthFromWeight(decimal weight, ref decimal length)
        {
            if (ReceivedWeight > 0)
                length = ReceivedLength * (weight / ReceivedWeight);
            else
            {
                length = 0;
            }
        }

        [DisplayName("Woven Date:")]
        public DateTime? WovenDate
        {
            get { return _wovenDate; }
            set { _wovenDate = value; }

        }

        [DisplayName("Received Date:")]
        [Required(ErrorMessage = "The Received Date field is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateReceived
        {
            get { if (_DateReceived == new DateTime()) { return DateTime.Now; } else { return _DateReceived; } }
            set { _DateReceived = value; }
        }

        [DisplayName("Tare Weight:")]
        [Range(0, 9999, ErrorMessage = "Value cannot be negative.")]
        public decimal TareWeight { get; set; }
        [DisplayName("Received Length:")]
        [Range(0, 9999, ErrorMessage = "Value cannot be negative.")]
        public decimal ReceivedLength { get; set; }

        private decimal _receivedWeight = 0;

        [DisplayName("Net Weight:")]
        [Range(0, 9999, ErrorMessage = "Value cannot be negative.")]
        public decimal ReceivedWeight
        {
            get { return _receivedWeight; } 
            set { _receivedWeight = Weight - TareWeight; }
        }

        [DisplayName("Received Tare Weight:")]
        [Range(0, 9999, ErrorMessage = "Value cannot be negative.")]
        public decimal ReceivedTareWeight { get; set; }
        
        private decimal _lengthUsed = 0;
        [DisplayName("Tare Weight:")]
        [Range(0, 9999, ErrorMessage = "Value cannot be negative.")]
        public decimal LengthUsed
        {
            get { return _lengthUsed; }
            set
            {
                _lengthUsed = value;
                if (IsLoaded)
                    SetWeightFromLength(_lengthUsed, ref _weightUsed);
            }
        }


        private decimal _weightUsed = 0;

        [DisplayName("Weight Weight:")]
        [Range(0, 9999, ErrorMessage = "Value cannot be negative.")]
        public decimal WeightUsed
        {
            get { return _weightUsed; }
            set
            {
                _weightUsed = value;
                if (IsLoaded)
                    SetLengthFromWeight(_weightUsed, ref _lengthUsed);
            }
        }


        private UnitOfMeasureModel _weightUnitOfMeasureModel = null;

        public UnitOfMeasureModel WeightUnitOfMeasure
        {
            get
            {
                if (_weightUnitOfMeasureModel == null)
                    _weightUnitOfMeasureModel = GetUnitOfMeasure(WeightUoMID);

                return _weightUnitOfMeasureModel;
            }
        }

        public string WeightUomText
        {
            get { return WeightUnitOfMeasure.Code;  }
        }

        private UnitOfMeasureModel _lengthUnitOfMeasureModel = null;

        public UnitOfMeasureModel LengthUnitOfMeasure
        {
            get
            {
                if (_lengthUnitOfMeasureModel == null)
                    _lengthUnitOfMeasureModel = GetUnitOfMeasure(LengthUoMID);

                return _lengthUnitOfMeasureModel;
            }
        }

        public string LengthUomText
        {
            get { return LengthUnitOfMeasure.Code; }
        }

        private UnitOfMeasureModel GetUnitOfMeasure(int uomId)
        {
            UnitOfMeasureModel uom = null;
            using (UnitOfMeasureService service = new UnitOfMeasureService())
            {
                var dto = service.Get(uomId);
                uom = Mapper.Map<UnitOfMeasureDto, UnitOfMeasureModel>(dto);
            }
            return uom;
        }



        public string WeightDisplay
        {
            get { return string.Format("{0} {1}", Weight, WeightUnitOfMeasure != null ? WeightUnitOfMeasure.Code : string.Empty); }
        }

        public string LengthDisplay
        {
            get { return string.Format("{0} {1}", Length, LengthUnitOfMeasure != null ? LengthUnitOfMeasure.Code : string.Empty); }
        }

        private double _area = 0.0;
        private string _areaUom = "";

        public string AreaDisplay
        {
            get 
            {
                CalcArea();
                return string.Format("{0} {1}", _area, _areaUom); 
            }
        }


        protected void CalcArea()
        {
            int areaUoMId = 0;

            using( ScrimTypeService service = new ScrimTypeService() )
            {
                _area = 0.0;
                ScrimTypeDto dto = service.Get(TypeID);
                _area = (double)Length * dto.Width;
                areaUoMId = dto.AreaUoMID;
            }
            using (UnitOfMeasureService service = new UnitOfMeasureService())
            {
                _areaUom = service.Get(areaUoMId).Code;
            }
        }


        #endregion

        #region Constructors
        public ScrimRollModel()
            : base()
        {
            _wovenDate = DateTime.MinValue;
            _DateReceived = DateTime.MinValue;
        }

        #endregion

        #region Public Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Events
        #endregion

        #region Event Handlers
        #endregion
    }
}