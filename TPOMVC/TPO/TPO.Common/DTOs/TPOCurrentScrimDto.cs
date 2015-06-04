using System;

namespace TPO.Common.DTOs
{
    public class TPOCurrentScrimDto
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int? Scrim1RollID { get; set; }
        public int? Scrim1TypeID { get; set; }
        public int? Scrim2RollID { get; set; }
        public int? Scrim2TypeID { get; set; }
        public int? FleeceRollID { get; set; }
        public int? FleeceTypeID { get; set; }
        public string ScrimPos { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public int ProdLineID { get; set; }
    }
}
