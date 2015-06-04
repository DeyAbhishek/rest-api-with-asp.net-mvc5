using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.DL.Repositories;
using TPO.Model.RawMaterials;

namespace TPO.BL.RawMaterials
{
    /// <summary>
    /// A class containing business logic for RawMaterialTest
    /// </summary>
    public class RawMaterialTest
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        #region Bind Methods
        /// <summary>
        /// Converts a RawMaterialTest Entity into a RawMaterialTestModel Model.
        /// </summary>
        /// <param name="entity">The RawMaterialTest Entity to convert.</param>
        /// <param name="to">The RawMaterialTestModel Model to which to convert.</param>
        /// <returns>An instance of RawMaterialTestModel representing the RawMaterialTest entity.</returns>
        private static RawMaterialTestModel Bind(TPO.DL.Models.RawMaterialTest entity, RawMaterialTestModel to) 
        {
            to.ID = entity.ID;
            to.AshContentLimitTypeID = entity.ACLimitTypeID.HasValue ? entity.ACLimitTypeID.Value : RawMaterialTestModel.INVALID_ID;
            to.AshContentMinimum = entity.ACLimit1.HasValue ? (decimal)entity.ACLimit1.Value : 0;
            to.AshContentMaximum = entity.ACLimit2.HasValue ? (decimal)entity.ACLimit2.Value : 0;
            to.CarbonBlackLimitTypeID = entity.CBLimitTypeID.HasValue ? entity.CBLimitTypeID.Value : RawMaterialTestModel.INVALID_ID;
            to.CarbonBlackMinimum = entity.CBLimit1.HasValue ? (decimal)entity.CBLimit1.Value : 0;
            to.CarbonBlackMaximum = entity.CBLimit2.HasValue ? (decimal)entity.CBLimit2.Value : 0;
            to.ColorLimitTypeID = entity.ColorLimitTypeID.HasValue ? entity.ColorLimitTypeID.Value : RawMaterialTestModel.INVALID_ID;
            to.ColorMinimum = entity.ColorLimit1.HasValue ? (decimal)entity.ColorLimit1.Value : 0;
            to.ColorMaximum = entity.ColorLimit2.HasValue ? (decimal)entity.ColorLimit2.Value : 0;
            to.MeltFlowLimitTypeID = entity.MFLimitTypeID.HasValue ? entity.MFLimitTypeID.Value : RawMaterialTestModel.INVALID_ID;
            to.MeltFlowMinimum = entity.MFLimit1.HasValue ? (decimal)entity.MFLimit1.Value : 0;
            to.MeltFlowMaximum = entity.MFLimit2.HasValue ? (decimal)entity.MFLimit2.Value : 0;
            to.MoistureLimitTypeID = entity.MoistLimitTypeID.HasValue ? entity.MoistLimitTypeID.Value : RawMaterialTestModel.INVALID_ID;
            to.MoistureMinimum = entity.MoistLimit1.HasValue ? (decimal)entity.MoistLimit1.Value : 0;
            to.MoistureMaximum = entity.MoistLimit2.HasValue ? (decimal)entity.MoistLimit2.Value : 0;
            to.PlantID = entity.PlantID;
            to.TestFrequency = entity.TestFrequency;
            to.UseAshContentTest = entity.UseACTest;
            to.UseCarbonBlackTest = entity.UseCBTest;
            to.UseColorTest = entity.UseColorTest;
            to.UseMeltFlowTest = entity.UseMFTest;
            to.UseMoistureTest = entity.UseMoistTest;
            to.UseSpecificGravityTest = entity.UseSpecGrav.HasValue ? entity.UseSpecGrav.Value : false;
            to.UseVisualInspectionTest = entity.UseVisual.HasValue ? entity.UseVisual.Value : false;
            return to;
        }
        #endregion

        #region Retrieval Methods
        /// <summary>
        /// Retrieves the test configuration details for the provided raw material.
        /// </summary>
        /// <param name="rawMaterialID">The unique code of the raw material.</param>
        /// <returns>An instance of RawMaterialTestModel.</returns>
        public RawMaterialTestModel GetQCConfigurationByRawMaterial(Int32 rawMaterialID) 
        {
            RawMaterialTestModel model = null;
            using (RawMaterialsRepository repo = new RawMaterialsRepository()) 
            {
                TPO.DL.Models.RawMaterialTest entity = repo.GetRawMaterialTestByRawMaterial(rawMaterialID);
                if (entity != null)
                {
                    model = Bind(entity, new RawMaterialTestModel());
                }
            }
            
            return model;
        }
        #endregion

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
