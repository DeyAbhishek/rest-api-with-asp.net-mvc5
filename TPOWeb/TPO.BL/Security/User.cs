using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Security;
using TPO.DL.Repositories;

namespace TPO.BL.Security
{
    public class User
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        #region Retrieval Methods
        public List<UserModel> GetQCTechUsers() 
        {
            List<UserModel> data = new List<UserModel>();
            using (UserRepository repo = new UserRepository()) 
            {
                List<TPO.DL.Models.User> entities = repo.GetUsersByRole(Role.QC_TECH).ToList();
                for (int i = 0; i < entities.Count; i++) 
                {
                    data.Add(Bind(entities[i], new UserModel()));
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
        private static UserModel Bind(TPO.DL.Models.User entity, UserModel to) 
        {
            to.ID = entity.ID;
            to.FullName = entity.FullName;
            to.Username = entity.Username;
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
