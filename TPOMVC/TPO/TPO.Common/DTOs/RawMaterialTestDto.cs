using System;

namespace TPO.Common.DTOs
{
    public class RawMaterialTestDto
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public bool UseColorTest { get; set; }
        public int? ColorLimitTypeId { get; set; }
        public double? ColorLimit1 { get; set; }
        public double? ColorLimit2 { get; set; }
        public bool UseMFTest { get; set; }
        public int? MFLimitTypeId { get; set; }
        public double? MFLimit1 { get; set; }
        public double? MFLimit2 { get; set; }
        public bool UseACTest { get; set; }
        public int? ACLimitTypeId { get; set; }
        public double? ACLimit1 { get; set; }
        public double? ACLimit2 { get; set; }
        public bool UseMoistTest { get; set; }
        public int? MoistLimitTypeId { get; set; }
        public double? MoistLimit1 { get; set; }
        public double? MoistLimit2 { get; set; }
        public bool UseCBTest { get; set; }
        public int? CBLimitTypeId { get; set; }
        public double? CBLimit1 { get; set; }
        public double? CBLimit2 { get; set; }
        public bool? UseSpecGrav { get; set; }
        public bool? UseVisual { get; set; }
        public string TestFrequency { get; set; }
        public int RawMaterialId { get; set; }

        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

    }
}
