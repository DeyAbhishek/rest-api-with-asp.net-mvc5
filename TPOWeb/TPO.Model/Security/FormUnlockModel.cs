using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Model.Security
{
    public class FormUnlockModel : Framework.TPOModelBase
    {
        #region Variables
        #endregion

        #region Properties
        [DisplayName("Password:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool TriedUnlock { get; set; }
        public bool Unlocked { get; set; }
        #endregion

        #region Constructors
        public FormUnlockModel() : base() { }
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
