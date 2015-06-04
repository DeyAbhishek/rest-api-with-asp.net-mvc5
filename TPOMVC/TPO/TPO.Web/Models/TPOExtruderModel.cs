using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class TPOExtruderModel : BaseViewModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int SortOrder { get; set; }
    }
}