using System;

namespace TPO.Common.DTOs
{
    public class ProductionLinesDto
    {
        public int ID { get; set; }
        public string LineDescCode { get; set; }
        public string LineDesc { get; set; }
        public int LineTypeID { get; set; }
        public int LabelID { get; set; }
        public int? CurrentWorkOrderID { get; set; }
        public string TPOMorC { get; set; }
        public int RepOrder { get; set; }
        public string RCComp { get; set; }
        public int PlantID { get; set; }
        public int LineRollConfigID { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public string TypeDescription { get; set; }
    }
}
