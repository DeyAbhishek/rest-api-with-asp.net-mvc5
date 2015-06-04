using System;

namespace TPO.Common.DTOs
{
    public class TPOFormulationDto
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int Extruders { get; set; }
        public int PlantID { get; set; }
        public DateTime LastModified { get; set; }
    }
}
