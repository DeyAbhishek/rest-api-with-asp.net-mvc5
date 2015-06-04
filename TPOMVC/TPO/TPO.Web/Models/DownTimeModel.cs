using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class DownTimeModel
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int LineID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public int WorkOrderID { get; set; }
        public string WorkOrderCode { get; set; }
        public int TypeID { get; set; }
        public string DownTimeTypeDescription { get; set; }
        public Nullable<int> ReasonID { get; set; }
        public int ShiftID { get; set; }
        public string ProductionShiftTypeCode { get; set; }
        public Nullable<int> EquipmentID { get; set; }
        public System.DateTime DateOccurred { get; set; }
        public string Comment { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> DownTimeEquipmentGroupID { get; set; }
        public int DownTimeMinutes { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public string DownTimeReasonDescription { get; set; }
        public string DownTimeEquipmentDescription { get; set; }
        public string DownTimeEquipmentGroupDescription { get; set; }
    }
}