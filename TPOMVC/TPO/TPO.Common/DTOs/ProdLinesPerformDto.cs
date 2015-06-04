using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class ProdLinesPerformDto
    {
        public int ID { get; set; }
        public int LocID { get; set; }
        public string LineCode { get; set; }
        public double Uptime { get; set; }
        public double Yield { get; set; }
        public double Throughput { get; set; }
        public double OEE { get; set; }
        public string TPTUse { get; set; }
        public double SchedFactor { get; set; }
        public int ProdLineID { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public int? ThroughputUoMID { get; set; }
    
    }
}
