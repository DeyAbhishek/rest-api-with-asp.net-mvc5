using System;

namespace TPO.Web.Models
{
    public class RawMaterialReceivedSizeLimitModel : BaseViewModel
    {
        public string Code { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public bool View { get; set; }
    }
}