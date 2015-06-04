using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TPO.Web.Models
{
    public class RawMaterialQcSpecificGravityDetail : BaseViewModel
    {
        
        //ID of SG form
        public int RawMaterialSpecGravId { get; set; }

        [DisplayName("Submerged Weight")]
        public bool Submerged { get; set; }

        [DisplayName("Weight:")]
        [Required]
        [DefaultValue(0.0000)]
        [DisplayFormat(DataFormatString = "{0:F4}", ApplyFormatInEditMode = true)]
        public double Value { get; set; }
        //Display order on SG form (1-5)
        [Range(1, 5)]
        public int Order { get; set; }

        [DisplayName("Date:")]
        public new System.DateTime DateEntered { get; set; }
        
        [DisplayName("Entered By:")]
        public new string EnteredBy { get; set; }
        
        [DisplayName("Last Modified:")]
        public new System.DateTime LastModified { get; set; }

        [DisplayName("Modified By:")]
        public new string ModifiedBy { get; set; }
    }
}