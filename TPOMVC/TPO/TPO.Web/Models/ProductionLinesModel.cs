using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class ProductionLinesModel : BaseViewModel
    {
        public string LineDescCode { get; set; }
        public string LineDesc { get; set; }
        public int LineTypeID { get; set; }
        public int LabelID { get; set; }
        public int? CurrentWorkOrderID { get; set; }
        public string TPOMorC { get; set; }
        public int RepOrder { get; set; }
        public string RCComp { get; set; }
        public DateTime ModDate { get; set; }
        public int LineRollConfigID { get; set; }
    }
}