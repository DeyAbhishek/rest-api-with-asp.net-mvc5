using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class TPOFormulationRawMaterialModel : BaseViewModel
    {
        public int ID { get; set; }
        public int TPOFormulationID { get; set; }
        public int TPOExtruderID { get; set; }
        public int FeedNumber { get; set; }
        public int RawMaterialID { get; set; }
        public int PlantID { get; set; }
        public DateTime LastModified { get; set; }

        public string RawMaterialCode { get; set; }
        public string ExtruderCode { get; set; }
    }
}