using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class RoleAssignmentDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int RoleId { get; set; }


        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public UserDto User { get; set; }
        public RoleDto Role { get; set; }

    }
}
