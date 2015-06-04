using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TPO.Web.Models
{
    public class TPOCurrentRawMaterialViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [DisplayName("Production Line:")]
        public int LineId { get; set; }

        [DisplayName("ID:")]
        public int? RawMaterialReceivedId { get; set; }

        [DisplayName("Raw Material:")]
        public string RawMaterialReceivedCode { get; set; }

        [DisplayName("Lot:")]
        public string LotId { get; set; }

        public double QuantityReceived { get; set; }
        public double QuantityUsed { get; set; }
        public double QuantityAvailable
        {
            get { return QuantityReceived - QuantityUsed; }
        }

        [DisplayName("Date Entered:")]
        [Required(ErrorMessage = "The date Entered field is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateEntered { get; set; }

        [DisplayName("Entered By:")]
        public string EnteredBy { get; set; }

        [DisplayName("Last Modified:")]
        public DateTime LastModified { get; set; }

        [DisplayName("Last Modified By:")]
        public string ModifiedBy { get; set; }
    }
}