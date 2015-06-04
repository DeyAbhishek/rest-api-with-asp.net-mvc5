using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TPO.DL.Models;

namespace TPOWeb.Models
{
    public class CurrentRawMaterialViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [DisplayName("Production Line")]
        public string LineId { get; set; }

        [DisplayName("ID")]
        public int? RawMaterialReceivedId { get; set; }

        [DisplayName("Raw Material")]
        public string RawMaterialReceivedCode { get; set; }

        [DisplayName("Lot")]
        public string LotId { get; set; }

        public double QuantityReceived { get; set; }
        public double QuantityUsed { get; set; }
        public double QuantityAvailable 
        {
            get { return QuantityReceived - QuantityUsed; }
        }

        [DisplayName("Date Entered")]
        [Required(ErrorMessage = "The date Entered field is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateEntered { get; set; }

        [DisplayName("Entered By")]
        public string EnteredBy { get; set; }

        [DisplayName("Last Modified")]
        public DateTime LastModified { get; set; }

        [DisplayName("Last Modified By")]
        public string ModifiedBy { get; set; }

        
    }
}