using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class ScrimActionReasonModel : BaseViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
    }
}