using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TPOWeb.Models
{
    public class RawMaterialQCSpecificGravityViewModel : BaseViewModel
    {
        public int ID { get; set; }

        [DisplayName("Raw Material QC Id")]
        public int RawMaterialQCId { get; set; }

        [DisplayName("Den ISO")]
        public double DenIso { get; set; }

        [DisplayName("Average Gravity")]
        public double AverageGravity { get; set; }

        [DisplayName("Raw Material")]
        public string RawMaterialReceivedCode { get; set; }

        [DisplayName("Raw Material Code")]
        public string RawMaterialCode { get; set; }

        [DisplayName("Raw Material Lot")]
        public string RawMaterialLotCode { get; set; }
        
        [DisplayName("Lab Tech")]
        public string LabTechUserID { get; set; }

        [DisplayName("Date Entered")]
        public DateTime DateEntered { get; set; }

        [DisplayName("Entered By")]
        public string EnteredBy { get; set; }

        [DisplayName("Last Modified")]
        public DateTime LastModified { get; set; }

        [DisplayName("Last Modified By")]
        public string ModifiedBy { get; set; }

        public List<RawMaterialQCSpecificGravityDetailViewModel> Details;

    }

    public class RawMaterialQCSpecificGravityDetailViewModel
    {
        public int Id { get; set; }

        public int rawMaterialQCSpecificGravityId { get; set; }

        [DisplayName("Submerged")]
        public bool Submerged { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.0000#}", ApplyFormatInEditMode = true)]
        [DisplayName("Value")]
        public double Value { get; set; }
        
        [DisplayName("Order")]
        public int Order { get; set; }

        [DisplayName("Date Entered")]
        public DateTime DateEntered { get; set; }

        [DisplayName("Entered By")]
        public string EnteredBy { get; set; }

        [DisplayName("Last Modified")]
        public DateTime LastModified { get; set; }

        [DisplayName("Last Modified By")]
        public string ModifiedBy { get; set; }
        
    }
}