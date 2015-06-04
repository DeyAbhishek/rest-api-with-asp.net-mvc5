using System;
using System.ComponentModel;

namespace TPO.Web.Models
{
    public class TPOCurrentScrimModel : BaseViewModel
    {
        #region Variables
        private int _Scrim1RollID;
        private int _Scrim1TypeID;
        private int _Scrim2RollID;
        private int _Scrim2TypeID;
        private int _FleeceRollID;
        private int _FleeceTypeID;
        #endregion

        #region Properties
        public int Scrim1RollID
        {
            get { return _Scrim1RollID; }
            set { _Scrim1RollID = value; }
        }
        public int Scrim1TypeID
        {
            get { return _Scrim1TypeID; }
            set { _Scrim1TypeID = value; }
        }
        public int Scrim2RollID
        {
            get { return _Scrim2RollID; }
            set { _Scrim2RollID = value; }
        }
        public int Scrim2TypeID
        {
            get { return _Scrim2TypeID; }
            set { _Scrim2TypeID = value; }
        }
        public int FleeceRollID
        {
            get { return _FleeceRollID; }
            set { _FleeceRollID = value; }
        }
        public int FleeceTypeID
        {
            get { return _FleeceTypeID; }
            set { _FleeceTypeID = value; }
        }
        public int ProdLineID { get; set; }

        public string ScrimPos { get; set; }
        [DisplayName("Production Line:")]
        public string LineID { get; set; }
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