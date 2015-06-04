using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Reference;
using TPO.Model.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace TPO.Model.RawMaterials
{
    public class RawMaterialQCRedHoldModel
    {
        #region Variables
        #endregion

        #region Properties

        //general fields
        //ID is used as lot number?
        public int ID {get;set;}

        
        
        
        public int RawMaterialQCID { get; set; }
        
        [Required(ErrorMessage = "The Plant field is required")]
        [DisplayName("Plant")]
        public int PlantID { get; set; }

        public int RawMaterialRecievedID { get; set; }

        [DisplayName("QC Tech")]
        public Nullable<int> QCTechID {get;set;}

        [DisplayName("Shift Supervisor")]
        public Nullable<int> SupervisorID { get; set; }

        [DisplayName("Lead Operator")]
        public Nullable<int> LeadOperatorID {get;set;}

        //Where is Box\Car Tested

               
        //Red Form Section
        [DisplayName("Date")]
        public System.DateTime? RedDate { get; set; }
        [DisplayName("Property Failing")]
        public Nullable<int> FailPropertyID { get; set; }
        [DisplayName("Zone")]
        public string Zone { get; set; } //Null?
        [DisplayName("Comments")]
        public string RedComments { get; set; }      //Null?
        [DisplayName("Corrective Action")]
        public string RedCorrectionAction {get;set;} //Null?
        //End Red Form Section


        //Begin Hold section
        [DisplayName("Date")]
        public System.DateTime? HoldDate { get; set; }
        [DisplayName("Lot")]
        public string HoldLotID { get; set; } //Null?        
        [DisplayName("Comments")]
        public string HoldComments { get; set; } //Null?

        //Management Actions Take(n?)
        [DisplayName("Manager")]
        public Nullable<int> ManagerID { get; set; }
        [DisplayName("Date")]
        public System.DateTime? ManagerDate {get;set;}
        [DisplayName("Comments")]
        public string ManagerComments { get; set; } //Null?

        public float PrimeBoxCar {get;set;}
        public float PrimeUOM {get;set;}
        public float ReworkBoxCar {get;set;}
        public float ReworkUOM {get;set;}
        public float ScrapBoxCar {get;set;}
        public float ScrapUOM {get;set;}

        [DisplayName("Date")]
        [Required]
        public System.DateTime? DateEntered { get; set; }

        [Required(ErrorMessage = "The Received By field is required")]
        [DisplayName("Receive By")]
        public string EnteredBy { get; set; }

        public System.DateTime? LastModified { get; set; }

        public string ModifiedBy { get; set; }
        
        #endregion

    }
}
