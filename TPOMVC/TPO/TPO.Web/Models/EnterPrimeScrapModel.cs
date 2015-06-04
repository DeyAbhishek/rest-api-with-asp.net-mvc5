using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class EnterPrimeScrapModel : BaseViewModel
    {
        public int ProductInId { get; set; }
        public double Width { get; set; }
        public int WidthUoMID { get; set; }
        public double Length { get; set; }
        public int LengthUoMID { get; set; }
        public double Weight { get; set; }
        public int WeightUoMID { get; set; }
        public string Comment { get; set; }
    }
}