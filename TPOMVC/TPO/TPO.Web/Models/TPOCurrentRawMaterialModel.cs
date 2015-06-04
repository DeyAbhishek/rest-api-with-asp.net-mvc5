using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class TPOCurrentRawMaterialModel : BaseViewModel
    {
        public int PlantId { get; set; }
        public string LineId { get; set; }
        public int? RawMaterialReceivedId { get; set; }
        
        public int newLineId { get; set; }
        public string LotNumber { get; set; }
    }
}