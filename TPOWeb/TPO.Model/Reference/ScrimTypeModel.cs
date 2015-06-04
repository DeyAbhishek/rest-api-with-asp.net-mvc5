using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Framework;

namespace TPO.Model.Reference
{
    public class ScrimTypeModel : TPOReferenceModelBase
    {
        #region Variables
        #endregion

        #region Properties
        public int PlantID { get; set; }
        public int WidthUnitOfMeasureID { get; set; }
        public int WeightUnitOfMeasureID { get; set; }
        public int LengthUnitOfMeasureID { get; set; }

        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public decimal Weight { get; set; }

        public bool IsLiner { get; set; }
        #endregion

        #region Constructors
        public ScrimTypeModel() : base() 
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
