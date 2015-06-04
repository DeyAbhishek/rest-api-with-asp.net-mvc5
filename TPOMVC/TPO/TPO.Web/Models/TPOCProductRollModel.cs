using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class TPOCProductRollModel : BaseViewModel
    {
        public int LineId { get; set; }
        public int ProductId { get; set; }
        public int WorkOrderId { get; set; }
        public int ShiftId { get; set; }
        public Nullable<int> MasterRollId { get; set; }
        public int LengthUoMId { get; set; }
        public int WeightUoMId { get; set; }
        public string Code { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public int BatchNumber { get; set; }
        public string Comments { get; set; }
        public bool Modified { get; set; }
        public Nullable<int> BatchID { get; set; }
    
        public string ShiftCode { get; set; }
        public string WorkOrderCode { get; set; }
        public string ProductCode { get; set; }
        public string LengthUoM { get; set; }
        public string WeightUoM { get; set; }
        public string ProductionDateStr { get { return ProductionDate.ToShortDateString();  } }

        public int RawMaterialReceivedId { get; set; }
        public string LotNumber { get; set; }
    }
}