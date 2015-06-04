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
    public class RawMaterialQCSpecificGravity
    {

        #region Variables
        #endregion


        #region Properties
        //ID of SG form
        public int ID { get; set; }
        //related test
        public int RawMaterialQCID { get; set; }

        [DisplayName("Raw Material")]
        public string RawMaterialCode { get; set; }
        [DisplayName("Lot")]
        public string RawMaterialLotCode { get; set; }

        [DisplayName("Lab Tech")]
        public Nullable<int> LabTechUserID { get; set; }

        [DisplayFormat(DataFormatString = "{0:F4}", ApplyFormatInEditMode = true)]
        [DefaultValue(0.7581)]
        [DisplayName("Den-Iso")]
        public double DenIso { get; set; }

        [DisplayName("Average")]
        public double AverageGravity { get; set; }
        
        [DisplayName("Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime DateEntered { get; set; }
        [DisplayName("Entered By")]
        public string EnteredBy { get; set; }
        [DisplayName("Last Modified")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime LastModified { get; set; }
        [DisplayName("Modified By")]
        public string ModifiedBy { get; set; }

        public virtual ICollection<RawMaterialQCSpecificGravityDetail> RawMaterialSpecificGravityDetails { get; set; }

        #endregion
    }
}
