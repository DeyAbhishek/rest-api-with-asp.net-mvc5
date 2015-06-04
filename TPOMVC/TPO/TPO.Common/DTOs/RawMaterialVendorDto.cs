using System;

namespace TPO.Common.DTOs
{
    public class RawMaterialVendorDto
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public string Vendor { get; set; }

        public string EnteredBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime LastModified { get; set; }



    }
}
