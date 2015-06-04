using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class TPOReworkRollDto
    {
        public int ID { get; set; }
        public int LineID { get; set; }
        public int ShiftID { get; set; }
        public int TPOProductID { get; set; }
        public int PlantID { get; set; }
        public string Code { get; set; }
        public double Length { get; set; }
        public int LengthUoMID { get; set; }
        public double Weight { get; set; }
        public int WeightUoMID { get; set; }
        public bool Processed { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public string ThicknessDisplay { get; set; }
        public string WidthDisplay { get; set; }
        public string LengthDisplay { get; set; }
        public string WeightDisplay { get; set; }
        public string ProductCode { get; set; }
        public string ScrapDescription { get; set; }
        public string ScrapComment { get; set; }
    }
}
