using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Scrim;
using TPO.DL.Repositories;

namespace TPO.BL.Scrim
{
    public class TPOCurrentScrim
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        #region Bind Methods
        private static TPOCurrentScrimModel Bind(TPO.DL.Models.TPOCurrentScrim entity, TPOCurrentScrimModel to) 
        {
            to.ID = entity.ID;
            to.PlantID = entity.PlantID;
            to.Scrim1RollID = entity.Scrim1RollID.HasValue ? entity.Scrim1RollID.Value : TPOCurrentScrimModel.INVALID_ID;
            to.Scrim1TypeID = entity.Scrim1TypeID.HasValue ? entity.Scrim1TypeID.Value : TPOCurrentScrimModel.INVALID_ID;
            to.Scrim2RollID = entity.Scrim2RollID.HasValue ? entity.Scrim2RollID.Value : TPOCurrentScrimModel.INVALID_ID;
            to.Scrim2TypeID = entity.Scrim2TypeID.HasValue ? entity.Scrim2TypeID.Value : TPOCurrentScrimModel.INVALID_ID;
            to.FleeceRollID = entity.FleeceRollID.HasValue ? entity.FleeceRollID.Value : TPOCurrentScrimModel.INVALID_ID;
            to.FleeceTypeID = entity.FleeceTypeID.HasValue ? entity.FleeceTypeID.Value : TPOCurrentScrimModel.INVALID_ID;
            to.ScrimPos = entity.ScrimPos;
            to.LineID = entity.LineID;
            to.DateEntered = entity.DateEntered;
            to.EnteredBy = entity.EnteredBy;
            to.LastModified = entity.LastModified;
            to.ModifiedBy = entity.ModifiedBy;
            return to;
        }
        private static TPO.DL.Models.TPOCurrentScrim Bind(TPOCurrentScrimModel model, TPO.DL.Models.TPOCurrentScrim to) 
        {
            //to.PlantID = model.PlantID;
            //TODO:  Implement retrieving PlantID
            to.PlantID = 1;
            to.LineID = model.LineID;
            if (model.Scrim1RollID != TPOCurrentScrimModel.INVALID_ID)
                to.Scrim1RollID = model.Scrim1RollID;
            else
                to.Scrim1RollID = null;
            if (model.Scrim1TypeID != TPOCurrentScrimModel.INVALID_ID)
                to.Scrim1TypeID = model.Scrim1TypeID;
            else
                to.Scrim1TypeID = null;
            if (model.Scrim2RollID != TPOCurrentScrimModel.INVALID_ID)
                to.Scrim2RollID = model.Scrim2RollID;
            else
                to.Scrim2RollID = null;
            if (model.Scrim2TypeID != TPOCurrentScrimModel.INVALID_ID)
                to.Scrim2TypeID = model.Scrim2TypeID;
            else
                to.Scrim2TypeID = null;
            if (model.FleeceRollID != TPOCurrentScrimModel.INVALID_ID)
                to.FleeceRollID = model.FleeceRollID;
            else
                to.FleeceRollID = null;
            if (model.FleeceTypeID != TPOCurrentScrimModel.INVALID_ID)
                to.FleeceTypeID = model.FleeceTypeID;
            else
                to.FleeceTypeID = null;

            //to.ScrimPos = model.ScrimPos;
            to.ScrimPos = " ";
            return to;
        }
        #endregion

        #region Insert Methods
        public TPOCurrentScrimModel InsertTPOCurrentScrimModel(TPOCurrentScrimModel model)
        {
            TPOCurrentScrimModel returnModel = null;
            
            using (ScrimRepository repo = new ScrimRepository()) 
            {
                TPO.DL.Models.TPOCurrentScrim entity = Bind(model, new TPO.DL.Models.TPOCurrentScrim());
                entity.EnteredBy = "ETG QA";
                entity.DateEntered = DateTime.Now;
                entity.ModifiedBy = "ETG QA";
                entity.LastModified = DateTime.Now;
                repo.InsertTPOCurrentScrim(entity);
                returnModel = Bind(entity, new TPOCurrentScrimModel());
            }
            return returnModel;
        }
        #endregion

        #region Retrieval Methods
        public TPOCurrentScrimModel GetTPOCurrentScrimModelByLineID(string lineID) 
        {
            TPOCurrentScrimModel model = null;
            using (ScrimRepository repo = new ScrimRepository()) 
            {
                TPO.DL.Models.TPOCurrentScrim entity = repo.GetTPOCurrentScrimByLineID(lineID);
                if (entity != null) 
                {
                    model = Bind(entity, new TPOCurrentScrimModel());
                }
            }
            return model;
        }
        #endregion

        #region Update Methods
        public void UpdateTPOCurrentScrimModel(TPOCurrentScrimModel model) 
        {
            using (ScrimRepository repo = new ScrimRepository()) 
            {
                TPO.DL.Models.TPOCurrentScrim entity = repo.GetTPOCurrentScrimByID(model.ID);
                if (entity != null) 
                {
                    entity = Bind(model, entity);
                    entity.ModifiedBy = "ETG QA";
                    entity.LastModified = DateTime.Now;
                    repo.SaveChanges();
                }
            }
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
