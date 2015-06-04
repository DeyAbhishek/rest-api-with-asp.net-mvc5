using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.BL.Constants;
using TPO.Model.RawMaterials;
using TPO.DL.Repositories;
using TPO.DL.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;



namespace TPO.BL.RawMaterials
{
    public class RawMaterialQC
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        #region Insert Methods
        public void UpdateRawMaterialTest(TPO.DL.Models.RawMaterialQC RawMaterialUpdateQC)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString))
            {
                using (var cmd = new SqlCommand(StoredProcedures.RawMaterialQcUpdate, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter {ParameterName = "@ID", Value = RawMaterialUpdateQC.ID});

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@PlantID",
                        Value = RawMaterialUpdateQC.PlantID
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@BoxCarTested",
                        Value = RawMaterialUpdateQC.BoxCarTested
                    });
                    

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@QCTechUserID",
                        Value = RawMaterialUpdateQC.QCTechUserID
                    });


                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@Comments",
                        Value = RawMaterialUpdateQC.Comments
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@DateEntered",
                        Value = RawMaterialUpdateQC.DateEntered
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@EnteredBy",
                        Value = RawMaterialUpdateQC.EnteredBy
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@LastModified",
                        Value = RawMaterialUpdateQC.LastModified
                    });
                    
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@ModifiedBy",
                        Value = RawMaterialUpdateQC.ModifiedBy
                    });

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string AddRawMaterialTest(TPO.DL.Models.RawMaterialQC RawMaterialQC)
        {
            int ID = 1;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString))
            {
                using (var cmd = new SqlCommand(StoredProcedures.RawMaterialQcCreate, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@PlantID",
                        Value = RawMaterialQC.PlantID
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@RawMaterialID",
                        Value = RawMaterialQC.RawMaterialReceivedID
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@LotID",
                        Value = RawMaterialQC.RawMaterialReceived.LotNumber
                    });
                    
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@DateEntered",
                        Value = RawMaterialQC.DateEntered
                    });
                    
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@EnteredBy",
                        Value = RawMaterialQC.EnteredBy
                    });
                    
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@LastModified",
                        Value = Convert.ToDateTime(DateTime.Now)
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@ModifiedBy",
                        Value = RawMaterialQC.ModifiedBy
                    });

                    con.Open();
                    ID = Int32.Parse(cmd.ExecuteScalar().ToString());
                }
            }
            return ID.ToString();
        }
        #endregion

        #region Insert Methods
        public RawMaterialQCModel InsertRawMaterialQCModel(TPO.Model.RawMaterials.RawMaterialQCModel model) 
        {
            using (RawMaterialsRepository repo = new RawMaterialsRepository()) 
            {
                TPO.DL.Models.RawMaterialQC entity = Bind(model, new TPO.DL.Models.RawMaterialQC());
                int id = repo.CreateRawMaterialQC(entity);
                model.ID = id;
            }
            return model;
        }
        #endregion

        #region Update Methods
        /// <summary>
        /// Updates the RawMaterialQC record in the database that is represented by the RawMaterialQCModel.
        /// </summary>
        /// <param name="model">The RawMaterialQCModel representing the RawMaterialQC record.</param>
        public void UpdateRawMaterialQCModel(TPO.Model.RawMaterials.RawMaterialQCModel model) 
        {
            using (RawMaterialsRepository repo = new RawMaterialsRepository()) 
            {
                TPO.DL.Models.RawMaterialQC entity = repo.GetRawMaterialQCByID(model.ID);
                if (entity != null) 
                {
                    entity = Bind(model, entity);
                    repo.SaveChanges();
                }
            }
        }
        #endregion

        #region Retrieval Methods
        /// <summary>
        /// Gets a RawMaterialQCModel representing the RawMaterialQC record indicated by the provided ID.
        /// The RawMaterialQCModel returns also includes configuration data for the various test available.
        /// </summary>
        /// <param name="id">The ID of the RawMaterialQC record in the database.</param>
        /// <returns>A RawMaterialQCModel</returns>
        public RawMaterialQCModel GetRawMaterialQCModelByRawMaterialQCID(int id) 
        {
            RawMaterialQCModel model = null;
            using (RawMaterialsRepository repo = new RawMaterialsRepository()) 
            {
                TPO.DL.Models.RawMaterialQC entity = repo.GetRawMaterialQCByID(id);
                if (entity != null)
                {
                    model = Bind(entity, new RawMaterialQCModel());
                    RawMaterialTest testBL = new RawMaterialTest();
                    Int32 rawMatID = 0;

                    TPO.DL.Models.RawMaterialReceived rawMaterialReceived =
                        new TPOMVCApplicationEntities().RawMaterialReceiveds.Where(w => w.ID == model.RawMaterialReceivedID).First();

                    model.QCConfiguration = testBL.GetQCConfigurationByRawMaterial(rawMaterialReceived.RawMaterial.ID);
                }
            }
            
            return model;
        }
        #endregion

        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods

        #region Bind Methods
        /// <summary>
        /// Converts a RawMaterialQC Entity into a RawMaterialQCModel Model.
        /// </summary>
        /// <param name="entity">The RawMaterialQC Entity to convert.</param>
        /// <param name="to">The RawMaterialQCModel Model to which to convert.</param>
        /// <returns>An instance of RawMaterialQCModel representing the RawMaterialQC Entity.</returns>
        private static RawMaterialQCModel Bind(TPO.DL.Models.RawMaterialQC entity, RawMaterialQCModel to) 
        {
            to.ID = entity.ID;
            to.PlantID = entity.PlantID;
            to.QCTechID = entity.QCTechUserID.HasValue ? entity.QCTechUserID.Value : RawMaterialQCModel.INVALID_ID;
            to.RawMaterialReceivedID = entity.RawMaterialReceivedID;
            //Code and Lot are one way read and not passed back to model (i.e. read-only)
            to.RawMaterialCode = entity.RawMaterialReceived.RawMaterial.Code;
            to.LotCode = entity.RawMaterialReceived.LotNumber;
            to.VisuallyInspected = entity.VisualInspection.HasValue ? entity.VisualInspection.Value : false;
            to.SpecificGravity = entity.SpecGrav.HasValue ? (decimal)entity.SpecGrav.Value : 0;
            to.ColorTestCoA = entity.ColorCoA.HasValue ? (decimal)entity.ColorCoA.Value : 0;
            to.ColorTestFS = entity.ColorFS.HasValue ? (decimal)entity.ColorFS.Value : 0;
            to.MeltFlowTestCoA = entity.MFCoA.HasValue ? (decimal)entity.MFCoA.Value : 0;
            to.MeltFlowTestFS = entity.MFFS.HasValue ? (decimal)entity.MFFS.Value : 0;
            to.AshContentTestCoA = entity.ACCoA.HasValue ? (decimal)entity.ACCoA.Value : 0;
            to.AshContentTestFS = entity.ACFS.HasValue ? (decimal)entity.ACFS.Value : 0;
            to.MoistureTestCoA = entity.MoistCoA.HasValue ? (decimal)entity.MoistCoA.Value : 0;
            to.MoistureTestFS = entity.MoistFS.HasValue ? (decimal)entity.MoistFS.Value : 0;
            to.CarbonBlackTestCoA = entity.CBCoA.HasValue ? (decimal)entity.CBCoA.Value : 0;
            to.CarbonBlackTestFS = entity.CBFS.HasValue ? (decimal)entity.CBFS.Value : 0;
            to.BoxCar = entity.BoxCarTested;
            to.Comments = entity.Comments;
            to.DateEntered = entity.DateEntered;
            to.EnteredBy = entity.EnteredBy;
            to.LastModified = entity.LastModified;
            to.ModifiedBy = entity.ModifiedBy;
            return to;
        }
        /// <summary>
        /// Converts a RawMaterialQCModel Model into a RawMaterialQC Entity.
        /// </summary>
        /// <param name="entity">The RawMaterialQCModel Model to convert.</param>
        /// <param name="to">The RawMaterialQC Entity to which to convert.</param>
        /// <returns>An instance of RawMaterialQC representing the RawMaterialQCModel Model.</returns>
        private static TPO.DL.Models.RawMaterialQC Bind(RawMaterialQCModel model, TPO.DL.Models.RawMaterialQC to) 
        {
            
            //to.PlantID = model.PlantID;
            //TODO:  Implement retrieving PlantID
            to.PlantID = 1;
            if (model.QCTechID != RawMaterialQCModel.INVALID_ID)
                to.QCTechUserID = model.QCTechID;
            else
                to.QCTechUserID = null;
            to.RawMaterialReceivedID = model.RawMaterialReceivedID;
            to.VisualInspection = model.VisuallyInspected;
            to.SpecGrav = (double?)model.SpecificGravity;
            to.ColorCoA = (double?)model.ColorTestCoA;
            to.ColorFS = (double?)model.ColorTestFS;
            to.MFCoA = (double?)model.MeltFlowTestCoA;
            to.MFFS = (double?)model.MeltFlowTestFS;
            to.ACCoA = (double?)model.AshContentTestCoA;
            to.ACFS = (double?)model.AshContentTestFS;
            to.MoistCoA = (double?)model.MoistureTestCoA;
            to.MoistFS = (double?)model.MoistureTestFS;
            to.CBCoA = (double?)model.CarbonBlackTestCoA;
            to.CBFS = (double?)model.CarbonBlackTestFS;
            to.BoxCarTested = model.BoxCar;
            to.Comments = model.Comments;

            return to;
        }
        #endregion


        #endregion

        #region Events
        #endregion

        #region Event Handlers
        #endregion
    }
}
