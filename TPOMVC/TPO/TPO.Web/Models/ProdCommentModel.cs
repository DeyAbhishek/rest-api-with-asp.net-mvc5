using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class ProdCommentModel
    {    
        public int LineID { get; set; }
        public int ShiftID { get; set; }
        public int WorkOrderID { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public string Comment { get; set; }
        
    }
}