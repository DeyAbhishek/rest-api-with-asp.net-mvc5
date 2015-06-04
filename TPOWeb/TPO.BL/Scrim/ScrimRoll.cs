using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Scrim;
using TPO.DL.Repositories;

namespace TPO.BL.Scrim
{
    public class ScrimRoll
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        #region Bind Methods
        private static ScrimRollModel Bind(TPO.DL.Models.ScrimRoll entity, ScrimRollModel to) 
        {
            to.ID = entity.ID;
            to.ScrimRollCode = entity.Code;
            to.PlantID = entity.PlantID;
            to.ScrimRollTypeID = entity.TypeID.HasValue ? entity.TypeID.Value : ScrimRollModel.INVALID_ID;
            to.WeightUnitOfMeasureID = entity.WeightUoMID;
            to.LengthUnitOfMeasureID = entity.LengthUoMID;
            to.WovenDate = entity.WovenDate.HasValue ? entity.WovenDate.Value : DateTime.MinValue;
            to.DateReceived = entity.DateReceived;
            to.Length = (decimal)entity.Length;
            to.Weight = (decimal)entity.Weight;
            to.TareWeight = (decimal)entity.TareWeight;
            to.ReceivedLength = (decimal)entity.ReceivedLength;
            to.ReceivedWeight = (decimal)entity.ReceivedWeight;
            to.ReceivedTareWeight = (decimal)entity.ReceivedTareWeight;
            to.LengthUsed = (decimal)entity.LengthUsed;
            to.WeightUsed = (decimal)entity.WeightUsed;

            to.WovenLotCode = entity.WovenLotNumber;
            to.WovenDate = entity.WovenDate ?? DateTime.MinValue;

            return to;
        }
        private static TPO.DL.Models.ScrimRoll Bind(ScrimRollModel model, TPO.DL.Models.ScrimRoll to) 
        {
            to.Code = model.ScrimRollCode;
            //to.PlantID = model.PlantID;

            //TODO:  Implement retrieving PlantID
            to.PlantID = 1;
            if (model.ScrimRollTypeID != ScrimRollModel.INVALID_ID)
                to.TypeID = model.ScrimRollTypeID;
            else
                to.TypeID = null;
            to.WeightUoMID = model.WeightUnitOfMeasureID;
            to.LengthUoMID = model.LengthUnitOfMeasureID;
            if (model.WovenDate != DateTime.MinValue)
                to.WovenDate = model.WovenDate;
            else
                to.WovenDate = null;
            to.DateReceived = model.DateReceived;
            to.Length = (double)model.Length;
            to.Weight = (double)model.Weight;
            to.TareWeight = (double)model.TareWeight;
            to.ReceivedLength = (double)model.ReceivedLength;
            to.ReceivedWeight = (double)model.ReceivedWeight;
            to.ReceivedTareWeight = (double)model.ReceivedTareWeight;
            to.LengthUsed = (double)model.LengthUsed;
            to.WeightUsed = (double)model.WeightUsed;

            to.DateEntered = (DateTime)model.DateEntered;
            to.EnteredBy = (string)model.EnteredBy;
            to.ModifiedBy = (string)model.ModifiedBy;
            to.LastModified = (DateTime)model.LastModified;

            to.WovenLotNumber = model.WovenLotCode;
            
            return to;
        }
        #endregion

        #region Insert Methods
        public ScrimRollModel InsertScrimRoll(ScrimRollModel model) 
        {
            ScrimRollModel returnModel = null;  
            using (ScrimRepository repo = new ScrimRepository()) 
            {
                TPO.DL.Models.ScrimRoll entity = Bind(model, new TPO.DL.Models.ScrimRoll());
                repo.InsertScrimRoll(entity);
                returnModel = Bind(entity, new ScrimRollModel());
            }
            return returnModel;
        }
        
        #endregion

        #region Retrieval Methods
        public ScrimRollModel GetScrimRollModelByID(int id)
        {
            ScrimRollModel model = null;
            using (ScrimRepository repo = new ScrimRepository())
            {
                TPO.DL.Models.ScrimRoll entity = repo.GetScrimRollByID(id);
                if (entity != null)
                {
                    model = Bind(entity, new ScrimRollModel());
                }
            }
            return model;
        }
        public List<ScrimRollModel> GetScrimRollModels() 
        {
            List<ScrimRollModel> models = new List<ScrimRollModel>();
            using (ScrimRepository repo = new ScrimRepository()) 
            {
                List<TPO.DL.Models.ScrimRoll> entities = repo.GetScrimRolls().ToList();
                for (int i = 0; i < entities.Count; i++) 
                {
                    models.Add(Bind(entities[i], new ScrimRollModel()));
                }
            }
            return models;
        }
        public List<ScrimRollModel> GetScrimRollModelsByPlantID(int plantID) 
        {
            List<ScrimRollModel> models = new List<ScrimRollModel>();
            using (ScrimRepository repo = new ScrimRepository())
            {
                List<TPO.DL.Models.ScrimRoll> entities = repo.GetScrimRollsByPlantID(plantID).ToList();
                for (int i = 0; i < entities.Count; i++)
                {
                    models.Add(Bind(entities[i], new ScrimRollModel()));
                }
            }
            return models;
        }
        public List<ScrimRollModel> GetScrimRollModelsByTypeID(int typeID) 
        {
            List<ScrimRollModel> data = new List<ScrimRollModel>();
            Reference.UnitOfMeasure uomBL = new Reference.UnitOfMeasure();
            using (ScrimRepository repo = new ScrimRepository()) 
            {
                List<TPO.DL.Models.ScrimRoll> entities = repo.GetScrimRollsByTypeID(typeID).ToList();
                for (int i = 0; i < entities.Count; i++) 
                {
                    var model = Bind(entities[i], new ScrimRollModel());
                    model.LengthUnitOfMeasure = uomBL.GetUnitOfMeasureByID(model.LengthUnitOfMeasureID);
                    model.WeightUnitOfMeasure = uomBL.GetUnitOfMeasureByID(model.WeightUnitOfMeasureID);
                    data.Add(model);
                }
            }
            return data;
        }
        #endregion

        #region Update Methods
        public void UpdateScrimRoll(ScrimRollModel model)
        {
            using (ScrimRepository repo = new ScrimRepository())
            {
                TPO.DL.Models.ScrimRoll entity = repo.GetScrimRollByID(model.ID);
                if (entity != null)
                {
                    entity = Bind(model, entity);
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
