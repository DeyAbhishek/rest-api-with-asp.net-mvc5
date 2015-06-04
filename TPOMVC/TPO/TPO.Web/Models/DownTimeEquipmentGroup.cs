using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class DownTimeEquipmentGroup : BaseViewModel
    {
        public int LineID { get; set; }
        public int DownTimeTypeID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}