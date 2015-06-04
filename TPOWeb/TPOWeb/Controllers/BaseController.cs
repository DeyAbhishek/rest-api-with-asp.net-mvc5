using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPO.BL.Constants;
using TPO.BL.Repositories;
using TPO.BL.Repositories.ProductionLine;
using TPO.BL.Repositories.RawMaterial.CurrentRawMaterial;
using TPO.BL.Repositories.RawMaterial.RawMaterialReceived;
using TPO.BL.Repositories.RawMaterial.RawMaterialQCRedHold;
using TPO.BL.Repositories.RawMaterial.RawMaterialReceivedRepository;
using TPO.BL.Repositories.User;

namespace TPOWeb.Controllers
{
    public class BaseController: Controller
    {
        protected IUserRepository UserRepo;
        protected IRawMaterialReceivedRepository RawMaterialReceivedRepo;
        protected IRawMaterialQCRedHoldRepository RawMaterialQCRedHoldRepo;
        protected IProductionLineRepository ProductionLineRepo;
        protected ICurrentRawMaterialRepository CurrentRawMaterialRepo;

        private int _plantId;
        public int CurrentPlantId
        {
            get { return _plantId; }
        }

        private string _currentUser;
        public string CurrentUser
        {
            get { return _currentUser; }
        }

        private int _currentUserID;
        public int CurrentUserID 
        {
            get { return _currentUserID; }
        }

        public BaseController()
        {
            UserRepo = new UserRepository();
            RawMaterialReceivedRepo = new RawMaterialReceivedRepository();
            ProductionLineRepo = new ProductionLineRepository();
            CurrentRawMaterialRepo = new CurrentRawMaterialRepository();
            RawMaterialQCRedHoldRepo = new RawMaterialQCRedHoldRepository();

            TPO.Domain.DTO.UserDTO user = UserRepo.GetUserByUserName("acorrington"); // TODO: Once the security model is finalized getting CurrentUser and CurrentPlantID will need to be revisited 
            _currentUser = user.Username;
            _plantId = user.PlantID;
            _currentUserID = user.ID;
        }
    }
}