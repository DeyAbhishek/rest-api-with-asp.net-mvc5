using System;
using System.Collections.Generic;
using TPO.Common.Enums;

namespace TPO.Common.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public string Password { get; set; }

        public List<UserPlantDto> UserPlants { get; set; }
        public List<RoleDto> Roles { get; set; } 

        public List<SecurityTask> SecurityTasks { get; set; } 
    }
}
