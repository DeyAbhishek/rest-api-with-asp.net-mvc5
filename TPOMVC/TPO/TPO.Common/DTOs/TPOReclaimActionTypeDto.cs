using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class TPOReclaimActionTypeDto
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
