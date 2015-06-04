using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.Interfaces
{
    public interface IProductEntryDto
    {
        int PlantID  { get; set; }
        int LineID { get; set; }
        int ShiftID { get; set; }
        DateTime ProductionDate { get; set; }
        int WorkOrderID { get; set; }
        int ProductID { get; set; }
        int? BatchID { get; set; }
        int BatchNumber { get; set; }
        int? MasterRollID { get; set; } // also LotID
        int RawMaterialReceivedID { get; set; }
        System.DateTime DateEntered { get; set; }
        string EnteredBy { get; set; }
        System.DateTime LastModified { get; set; }
        string ModifiedBy { get; set; }

    }
}
