using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Framework;
using TPO.Model.Reference;

namespace TPO.Model.RawMaterials
{
    public class RawMaterialTestModel : TPOModelBase
    {
        #region Variables
        
        #endregion

        #region Properties

        #region Read-Only Properties
        #endregion

        public TestLimitTypeModel ColorLimitType { get; set; }
        public TestLimitTypeModel MeltFlowLimitType { get; set; }
        public TestLimitTypeModel AshContentLimitType { get; set; }
        public TestLimitTypeModel MoistureLimitType { get; set; }
        public TestLimitTypeModel CarbonBlackLimitType { get; set; }

        public string TestFrequency { get; set; }

        public int PlantID { get; set; }
        public int ColorLimitTypeID { get; set; }
        public int MeltFlowLimitTypeID { get; set; }
        public int AshContentLimitTypeID { get; set; }
        public int MoistureLimitTypeID { get; set; }
        public int CarbonBlackLimitTypeID { get; set; }

        public decimal ColorMinimum { get; set; }
        public decimal ColorMaximum { get; set; }
        public decimal MeltFlowMinimum { get; set; }
        public decimal MeltFlowMaximum { get; set; }
        public decimal AshContentMinimum { get; set; }
        public decimal AshContentMaximum { get; set; }
        public decimal MoistureMinimum { get; set; }
        public decimal MoistureMaximum { get; set; }
        public decimal CarbonBlackMinimum { get; set; }
        public decimal CarbonBlackMaximum { get; set; }

        public bool UseColorTest { get; set; }
        public bool UseMeltFlowTest { get; set; }
        public bool UseAshContentTest { get; set; }
        public bool UseMoistureTest { get; set; }
        public bool UseCarbonBlackTest { get; set; }
        public bool UseSpecificGravityTest { get; set; }
        public bool UseVisualInspectionTest { get; set; }

        #endregion

        #region Constructors
        public RawMaterialTestModel() :base()
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
