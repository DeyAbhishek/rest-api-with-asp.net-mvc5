using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class TPOReworkActionDto
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int LineID { get; set; }
        public int ShiftID { get; set; }
        public int StartRollID { get; set; }
        public Nullable<int> StartRoll2ID { get; set; }
        public Nullable<int> OutputRollID { get; set; }
        public int NewProductID { get; set; }
        public int TypeID { get; set; }
        public int WidthUoMID { get; set; }
        public int LengthUoMID { get; set; }
        public int WeightUoMID { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public string Comments { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public Nullable<int> OutputScrapID { get; set; }
    }
}
