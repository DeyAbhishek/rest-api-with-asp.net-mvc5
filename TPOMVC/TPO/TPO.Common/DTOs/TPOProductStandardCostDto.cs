using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class TPOProductStandardCostDto
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int TPOProductID { get; set; }
        public double StandardWeightPerArea { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public double StandardCost { get; set; }

        public System.DateTime LastModified { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
