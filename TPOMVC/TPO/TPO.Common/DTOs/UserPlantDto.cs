using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class UserPlantDto
    {
        public int UserId { get; set;}
        public int PlantId { get; set; }

        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public virtual PlantDto Plant { get; set; }
        public virtual UserDto User { get; set; }
    }
}
