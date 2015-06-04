using System;

namespace TPO.Common.DTOs
{
    public class TPOLineScrapCodeDto
    {
        public int ID { get; set; }
        public int GroupID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool View { get; set; }
        public string EnteredBy { get; set; }
        public DateTime DateEntered { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
    }
}
