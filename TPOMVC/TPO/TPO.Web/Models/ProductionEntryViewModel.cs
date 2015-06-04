using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TPO.Web.Models
{
    public class ProductionEntryViewModel : BaseViewModel
    {
        [DisplayName("Production Line")]
        public int ProductionLineId { get; set; }

        [DisplayName("Current Work Order")]
        public int WorkOrderId { get; set; }

        [DisplayName("Production Date")]
        public DateTime ProductionDate { get; set; }

        [DisplayName("Production Shift")]
        public int ShiftId { get; set; }

        [DisplayName("Supervisor On Shift")]
        public int SupervisorId { get; set; }

        [DisplayName("Shift Comments")]
        public string ShiftComments { get; set; }

        public SelectList SupervisorList { get; set; }
        public SelectList ProductionLineList { get; set; }

        public int FormulationId { get; set; }
        public int ExtruderId { get; set; }

        public string ProductionLineTypeCode { get; set; }
        public string ProductionLineTypeDesc { get; set; }
        public string TPOMOrC { get; set; }

        public int MasterRollId { get; set;  }
    }
}