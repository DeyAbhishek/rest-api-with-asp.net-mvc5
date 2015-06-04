using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Framework;

namespace TPO.Model.Security
{
    public class UserModel : TPOModelBase
    {
        #region Variables
        private int _PlantID;

        
        #endregion

        #region Properties
        [DisplayName("Plant")]
        [Required(ErrorMessage = "The Plant field is required")]
        public int PlantID
        {
            get { return _PlantID; }
            set { _PlantID = value; }
        }
        [StringLength(50), Required(ErrorMessage = "The User name field is required")]
        public string Username { get; set; }
        [DisplayName("Full Name")]
        [StringLength(100), Required(ErrorMessage = "The Full Name field is required")]
        public string FullName { get; set; }
        #endregion

        #region Constructors
        public UserModel() : base() 
        {
            _PlantID = INVALID_ID;
        }
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
