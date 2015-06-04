using System;

namespace TPO.Common.DTOs
{
    public class RawMaterialReceivedDto
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public string PlantCode { get; set; }
        public int RawMaterialId { get; set; }
        public string RawMaterialCode { get; set; }
        public string RawMaterialDescription { get; set; }
        public string LotNumber { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public double QuantityShipped { get; set; }
        public double QuantityReceived { get; set; }
        public double QuantityNotReceived { get; set; }
        public double QuantityUsedThisLot { get; set; }
        public string CoA { get; set; }
        public int ReceivedSizeLimitId { get; set; }
        public string ReceivedSizeLimitCode { get; set; }
        public int UoMId { get; set; }
        public string UoMCode { get; set; }
    }
}
