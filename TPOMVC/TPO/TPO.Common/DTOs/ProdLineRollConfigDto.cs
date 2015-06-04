using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class ProdLineRollConfigDto
    {
        public int ID { get; set; }
        public string RollName { get; set; }
        public int TypeID { get; set; }
        public int Order { get; set; }
    }
}
