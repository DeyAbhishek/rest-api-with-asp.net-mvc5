using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class ProdCommentDto
    {
        public int ID { get; set; }
        public int LineID { get; set; }
        public int ShiftID { get; set; }
        public int WorkOrderID { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public string Comment { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
