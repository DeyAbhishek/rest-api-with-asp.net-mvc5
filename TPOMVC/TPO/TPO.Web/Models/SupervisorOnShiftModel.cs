using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class SupervisorOnShiftModel : BaseViewModel
    {
        public int PlantID { get; set; }
        public int LineID { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public int ShiftID { get; set; }
        public string SupervisorName { get; set; }
    }
}