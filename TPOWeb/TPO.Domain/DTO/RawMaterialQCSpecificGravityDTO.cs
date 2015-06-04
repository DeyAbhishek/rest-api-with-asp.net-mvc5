using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Domain.DTO
{
    public class RawMaterialQCSpecificGravityDTO
    {
        public int ID { get; set; }
        public int RawMaterialQCID { get; set; }
        public string RawMaterialCode { get; set; }
        public string RawMaterialLotCode { get; set; }
        public int? LabTechUserID { get; set; }
        public double DenIso { get; set; }
        public double AverageGravity { get; set; }

        public System.DateTime? DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime? LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public ICollection<RawMaterialQCSpecificGravityDetailDTO> RawMaterialSpecificGravityDetails { get; set; }
    }
}
