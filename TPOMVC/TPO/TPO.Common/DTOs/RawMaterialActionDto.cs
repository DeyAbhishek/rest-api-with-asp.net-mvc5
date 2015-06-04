using System;
using System.Collections.Generic;
using TPO.Common.Enums;

namespace TPO.Common.DTOs
{
    public class RawMaterialActionDto
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int TypeID { get; set; }
        public Nullable<int> ReasonID { get; set; }
        public Nullable<int> LineID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public Nullable<int> ShiftID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> LotNumber { get; set; }
        public Nullable<System.DateTime> ProductionDate { get; set; }
        public double Quantity { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public int RawMaterialID { get; set; }
        public string ActionReasonText { get; set; }
    }
}
