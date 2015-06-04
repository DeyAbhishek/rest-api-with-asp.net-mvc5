using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class ProductionShiftTypeModel : BaseViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
    }
}