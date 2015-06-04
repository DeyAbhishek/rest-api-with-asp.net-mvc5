using System;
using System.Collections.Generic;

namespace TPO.Common.DTOs
{
    public class RawMaterialSpecificGravityDto
    {
        public int Id { get; set; }
        public int RawMaterialQcId { get; set; }
        public string RawMaterialCode { get; set; }
        public string RawMaterialLotCode { get; set; }
        public int? LabTechUserId { get; set; }
        public double DenIso { get; set; }
        public double AverageGravity { get; set; }

        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public ICollection<RawMaterialQcSpecificGravityDetailDto> RawMaterialSpecificGravityDetails { get; set; }
    }
}
