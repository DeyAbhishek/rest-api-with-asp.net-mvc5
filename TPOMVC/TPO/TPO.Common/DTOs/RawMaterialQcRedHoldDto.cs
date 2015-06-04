using System;

namespace TPO.Common.DTOs
{
    public class RawMaterialQcRedHoldDto
    {
        public int Id { get; set; }
        public int RawMaterialQcId { get; set; }
        public int PlantId { get; set; }
        public int RawMaterialReceivedId { get; set; }
        public int? QcTechId { get; set; }
        public int? SupervisorId { get; set; }
        public int? LeadOperatorId { get; set; }

        public DateTime? RedDate { get; set; }
        public int? FailPropertyId { get; set; }
        public string Zone { get; set; } 
        public string RedComments { get; set; }
        public string RedCorrectionAction { get; set; } 
        
        public DateTime? HoldDate { get; set; }
        public string HoldLotId { get; set; }      
        public string HoldComments { get; set; } 
        
        public int? ManagerId { get; set; }
        public DateTime? ManagerDate { get; set; }
        public string ManagerComments { get; set; }

        public float PrimeBoxCar { get; set; }
        public float PrimeUOM { get; set; }
        public float ReworkBoxCar { get; set; }
        public float ReworkUOM { get; set; }
        public float ScrapBoxCar { get; set; }
        public float ScrapUOM { get; set; }

        public DateTime? DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public string BoxCarTested { get; set; }
        public string RawMaterialReceived { get; set; }
    }
}
