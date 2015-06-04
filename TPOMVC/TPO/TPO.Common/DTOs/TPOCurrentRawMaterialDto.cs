using System;
using System.ComponentModel;

namespace TPO.Common.DTOs
{
    public class TPOCurrentRawMaterialDto
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        [DisplayName("Raw Material Line:")]
        public int LineID { get; set; }
        public int? RawMaterialReceivedID { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public string LotNumber { get; set;  }
    }
}
