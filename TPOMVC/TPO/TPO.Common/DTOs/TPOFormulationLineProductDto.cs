using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class TPOFormulationLineProductDto
    {
        public int ID { get; set; }
        public int ProdLineID { get; set; }
        public string ProdLineLineDesc { get; set; }
        public int TPOProductID { get; set; }
        public string TPOProductDescription { get; set; }
        public int TPOFormulationID { get; set; }
        public string TPOFormulationDescription { get; set; }
        public int TPOFormulationExtruders { get; set; }
        public int PlantID { get; set; }
        public DateTime LastModified { get; set; }
    }
}
