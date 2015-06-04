using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPO.Common.Interfaces;

namespace TPO.Common.DTOs
{
    public class TPOCProductRollDto : IProductEntryDto
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int LineID { get; set; }
        public int ProductID { get; set; }
        public int WorkOrderID { get; set; }
        public int ShiftID { get; set; }
        public Nullable<int> MasterRollID { get; set; }
        public int LengthUoMID { get; set; }
        public int WeightUoMID { get; set; }
        public string Code { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public int BatchNumber { get; set; }
        public string Comments { get; set; }
        public bool Modified { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> BatchID { get; set; }
    
        public string ShiftCode { get; set; }
        public string WorkOrderCode { get; set;  }
        public string ProductCode { get; set;  }
        public string LengthUoM { get; set; }
        public string WeightUoM { get; set; }

        public int RawMaterialReceivedID { get; set; }
    }
}
