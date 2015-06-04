using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class TPOBatchDto
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int LineID { get; set; }
        public int RawMaterialID { get; set; }
        public int RawMaterialReceivedID { get; set; }
        public int BatchNumber { get; set; }
        public int FormulationID { get; set; }
        public bool IsScrim { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
