using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Framework;

namespace TPO.Model.Reference
{
    public class UnitOfMeasureModel : TPOReferenceModelBase
    {
        #region Variables
        private int _UnitOfMeasureTypeID;

        
        #endregion

        #region Properties
        public int UnitOfMeasureTypeID
        {
            get { return _UnitOfMeasureTypeID; }
            set { _UnitOfMeasureTypeID = value; }
        }

        public UnitOfMeasureTypeModel UnitOfMeasureType { get; set; }
        #endregion

        #region Constructors
        public UnitOfMeasureModel() : base() 
        {
            _UnitOfMeasureTypeID = INVALID_ID;
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
