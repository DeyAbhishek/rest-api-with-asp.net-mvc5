using System;

namespace TPO.Common.DTOs
{
    //TODO:  Added rest of 'audit' fields to table
    public class IMProductDto
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int IMProductTypeID { get; set; }
        public int PartsPerCarton { get; set; }
        public int CartonsPerLot { get; set; }
        public int RawMaterialID { get; set; }
        public int ThickUOM { get; set; }
        public int WeightUOM { get; set; }
        public double MinimumThick { get; set; }
        public double MinimumWeight { get; set; }
        public string Label1 { get; set; }
        public string Label2 { get; set; }
        public string Label3 { get; set; }
        public bool Active { get; set; }
        public int PlantID { get; set; }
        public DateTime LastModified { get; set; }
    }
}
