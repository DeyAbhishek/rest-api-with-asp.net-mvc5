using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class RawMaterialVendorModel : BaseViewModel
    {
        
        [DisplayName("Vendor Name:")]
        [Required]
        public string Vendor { get; set; }
    }
}