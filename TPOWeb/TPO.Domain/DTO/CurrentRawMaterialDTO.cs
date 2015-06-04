using System;

namespace TPO.Domain.DTO
{
    public class CurrentRawMaterialDTO
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public string LineId { get; set; }
        public int? RawMaterialReceivedId { get; set; }
        public string RawMaterialReceivedCode { get; set; }
        public string LotId { get; set; }
        public double QuantityShipped { get; set; }
        public double QuantityReceived { get; set; }
        public double QuantityUsed { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
