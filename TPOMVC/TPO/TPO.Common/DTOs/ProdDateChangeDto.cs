using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TPO.Common.DTOs
{
    public class ProdDateChangeDto
    {

        public int ID { get; set; }
        public int PlantID { get; set; }
        public int ShiftTypeID { get; set; }
        public DateTime DateChange { get; set; }
        public DateTime RotationStart { get; set; }
        public int LineID { get; set; }
        public string ProdLineLineDesc { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public DateTime CurrentProductionDate { get; set; }
    }
}
