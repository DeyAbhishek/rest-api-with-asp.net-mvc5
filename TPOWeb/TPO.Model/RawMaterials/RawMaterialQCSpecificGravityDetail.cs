using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Reference;
using TPO.Model.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TPO.Model.RawMaterials
{
    public class RawMaterialQCSpecificGravityDetail
    {
        public int ID { get; set; }
        //ID of SG form
        public int RawMaterialSpecGravID { get; set; }
        
        [DisplayName("Submerged Weight")]
        public bool Submerged { get; set; }
        
        [DisplayName("Weight")]
        [Required]
        [DefaultValue(0.0000)]
        [DisplayFormat(DataFormatString="{0:F4}", ApplyFormatInEditMode=true)]
        public double Value { get; set; }
        //Display order on SG form (1-5)
        [Range(1,5)]
        public int Order { get; set; }

        [DisplayName("Date")]
        public System.DateTime DateEntered { get; set; }
        [DisplayName("Entered By")]
        public string EnteredBy { get; set; }
        [DisplayName("Last Modified")]
        public System.DateTime LastModified { get; set; }
        [DisplayName("Modified By")]
        public string ModifiedBy { get; set; }

    }
}
