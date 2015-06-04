using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Framework;

namespace TPO.Model.Scrim
{
    public class TPOCurrentScrimModel : TPOModelBase
    {
        #region Variables
        private int _PlantID;
        private int _Scrim1RollID;
        private int _Scrim1TypeID;
        private int _Scrim2RollID;
        private int _Scrim2TypeID;
        private int _FleeceRollID;
        private int _FleeceTypeID;
        #endregion

        #region Properties
        public int PlantID
        {
            get { return _PlantID; }
            set { _PlantID = value; }
        }
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

        public string ScrimPos { get; set; }
        [DisplayName("Production Line")]
        public string LineID { get; set; }
        #endregion

        #region Constructors
        public TPOCurrentScrimModel() : base() 
        {
            _PlantID = INVALID_ID;
            _Scrim1RollID = INVALID_ID;
            _Scrim1TypeID = INVALID_ID;
            _Scrim2RollID = INVALID_ID;
            _Scrim2TypeID = INVALID_ID;
            _FleeceRollID = INVALID_ID;
            _FleeceTypeID = INVALID_ID;
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
