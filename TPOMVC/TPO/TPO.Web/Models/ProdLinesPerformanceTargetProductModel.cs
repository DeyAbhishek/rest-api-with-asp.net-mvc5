using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class ProdLinesPerformanceTargetProductModel : BaseViewModel
    {
        public int LocID { get; set; }
        public int ProdLineID { get; set; }
        public int ProductID { get; set; }
        public double Throughput { get; set; }
        public System.DateTime DateChange { get; set; }
        public string ProductName { get; set; }
    }
}