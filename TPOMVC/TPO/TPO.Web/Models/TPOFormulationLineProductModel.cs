using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class TPOFormulationLineProductModel : BaseViewModel
    {
        public int ProdLineID { get; set; }
        public string ProdLineLineDesc { get; set; }
        public int TPOProductID { get; set; }
        public string TPOProductDescription { get; set; }
        public int TPOFormulationID { get; set; }
        public string TPOFormulationDescription { get; set; }
        public int TPOFormulationExtruders { get; set; }
    }
}