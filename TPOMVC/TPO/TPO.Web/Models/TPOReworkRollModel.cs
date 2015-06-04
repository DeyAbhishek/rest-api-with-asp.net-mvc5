using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class TPOReworkRollModel : BaseViewModel
    {
        public int LineID { get; set; }
        public int ShiftID { get; set; }
        public int TPOProductID { get; set; }
        public string Code { get; set; }
        public double Length { get; set; }
        public int LengthUoMID { get; set; }
        public double Weight { get; set; }
        public int WeightUoMID { get; set; }
        public bool Processed { get; set; }
        public double Width { get; set; }
        public int WidthUoMID { get; set; }

        public string ThicknessDisplay { get; set; }
        public string WidthDisplay { get; set; }
        public string LengthDisplay { get; set; }
        public string WeightDisplay { get; set; }
        public string ProductCode { get; set; }
        public string ScrapDescription { get; set; }
        public string ScrapComment { get; set; }

    }
}