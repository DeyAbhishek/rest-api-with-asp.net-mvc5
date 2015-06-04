using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class IMProductTypeDto
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ThickLabel1 { get; set; }
        public string ThickLabel2 { get; set; }
        public DateTime LastModified { get; set; }
    }
}
