using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPO.Common.DTOs;

namespace TPO.Web.Models
{
    public class UnitOfMeasureDefaultViewModel : BaseViewModel
    {
        public int PlantID { get; set; }
        public int UoMID { get; set; }
        public int UoMTypeID { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public string UoMCode { get; set; }
        public string UoMTypeDescription { get; set; }
        public string UomTypeCode { get; set; }
    }
}