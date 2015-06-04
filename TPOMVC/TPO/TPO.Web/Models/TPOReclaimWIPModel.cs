using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper;
using TPO.Common;
using TPO.Services;
using TPO.Services.Users;

namespace TPO.Web.Models
{
    public class TPOReclaimWIPModel : BaseViewModel
    {
        #region Variables
        #endregion

        #region Properties
        
        
        [DisplayName("Current WIP Level:")]
        public float Wip { get; set; }
        
        [DisplayName("Reclaim Type:")]
        public string ReclaimType { get; set; }
        [DisplayName("AdjustmentType:")]
        public string AdjustmentType { get; set; }

        public  IEnumerable<SelectListItem> ReclaimTypeList
        {
            get 
            {
                List<SelectListItem> reclaimTypes = new List<SelectListItem>();
                SelectListItem repel = new SelectListItem();
                repel.Text = "Reclaim WIP";
                repel.Value = "REPEL";

                SelectListItem georepel = new SelectListItem();
                georepel.Text = "Geomembrane Reclaim WIP";
                georepel.Value = "GEOREPEL";

                reclaimTypes.Add(repel);
                reclaimTypes.Add(georepel);

                return reclaimTypes;
            }
           
        }
        public  IEnumerable<SelectListItem> AdjustementTypeList
        {
            get
            {
                List<SelectListItem> adjustementTypes = new List<SelectListItem>();
                SelectListItem sa = new SelectListItem();
                sa.Text = "Adjust Level By";
                sa.Value = "SA";

                SelectListItem ss = new SelectListItem();
                ss.Text = "Adjust Level To";
                ss.Value = "SS";

                adjustementTypes.Add(sa);
                adjustementTypes.Add(ss);

                return adjustementTypes;
            }
        }
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Events
        #endregion

        #region Event Handlers
        #endregion
    }
}