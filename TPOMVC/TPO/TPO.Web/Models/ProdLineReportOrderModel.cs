using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class ProdLineReportOrderModel : BaseViewModel
    {
        //public int ID { get; set; }
        //public string LineDescCode { get; set; }
        [DisplayName("Line Description:")]
        public string LineDesc { get; set; }
        [DisplayName("Report Order:")]
        public int RepOrder { get; set; }
        
    }
}