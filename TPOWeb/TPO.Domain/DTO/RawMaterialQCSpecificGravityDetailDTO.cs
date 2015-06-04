using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Domain.DTO
{
    public class RawMaterialQCSpecificGravityDetailDTO
    {
        public int ID { get; set; }
        public int RawMaterialSpecGravID { get; set; }
        public bool Submerged { get; set; }
        public double Value { get; set; }
        public int Order { get; set; }
        public System.DateTime? DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime? LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
