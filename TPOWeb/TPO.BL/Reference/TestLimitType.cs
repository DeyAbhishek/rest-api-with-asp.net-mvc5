using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.DL.Repositories;
using TPO.Model.Reference;

namespace TPO.BL.Reference
{
    public class TestLimitType
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
        /// Converts a TestLimitType Entity into a TestLimitTypeModel Model.
        /// </summary>
        /// <param name="entity">The TestLimitType Entity to convert.</param>
        /// <param name="to">The TestLimitTypeModel Model to which to convert.</param>
        /// <returns>An instance of TestLimitTypeModel representing the TestLimitType Entity.</returns>
        private static TestLimitTypeModel Bind(TPO.DL.Models.TestLimitType entity, TestLimitTypeModel to) 
        {
            to.ID = entity.ID;
            to.Code = entity.Code;
            to.Description = entity.Description;
            to.SortOrder = entity.SortOrder;
            to.EnteredBy = entity.EnteredBy;
            to.DateEntered = entity.DateEntered;
            to.LastModified = entity.LastModified;
            to.ModifiedBy = entity.ModifiedBy;
            return to;
        }
        /// <summary>
        /// Converts a TestLimitTypeModel Model into a TestLimitType Entity.
        /// </summary>
        /// <param name="entity">The TestLimitTypeModel Model to convert.</param>
        /// <param name="to">The TestLimitType Entity to which to convert.</param>
        /// <returns>An instance of TestLimitType representing the TestLimitTypeModel Model.</returns>
        private static TPO.DL.Models.TestLimitType Bind(TestLimitTypeModel model, TPO.DL.Models.TestLimitType to) 
        {
            to.Code = model.Code;
            to.Description = model.Description;
            to.SortOrder = model.SortOrder;
            return to;
        }
        #endregion

        #region Retrieval Methods
        /// <summary>
        /// Gets a TestLimitTypeModel representing the TestLimitType record indicated by the provided ID.
        /// </summary>
        /// <param name="id">The ID of the TestLimitType record in the database.</param>
        /// <returns>A TestLimitTypeModel</returns>
        public TestLimitTypeModel GetTestLimitTypeModelByID(int id) 
        {
            TestLimitTypeModel model = null;
            using (ReferenceDataRepository repo = new ReferenceDataRepository()) 
            {
                TPO.DL.Models.TestLimitType entity = repo.GetTestLimitTypeByID(id);
                if (entity != null)
                {
                    model = Bind(entity, new TestLimitTypeModel());
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
