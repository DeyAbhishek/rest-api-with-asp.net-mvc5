using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Framework;
using TPO.Model.Reference;

namespace TPO.Model.Scrim
{
    [DisplayName("Scrim Roll")]
    public class ScrimRollModel : TPOModelBase
    {
        #region Variables
        private DateTime _WovenDate;
        private DateTime _DateReceived;

        
        
        #endregion

        #region Properties
        [DisplayName("Roll Number")]
        [Required(ErrorMessage="The Roll Number field is required")]
        [StringLength(25, ErrorMessage="Roll Number must be under 25 characters")]
        public string ScrimRollCode { get; set; }
        [DisplayName("Lot")]
        [StringLength(10, ErrorMessage = "Lot code must be under 10 characters")]
        public string LotCode { get; set; }
        [DisplayName("Woven Lot")]
        public Nullable<int> WovenLotCode { get; set; }    
        
        public int PlantID { get; set; }
        [DisplayName("Roll Type")]
        public int ScrimRollTypeID { get; set; }

        [Required]
        public int WeightUnitOfMeasureID { get; set; }
        [Required]
        public int LengthUnitOfMeasureID { get; set; }


        [Range(0, 9999, ErrorMessage="Value cannot be negative.")]
        public decimal Length { get; set; }
        [Range(0, 9999, ErrorMessage = "Value cannot be negative.")]
        public decimal Weight { get; set; }

        [DisplayName("Woven Date")]
        public DateTime WovenDate
        {
            get { return _WovenDate; }
            set { _WovenDate = value; }
           
        }

        [DisplayName("Received Date")]
        [Required(ErrorMessage = "The Received Date field is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateReceived
        {
            get { if (_DateReceived == new DateTime()) { return DateTime.Now; } else { return _DateReceived; } }
            set { _DateReceived = value; }
        }

        [DisplayName("Tare Weight")]
        [Range(0, 9999, ErrorMessage = "Value cannot be negative.")]
        public decimal TareWeight { get; set; }
        [DisplayName("Received Length")]
        [Range(0, 9999, ErrorMessage = "Value cannot be negative.")]
        public decimal ReceivedLength { get; set; }
        [DisplayName("Net Weight")]
        [Range(0, 9999, ErrorMessage = "Value cannot be negative.")]
        public decimal ReceivedWeight { get; set; }
        [DisplayName("Received Tare Weight")]
        [Range(0, 9999, ErrorMessage = "Value cannot be negative.")]
        public decimal ReceivedTareWeight { get; set; }
        [DisplayName("Tare Weight")]
        [Range(0, 9999, ErrorMessage = "Value cannot be negative.")]
        public decimal LengthUsed { get; set; }
        [DisplayName("Weight Weight")]
        [Range(0, 9999, ErrorMessage = "Value cannot be negative.")]
        public decimal WeightUsed { get; set; }
        
        

        public UnitOfMeasureModel WeightUnitOfMeasure { get; set; }
        public UnitOfMeasureModel LengthUnitOfMeasure { get; set; }

        public string WeightDisplay 
        {
            get { return string.Format("{0} {1}", Weight, WeightUnitOfMeasure != null ? WeightUnitOfMeasure.Code : string.Empty);}
        }

        public string LengthDisplay
        {
            get { return string.Format("{0} {1}", Length, LengthUnitOfMeasure != null ? LengthUnitOfMeasure.Code : string.Empty); }
        }

        #endregion

        #region Constructors
        public ScrimRollModel() : base() 
        {
            _WovenDate = DateTime.MinValue;
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
