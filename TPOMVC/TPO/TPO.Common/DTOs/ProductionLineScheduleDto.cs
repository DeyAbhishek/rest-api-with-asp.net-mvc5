using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class ProductionLineScheduleDto
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int ShiftID { get; set; }
        public int LineID { get; set; }
        public DateTime ProductionDate { get; set; }
        public int MinutesScheduled { get; set; }
        
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
