using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
   public class ProductionBudgetDto
    {
        public int ID { get; set; }
        public int TypeID { get; set; }
        public int PlantID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public double Budget { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
