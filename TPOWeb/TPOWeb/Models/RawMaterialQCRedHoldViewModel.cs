using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TPO.DL.Models;

namespace TPOWeb.Models
{
    public class RawMaterialQCRedHoldViewModel : BaseViewModel
    {

        public int Id { get; set; }

        public int RawMaterialQCId { get; set; }

        //public int PlantId { get; set; }
        //from BaseViewModel

        [DisplayName("Raw Material ID")]
        public int RawMaterialReceivedId { get; set; }
        public string RawMaterialReceived { get; set; }


        [DisplayName("Lot Number")]
        public string LotNumber { get; set; }

        [DisplayName("QC Tech")]
        public Nullable<int> QCTechId { get; set; }

        [DisplayName("Shift Supervisor")]
        public Nullable<int> SupervisorId { get; set; }

        [DisplayName("Lead Operator")]
        public Nullable<int> LeadOperatorId { get; set; }

        //Load Box/Car Tested from RawMaterial QC record
        [DisplayName("Box \\ Car Tested")]
        public string BoxCarTested { get; set; }
 
        //Red Form Section
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date")]
        public System.DateTime? RedDate { get; set; }
        [DisplayName("Property Failing")]
        public Nullable<int> FailPropertyId { get; set; }
        [DisplayName("Zone")]
        public string Zone { get; set; } //Null?
        [DisplayName("Comments")]
        public string RedComments { get; set; }      //Null?
        [DisplayName("Corrective Action")]
        public string RedCorrectionAction { get; set; } //Null?
        //End Red Form Section


        //Begin Hold section
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date")]
        public System.DateTime? HoldDate { get; set; }
        [DisplayName("Lot")]
        public string HoldLotNumber { get; set; } //Null?        
        [DisplayName("Comments")]
        public string HoldComments { get; set; } //Null?

        //Management Actions Take(n?)
        [DisplayName("Manager")]
        public Nullable<int> ManagerID { get; set; }
        [DisplayName("Date")]
        public System.DateTime? ManagerDate { get; set; }
        [DisplayName("Comments")]
        public string ManagerComments { get; set; } //Null?

        public float PrimeBoxCar { get; set; }
        public float PrimeUOM { get; set; }
        public float ReworkBoxCar { get; set; }
        public float ReworkUOM { get; set; }
        public float ScrapBoxCar { get; set; }
        public float ScrapUOM { get; set; }

        [DisplayName("Date Entered")]
        public System.DateTime? DateEntered { get; set; }

        [DisplayName("Entered By")]
        public string EnteredBy { get; set; }

        [DisplayName("Last Modified")]
        public System.DateTime? LastModified { get; set; }
        [DisplayName("Last Modified By")]
        public string ModifiedBy { get; set; }

    }
}