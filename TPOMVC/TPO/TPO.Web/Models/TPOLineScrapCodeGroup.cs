using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class TPOLineScrapCodeGroup : BaseViewModel
    {
        #region Variables
        private List<TPOLineScrapCode> _tpoLineScrapCodes = new List<TPOLineScrapCode>(); 
        #endregion

        #region Properties
        public string Code { get; set; }
        public string Description { get; set; }
        public bool View { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime LastModified { get; set; }

      

        public List<TPOLineScrapCode> TPOLineScrapCodes
        {
            get { return _tpoLineScrapCodes; }
            set { _tpoLineScrapCodes = value; }
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