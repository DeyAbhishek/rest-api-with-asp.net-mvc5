using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class ReworkProductionEntryModel : BaseViewModel
    {
        public int RWType { get; set; }

        public int LineID { get; set; }
        public int ShiftID { get; set; }
        public int TPOProductID { get; set; }

        public double Length { get; set; }
        public int LengthUoMID { get; set; }
        public double Weight { get; set; }
        public int WeightUoMID { get; set; }
        public double Width { get; set; }
        public int WidthUoMID { get; set; }

        public string Comments { get; set; }

        public int StartRollID { get; set; }
        public int? StartRoll2ID { get; set; }
        public int? RollInID { get; set; }


        public DateTime ProductionDate { get; set; }

    }
}