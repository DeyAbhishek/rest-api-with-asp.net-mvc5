using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class ProductionLineManagementModel : BaseViewModel
    {
        #region Variables
        #endregion

        #region Properties
        public string LineDescCode { get; set; }
        public string LineDesc { get; set; }
        public int LineTypeID { get; set; }
        public int LabelID { get; set; }
        public string Adhesive { get; set; }
        public string Compatibilizer { get; set; }
        public string RollsProcessed { get; set; }
        public string TPOLineRolls { get; set; }
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