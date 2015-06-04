using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class IMProductTypeModel : BaseViewModel
    {
        
        public string Code { get; set; }
        public string Description { get; set; }
        public string ThickLabel1 { get; set; }
        public string ThickLabel2 { get; set; }
    }
}