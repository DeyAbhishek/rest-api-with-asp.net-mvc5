using System;

namespace TPO.Common.DTOs
{
    public class RawMaterialQcSpecificGravityDetailDto
    {
        public int Id { get; set; }
        public int RawMaterialSpecGravId { get; set; }
        public bool Submerged { get; set; }
        public double Value { get; set; }
        public int Order { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
