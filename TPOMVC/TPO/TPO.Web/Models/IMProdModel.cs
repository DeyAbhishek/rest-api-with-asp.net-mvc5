using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class IMProdModel : BaseViewModel
    {
        public int LineID { get; set; }
        public int ShiftID { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public int WorkOrderID { get; set; }
        public int IMProductID { get; set; }
        public Nullable<int> BatchID { get; set; }
        public Nullable<int> IMQCID { get; set; }
        public string Code { get; set; }
        public int PartsCarton { get; set; }
        public double CartonWeight { get; set; }
        public int WeightUoMID { get; set; }
        public Nullable<System.DateTime> AdhesionManufacturesDate { get; set; }
        public string Comment { get; set; }

        public int BatchNumber { get; set;  }
        public string WeightUoM { get; set; }
        public string LotNumber { get; set;  }
        public int NumberOfEntries { get; set;  }
        public int RawMaterialReceivedID { get; set; }
    }
}