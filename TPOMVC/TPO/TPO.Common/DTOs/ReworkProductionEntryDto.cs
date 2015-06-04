using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class ReworkProductionEntryDto
    {
        public int PlantID { get; set; }
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
        public string RollIn { get; set; }


        public DateTime ProductionDate { get; set; }

        public bool PrintLabel { get; set; }
        public string Message { get; set; }

        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
