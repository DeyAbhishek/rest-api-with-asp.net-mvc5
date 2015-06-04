using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Domain.DTO
{
    public class RawMaterialReceivedDTO
    {        public int ID { get; set; }
        public int PlantID { get; set; }
        public string PlantCode { get; set; }
        public int RawMaterialID { get; set; }
        public string RawMaterialCode { get; set; }
        public string LotNumber { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public double QuantityShipped { get; set; }
        public double QuantityReceived { get; set; }
        public double QuantityNotReceived { get; set; }
        public double QuantityUsedThisLot { get; set; }
        public string CoA { get; set; }
        public int ReceivedSizeLimitID { get; set; }
        public int UoMID { get; set; }
        public string UoMCode { get; set; }
    }
}
