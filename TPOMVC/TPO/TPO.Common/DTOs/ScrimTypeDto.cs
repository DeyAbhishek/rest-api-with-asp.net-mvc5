using System;

namespace TPO.Common.DTOs
{
    public class ScrimTypeDto
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int AreaUoMID { get; set; }
        public int WidthUoMID { get; set; }
        public int WeightUoMID { get; set; }
        public int LengthUoMID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public bool IsLiner { get; set; }
        public DateTime LastModified { get; set; }
    
    }
}
