using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    class SecurityRoleDto
    {
        public int SecurityTaskId { get; set; }
        public string SecurityTaskName { get; set; }
        public bool Active { get; set; }
    }
}
