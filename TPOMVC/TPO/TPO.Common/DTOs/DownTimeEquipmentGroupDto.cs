using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class DownTimeEquipmentGroupDto
    {
        public int ID { get; set; }
        public int LineID { get; set; }
        public int PlantID { get; set; }
        public int DownTimeTypeID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
