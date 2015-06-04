using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class RawMaterialStockAdjustmentModel : BaseViewModel
    {
        public int RawMaterialId { get; set; }
        public int LotNumber { get; set; }
        public int AdjustmentType { get; set; }
        public double AdjustmentAmount { get; set; }
        public int AdjustmentUoM { get; set; }
        public string AdjustmentReason { get; set; }
    }
}