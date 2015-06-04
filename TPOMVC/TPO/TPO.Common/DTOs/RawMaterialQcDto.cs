using System;
using System.Collections.Generic;

namespace TPO.Common.DTOs
{
    public class RawMaterialQcDto
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public string PlantCode { get; set; }
        
        public string LotId { get; set; }

        public int? QCTechUserId { get; set; }
        public int RawMaterialId { get; set; }
        public string RawMaterialCode { get; set; }
        public int QCVisualInspectionID { get; set; }
        public double? SpecGrav { get; set; }
        public double? ColorCoA  { get; set; }
        public double? ColorFS { get; set; }
        public double? MFCoA { get; set; }
        public double? MFFS { get; set; }
        public double? ACCoA { get; set; }
        public double? ACFS { get; set; }
        public double? MoistCoA { get; set; }
        public double? MoistFS { get; set; }
        public double? CBCoA { get; set; }
        public double? CBFS { get; set; }
        public string BoxCarTested { get; set; }
        public string Comments { get; set; }
        public int RawMaterialReceivedId { get; set; }
        
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public RawMaterialTestDto RawMaterialTest { get; set; }
        public List<RawMaterialSpecificGravityDto> RawMatSpecGravities { get; set; }
        public List<RawMaterialQcRedHoldDto> RawMatQcRedHolds{ get; set; }
    }
}
