using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPO.Common.DTOs;

namespace TPO.Web.Models
{
    public class SecurityAddModel: BaseViewModel
    {
        [Required]
        [DisplayName("Default Plant")]
        public int PlantId { get; set; }
        [Required]
        [DisplayName("Username")]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [DisplayName("Full Name")]
        [MaxLength(50)]
        public string FullName { get; set; }

        public TPO.Web.Models.Plant DefaultPlant { get; set; }

        [DisplayName("Default Plant")]
        public string PlantName { get; set; }

        public List<SelectListItem> PlantList { get; set; }

        public List<SelectListItem> RoleList { get; set; }

        public SelectedRoles SelectedRoles { get; set; }

        public SelectedPlants SelectedPlants { get; set; }

        [DisplayName("Related Plants")]
        public List<SelectListItem> RelatedPlantList { get; set; }

        [DisplayName("Group Assignment")]
        public List<SelectListItem> RoleAssignmentList { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Change Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "Min password length is 7 characters")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Password must be confirmed")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class SelectedPlants
    {
        public string[] Plants { get; set; }
    }

    public class SelectedRoles
    {
        public string[] Roles { get; set; }
    }
}