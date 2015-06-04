using System;
using System.Collections.Generic;
using System.ComponentModel;
using TPO.Common.DTOs;

namespace TPO.Web.Models
{
    public class CurrentScrimViewModel : BaseViewModel
    {
        [DisplayName("Production Line:")]
        public string LineID { get; set; }

        public int PlantID { get; set; }

        public int RawMaterialReceivedID { get; set; }

        public int RawMaterialID { get; set; }

        public int RawMaterialVendorID { get; set; }

        public string EnteredBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime DateEntered { get; set; }

        public DateTime LastModified { get; set; }

        public string RawMaterialCode { get; set; }

        public string LotNumber { get; set; }

        public int CurrentScrimID 
        { 
            get 
            {
                return TPOCurrentScrim == null ? 0 : TPOCurrentScrim.ID; 
            } 
        }
        public int CurrentScrimProdLineID 
        { 
            get 
            {
                return TPOCurrentScrim == null ? 0 : TPOCurrentScrim.ProdLineID;
            } 
        }
        //public RawMaterialReceivedDto RawMaterialReceived { get; set; }
        //public RawMaterialDto RawMaterial { get; set; }
        public TPOCurrentScrimDto TPOCurrentScrim { get; set; }
        public TPOCurrentRawMaterialDto CurrentRawMaterial { get; set; }
        public IEnumerable<TPOCurrentRawMaterialDto> CurrentRawMaterialList { get; set; }
    }
}