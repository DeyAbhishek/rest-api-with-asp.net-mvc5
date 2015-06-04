using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TPO.Model.Framework
{
    public class TPOModelBase
    {
        #region Variables
        private int _ID;
        #endregion

        #region Constants
        public const int INVALID_ID = -1;
        #endregion

        #region Properties
        public int ID 
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [DisplayName("Entered By")]
        public string EnteredBy { get; set; }
        [DisplayName("Modified By")]
        public string ModifiedBy { get; set; }
        [DisplayName("Entered ")]
        public DateTime DateEntered { get; set; }
        [DisplayName("Last Modified")]
        public DateTime LastModified { get; set; }
        #endregion

        #region Constructors
        public TPOModelBase() 
        {
            _ID = INVALID_ID;
        }
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
