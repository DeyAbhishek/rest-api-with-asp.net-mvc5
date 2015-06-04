using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Reference;

namespace TPO.BL.Reference
{
    public class UnitOfMeasure
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        #region Retrieval Methods
        public UnitOfMeasureModel GetUnitOfMeasureByID(int id) 
        {
            UnitOfMeasureModel model = null;
            using (TPO.DL.Repositories.ReferenceDataRepository repo = new DL.Repositories.ReferenceDataRepository()) 
            {
                var entity = repo.GetUnitOfMeasureByID(id);
                if (entity != null) 
                {
                    model = Bind(entity, new UnitOfMeasureModel());
                }
            }
            return model;
        }
        public List<UnitOfMeasureModel> GetWeightUnitsOfMeasure()
        {
            List<UnitOfMeasureModel> data = new List<UnitOfMeasureModel>();
            using (TPO.DL.Repositories.ReferenceDataRepository repo = new DL.Repositories.ReferenceDataRepository())
            {
                List<TPO.DL.Models.UnitOfMeasure> entities = repo.GetWeightUnitsOfMeasure().ToList();
                for (int i = 0; i < entities.Count; i++)
                {
                    data.Add(Bind(entities[i], new UnitOfMeasureModel()));
                }
            }
            return data;
        }
        public List<UnitOfMeasureModel> GetLengthUnitsOfMeasure()
        {
            List<UnitOfMeasureModel> data = new List<UnitOfMeasureModel>();
            using (TPO.DL.Repositories.ReferenceDataRepository repo = new DL.Repositories.ReferenceDataRepository())
            {
                List<TPO.DL.Models.UnitOfMeasure> entities = repo.GetLengthUnitsOfMeasure().ToList();
                for (int i = 0; i < entities.Count; i++)
                {
                    data.Add(Bind(entities[i], new UnitOfMeasureModel()));
                }
            }
            return data;
        }
        #endregion

        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods

        #region Bind Methods
        private static UnitOfMeasureModel Bind(TPO.DL.Models.UnitOfMeasure entity, UnitOfMeasureModel to) 
        {
            to.ID = entity.ID;
            to.Code = entity.Code;
            to.Description = entity.Description;
            to.UnitOfMeasureTypeID = entity.TypeID;
            to.EnteredBy = entity.EnteredBy;
            to.DateEntered = entity.DateEntered;
            to.ModifiedBy = entity.ModifiedBy;
            to.LastModified = entity.LastModified;
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
