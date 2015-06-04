using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TPO.Services;

namespace TPO.Web.Models
{
    public class EquipmentRunTimeModel : BaseViewModel
    {
        [DisplayName("Production Line")]
        public int ProductionLineId { get; set; } 
        [DisplayName("Week Starting")]
        public DateTime WeekStarting { get; set; }

        public SelectList ProductionLineList { get; set; }
        public SelectList WeekStartingList{ get; set; }
     
    }
}