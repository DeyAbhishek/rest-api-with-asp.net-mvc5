using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.DL.Framework;
using TPO.DL.Models;

namespace TPO.DL.Repositories
{
    public class UserRepository : RepositoryBase
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        #region User
        public IEnumerable<User> GetUsersByRole(string roleName) 
        {
            return (from r in Entities.Roles
                    join ur in Entities.RoleAssignments on r.ID equals ur.RoleID
                    join u in Entities.Users on ur.UserID equals u.ID
                    where r.RoleName == roleName
                    select u).ToList();
        }

        public User GetUserByUserName(string userName)
        {
            return (from u in Entities.Users
                where u.Username == userName
                select u).FirstOrDefault();
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
