using System;

namespace TPO.Domain.DTO
{
    public class UserDTO
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public virtual PlantDTO Plant { get; set; }
    }
}
