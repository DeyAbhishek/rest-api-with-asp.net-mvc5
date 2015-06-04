using System;

namespace TPO.Common.DTOs
{
    public class TPOFormulationRawMaterialDto
    {
        public int ID { get; set; }
        public int TPOFormulationID { get; set; }
        public int TPOExtruderID { get; set; }
        public int FeedNumber { get; set; }
        public int RawMaterialID { get; set; }
        public double CompPerc { get; set; }
        public int PlantID { get; set; }
        public DateTime LastModified { get; set; }

        public string RawMaterialCode { get; set; }
        public string ExtruderCode { get; set; }
    }
}
