using System;

namespace TPO.Web.Models
{
    public class RoleAssignmentModel : BaseViewModel
    {
        #region Variables
        #endregion

        #region Properties

        public int UserId { get; set; }
        public int RoleId { get; set; }

        public UserModel User { get; set; }
        public RoleModel Role { get; set; }

        #endregion

        #region Constructors
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