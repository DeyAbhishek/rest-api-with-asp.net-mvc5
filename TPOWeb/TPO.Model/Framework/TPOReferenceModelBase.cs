using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Model.Framework
{
    public class TPOReferenceModelBase : TPOModelBase
    {
        #region Variables
        #endregion

        #region Properties
        public string Code { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        #endregion

        #region Constructors
        public TPOReferenceModelBase() :base()
        {

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
