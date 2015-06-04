using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class ScrimActionModel : BaseViewModel
    {
        public Nullable<int> LineID { get; set; }
        public Nullable<int> ShiftID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public Nullable<int> RollID { get; set; }
        public int ScrimRollID { get; set; }
        public int TypeID { get; set; }
        public Nullable<int> ReasonID { get; set; }
        public int UserID { get; set; }
        public double ActionLength { get; set; }
        public double ActionWeight { get; set; }
        public double StartLength { get; set; }
        public double StartWeight { get; set; }
        public double EndLength { get; set; }
        public double EndWeight { get; set; }
        public System.DateTime ActionDate { get; set; }
        public Nullable<bool> FleeceProd { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public string ActionReasonText { get; set; }
    }
}