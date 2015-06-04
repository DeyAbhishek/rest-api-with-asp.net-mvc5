using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TPO.Web.Models
{
    public class TPOFormulationModel  : BaseViewModel
    {

        [DisplayName("Description:")]
        public string Description { get; set; }

        [DisplayName("Extruders:")]
        public int Extruders { get; set; }



    }
}