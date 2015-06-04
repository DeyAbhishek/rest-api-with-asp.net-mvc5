using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class TPOReclaimWIPDto
    {
        public int ID { get; set; }
        public int PlantId { get; set; }
        public string ReclaimType { get; set; }
        public float Wip { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

    }
}
