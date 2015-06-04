using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class WorkOrderDto
    {
        public int ID { get; set; }
        public int LineID { get; set; }
        public int? TPOProductID { get; set; }
        public int? IMProductID { get; set; }
        public int PlantID { get; set; }
        public string Code { get; set; }
        public int RunOrder { get; set; }
        public double RunArea { get; set; }
        public bool Pal { get; set; }
        public bool Complete { get; set; }
        public string Comment { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
