using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class DownTimeEquipmentDto
    {
        public int ID { get; set; }
        public int DownTimeTypeID { get; set; }
        public int PlantID { get; set; }
        public Nullable<int> LineID { get; set; }
        public string Description { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public int DownTimeEquipmentGroupID { get; set; }
    }
}
