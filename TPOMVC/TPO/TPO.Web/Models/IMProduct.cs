using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace TPO.Web.Models
{
    public class IMProduct : BaseViewModel
    {
        #region Variables
        #endregion

        #region Properties
        

        [Required(ErrorMessage = "Product Code is required")]
        [DisplayName("Product Code:")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Product Description is required")]
        [DisplayName("Product Description:")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Product Type is required")]
        [DisplayName("Product Type:")]
        public int IMProductTypeID { get; set; }

        [DisplayName("Parts Per Carton:")]
        public int PartsPerCarton { get; set; }

        [DisplayName("Cartons Per Lot:")]
        public int CartonsPerLot { get; set; }

        [Required(ErrorMessage = "Raw Material is required")]
        [DisplayName("Raw Material:")]
        public int RawMaterialID { get; set; }

        [Required(ErrorMessage = "Thickness UoM is required")]
        public int ThickUOM { get; set; }

        [Required(ErrorMessage = "Weight UoM is required")]
        public int WeightUOM { get; set; }

        [DisplayName("Minimum Thickness:")]
        public double MinimumThick { get; set; }

        [DisplayName("Minimum Weight:")]
        public double MinimumWeight { get; set; }

        public string Label1 { get; set; }

        [DisplayName("Label Line 2:")]
        public string Label2 { get; set; }

        public string Label3 { get; set; }

        [DisplayName("Active:")]
        public bool Active { get; set; }
        #endregion

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
    }
}