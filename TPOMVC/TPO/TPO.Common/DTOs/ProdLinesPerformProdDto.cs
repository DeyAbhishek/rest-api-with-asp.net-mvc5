using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class ProdLinesPerformProdDto
    {
        public int ID { get; set; }
        public int LocID { get; set; }
        public int ProdLineID { get; set; }
        public int ProductID { get; set; }
        public double Throughput { get; set; }
        public System.DateTime DateChange { get; set; }
        public string ProductName { get; set; }

    }
}
