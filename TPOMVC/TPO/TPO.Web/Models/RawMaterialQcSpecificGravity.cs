using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TPO.Web.Models
{
    public class RawMaterialQcSpecificGravity : BaseViewModel
    {
        //related test
        public int RawMaterialQcId { get; set; }

        [DisplayName("Raw Material:")]
        public string RawMaterialCode { get; set; }
        [DisplayName("Lot:")]
        public string RawMaterialLotCode { get; set; }

        [DisplayName("Lab Tech:")]
        public int? LabTechUserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:F4}", ApplyFormatInEditMode = true)]
        [DefaultValue(0.7581)]
        [DisplayName("Den-Iso:")]
        public double DenIso { get; set; }

        [DisplayName("Average:")]
        public double AverageGravity { get; set; }

        [DisplayName("Date:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateEntered { get; set; }
        
        [DisplayName("Entered By:")]
        public string EnteredBy { get; set; }
        
        [DisplayName("Last Modified:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime LastModified { get; set; }
        
        [DisplayName("Modified By:")]
        public string ModifiedBy { get; set; }

        public virtual ICollection<RawMaterialQcSpecificGravityDetail> RawMaterialSpecificGravityDetails { get; set; }

    }
}