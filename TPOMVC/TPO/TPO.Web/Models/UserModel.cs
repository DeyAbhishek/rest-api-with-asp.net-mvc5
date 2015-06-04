using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper;
using TPO.Common;
using TPO.Services;
using TPO.Services.Users;


namespace TPO.Web.Models
{
    public class UserModel : BaseViewModel
    {
        #region Variables

        private Plant _defaultPlant = null;

        private IEnumerable<Plant> _plantList;
        private IEnumerable<UserPlantModel> _relatedPlants;


        private IEnumerable<RoleModel> _roleList;
        private IEnumerable<RoleAssignmentModel> _roleAssignments;

        private string _plantName;

        #endregion

        #region Properties

        [Required]
        [DisplayName("Default Plant")]
        public new int PlantId { get; set; }
        [Required]
        [DisplayName("Username")]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [DisplayName("Full Name")]
        [MaxLength(50)]
        public string FullName { get; set; }

        public TPO.Web.Models.Plant DefaultPlant
        {
            get
            {
                if (_defaultPlant == null)
                {
                    using (Services.PlantService service = new Services.PlantService())
                        _defaultPlant = Mapper.Map<TPO.Common.DTOs.PlantDto, Plant>(service.Get(PlantId));
                }
                return _defaultPlant;
            }
        }

        [DisplayName("Default Plant")]
        public string PlantName
        {
            get { return this.DefaultPlant.Name; }
        }

        public IEnumerable<SelectListItem> PlantList
        {
            get
            {
                return new SelectList(_plantList, "ID", "Name");
            }
        }
        public IEnumerable<SelectListItem> RoleList
        {
            get
            {
                return new SelectList(_roleList, "ID", "RoleName");
            }
        }

        public IEnumerable<RoleAssignmentModel> RoleAssignments
        {
            get
            {
                return _roleAssignments;
            }
        }

        public IEnumerable<UserPlantModel> RelatedPlants
        {
            get
            {
                return _relatedPlants;
            }
        }

        [DisplayName("Related Plants")]
        public MultiSelectList RelatedPlantList
        {
            get
            {
                return new MultiSelectList(_plantList, "Id", "Name", RelatedPlants.Select(p => p.PlantId).ToList());
            }
        }

        [DisplayName("Group Assignment")]
        public MultiSelectList RoleAssignmentList
        {
            get
            {
                return new MultiSelectList(_roleList, "Id", "RoleName", RoleAssignments.Select(p => p.RoleId).ToList());
            }
        }

        #endregion

        #region Constructors

        public UserModel()
            : base()
        {
            _plantList = new List<Plant>();
            _roleList = new List<RoleModel>();

            using (Services.PlantService service = new Services.PlantService())
                Mapper.Map(service.GetAll().OrderBy(p => p.Name), _plantList);
            using (Services.Users.RoleService service = new Services.Users.RoleService())
                Mapper.Map(service.GetAll().OrderBy(r => r.RoleName), _roleList);
            
            _relatedPlants = new List<UserPlantModel>();
            _roleAssignments = new List<RoleAssignmentModel>();
        }

        #endregion

        #region Public Methods

        public void LoadLists()
        {
            using (UserPlantService service = new UserPlantService())
                _relatedPlants = Mapper.Map<List<TPO.Common.DTOs.UserPlantDto>, List<UserPlantModel>>(service.GetByUserId(Id));
            
            using (Services.Users.RoleAssignmentService service = new Services.Users.RoleAssignmentService())
                _roleAssignments = Mapper.Map<List<TPO.Common.DTOs.RoleAssignmentDto>, List<RoleAssignmentModel>>(service.GetByUserId(Id));          
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