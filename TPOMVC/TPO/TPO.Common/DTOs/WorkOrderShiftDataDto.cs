using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class WorkOrderShiftDataDto
    {
        public int ID { get; set; }
        public int LineID { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public double ScrimAreaUsed { get; set; }
        public double ScrimWeightUsed { get; set; }
        public double DrainedResin { get; set; }
        public int PlantID { get; set; }
        public string EnteredBy { get; set; }
        public string ModifiedBy { get; set; }
        public int ShiftID { get; set; }
        public System.DateTime DateEntered { get; set; }
        public System.DateTime LastModified { get; set; }
    }
}
