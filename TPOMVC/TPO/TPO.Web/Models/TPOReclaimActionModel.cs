using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class TPOReclaimActionModel
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public DateTime ProductionDate { get; set; }
        public int ProductionShiftId { get; set; }
        public string ProductionShiftCode { get; set; }
        public int ProdLineId { get; set; }
        public int RecalimActionTypeId { get; set; }
        public string TPOReclaimActionTypeCode { get; set; }
        public float ActionAmount { get; set; }
        public float CompAmount { get; set; }
        public string AssocAction { get; set; }
        public string RecalimType { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}