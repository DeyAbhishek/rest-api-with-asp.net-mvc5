
using System;
namespace TPO.Common.DTOs
{
    public class RawMaterialDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public double PricePerUOM { get; set; }
        public double Density { get; set; }
        public double Stock { get; set; }
        public string FSBPID { get; set; }
        public int PlantId { get; set; }
        public DateTime LastModified { get; set; }
        public int UOMId { get; set; }
        public int RawMaterialVendorId { get; set; }

        public string VendorName { get; set;  }
    }
}
