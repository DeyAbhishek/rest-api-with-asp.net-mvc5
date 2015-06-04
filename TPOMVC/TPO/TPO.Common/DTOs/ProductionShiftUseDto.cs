using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class ProductionShiftUseDto
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int LineID { get; set; }
        public int ShiftID { get; set; }
        public bool UseShift { get; set; }
        public int Day1Minutes { get; set; }
        public int Day2Minutes { get; set; }
        public int Day3Minutes { get; set; }
        public int Day4Minutes { get; set; }
        public int Day5Minutes { get; set; }
        public int Day6Minutes { get; set; }
        public int Day7Minutes { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public string ShiftCode { get; set; }
    }
}
