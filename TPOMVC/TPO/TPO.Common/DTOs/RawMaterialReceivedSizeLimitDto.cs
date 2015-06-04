using System;

namespace TPO.Common.DTOs
{
    public class RawMaterialReceivedSizeLimitDto
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public string Code { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public bool View { get; set; }
        public string EnteredBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime LastModified { get; set; }
        
    }
}
