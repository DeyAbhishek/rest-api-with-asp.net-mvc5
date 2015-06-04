using System;

namespace TPO.Common.DTOs
{
    public class ScrimRollDto
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int? TypeID { get; set; }
        public int PlantID { get; set; }
        public int? LotNumber { get; set; }
        public int? WovenLotNumber { get; set; }
        public int WeightUoMID { get; set; }
        public int LengthUoMID { get; set; }
        public DateTime? WovenDate { get; set; }
        public DateTime DateReceived { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public double TareWeight { get; set; }
        public double ReceivedLength { get; set; }
        public double ReceivedWeight { get; set; }
        public double ReceivedTareWeight { get; set; }
        public double LengthUsed { get; set; }
        public double WeightUsed { get; set; }


        public string EnteredBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime LastModified { get; set; }

    }
}
