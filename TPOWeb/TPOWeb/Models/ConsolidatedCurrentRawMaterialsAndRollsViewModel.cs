using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPO.Model.Scrim;

namespace TPOWeb.Models
{
    public class ConsolidatedCurrentRawMaterialsAndRollsViewModel
    {
        [DisplayName("Production Line")]
        public string LineID { get; set; }

        public TPOCurrentScrimModel TPOCurrentScrim { get; set; }
        public CurrentRawMaterialViewModel CurrentRawMaterial { get; set; }
        public IEnumerable<CurrentRawMaterialViewModel> CurrentRawMaterialList { get; set; }
    }
}