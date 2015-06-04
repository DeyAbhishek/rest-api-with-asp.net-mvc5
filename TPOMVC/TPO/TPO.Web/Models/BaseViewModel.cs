using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TPO.Web.Core;

namespace TPO.Web.Models
{
    public abstract class BaseViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int PlantId { get; set; }

        [DisplayName("Plant:")]
        public string PlantName { get; set; }

        [DisplayName("Plant Code:")]
        public string PlantCode { get; set; }

        [DisplayName("Entered :")]
        public DateTime DateEntered { get; set; }
        [DisplayName("Entered By:")]
        public string EnteredBy { get; set; }
        [DisplayName("Last Modified:")]
        public DateTime LastModified { get; set; }
        [DisplayName("Modified By:")]
        public string ModifiedBy { get; set; }

        public ResponseMessage ResponseMessage { get; set; }
    }
}