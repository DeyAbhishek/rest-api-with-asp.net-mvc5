using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;
using TPO.Common.DTOs;

namespace TPO.Web.Models
{
    public class ProdLineRollConfigViewModel : BaseViewModel
    {
        public int ID { get; set; }
        public string RollName { get; set; }
        public int TypeID { get; set; }
        public int Order { get; set; }
    }
}