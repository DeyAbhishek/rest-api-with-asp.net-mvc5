using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class WorkOrderShiftDataFormulationDto
    {
        public int ID { get; set; }
        public int LineID { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public string ProductionShift { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public string Extruder { get; set; }
        public int Feeder { get; set; }
        public int RawMaterialID { get; set; }
        public string RawMaterialCode { get; set; }
        public double QuantityUsed { get; set; }
        public int PlantID { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
