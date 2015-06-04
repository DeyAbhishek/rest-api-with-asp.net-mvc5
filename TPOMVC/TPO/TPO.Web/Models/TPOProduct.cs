using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace TPO.Web.Models
{
    public class TPOProduct : BaseViewModel
    {
        #region Variables
        #endregion

        #region Properties


        [Required(ErrorMessage = "Product Code is required")]
        [DisplayName("Product Code:")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Product Description is required")]
        [DisplayName("Product Description:")]
        public string ProductDesc { get; set; }

        [DisplayName("Thickness:")]
        public double Thick { get; set; }

        public int ProductLabelID { get; set; }

        [Required(ErrorMessage = "Thickness Unit is required")]
        public int ThickUnit { get; set; }

        [Required(ErrorMessage = "Width Unit is required")]
        public int LengthWidthUnit { get; set; }

        [DisplayName("Width:")]
        public double Width { get; set; }

        [DisplayName("Master Roll Length:")]
        public double MasRollLength { get; set; }

        [DisplayName("Contractor Roll Length:")]
        public double ContRollLength { get; set; }

        [Required(ErrorMessage = "Area Unit is required")]
        [DisplayName("Area:")]
        public int AreaUnit { get; set; }

        [Required(ErrorMessage = "Weight Unit is required")]
        [DisplayName("Weight:")]
        public int WeightUnit { get; set; }

        [Required(ErrorMessage = "Scrim Type is required")]
        [DisplayName("Scrim Type:")]
        public int ScrimTypeID { get; set; }

        [DisplayName("Production Quantity:")]
        public int QtyMade { get; set; }

        [DisplayName("Weight / Area:")]
        public double WtperArea { get; set; }

        [DisplayName("Rolls Per Lot:")]
        public int RollsPerLotPal { get; set; }
        public int RollsPerLot { get; set; }


        [DisplayName("Active:")]
        public bool Active { get; set; }

        [DisplayName("Display CE Logo on Label:")]
        public bool UseCELogo { get; set; }
        #endregion
        public LabelDetail TPOProductLabelDetail { get; set; }

        #region Constructors
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

        #region Detail Classes
        
        public class LabelDetail
        {
            [DisplayName("Label Lines 2-3:")]
            public int ID { get; set; }
            
            public string Label1 { get; set; }
            public string Label2 { get; set; }
            public string Label3 { get; set; }
        }
        #endregion
    }
}