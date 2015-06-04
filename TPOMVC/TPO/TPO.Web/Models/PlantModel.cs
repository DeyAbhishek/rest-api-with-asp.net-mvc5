using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class Plant : BaseViewModel
    {
        #region Constructors

        public Plant() : base()
        {
        }

        #endregion

        [DisplayName("Plant ID:")]
        [Required]
        public int ID { get; set; }
        [DisplayName("Plant Code:")]
        [Required]
        [MaxLength(5)]
        public string Code { get; set; }
        [DisplayName("Plant Name:")]
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        
    }
}