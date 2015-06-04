using System;

namespace TPO.Web.Models
{
    public class UserPlantModel : BaseViewModel
    {

        #region Properties

        public int UserId { get; set; }
        public new int PlantId { get; set; }

        public Plant Plant { get; set; }
        public UserModel User { get; set; }

        #endregion

    }
}