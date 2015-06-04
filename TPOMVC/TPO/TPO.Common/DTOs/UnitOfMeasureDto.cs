using System;

namespace TPO.Common.DTOs
{
    public class UnitOfMeasureDto
    {
        public int ID { get; set; }
        public int TypeID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
