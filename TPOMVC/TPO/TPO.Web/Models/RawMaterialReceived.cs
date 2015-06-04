using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class RawMaterialReceived: BaseViewModel
    {

        [DisplayName("Raw Material:")]
        public string RawMaterialId { get; set; }

        [DisplayName("Raw Material Description:")]
        public string RawMaterialDescription { get; set; }

        [DisplayName("Raw Material Code:")]
        public string RawMaterialCode { get; set; }

        [Required]
        [DisplayName("Material Lot No:")]
        public String LotNumber { get; set; }

        [DisplayName("Received Date:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateEntered { get; set; }

        public string DateEnteredDisplay
        {
            get
            {
                return DateEntered.HasValue ? DateEntered.Value.ToShortDateString() : string.Empty;
            }
        }
        [Required]
        [DisplayName("Received By:")]
        public string EnteredBy { get; set; }

        [DisplayName("Last Modified Date:")]
        public DateTime? LastModified { get; set; }

        [DisplayName("Last Modified By:")]
        public string ModifiedBy { get; set; }

        [Required(ErrorMessage = "The Quantity Shipped field is required")]
        [DisplayName("Quantity Shipped:")]
        public double QuantityShipped { get; set; }

        [Required(ErrorMessage = "The Quantity Received field is required")]
        [DisplayName("Quantity Received:")]
        public double QuantityReceived { get; set; }

        [Required(ErrorMessage = "The Quantity Rejected field is required")]
        [DisplayName("Quantity Rejected:")]
        public double QuantityNotReceived { get; set; }

        [DisplayName("Quantity Used By This Lot:")]
        public double QuantityUsedThisLot { get; set; }

        [DisplayName("Quantity Available:")]
        public double QuantityAvailable
        {
            get
            {
                return Math.Round((QuantityReceived - QuantityUsedThisLot),2);
            }
        }

        [Required(ErrorMessage = "The Certificate of Analysis field is required")]
        [DisplayName("C of A:")]
        public string CoA { get; set; }

        [DisplayName("Receipt Container:")]
        public int ReceivedSizeLimitId { get; set; }
        
        public string ReceivedSizeLimitCode 
        {
            get
            {
                return new Services.RawMaterials.RawMaterialReceivedSizeLimitService().Get(ReceivedSizeLimitId).Code;
            }

        }

        public int UoMId { get; set; }

        [DisplayName("Material UOM:")]
        public string UoMCode 
        { 
            get
            {
                return new Services.Application.UnitOfMeasureService().Get(UoMId).Code;
            }

        }

        public IEnumerable<System.Web.Mvc.SelectListItem> ProductionLines { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Users { get; set; }

        public string UrlReferrer { get; set; }
    }
}