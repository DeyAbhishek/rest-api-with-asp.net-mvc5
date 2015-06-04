using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class ProdDateChangeModel: BaseViewModel
    {

        [DisplayName("Shift Schedule Type: ")]
        public int ShiftTypeID { get; set; }

        [DisplayName("Production Date Change Over Time: ")]
        public DateTime DateChange { get; set; }

        [DisplayName("Rotation Starting Date: ")]
        public DateTime RotationStart { get; set; }

        public int LineID { get; set; }
        public string ProdLineLineDesc { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public string ResponseMessage { get; set; }
        public string ResponseStatus { get; set; }
    }
}