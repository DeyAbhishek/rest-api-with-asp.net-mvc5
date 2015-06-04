using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.DL.Framework;

namespace TPO.DL.Repositories
{
    public class SystemSettingsRepository : RepositoryBase
    {
        #region Variables
        #endregion

        #region Constants
        private const string TEMP_FORM_PASSWORD = "FSBP";
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public SystemSettingsRepository() : base()
        {

        }
        #endregion

        #region Public Methods
        public string GetFormPassword()
        {
            //TODO:  Replace TEMP_FORM_PASSWORD with actual retrieval of form password (either from DB or from config)
            return TEMP_FORM_PASSWORD;
        }

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
