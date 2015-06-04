using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.DL.Repositories;

namespace TPO.BL.System
{
    public class SystemSettings
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        #region Static Methods
        public static bool CheckFormPassword(string suppliedPassword) 
        {
            bool passedCheck = false;
            using (SystemSettingsRepository repo = new SystemSettingsRepository()) 
            {
                passedCheck = suppliedPassword == repo.GetFormPassword();
            }
            return passedCheck;
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
