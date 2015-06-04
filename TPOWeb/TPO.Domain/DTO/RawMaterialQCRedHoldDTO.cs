using System;

namespace TPO.Domain.DTO
{
    public class RawMaterialQCRedHoldDTO
    {
        public int ID { get; set;}
        public int RawMaterialQCID { get; set; }
        public int PlantID { get; set; }
        public int RawMaterialReceivedID { get; set; }
        public Nullable<int> QCTechID { get; set; }
        public Nullable<int> SupervisorID { get; set; }
        public Nullable<int> LeadOperatorID { get; set; }

        //Where is Box\Car Tested
        //pull from RawMaterialQC Record

        //Red Form Section
        public System.DateTime? RedDate { get; set; }
        public Nullable<int> FailPropertyID { get; set; }
        public string Zone { get; set; } //Null?
        public string RedComments { get; set; }      //Null?
        public string RedCorrectionAction { get; set; } //Null?
        //End Red Form Section

        //Begin Hold section
        public System.DateTime? HoldDate { get; set; }
        public string HoldLotNumber { get; set; } //Null?        
        public string HoldComments { get; set; } //Null?

        //Management Actions Take(n?)
        public Nullable<int> ManagerID { get; set; }
        public System.DateTime? ManagerDate { get; set; }
        public string ManagerComments { get; set; } //Null?

        public float PrimeBoxCar { get; set; }
        public float PrimeUOM { get; set; }
        public float ReworkBoxCar { get; set; }
        public float ReworkUOM { get; set; }
        public float ScrapBoxCar { get; set; }
        public float ScrapUOM { get; set; }

        public System.DateTime? DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime? LastModified { get; set; }
        public string ModifiedBy { get; set; }

    }
}
