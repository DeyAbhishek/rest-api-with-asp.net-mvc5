using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TPO.Model.RawMaterials
{
   public class RawMaterialReceived
    {
        public int ID { get; set; }

        [Required]
        public int PlantID { get; set; }

        [DisplayName("Plant Code")]
        public string PlantCode { get; set; }

        [Required]
        [DisplayName("Raw Material ID")]
        public int RawMaterialID { get; set; }

        [DisplayName("Raw Material")]
        public string RawMaterialCode { get; set; }
       
        [Required(ErrorMessage = "The Material Lot No field is required")]
        [DisplayName("Material Lot No")]
        public String LotNumber { get; set; }

        [DisplayName("Received Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime? DateEntered { get; set; }

        [Required(ErrorMessage = "The Received By field is required")]
        [DisplayName("Received By")]
        public string EnteredBy { get; set; }

        [DisplayName("Last Modified Date")]
        public System.DateTime? LastModified { get; set; }

        [DisplayName("Last Modified By")]
        public string ModifiedBy { get; set; }

        [Required(ErrorMessage = "The Quantity Shipped field is required")]
        [DisplayName("Quantity Shipped")]
        public double QuantityShipped { get; set; }

        [Required(ErrorMessage = "The Quantity Received field is required")]
        [DisplayName("Quantity Received")]
        public double QuantityReceived { get; set; }

        [Required(ErrorMessage = "The Quantity Rejected field is required")]
        [DisplayName("Quantity Rejected")]
        public double QuantityNotReceived { get; set; }

        [DisplayName("Quantity Used By This Lot")]
        public double QuantityUsedThisLot { get; set; }

        [Required(ErrorMessage = "The Certificate of Analysis field is required")]
        [DisplayName("C of A")]
        public string CoA { get; set; }

        [DisplayName("Received Size Limit ID")]
        public int ReceivedSizeLimitID { get; set; }

        [Required]
        public int UoMID { get; set; }

        //[Required(ErrorMessage = "The Material Unit of Measure field is required")]
        [DisplayName("Material UOM")]
        public string UoMCode { get; set; }

        
        public IEnumerable<System.Web.Mvc.SelectListItem> RawMaterials { get; set; }
        
        public IEnumerable<System.Web.Mvc.SelectListItem> Users { get; set; }

        public string UrlReferrer { get; set; }

    }
}
