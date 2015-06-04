using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Reference;
using TPO.DL.Repositories;

namespace TPO.BL.Reference
{
    public class ScrimType
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        #region Bind Methods
        private static ScrimTypeModel Bind(TPO.DL.Models.ScrimType entity, ScrimTypeModel to) 
        {
            to.ID = entity.ID;
            to.PlantID = entity.PlantID;
            to.WidthUnitOfMeasureID = entity.WidthUoMID;
            to.WeightUnitOfMeasureID = entity.WeightUoMID;
            to.LengthUnitOfMeasureID = entity.LengthUoMID;
            to.Code = entity.Code;
            to.Description = entity.Description;
            to.Width = (decimal)entity.Width;
            to.Length = (decimal)entity.Length;
            to.Weight = (decimal)entity.Weight;
            to.IsLiner = entity.IsLiner;
            to.LastModified = entity.LastModified;
            return to;
        }
        #endregion

        #region Retrieval Methods
        public List<ScrimTypeModel> GetScrimTypeModels() 
        {
            List<ScrimTypeModel> models = new List<ScrimTypeModel>();
            using (ReferenceDataRepository repo = new ReferenceDataRepository()) 
            {
                IEnumerable<TPO.DL.Models.ScrimType> entities = repo.GetScrimTypes();
                for (int i = 0; i < entities.Count(); i++) 
                {
                    models.Add(Bind(entities.ElementAt(i), new ScrimTypeModel()));
                }
            }
            return models;
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
